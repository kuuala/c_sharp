using System;
using System.Linq;

namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            var clearData = names.Where(n => n.BirthDate.Day != 1);
            var xAxis = new string[30];
            var yAxis = new string[12];
            for (int i = 0; i < 30; ++i)
                xAxis[i] = (i + 2).ToString();
            for (int i = 0; i < 12; ++i)
                yAxis[i] = (i + 1).ToString();
            var birthsCounts = new double[30, 12];
            foreach (var elem in clearData)
                birthsCounts[elem.BirthDate.Day - 2, elem.BirthDate.Month - 1]++;
            return new HeatmapData(
                "Пример карты интенсивностей",
                birthsCounts, 
                xAxis, 
                yAxis);
        }
    }
}
