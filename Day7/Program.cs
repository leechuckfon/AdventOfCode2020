using AOC.Base.Helpers;
using AOC.Template.Solutions;
using System;
using System.Diagnostics;

namespace AOC.Template
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
