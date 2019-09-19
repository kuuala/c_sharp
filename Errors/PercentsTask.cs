public static double Calculate(string userInput)
{
	var parametrs = userInput.Split(' ');
	var initialMoney = double.Parse(parametrs[0]);
	var yearPercent = double.Parse(parametrs[1]);
	var monthCount = double.Parse(parametrs[2]);
	return initialMoney * Math.Pow(1 + yearPercent / 100 / 12, monthCount);
}
