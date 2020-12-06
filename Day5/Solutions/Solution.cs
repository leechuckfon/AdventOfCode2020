using AOC.Base;
using AOC.Base.Helpers;
using AOC.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC.Template.Solutions
{
    partial class Solution : Excercise<long>
    {
        List<BoardingPass> passes = new List<BoardingPass>();
        protected override void DoGold()
        {
            PerfMon.Monitor("Calculate", () =>
            {
                var allPasses = passes.Select(x => x.GetSeatId()).OrderBy(x => x);
                var allNumbers = Enumerable.Range(allPasses.Min(), allPasses.Count());
                Result = allNumbers.Except(allPasses).First();
            });

        }

        protected override void DoSilver()
        {
            ParseInput();
            PerfMon.Monitor("Calculate", () =>
            {
                Result = passes.Select(x => x.GetSeatId()).Max();
            });
        }

        protected override void ParseInput()
        {
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
        }
    }
}
