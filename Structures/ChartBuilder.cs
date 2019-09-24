using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

namespace Profiling
{
    class ChartBuilder : IChartBuilder
    {
        public Control Build(List<ExperimentResult> result)
        {
            var graph = new ZedGraphControl();
            var classInfo = new PointPairList();
            var structInfo = new PointPairList();
            foreach (var exp in result)
            {
                classInfo.Add(exp.Size, exp.ClassResult);
                structInfo.Add(exp.Size, exp.StructResult);
            }
            graph.GraphPane.AddCurve("class", classInfo, Color.Red);
            graph.GraphPane.AddCurve("struct", structInfo, Color.Blue);
            graph.GraphPane.AxisChange();
            return graph;
        }
    }
}
