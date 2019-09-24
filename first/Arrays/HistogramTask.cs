using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            var clearData = names.Where(n => n.Name == name)
                                .Where(n => n.BirthDate.Day != 1);
            var days = new string[31];
            for (var i = 0; i < days.Length; ++i)
                days[i] = (i + 1).ToString();
            var birthsCounts = new double[31];
            foreach (var elem in clearData)
                birthsCounts[elem.BirthDate.Day - 1]++;
            return new HistogramData(string.Format("Рождаемость людей с именем '{0}'", name), days, birthsCounts);
        }
    }
}
