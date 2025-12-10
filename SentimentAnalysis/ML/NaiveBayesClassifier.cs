using System;
using System.Collections.Generic;
using SentimentAnalysis.Entities;

namespace SentimentAnalysis.ML
{
    /// <summary>
    /// Sadeleştirilmiş Naive Bayes Sınıflandırıcısı
    /// </summary>
    public class NaiveBayesClassifier
    {
        private Dictionary<SentimentType, Dictionary<string, int>> _wordCounts = new();
        private Dictionary<SentimentType, int> _totalWords = new();
        private Dictionary<SentimentType, int> _documentCounts = new();
        private HashSet<string> _vocabulary = new();
        private int _totalDocuments;
        private const double Alpha = 1.0; // Laplace smoothing

        public void Train(Dictionary<SentimentType, List<List<string>>> data)
        {
            _wordCounts.Clear();
            _totalWords.Clear();
            _documentCounts.Clear();
            _vocabulary.Clear();
            _totalDocuments = 0;

            foreach (var category in data)
            {
                var wordFreq = new Dictionary<string, int>();
                int totalWordsInCategory = 0;

                _documentCounts[category.Key] = category.Value.Count;
                _totalDocuments += category.Value.Count;

                foreach (var sentence in category.Value)
                {
                    foreach (var word in sentence)
                    {
                        _vocabulary.Add(word);

                        if (!wordFreq.ContainsKey(word))
                            wordFreq[word] = 0;

                        wordFreq[word]++;
                        totalWordsInCategory++;
                    }
                }

                _wordCounts[category.Key] = wordFreq;
                _totalWords[category.Key] = totalWordsInCategory;
            }
        }

        public SentimentType Predict(List<string> input)
        {
            var (sentiment, _) = PredictWithConfidence(input);
            return sentiment;
        }

        public (SentimentType sentiment, double confidence) PredictWithConfidence(List<string> input)
        {
            if (input.Count == 0 || _totalDocuments == 0)
                return (SentimentType.Negative, 0.5);

            double posScore = CalculateLogScore(input, SentimentType.Positive);
            double negScore = CalculateLogScore(input, SentimentType.Negative);

            // Softmax normalizasyon
            double maxScore = Math.Max(posScore, negScore);
            double posProb = Math.Exp(posScore - maxScore);
            double negProb = Math.Exp(negScore - maxScore);
            double total = posProb + negProb;

            double posProbNorm = posProb / total;
            double negProbNorm = negProb / total;

            if (posProbNorm > negProbNorm)
                return (SentimentType.Positive, posProbNorm);
            else
                return (SentimentType.Negative, negProbNorm);
        }

        private double CalculateLogScore(List<string> input, SentimentType type)
        {
            // Prior: log P(class)
            double score = Math.Log((double)_documentCounts[type] / _totalDocuments);

            int vocabSize = _vocabulary.Count;
            int totalWordsInClass = _totalWords[type];

            foreach (var word in input)
            {
                int wordCount = _wordCounts[type].TryGetValue(word, out int count) ? count : 0;

                // Laplace smoothing: P(word|class) = (count + alpha) / (total + alpha * V)
                double probability = (wordCount + Alpha) / (totalWordsInClass + Alpha * vocabSize);
                score += Math.Log(probability);
            }

            return score;
        }
    }
}


