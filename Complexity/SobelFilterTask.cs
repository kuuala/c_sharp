using System;
using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var result = new double[width, height];
            var sy = DoTraspose(sx);
            var kernelLen = sx.GetLength(0);
            var kernelRaduis = (kernelLen - 1) / 2;
            for (int x = kernelRaduis; x < width - kernelRaduis; x++)
                for (int y = kernelRaduis; y < height - kernelRaduis; y++)
                {
                    var neigh = GetNeighborhood(g, x, y, kernelRaduis);
                    var gx = Convolute(neigh, sx);
                    var gy = Convolute(neigh, sy);
                    result[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }
            return result;
        }

        private static double[,] DoTraspose(double[,] matrix)
        {
            var lenX = matrix.GetLength(0);
            var lenY = matrix.GetLength(1);
            var result = new double[lenY, lenX];
            for (int x = 0; x < lenX; ++x)
                for (int y = 0; y < lenY; ++y)
                    result[y, x] = matrix[x, y];
            return result;
        }

        private static double Convolute(double[,] matrix, double[,] kernel)
        {
            var muls = new List<double>();
            var size = matrix.GetLength(0);
            for (int i = 0; i < size; ++i)
                for (int j = 0; j < size; ++j)
                    muls.Add(matrix[i, j] * kernel[i, j]);
            return muls.Aggregate((x, y) => x + y);
        }

        private static double[,] GetNeighborhood(double[,] matrix, int x, int y, int radius)
        {
            return GetBlock(matrix, x - radius, x + radius, y - radius, y + radius);
        }

        private static double[,] GetBlock(double[,] matrix, int x0, int x1, int y0, int y1)
        {
            var lenX = x1 - x0 + 1;
            var lenY = y1 - y0 + 1;
            var result = new double[lenX, lenY];
            for (int i = 0; i < lenX; ++i)
                for (int j = 0; j < lenY; ++j)
                    result[i, j] = matrix[x0 + i, y0 + j];
            return result;
        }
    }
}
