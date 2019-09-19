using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        private static readonly char[] _endOfSentence = { '.', '!', '?', ';', ':', '(', ')'};

        private static bool IsCharOrApostrophe(char c)
        {
            return char.IsLetter(c) || (c == '\'');
        }

        private static bool IsCharEndOfSentece(char c)
        {
            return _endOfSentence.Contains(c);
        }

        private static void AddWordInListAndClearWord(StringBuilder word, List<string> list)
        {
            list.Add(word.ToString().ToLower());
            word.Clear();
        }

        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            var sentences = text.Split(_endOfSentence);
            var word = new StringBuilder();
            foreach (var sentence in sentences)
            {
                var currentSentence = new List<string>();
                foreach (var c in sentence)
                {
                    if (IsCharOrApostrophe(c))
                        word.Append(c);
                    else if (word.Length != 0)
                        AddWordInListAndClearWord(word, currentSentence);
                }
                if (word.Length != 0)
                    AddWordInListAndClearWord(word, currentSentence);
                if (currentSentence.Count != 0)
                    sentencesList.Add(currentSentence);
            }
            return sentencesList;
        }
    }
}
