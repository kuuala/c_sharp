using System.Collections.Generic;
using System.Drawing;
using GeometryTasks;

namespace GeometryPainting
{
    public static class SegmentExtension
    {
        private static Dictionary<Segment, Color> colors = new Dictionary<Segment, Color>();

        public static Color GetColor(this Segment seg)
            => colors.ContainsKey(seg) ? colors[seg] : Color.Black;

        public static void SetColor(this Segment seg, Color color)
        {
            if (colors.ContainsKey(seg))
                colors[seg] = color;
            else
                colors.Add(seg, color);
        }
    }
}
