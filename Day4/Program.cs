using AOC.Base.Helpers;
using Day4.Solutions;

namespace Day4
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
