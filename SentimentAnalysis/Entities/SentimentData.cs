using Microsoft.ML.Data;

namespace SentimentAnalysis.Entities
{
    /// <summary>
    /// ML.NET e?itim verisi için veri modeli
    /// </summary>
    public class SentimentData
    {
        [LoadColumn(0)]
        public string Text { get; set; } = string.Empty;

        [LoadColumn(1)]
        public bool Label { get; set; } // True: Olumlu, False: Olumsuz
    }

    /// <summary>
    /// ML.NET tahmin sonucu için veri modeli
    /// </summary>
    public class SentimentPrediction : SentimentData
    {
        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }

        public float Probability { get; set; }

        public float Score { get; set; }
    }

    /// <summary>
    /// Detayl? analiz sonucu
    /// </summary>
    public class AnalysisResult
    {
        public SentimentType Sentiment { get; set; }
        public double Confidence { get; set; }
        public string ProcessedText { get; set; } = string.Empty;
    }
}
