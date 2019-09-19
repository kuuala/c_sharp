namespace Mazes
{
	public static class EmptyMazeTask
	{
        public static void MoveToDirection(Robot robot, Direction direction, int stepCount)
        {
            for (int i = 0; i < stepCount; ++i)
                robot.MoveTo(direction);
        }
        
		public static void MoveOut(Robot robot, int width, int height)
		{
            MoveToDirection(robot, Direction.Right, width - 3);
            MoveToDirection(robot, Direction.Down, height - 3);
		}
	}
}
