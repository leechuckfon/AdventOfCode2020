using AOC.Base.Helpers;
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
            PerfMon.Monitor("DayTotal", () =>
            {
                var solution = new Solution();
                solution.Solve();
            });
        }
    }
}
