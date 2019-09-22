using System;
using System.Drawing;
using NUnit.Framework;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        /// <summary>
        /// По значению углов суставов возвращает массив координат суставов
        /// в порядке new []{elbow, wrist, palmEnd}
        /// </summary>
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {
            var elbowPos = new PointF(
                GetX(Manipulator.UpperArm, shoulder),
                GetY(Manipulator.UpperArm, shoulder));
            var newAngle = GetAngle(shoulder, elbow);
            var wristPos = new PointF(
                elbowPos.X + GetX(Manipulator.Forearm, newAngle),
                elbowPos.Y + GetY(Manipulator.Forearm, newAngle));
            newAngle = GetAngle(newAngle, wrist);
            var palmEndPos = new PointF(
                wristPos.X + GetX(Manipulator.Palm, newAngle),
                wristPos.Y + GetY(Manipulator.Palm, newAngle));
            return new PointF[]
            {
                elbowPos,
                wristPos,
                palmEndPos
            };
        }

        private static float GetX(double line, double angle)
            => (float)(line * Math.Cos(angle));

        private static float GetY(double line, double angle)
            => (float)(line * Math.Sin(angle));

        private static double GetAngle(double first, double second)
            => first + second - Math.PI;
    }

    [TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
        [TestCase(Math.PI / 2, 0, - Math.PI / 2, Manipulator.Palm, Manipulator.UpperArm - Manipulator.Forearm)]
        [TestCase(Math.PI / 2, - Math.PI / 2, - Math.PI, -Manipulator.Forearm - Manipulator.Palm, Manipulator.UpperArm)]
        public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
        }

        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, 0, Manipulator.UpperArm)]
        [TestCase(0, 0, -Math.PI / 2, Manipulator.UpperArm, 0)]
        [TestCase(Math.PI, -Math.PI / 2, -Math.PI, -Manipulator.UpperArm, 0)]
        public void TestGetUpperArmPositions(double shoulder, double elbow, double wrist, double cordX, double cordY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(cordX, joints[0].X, 1e-5, "X");
            Assert.AreEqual(cordY, joints[0].Y, 1e-5, "Y");
        }

        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm, Manipulator.UpperArm)]
        [TestCase(0, -Math.PI / 2, -Math.PI / 2, Manipulator.UpperArm, Manipulator.Forearm)]
        [TestCase(Math.PI, Math.PI / 2, -Math.PI, -Manipulator.UpperArm, Manipulator.Forearm)]
        public void TestGetForeArmPositions(double shoulder, double elbow, double wrist, double cordX, double cordY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(cordX, joints[1].X, 1e-5, "X");
            Assert.AreEqual(cordY, joints[1].Y, 1e-5, "Y");
        }
    }
}
