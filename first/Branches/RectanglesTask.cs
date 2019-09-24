using System;

namespace Rectangles
{
	public static class RectanglesTask
	{
        // Пересекаются ли проекции
        public static bool AreProjectionIntersected(int c0, int len0, int c1, int len1)
        {
            return c0 < c1 ? (c0 + len0) >= c1 : (c1 + len1) >= c0;
        }

		// Пересекаются ли два прямоугольника (пересечение только по границе также считается пересечением)
		public static bool AreIntersected(Rectangle r1, Rectangle r2)
		{
            // так можно обратиться к координатам левого верхнего угла первого прямоугольника: r1.Left, r1.Top
            return AreProjectionIntersected(r1.Top, r1.Height, r2.Top, r2.Height) &&
                AreProjectionIntersected(r1.Left, r1.Width, r2.Left, r2.Width);
		}

        // Площадь пересечения
        public static int IntersectSquare(Rectangle r1, Rectangle r2)
        {
            var xIntersection = Math.Min(r1.Right, r2.Right) - (r1.Left < r2.Left ? r2.Left : r1.Left);
            var yIntersection = Math.Min(r1.Bottom, r2.Bottom) - (r1.Top < r2.Top ? r2.Top : r1.Top);
            return xIntersection * yIntersection;
        }

		// Площадь пересечения прямоугольников
		public static int IntersectionSquare(Rectangle r1, Rectangle r2)
		{
			return AreIntersected(r1, r2) ? IntersectSquare(r1, r2) : 0;
		}
        
        // Содержит ли r1 вершины r2
        public static bool IsContainPoints(Rectangle r1, Rectangle r2)
        {
            return (r1.Left <= r2.Left) && (r1.Top <= r2.Top)
                && (r1.Right >= r2.Right) && (r1.Bottom >= r2.Bottom);
        }

		// Если один из прямоугольников целиком находится внутри другого — вернуть номер (с нуля) внутреннего.
		// Иначе вернуть -1
		// Если прямоугольники совпадают, можно вернуть номер любого из них.
		public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
		{
            if (AreIntersected(r1, r2))
                if (IsContainPoints(r1, r2))
                    return 1;
                else if (IsContainPoints(r2, r1))
                    return 0;
			return -1;
		}
	}
}
