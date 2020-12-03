using AOC.Base.Helpers;
using Day1.Solutions;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day1 {
    class Program {
        static void Main(string[] args) {
            PerfMon.Monitor("DayTotal", () =>
            {
                var solution = new Solution();
                solution.Solve();
            });
        }
    }
}
