using AOC.Base;
using AOC.Base.Helpers;
using AOC.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Template.Solutions
{
    partial class Solution : Excercise<long>
    {
        protected override void DoGold()
        {
            var passes = new List<BoardingPass>();

            PerfMon.Monitor("Read", () =>
            {
                var lines = ReadInput();
                foreach (var line in lines)
                {
                    passes.Add(new BoardingPass()
                    {
                        Pass = line
                    });
                }
            });


            PerfMon.Monitor("Calculate", () =>
            {
                var allPasses = passes.Select(x => x.GetSeatId()).OrderBy(x => x);
                var allNumbers = Enumerable.Range(allPasses.Min(), allPasses.Count());
                Result = allNumbers.Except(allPasses).First();
            });

        }

        protected override void DoSilver()
        {
            var passes = new List<BoardingPass>();

            PerfMon.Monitor("Read", () =>
            {
                var lines = ReadInput();
                foreach (var line in lines)
                {
                    passes.Add(new BoardingPass()
                    {
                        Pass = line
                    });
                }
            });

            PerfMon.Monitor("Calculate", () =>
            {
                Result = passes.Select(x => x.GetSeatId()).Max();
            });
        }
    }
}
