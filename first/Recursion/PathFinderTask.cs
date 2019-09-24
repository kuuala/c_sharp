using System;
using System.Drawing;

namespace RoutePlanning
{
	public static class PathFinderTask
	{
		public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
		{
            var minLen = double.MaxValue;
            var bestOrder = new int[checkpoints.Length];
            MakeOrdPermutation(checkpoints, new int[checkpoints.Length], 1, ref minLen, bestOrder);
            return bestOrder;
		}

        private static void MakeOrdPermutation(
            Point[] points,
            int[] order,
            int pos,
            ref double minLen,
            int[] bestOrder)
        {
            var currLen = GetLength(points, order, pos);
            if (currLen > minLen)
                return;
            if (pos == order.Length)
            {
                if (currLen < minLen)
                {
                    minLen = currLen;
                    order.CopyTo(bestOrder, 0);
                }
                return;
            }
            for (int i = 1; i < order.Length; ++i)
                if (Array.IndexOf(order, i, 1, pos - 1) == -1)
                {
                    order[pos] = i;
                    MakeOrdPermutation(points, order, pos + 1, ref minLen, bestOrder);
                }
        }

        private static double GetLength(Point[] points, int[] order, int pos)
        {
            double len = 0;
            for (int i = 0; i < pos - 1; ++i)
                len += points[order[i]].DistanceTo(points[order[i + 1]]);
            return len;
        }
    }
}
