using Microsoft.ML;
using SentimentAnalysis.Data;
using SentimentAnalysis.Entities;

namespace SentimentAnalysis.Services
{
    /// <summary>
    /// Hibrit Duygu Analizi Servisi
    /// ML.NET + Anahtar Kelime Tabanl? Analiz
    /// </summary>
    public class SentimentService
    {
        private readonly MLContext _mlContext;
        private ITransformer? _model;
        private PredictionEngine<SentimentData, SentimentPrediction>? _predictionEngine;

        public double Accuracy { get; private set; }
        public double F1Score { get; private set; }
        public double Precision { get; private set; }
        public double Recall { get; private set; }

        public SentimentService()
        {
            _mlContext = new MLContext(seed: 42);
            Train();
        }

        private void Train()
        {
            var trainingData = PrepareTrainingData();
            var dataView = _mlContext.Data.LoadFromEnumerable(trainingData);

            // Tüm veriyi e?itim için kullan (küçük veri setinde split yapmak sorunlu)
            var pipeline = BuildSimplePipeline();
            _model = pipeline.Fit(dataView);

            // Cross-validation ile de?erlendir
            var cvResults = _mlContext.BinaryClassification.CrossValidate(
                dataView, pipeline, numberOfFolds: 5, labelColumnName: nameof(SentimentData.Label));

            Accuracy = cvResults.Average(x => x.Metrics.Accuracy);
            F1Score = cvResults.Average(x => x.Metrics.F1Score);
            Precision = cvResults.Average(x => x.Metrics.PositivePrecision);
            Recall = cvResults.Average(x => x.Metrics.PositiveRecall);

            _predictionEngine = _mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(_model);
        }

        private List<SentimentData> PrepareTrainingData()
        {
            var result = new List<SentimentData>();

            foreach (var category in TrainingData.Data)
            {
                bool isPositive = category.Key == SentimentType.Positive;

                foreach (var text in category.Value)
                {
                    // Basit ön i?leme
                    string processedText = TextPreprocessor.AdvancedPreprocess(text);

                    result.Add(new SentimentData
                    {
                        Text = processedText,
                        Label = isPositive
                    });
                }
            }

            return result;
        }

        /// <summary>
        /// ML pipeline - LBFGS Logistic Regression
        /// </summary>
        private IEstimator<ITransformer> BuildSimplePipeline()
        {
            return _mlContext.Transforms.Text.FeaturizeText(
                    outputColumnName: "Features",
                    inputColumnName: nameof(SentimentData.Text))
                .Append(_mlContext.BinaryClassification.Trainers.LbfgsLogisticRegression(
                    labelColumnName: nameof(SentimentData.Label),
                    featureColumnName: "Features",
                    l2Regularization: 0.01f,
                    l1Regularization: 0.01f));
        }

        public SentimentType Analyze(string text)
        {
            return AnalyzeDetailed(text).Sentiment;
        }

        /// <summary>
        /// Hibrit analiz: ML + Anahtar kelime
        /// </summary>
        public AnalysisResult AnalyzeDetailed(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return new AnalysisResult
                {
                    Sentiment = SentimentType.Negative,
                    Confidence = 0,
                    ProcessedText = text
                };
            }

            // 1. Anahtar kelime analizi
            var tokens = TextPreprocessor.CleanAndTokenize(text);
            var (positiveCount, negativeCount, hasNegation) = TextPreprocessor.AnalyzeKeywords(tokens);

            // 2. ML tahmin
            string processedText = TextPreprocessor.AdvancedPreprocess(text);
            SentimentPrediction? mlPrediction = null;

            if (_predictionEngine != null)
            {
                var input = new SentimentData { Text = processedText };
                mlPrediction = _predictionEngine.Predict(input);
            }

            // 3. Hibrit karar
            return MakeHybridDecision(positiveCount, negativeCount, hasNegation, mlPrediction, processedText);
        }

        private AnalysisResult MakeHybridDecision(int positiveCount, int negativeCount, 
            bool hasNegation, SentimentPrediction? mlPrediction, string processedText)
        {
            // ML skoru
            double mlScore = 0;
            bool mlIsPositive = false;
            
            if (mlPrediction != null)
            {
                mlIsPositive = mlPrediction.Prediction;
                mlScore = mlIsPositive 
                    ? (mlPrediction.Probability - 0.5) * 2
                    : -(1 - mlPrediction.Probability) * 2;
            }

            // Anahtar kelime skoru
            double keywordScore = 0;
            int totalKeywords = positiveCount + negativeCount;
            
            if (totalKeywords > 0)
            {
                keywordScore = (double)(positiveCount - negativeCount) / totalKeywords;
            }

            // Olumsuzluk kelimesi varsa ve pozitif kelime az veya esitse negatif bias
            if (hasNegation && positiveCount <= negativeCount)
            {
                keywordScore -= 0.2;
            }

            // Karar mantigi - anahtar kelime oncelikli
            double finalScore;
            
            if (totalKeywords >= 2)
            {
                // Cok anahtar kelime varsa kelimelere guven
                finalScore = (keywordScore * 0.7) + (mlScore * 0.3);
            }
            else if (totalKeywords == 1)
            {
                // Tek kelime varsa esit agirlik
                finalScore = (keywordScore * 0.5) + (mlScore * 0.5);
            }
            else
            {
                // Anahtar kelime yoksa ML'e guven
                finalScore = mlScore;
            }

            // Karar
            SentimentType sentiment = finalScore >= 0 ? SentimentType.Positive : SentimentType.Negative;
            
            // Guven skoru (0-1 arasi)
            double confidence = Math.Min(Math.Abs(finalScore), 1.0);

            return new AnalysisResult
            {
                Sentiment = sentiment,
                Confidence = confidence,
                ProcessedText = processedText
            };
        }

        public string GetMetricsReport()
        {
            return $"Dogruluk: {Accuracy:P1} | F1: {F1Score:P1} | Kesinlik: {Precision:P1} | Duyarlilik: {Recall:P1}";
        }
    }
}


