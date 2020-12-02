using Day2.Solutions;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day2
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
