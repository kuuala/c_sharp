[TestCase("text", new[] {"text"})]
[TestCase("hello world", new[] {"hello", "world"})]
[TestCase("\'\' \"bcd ef\" \'x y\'", new [] {"", "bcd ef", "x y"})]
[TestCase("", new string[0])]
[TestCase("''", new [] {""})]
[TestCase("\"\\\\\"", new[] { "\\" })]
[TestCase("\'\\\'test\\\'\'", new[] {"\'test\'"})]
[TestCase("\"\\\"test\\\"\"", new[] {"\"test\""})]
[TestCase("  test  ignore  ", new [] {"test", "ignore"})]
[TestCase("'first ", new [] {"first "})]
[TestCase("first 'second'", new [] {"first", "second"})]
[TestCase("'second' first", new [] {"second", "first"})]
[TestCase("\"'first'\"", new [] {"'first'"})]
[TestCase("'\"first\"'", new [] {"\"first\""})]
[TestCase("'a'b'c'", new [] {"a", "b", "c"})]
// Вставляйте сюда свои тесты
public static void RunTests(string input, string[] expectedOutput)
{
    // Тело метода изменять не нужно
    Test(input, expectedOutput);
}
