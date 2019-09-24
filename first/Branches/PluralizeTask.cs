namespace Pluralize
{
    public static class PluralizeTask
    {
        public static string PluralizeRubles(int count)
        {
            // Напишите функцию склонения слова "рублей" в зависимости от предшествующего числительного count.
            var countModHundred = count % 100;
            if (countModHundred < 10 || countModHundred > 20)
            {
                var lastDigit = countModHundred % 10;
                return lastDigit == 1 ? "рубль" : (1 < lastDigit && lastDigit < 5) ? "рубля" : "рублей";
            }
            return "рублей";
		}
	}
}
