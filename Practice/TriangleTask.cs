using System;
using NUnit.Framework;

namespace Manipulation
{
    public class TriangleTask
    {
        /// <summary>
        /// Возвращает угол (в радианах) между сторонами a и b в треугольнике со сторонами a, b, c 
        /// </summary>
        public static double GetABAngle(double a, double b, double c)
            => IsTriangle(a, b, c) ? Math.Acos((a * a + b * b - c * c) / (2 * a * b)) : double.NaN;
        

        private static bool IsTriangle(double a, double b, double c)
            => a >= 0 && b >= 0 && c >= 0 && a + b >= c && a + c >= b && b + c >= a;
    }

    [TestFixture]
    public class TriangleTask_Tests
    {
        [TestCase(3, 4, 5, Math.PI / 2)]
        [TestCase(1, 1, 1, Math.PI / 3)]
        [TestCase(1, 1, 2, Math.PI)]
        [TestCase(1, 2, 1, 0)]
        [TestCase(1, 1, 0, 0)]
        [TestCase(-3, 4, 5, double.NaN)]
        [TestCase(3, -4, 5, double.NaN)]
        [TestCase(3, 4, -5, double.NaN)]
        [TestCase(0, 4, 5, double.NaN)]
        [TestCase(3, 0, 3, double.NaN)]
        [TestCase(3, 4, 0, double.NaN)]
        [TestCase(0, 0, 0, double.NaN)]
        public void TestGetABAngle(double a, double b, double c, double expectedAngle)
        {
            Assert.AreEqual(expectedAngle, TriangleTask.GetABAngle(a, b, c), 1e-5);
        }
    }
}
