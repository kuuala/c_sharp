using System;

namespace GeometryTasks
{
    public class Vector
    {
        public double X;
        public double Y;

        public double GetLength()
            => Geometry.GetLength(this);

        public Vector Add(Vector a)
            => Geometry.Add(this, a);

        public bool Belongs(Segment seg)
            => Geometry.IsVectorInSegment(this, seg);
    }

    public class Segment
    {
        public Vector Begin;
        public Vector End;

        public double GetLength()
            => Geometry.GetLength(this);

        public bool Contains(Vector vec)
            => Geometry.IsVectorInSegment(vec, this);
    }

    public class Geometry
    {
        public static double GetLength(Vector vec)
            => Math.Sqrt(vec.X * vec.X + vec.Y * vec.Y);

        public static Vector Add(Vector a, Vector b)
            => new Vector { X = a.X + b.X, Y = a.Y + b.Y };

        public static double GetLength(Segment seg)
        {
            var diffX = seg.End.X - seg.Begin.X;
            var diffY = seg.End.Y - seg.Begin.Y;
            return Math.Sqrt(diffX * diffX + diffY * diffY);
        }

        public static bool IsVectorInSegment(Vector vec, Segment seg)
        {
            return seg.Begin.X <= vec.X && vec.X <= seg.End.X
                && seg.Begin.Y <= vec.Y && vec.Y <= seg.End.Y
                && ((vec.X - seg.Begin.X) * (seg.End.Y - seg.Begin.Y)
                - (vec.Y - seg.Begin.Y) * (seg.End.X - seg.Begin.X) == 0);
        }
    }
}
