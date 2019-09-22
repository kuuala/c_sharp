using System;
using NUnit.Framework;
using static Manipulation.Manipulator;

namespace Manipulation
{
    public static class ManipulatorTask
    {
        /// <summary>
        /// Возвращает массив углов (shoulder, elbow, wrist),
        /// необходимых для приведения эффектора манипулятора в точку x и y 
        /// с углом между последним суставом и горизонталью, равному angle (в радианах)
        /// См. чертеж manipulator.png!
        /// </summary>
        public static double[] MoveManipulatorTo(double x, double y, double angle)
        {
            var wristX = x - Palm * Math.Cos(angle);
            var wristY = y + Palm * Math.Sin(angle);
            var shoulderWristLen = Math.Sqrt(wristX * wristX + wristY * wristY);
            var elbow = TriangleTask.GetABAngle(Forearm, UpperArm, shoulderWristLen);
            var shoulder = TriangleTask.GetABAngle(shoulderWristLen, UpperArm, Forearm)
                + Math.Atan2(wristY, wristX);
            var wrist = -angle - elbow - shoulder;
            return new[] { shoulder, elbow, wrist };
        }
    }

    [TestFixture]
    public class ManipulatorTask_Tests
    {
        [Test]
        public void TestMoveManipulatorTo()
        {
            var random = new Random();
            for (int i = 0; i < 10; ++i)
            {
                var x = random.Next(200);
                var y = random.Next(200);
                var angle = Math.PI * random.NextDouble();
                var result = ManipulatorTask.MoveManipulatorTo(x, y, angle);
                var joints = AnglesToCoordinatesTask.GetJointPositions(result[0],
                    result[1], result[2]);
                Assert.AreEqual(x, joints[2].X, 1e-4);
                Assert.AreEqual(y, joints[2].Y, 1e-4);
            }
        }
    }
}
