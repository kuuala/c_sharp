using System.Text;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("''", 0, "", 2)]
        [TestCase("'a'", 0, "a", 3)]
        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(actualToken, new Token(expectedValue, startIndex, expectedLength));
        }

        // Добавьте свои тесты
        [TestCase("\"a 'b' 'c' d\" '\"1\" \"2\" \"3\"'", 0, "a 'b' 'c' d", 13)]
        [TestCase("\"a 'b' 'c' d\" '\"1\" \"2\" \"3\"'", 14, "\"1\" \"2\" \"3\"", 13)]
        [TestCase("some_text \"QF \\\"\"\"\" other_text", 10, "QF \"", 7)]
        [TestCase("\'\' \"bcd ef\" \'x y\'", 0, "", 2)]
        [TestCase("\'\' \"bcd ef\" \'x y\'", 3, "bcd ef", 8)]
        [TestCase("\'\' \"bcd ef\" \'x y\'", 12, "x y", 5)]
        [TestCase("abc \"def g h", 4, "def g h", 8)]
        [TestCase("\"\\\\\"b", 0, "\\", 4)]
        public void MyTests(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(actualToken, new Token(expectedValue, startIndex, expectedLength));
        }
    }

    class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            var builder = new StringBuilder();
            var isEcraned = false;
            var currentIndex = startIndex;
            var startChar = line[currentIndex++];
            while (currentIndex < line.Length)
            {
                var currentChar = line[currentIndex++];
                if (currentChar == '\\')
                    isEcraned = AddCharReturnNewEcran(builder, currentChar, isEcraned);
                else if (currentChar == startChar)
                {
                    if (!isEcraned) break;
                    isEcraned = AddCharReturnNewEcran(builder, currentChar, isEcraned);
                }
                else builder.Append(currentChar);
            }
            return new Token(builder.ToString(), startIndex, currentIndex - startIndex);
        }

        private static bool AddCharReturnNewEcran(StringBuilder builder, char currentChar, bool isEcraned)
        {
            if (isEcraned)
            {
                builder.Append(currentChar);
                return false;
            }
            return true;
        }
    }
}
