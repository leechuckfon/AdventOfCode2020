using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AOC.Base.Helpers
{
    public static class PerfMon
    {
        public static void Monitor(string monitorLabel,Action action)
        {
            Stopwatch s = Stopwatch.StartNew();
            action();
            s.Stop();
            Console.WriteLine(string.Format("T-{0} (millis): {1}", monitorLabel, s.Elapsed.TotalMilliseconds));
        }
    }
}
