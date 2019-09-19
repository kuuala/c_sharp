using System;

namespace Fractals
{
	internal static class DragonFractalTask
	{
		public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
		{
            double x = 1;
            double y = 0;
            double dx, dy;
            var random = new Random(seed);
            for (int i = 0; i < iterationsCount; ++i)
            {
                var type = random.Next(2);
                dx = GetX(x, y, type);
                dy = GetY(x, y, type);
                x = dx;
                y = dy;
                pixels.SetPixel(x, y);
            }
		}

        public static double GetX(double x, double y, int type)
        {
            if (type == 0)
                return (x * Math.Cos(Math.PI / 4) - y * Math.Sin(Math.PI / 4)) / Math.Sqrt(2);
            return (x * Math.Cos(3 * Math.PI / 4) - y * Math.Sin(3 * Math.PI / 4)) / Math.Sqrt(2) + 1;
        }

        public static double GetY(double x, double y, int type)
        {
            if (type == 0)
                return (x * Math.Sin(Math.PI / 4) + y * Math.Cos(Math.PI / 4)) / Math.Sqrt(2);
            return (x * Math.Sin(3 * Math.PI / 4) + y * Math.Cos(3 * Math.PI / 4)) / Math.Sqrt(2);
        }
    }
}
