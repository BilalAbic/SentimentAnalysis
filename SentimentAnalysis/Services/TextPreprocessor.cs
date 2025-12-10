using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SentimentAnalysis.Services
{
    /// <summary>
    /// Turkce metin on isleme sinifi - Gelistirilmis versiyon
    /// </summary>
    internal static class TextPreprocessor
    {
        // Turkce olumsuzluk kelimeleri
        private static readonly HashSet<string> NegationWords = new(StringComparer.OrdinalIgnoreCase)
        {
            "degil", "yok", "hic", "asla", "olmaz", "olmadi", "hayir", "hicbir"
        };

        // Zitlik/Ters ceviren kelimeler (bundan sonraki kisim onemli)
        private static readonly HashSet<string> ContrastWords = new(StringComparer.OrdinalIgnoreCase)
        {
            "ama", "fakat", "ancak", "lakin", "oysa", "yerine", "aksine", "tersine",
            "ragmen", "karsın", "karsin"
        };

        // Yogunlastirici kelimeler (etkiyi arttirir)
        private static readonly HashSet<string> IntensifierWords = new(StringComparer.OrdinalIgnoreCase)
        {
            "cok", "asiri", "son derece", "oldukca", "fazla", "daha", "epey",
            "gercekten", "harbiden", "bayagi", "iyice", "adamakilli"
        };

        // Olumlu anahtar kelimeler
        private static readonly HashSet<string> PositiveKeywords = new(StringComparer.OrdinalIgnoreCase)
        {
            // Duygular
            "mutlu", "sevinc", "sevindim", "memnun", "hosnut", "keyifli", "neseli",
            "huzur", "huzurlu", "rahat", "ferah", "umut", "umutlu", "heyecan", "heyecanli",
            
            // Degerler
            "guzel", "harika", "mukemmel", "super", "muhtesem", "fevkalade", "enfes", "olaganustu",
            "iyi", "hos", "tatli", "sevimli", "sicak", "samimi", "essiz", "nadide",
            
            // Basari
            "basari", "basarili", "basardim", "kazandim", "zafer", "galip", "birinci", "sampion",
            "gurur", "gururlu", "gururlandim", "ovunc", "iftihar",
            
            // Olumlu fiiller
            "seviyorum", "bayildim", "asikim", "tutkunum", "hayranim", "begendim",
            "etkilendim", "buyulendim", "rahatlattı", "rahatlatti", "ferahlattı", "ferahlatti",
            
            // Diger
            "tesekkur", "minnet", "sukur", "sans", "sansli", "talihli",
            "ozgur", "degerli", "onemli", "ilham", "motive", "motivasyon",
            "enerji", "enerjik", "canli", "dinc", "verimli", "uretken",
            "hatasiz", "kusursuz", "eksiksiz", "dogru", "isabetli"
        };

        // Olumsuz anahtar kelimeler
        private static readonly HashSet<string> NegativeKeywords = new(StringComparer.OrdinalIgnoreCase)
        {
            // Duygular
            "uzgun", "uzuntu", "mutsuz", "kederli", "huzun", "huzunlu", "kasvetli",
            "sinir", "sinirli", "kizgin", "ofke", "ofkeli", "hiddet", "hiddetli",
            "korku", "korkulu", "endise", "endiseli", "kaygi", "kaygili", "tedirgin",
            
            // Degerler  
            "kotu", "berbat", "rezalet", "felaket", "korkunc", "igrenc", "cirkin",
            "sikici", "bunaltici", "biktirici", "yorucu", "yipratici",
            
            // Basarisizlik
            "basarisiz", "kaybettim", "yenildim", "battim", "cokus", "hezimet",
            "hayal kirikligi", "duskunluk", "mahcup", "utanc",
            
            // Olumsuz fiiller
            "nefret", "tiksiniyorum", "pisman", "pismanim", "uzuldum", "agliyorum",
            "kirildim", "kirildi", "incindin", "yaralandi",
            
            // Yalnizlik ve olumsuz durumlar
            "yalniz", "yalnizlik", "yalnızlık", "yalnızlığı", "yalnizligi",
            "caresiz", "aciz", "zavalli", "issiz", "terk", "terkedilmis",
            
            // Diger
            "stres", "stresli", "yorgun", "yorgunluk", "tukenmis", "bunalmis", "sikinti",
            "bozuk", "hatali", "yanlis", "eksik", "yetersiz",
            "derin", "derinlesti", "derinlestirdi", "agırlasti", "agirlasti", "kotulesti"
        };

        /// <summary>
        /// Metni temizler ve token'lara ayirir
        /// </summary>
        internal static List<string> CleanAndTokenize(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new List<string>();

            text = NormalizeTurkish(text.ToLowerInvariant());
            text = Regex.Replace(text, @"[^\w\s]", " ");
            text = Regex.Replace(text, @"\s+", " ").Trim();

            return text.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                       .Where(x => x.Length > 1)
                       .ToList();
        }

        /// <summary>
        /// Turkce karakterleri normalize eder
        /// </summary>
        private static string NormalizeTurkish(string text)
        {
            return text
                .Replace("ı", "i").Replace("İ", "i")
                .Replace("ğ", "g").Replace("Ğ", "g")
                .Replace("ü", "u").Replace("Ü", "u")
                .Replace("ş", "s").Replace("Ş", "s")
                .Replace("ö", "o").Replace("Ö", "o")
                .Replace("ç", "c").Replace("Ç", "c");
        }

        /// <summary>
        /// Basit on isleme
        /// </summary>
        internal static string AdvancedPreprocess(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            text = NormalizeTurkish(text.ToLowerInvariant());
            text = Regex.Replace(text, @"[^\w\s]", " ");
            text = Regex.Replace(text, @"\s+", " ").Trim();

            return text;
        }

        /// <summary>
        /// Metindeki duygu skorunu hesaplar - Gelistirilmis versiyon
        /// </summary>
        internal static (int positive, int negative, bool hasNegation) AnalyzeKeywords(List<string> tokens)
        {
            int positiveCount = 0;
            int negativeCount = 0;
            bool hasNegation = false;
            bool afterContrast = false; // Zitlik kelimesinden sonra mi?
            int contrastIndex = -1;

            // Once zitlik kelimesi var mi kontrol et
            for (int i = 0; i < tokens.Count; i++)
            {
                if (ContrastWords.Contains(tokens[i]))
                {
                    afterContrast = true;
                    contrastIndex = i;
                    break;
                }
            }

            for (int i = 0; i < tokens.Count; i++)
            {
                string token = tokens[i];
                
                // Zitlik kelimesinden sonraki kisma daha fazla agirlik ver
                double weight = 1.0;
                if (afterContrast && i > contrastIndex)
                {
                    weight = 1.5; // Zitliktan sonraki kisim daha onemli
                }

                // Olumsuzluk kelimesi kontrolu
                if (NegationWords.Contains(token))
                {
                    hasNegation = true;
                    continue;
                }

                // Yogunlastirici kontrolu
                bool hasIntensifier = (i > 0 && IntensifierWords.Contains(tokens[i - 1]));
                if (hasIntensifier) weight *= 1.3;

                // Olumlu kelime kontrolu
                if (PositiveKeywords.Contains(token) || ContainsPositiveRoot(token))
                {
                    if (i > 0 && NegationWords.Contains(tokens[i - 1]))
                    {
                        negativeCount += (int)Math.Ceiling(weight);
                    }
                    else
                    {
                        positiveCount += (int)Math.Ceiling(weight);
                    }
                }

                // Olumsuz kelime kontrolu
                if (NegativeKeywords.Contains(token) || ContainsNegativeRoot(token))
                {
                    negativeCount += (int)Math.Ceiling(weight);
                }

                // Fiil olumsuzluk eki kontrolu
                if (HasClearNegationSuffix(token))
                {
                    negativeCount += (int)Math.Ceiling(weight);
                }
            }

            return (positiveCount, negativeCount, hasNegation);
        }

        /// <summary>
        /// Kelimenin olumlu kok icerip icermedigini kontrol eder
        /// </summary>
        private static bool ContainsPositiveRoot(string word)
        {
            string[] positiveRoots = { "mutlu", "sevin", "huzur", "guzel", "iyi", "basari", "umut" };
            foreach (var root in positiveRoots)
            {
                if (word.StartsWith(root)) return true;
            }
            return false;
        }

        /// <summary>
        /// Kelimenin olumsuz kok icerip icermedigini kontrol eder
        /// </summary>
        private static bool ContainsNegativeRoot(string word)
        {
            string[] negativeRoots = { "uzgun", "uzuntu", "yalniz", "kotu", "sikinti", "kaygi", "korku", "derin" };
            foreach (var root in negativeRoots)
            {
                if (word.StartsWith(root)) return true;
            }
            return false;
        }

        /// <summary>
        /// Kelimenin acik olumsuzluk eki icerip icermedigini kontrol eder
        /// </summary>
        private static bool HasClearNegationSuffix(string word)
        {
            if (word.Length < 5) return false;

            string[] clearPatterns = { "madim", "medim", "mamis", "memis", "miyor", "muyor" };

            foreach (var pattern in clearPatterns)
            {
                if (word.EndsWith(pattern))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// N-Gram olusturur
        /// </summary>
        internal static List<string> GenerateNGrams(List<string> tokens, int n = 2)
        {
            var ngrams = new List<string>(tokens);

            if (n >= 2 && tokens.Count > 1)
            {
                for (int i = 0; i < tokens.Count - 1; i++)
                {
                    ngrams.Add($"{tokens[i]}_{tokens[i + 1]}");
                }
            }

            return ngrams;
        }
    }
}


