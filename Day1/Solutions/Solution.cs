using AOC.Base;
using AOC.Base.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Day1.Solutions
{
    class Solution : Excercise<long>
    {
        IEnumerable<int> allNumbers = Enumerable.Empty<int>();

        protected override void DoGold()
        {
            PerfMon.Monitor("Calculate", () =>
            {
                foreach (var number in allNumbers)
                {
                    foreach (var nu in allNumbers.Except(new int[] { number }))
                    {
                        var result = allNumbers.Except(new int[] { number, nu }).Where(y => y + number + nu == 2020).FirstOrDefault();
                        if (result != default(int))
                        {
                            Result = result * number * nu;
                            return;
                        }
                    }
                }
            });

        }

        protected override void DoSilver()
        {
            ParseInput();

            PerfMon.Monitor("Calculate", () =>
            {
                foreach (var number in allNumbers)
                {
                    var result = allNumbers.Except(new int[] { number }).Where(y => y + number == 2020).FirstOrDefault();
                    if (result != default(int))
                    {
                        Result = result * number;
                        return;
                    }
                }
            });
        }

        protected override void ParseInput()
        {
            PerfMon.Monitor("Read", () => { var lines = ReadInput(); allNumbers = lines.Select(x => int.Parse(x)); });
        }
    }
}
