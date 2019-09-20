using System.Collections.Generic;

namespace Recognizer
{
    internal static class MedianFilterTask
    {
        /* 
		 * Для борьбы с пиксельным шумом, подобным тому, что на изображении,
		 * обычно применяют медианный фильтр, в котором цвет каждого пикселя, 
		 * заменяется на медиану всех цветов в некоторой окрестности пикселя.
		 * https://en.wikipedia.org/wiki/Median_filter
		 * 
		 * Используйте окно размером 3х3 для не граничных пикселей,
		 * Окно размером 2х2 для угловых и 3х2 или 2х3 для граничных.
		 */
        public static double[,] MedianFilter(double[,] original)
        {
            var lenX = original.GetLength(0);
            var lenY = original.GetLength(1);
            var result = new double[lenX, lenY];
            for (var x = 0; x < lenX; ++x)
                for (var y = 0; y < lenY; ++y)
                    result[x, y] = GetMedianValue(x, y, lenX, lenY, original);
            return result;
        }

        private static double GetMedianValue(int x, int y, int lenX, int lenY, double[,] original)
        {
            var values = new List<double>();
            for (int i = -1; i <= 1; i++)
                for (int j = -1; j <= 1; j++)
                    if (IsInRange(x + i, y + j, lenX, lenY))
                        values.Add(original[x + i, y + j]);
            values.Sort();
            return GetMedianFromSortedList(values);
        }

        private static double GetMedianFromSortedList(List<double> list)
        {
            var len = list.Count;
            if (len % 2 == 0)
            {
                var p1 = list[len / 2];
                var p2 = list[(len / 2) - 1];
                return (p1 + p2) / 2;
            }
            return list[len / 2];
        }

        private static bool IsInRange(int x, int y, int lenX, int lenY)
        {
            return (0 <= x) && (x < lenX) && (0 <= y) && (y < lenY);
        }
    }
}
