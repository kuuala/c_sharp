namespace Mazes
{
	public static class SnakeMazeTask
	{
        public static void MoveToDirection(Robot robot, Direction direction, int stepCount)
        {
            for (int i = 0; i < stepCount; ++i)
                robot.MoveTo(direction);
        }

        public static void MoveOut(Robot robot, int width, int height)
		{
            while (true)
            {
                MoveToDirection(robot, Direction.Right ,width - 3);
                MoveToDirection(robot, Direction.Down, 2);
                MoveToDirection(robot, Direction.Left, width - 3);
                if (robot.Finished) break;
                MoveToDirection(robot, Direction.Down, 2);
            }
		}
	}
}
