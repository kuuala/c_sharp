using System.Collections.Generic;

namespace Recognizer
{
	public static class ThresholdFilterTask
	{
		public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
		{
            var lenX = original.GetLength(0);
            var lenY = original.GetLength(1);
            var result = new double[lenX, lenY];
            var tValue = GetTValue(original, whitePixelsFraction);
            for (var x = 0; x < lenX; ++x)
                for (var y = 0; y < lenY; ++y)
                    if (original[x, y] >= tValue)
                        result[x, y] = 1;
                    else
                        result[x, y] = 0;
            return result;
		}

        private static double GetTValue(double[,] original, double whitePixelsFraction)
        {
            var pixels = new List<double>();
            foreach (var p in original)
                pixels.Add(p);
            pixels.Sort();
            var pCount = pixels.Count;
            var minWhite = (int)(whitePixelsFraction * pCount);
            if (minWhite <= 0)
                return double.MaxValue;
            if (minWhite <= pCount)
                return pixels[pCount - minWhite];
            return double.MinValue;
        }
	}
}
