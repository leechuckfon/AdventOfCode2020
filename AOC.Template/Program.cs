using System;
using System.Diagnostics;

namespace AOC.Template
{
    class Program
    {
        static void Main(string[] args)
        {
            var solution = new Solution();
            Stopwatch s = Stopwatch.StartNew();
            solution.Solve();
            s.Stop();
            Console.WriteLine("T-DayTotal: " + s.Elapsed.TotalMilliseconds);
        }
    }
}
