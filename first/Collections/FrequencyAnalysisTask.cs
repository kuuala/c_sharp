using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();
            AddMostFrequentNextWords(result, text, 2);
            AddMostFrequentNextWords(result, text, 3);
            return result;
        }

        private static Dictionary<string, Dictionary<string, int>> GetFrequentDictionary(
            List<List<string>> text,
            int rankN)
        {
            var result = new Dictionary<string, Dictionary<string, int>>();
            foreach (var sentence in text)
            {
                int count = sentence.Count - rankN;
                if (count >= 0)
                    for (int i = 0; i <= count; ++i)
                    {
                        var start = string.Join(" ", sentence.GetRange(i, rankN - 1));
                        var end = sentence[i + rankN - 1];
                        if (result.ContainsKey(start))
                            if (result[start].ContainsKey(end))
                                result[start][end]++;
                            else
                                result[start].Add(end, 1);
                        else
                        {
                            result.Add(start, new Dictionary<string, int>());
                            result[start].Add(end, 1);
                        }
                    }
            }
            return result;
        }

        private static Dictionary<string, string> GetMostFrequentNGramm(
            Dictionary<string, Dictionary<string, int>> dict)
        {
            var result = new Dictionary<string, string>();
            foreach (var startN in dict)
            {
                var maxValue = startN.Value.Max(x => x.Value);
                var dictWithMostFrequent = startN.Value.Where(x => x.Value == maxValue);
                var listFrequent = new List<string>();
                foreach (var x in dictWithMostFrequent)
                    listFrequent.Add(x.Key);
                var nextWord = GetLowerOrderStringFromList(listFrequent);
                result.Add(startN.Key, nextWord);
            }
            return result;
        }

        private static string GetLowerOrderStringFromList(List<string> list)
        {
            var str = list[0];
            for (int i = 1; i < list.Count; ++i)
                if (string.CompareOrdinal(str, list[i]) > 0)
                    str = list[i];
            return str;
        }

        private static void AddMostFrequentNextWords(
            Dictionary<string, string> dict,
            List<List<string>> text,
            int rankN)
        {
            var nDictionary = GetFrequentDictionary(text, rankN);
            var nGramm = GetMostFrequentNGramm(nDictionary);
            foreach (var gramm in nGramm)
                dict.Add(gramm.Key, gramm.Value);
        }
    }
}
