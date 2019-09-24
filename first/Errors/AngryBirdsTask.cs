using System;

namespace AngryBirds
{
	public static class AngryBirdsTask
	{
        const double GravityAcceleration = 9.8;

		// Ниже — это XML документация, её использует ваша среда разработки, 
		// чтобы показывать подсказки по использованию методов. 
		// Но писать её естественно не обязательно.
		/// <param name="v">Начальная скорость</param>
		/// <param name="distance">Расстояние до цели</param>
		/// <returns>Угол прицеливания в радианах от 0 до Pi/2</returns>
		public static double FindSightAngle(double v, double distance)
		{
            return 0.5 * Math.Asin(distance * GravityAcceleration / (v * v));
		}
	}
}
