using AOC.Base;
using AOC.Base.Helpers;
using AOC.Base.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day2.Solutions
{
    class Solution : Excercise<long>
    {
        protected override void DoGold()
        {
            var policies = new List<Policy>();
            
            PerfMon.Monitor("Read", () =>
            {
                var lines = ReadInput();
                foreach (var line in lines)
                {
                    var match = Regex.Match(line, "(\\d*)-(\\d*) (.*): (.*)");
                    policies.Add(new Policy
                    {
                        MinOccur = int.Parse(match.Groups[1].Value),
                        MaxOccur = int.Parse(match.Groups[2].Value),
                        Sequence = match.Groups[3].Value[0],
                        Password = match.Groups[4].Value,
                    });

                }
            });

            PerfMon.Monitor("Calculate", () => Result = policies.Select(x => x.CheckOccurPolicy()).Count(x => x == true));

        }

        protected override void DoSilver()
        {
            var policies = new List<Policy>();
            
            PerfMon.Monitor("Read", () =>
            {
                var lines = ReadInput();
                foreach (var line in lines)
                {
                    var match = Regex.Match(line, "(\\d*)-(\\d*) (.*): (.*)");
                    policies.Add(new Policy
                    {
                        MinOccur = int.Parse(match.Groups[1].Value),
                        MaxOccur = int.Parse(match.Groups[2].Value),
                        Sequence = match.Groups[3].Value[0],
                        Password = match.Groups[4].Value,
                    });

                }
            });

            PerfMon.Monitor("Calculate", () => Result = policies.Select(x => x.CheckPolicy()).Count(x => x == true));
        }
    }
}
