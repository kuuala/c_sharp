using System;

namespace DistanceTask
{
	public static class DistanceTask
	{
        // Расстояние от точки до точки
        public static double GetDistanceToPoint(double x0, double y0, double x1, double y1)
        {
            return Math.Sqrt(Math.Pow(x1 - x0, 2) + Math.Pow(y1 - y0, 2));
        }

        public static double GetScalarMultiply(double ax, double ay, double bx, double by, double x, double y)
        {
            var v1x = x - ax;
            var v1y = y - ay;
            var v2x = bx - ax;
            var v2y = by - ay;
            return v1x * v2x + v1y * v2y;
        }

        public static bool IsVerticalInSegment(double ax, double ay, double bx, double by, double x, double y)
        {
            return (GetScalarMultiply(ax, ay, bx, by, x, y) >= 0) && (GetScalarMultiply(bx, by, ax, ay, x, y) >= 0);
        }

        public static double GetVerticalLength(double ax, double ay, double bx, double by, double x, double y)
        {
            return Math.Abs(((bx - ax) * (y - ay) - (by - ay) * (x - ax)) / GetDistanceToPoint(ax, ay, bx, by));
        }

		// Расстояние от точки (x, y) до отрезка AB с координатами A(ax, ay), B(bx, by)
		public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
		{
            if ((ax == bx) && (ay == by))
                return GetDistanceToPoint(x, y, ax, ay);
            if (IsVerticalInSegment(ax, ay, bx, by, x, y))
                return GetVerticalLength(ax, ay, bx, by, x, y);
			return Math.Min(GetDistanceToPoint(x, y, ax, ay), GetDistanceToPoint(x, y, bx, by));
		}
	}
}
