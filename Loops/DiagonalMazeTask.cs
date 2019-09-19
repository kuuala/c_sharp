using System;

namespace Mazes
{
	public static class DiagonalMazeTask
	{
        public static void MoveToDirection(Robot robot, Direction direction, int stepCount)
        {
            for (int i = 0; i < stepCount; ++i)
                robot.MoveTo(direction);
        }
        
        public static Direction GetFirstDirection(int width, int height)
        {
            return width > height ? Direction.Right : Direction.Down;
        }

        public static Direction GetNextDirection (Direction direction)
        {
            return direction == Direction.Down ? Direction.Right : Direction.Down;
        }

        public static int GetStepsInMainDirection(int width, int height)
        {
            var w = width - 2;
            var h = height - 2;
            return Math.Max(w, h) / Math.Min(w, h);
        }

        public static void MoveOut(Robot robot, int width, int height)
		{
            var firstDirection = GetFirstDirection(width, height);
            var nextDirection = GetNextDirection(firstDirection);
            var stepsInMainDirection = GetStepsInMainDirection(width, height);
            while (true)
            {
                MoveToDirection(robot, firstDirection, stepsInMainDirection);
                if (robot.Finished) break;
                MoveToDirection(robot, nextDirection, 1);
            }
		}
	}
}
