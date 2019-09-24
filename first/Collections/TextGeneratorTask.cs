using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            var result = new StringBuilder(phraseBeginning);
            var words = phraseBeginning.Split(' ');
            var lastWords = words.Skip(words.Length - 2).ToList();
            for (int i = 0; i < wordsCount; ++i)
            {
                if (lastWords.Count == 0) break;
                var keys = FindKeys(lastWords);
                var newWord = AddNextWord(nextWords, keys, result);
                if (newWord == null) break;
                ChangeLastWords(lastWords, newWord);
            }
            return result.ToString();
        }

        private static string AddNextWord(
            Dictionary<string, string> nextWords,
            List<string> keys,
            StringBuilder builder)
        {
            foreach (var key in keys)
                if (nextWords.ContainsKey(key))
                {
                    var newWord = nextWords[key];
                    builder.Append(" " + newWord);
                    return newWord;
                }
            return null;
        }

        private static List<string> FindKeys(List<string> lastWords)
        {
            var keys = new List<string>();
            for (int i = 0; i < lastWords.Count; ++i)
            {
                var currentKey = string.Join(" ", lastWords.Skip(i));
                keys.Add(currentKey);
            }
            return keys;
        }

        private static void ChangeLastWords(List<string> lastWords, string newWord)
        {
            if (lastWords.Count == 1)
                lastWords.Add(newWord);
            else
            {
                lastWords.RemoveAt(0);
                lastWords.Add(newWord);
            }
        }
    }
}
