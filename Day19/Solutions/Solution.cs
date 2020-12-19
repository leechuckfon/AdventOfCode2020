using AOC.Base;
using AOC.Base.Helpers;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC.Template.Solutions
{
    class Solution : Excercise<long>
    {
        List<Rule> Rules = new List<Rule>();
        List<string> Inputs = new List<string>();
        MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        protected override void DoGold()
        {
            PerfMon.Monitor("Calculate", () =>
            {
                Result = 0;
                Rules.First(x => x.Id == 8).Constraint = "42 | 42 8";
                Rules.First(x => x.Id == 11).Constraint = "42 31 | 42 11 31";

                var allCombinations = GetString(42);
                var allCombinations2 = GetString(31);

                foreach (var input in Inputs)
                {
                    for (int i = 1; i < 20; i++)
                    {
                        var ft = allCombinations.Aggregate((a, b) => a + "|" + b);
                        var to = allCombinations2.Aggregate((a, b) => a + "|" + b);
                        var a = Regex.Match(input, $"^({ft})+(({ft})" + "{" + i + "}" + $"({to})" + "{" + i + "}" + ")$");
                        if (a.Success)
                        {
                            Result++;
                        }
                    }
                }
            });
        }

        protected override void DoSilver()
        {
            PerfMon.Monitor("Read", () =>
            {
                ParseInput();
            });
            PerfMon.Monitor("Calculate", () =>
            {
                var allCombinations = GetString(0);


                Result = Inputs.Intersect(allCombinations).Count();
            });

        }

        private string[] GetString(int cons)
        {
            return _cache.GetOrCreate(cons, (e) =>
            {
                e.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                if (Regex.IsMatch(Rules.First(x => x.Id == cons).Constraint, "^[a-z]"))
                {
                    return new string[] { Rules[cons].Constraint };
                }
                else
                {
                    var allNumbers = Rules.First(x => x.Id == cons).Constraint.Split('|').Select(x => x.Trim());
                    var stringResult = new List<string>();
                    foreach (var partition in allNumbers)
                    {
                        var toReplace = partition;
                        var positionPossibilities = new List<string[]>();
                        foreach (var part in partition.Split(" "))
                        {
                            var result = GetString(int.Parse(part));
                            positionPossibilities.Add(result);
                        }

                        var interim = positionPossibilities[0].ToList();

                        for (int i = 1; i < positionPossibilities.Count; i++)
                        {
                            var interim2 = new List<string>();
                            foreach (var a in interim)
                            {
                                foreach (var b in positionPossibilities[i])
                                {
                                    interim2.Add(a + b);
                                }
                            }
                            interim = interim2;
                        }

                        stringResult.AddRange(interim);

                    }
                    return stringResult.ToArray();
                }
            });
        }

        protected override void ParseInput()
        {
            var lines = ReadInput();

            foreach (var line in lines)
            {
                var match = Regex.Match(line, "(\\d+):(.*)");
                if (match.Success)
                {
                    Rules.Add(new Rule
                    {
                        Id = int.Parse(match.Groups[1].Value),
                        Constraint = match.Groups[2].Value.Replace("\"", "").Trim(),
                    });
                }
                else
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        Inputs.Add(line);
                    }
                }
            }

            Rules = Rules.OrderBy(x => x.Id).ToList();
        }
    }

    class Rule
    {
        public int Id { get; set; }
        public string Constraint { get; set; }
    }
}
