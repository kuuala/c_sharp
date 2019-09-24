using System.Collections.Generic;
using System.Diagnostics;

namespace Profiling
{
	public class ProfilerTask : IProfiler
	{
		public List<ExperimentResult> Measure(IRunner runner, int repetitionsCount)
		{
            var result = new List<ExperimentResult>();
            foreach (var fieldCount in Constants.FieldCounts)
            {
                var classResult = test(true, runner, repetitionsCount, fieldCount);
                var structResult = test(false, runner, repetitionsCount, fieldCount);
                result.Add(new ExperimentResult(fieldCount, classResult, structResult));
            }
            return result;
		}

        private double test(bool isClass, IRunner runner, int repetitionsCount, int fieldCount)
        {
            var timer = Stopwatch.StartNew();
            runner.Call(isClass, fieldCount, 1);
            timer.Restart();
            runner.Call(isClass, fieldCount, repetitionsCount);
            return timer.Elapsed.TotalMilliseconds / repetitionsCount;
        }
	}
}
