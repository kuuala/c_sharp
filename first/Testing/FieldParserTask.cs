using System.Collections.Generic;
using NUnit.Framework;
using System.Text;
using System.Linq;

namespace TableParser
{
    [TestFixture]
    public class FieldParserTaskTests
    {
        public static void Test(string input, string[] expectedResult)
        {
            var actualResult = FieldsParserTask.ParseLine(input).Select(x => x.Value).ToList();
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            for (int i = 0; i < expectedResult.Length; ++i)
            {
                Assert.AreEqual(expectedResult[i], actualResult[i]);
            }
        }

        // Скопируйте сюда метод с тестами из предыдущей задачи.
        [TestCase("text", new[] { "text" })]
        [TestCase("hello world", new[] { "hello", "world" })]
        [TestCase("\'\' \"bcd ef\" \'x y\'", new[] { "", "bcd ef", "x y" })]
        [TestCase("", new string[0])]
        [TestCase("''", new[] { "" })]
        [TestCase("\"\\\\\"", new[] { "\\" })]
        [TestCase("\'\\\'test\\\'\'", new[] { "\'test\'" })]
        [TestCase("\"\\\"test\\\"\"", new[] { "\"test\"" })]
        [TestCase("  test  ignore  ", new[] { "test", "ignore" })]
        [TestCase("'first ", new[] { "first " })]
        [TestCase("first 'second'", new[] { "first", "second" })]
        [TestCase("'second' first", new[] { "second", "first" })]
        [TestCase("\"'first'\"", new[] { "'first'" })]
        [TestCase("'\"first\"'", new[] { "\"first\"" })]
        [TestCase("'a'b'c'", new[] { "a", "b", "c" })]
        [TestCase(@"\", new[] { @"\" })]
        // Вставляйте сюда свои тесты
        public static void RunTests(string input, string[] expectedOutput)
        {
            // Тело метода изменять не нужно
            Test(input, expectedOutput);
        }
    }

    public class FieldsParserTask
	{
        public static List<Token> ParseLine(string line)
		{
            var result = new List<Token>();
            var startIndex = SkipWhitespaces(line, 0);
            while (startIndex < line.Length)
            {
                var currentToken = ReadField(line, startIndex);
                if ((currentToken.Value != " ") || ((currentToken.Value == " ") && (currentToken.Length > 1)))
                    result.Add(currentToken);
                startIndex = SkipWhitespaces(line, currentToken.GetIndexNextToToken());
            }
            return result;
		}
		
		private static Token ReadField(string line, int startIndex)
		{
            switch (line[startIndex])
            {
                case ' ':
                    return new Token(" ", startIndex, 1);
                case '\'':
                    return ReadQuotedField(line, startIndex);
                case '"':
                    return ReadQuotedField(line, startIndex);
                default:
                    return ReadSimpleField(line, startIndex);
            }
		}

        private static Token ReadSimpleField(string line, int startIndex)
        {
            return ReadField(line, startIndex, new[] { ' ', '\'', '"' }, false);
        }

        private static Token ReadQuotedField(string line, int startIndex)
        {
            return ReadField(line, startIndex, new[] { line[startIndex] }, true);
        }

        private static Token ReadField(string line, int startIndex, char[] stopChars, bool quotes)
        {
            var value = new StringBuilder();
            var i = startIndex;
            if (quotes) i++; //пропускаем открывающую кавычку
            while ((i < line.Length) && !stopChars.Contains(line[i]))
            {
                if (line[i] == '\\' && quotes)
                    ++i;
                if (i < line.Length)
                    value.Append(line[i++]);
                else
                {
                    --i;
                    break;
                }
            }   
            if (quotes && i < line.Length && line[i] == stopChars[0]) i++; //добавляем закрывающий символ
            return new Token(value.ToString(), startIndex, i - startIndex);
        }

        private static int SkipWhitespaces(string line, int startIndex)
        {
            var i = startIndex;
            while ((i < line.Length) && (line[i] == ' '))
                ++i;
            return i;
        }
    }
}
