using AOC.Base;
using AOC.Base.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC.Template.Solutions
{
    class Solution : Excercise<long>
    {
        List<Rule> rules = new List<Rule>();
        long[] myTicket;
        List<long[]> nearbyTickets = new List<long[]>();
        List<string> ruleOrder = new List<string>();
        Dictionary<int, List<string>> allCombis = new Dictionary<int, List<string>>();
        protected override void DoGold()
        {
            PerfMon.Monitor("Parse", () => ParseInput());

            PerfMon.Monitor("Calculate", () =>
            {
                var wrongNumbers = new List<long[]>();
                nearbyTickets.ForEach(x =>
                {
                    var remove = false;
                    foreach (var number in x)
                    {
                        if (!rules.Any(y => (y.LowerValues.Item1 <= number && number <= y.LowerValues.Item2) || (y.HigherValues.Item1 <= number && number <= y.HigherValues.Item2)))
                        {
                            remove = !remove;
                        }
                    }
                    if (remove)
                    {
                        wrongNumbers.Add(x);
                    }
                });
                wrongNumbers.ForEach(x => nearbyTickets.Remove(x));

                for (int i = 0; i < nearbyTickets[0].Length; i++)
                {
                    var valuesOfOneField = nearbyTickets.Select(x => x[i]);
                    var matches = new List<string>();
                    foreach (var rule in rules)
                    {
                        //if (rule.Any(y => (y.LowerValues.Item1 <= number && number <= y.LowerValues.Item2) || (y.HigherValues.Item1 <= number && number <= y.HigherValues.Item2)))
                        if (valuesOfOneField.All(y => (rule.LowerValues.Item1 <= y && y <= rule.LowerValues.Item2) || (rule.HigherValues.Item1 <= y && y <= rule.HigherValues.Item2)))
                        {
                            matches.Add(rule.Name);
                        }
                    }

                    allCombis.Add(i, matches);
                }

                ruleOrder = Filter(allCombis);
                var result = (long)1;
                for (int i = 0; i < ruleOrder.Count; i++)
                {
                    if (ruleOrder[i].IndexOf("departure") != -1)
                        result *= myTicket[i];
                }

                Result = result;
            });
        }

        private List<string> Filter(Dictionary<int, List<string>> allCombis)
        {
            var temp = allCombis.ToDictionary(k => k.Key, v => v.Value);
            var te = temp.Select(x => x.Value.Count);
            while (temp.Select(x => x.Value.Count).Any(x => x!=1))
            {
                foreach (var pair in allCombis)
                {
                    if (pair.Value.Count == 1)
                    {
                        foreach (var p in temp)
                        {
                            if (p.Key != pair.Key) { 
                                p.Value.Remove(pair.Value[0]);
                            }
                        }
                    }
                }
            }

            return temp.SelectMany(x => x.Value).ToList();
        }

        protected override void DoSilver()
        {
            PerfMon.Monitor("Parse", () => ParseInput());
            PerfMon.Monitor("Calculate", () =>
            {
                var wrongNumbers = new List<long>();
                nearbyTickets.ForEach(x =>
                {
                    foreach (var number in x)
                    {
                        if (!rules.Any(y => (y.LowerValues.Item1 <= number && number <= y.LowerValues.Item2) || (y.HigherValues.Item1 <= number && number <= y.HigherValues.Item2)))
                        {
                            wrongNumbers.Add(number);
                        }
                    }
                });

                Result = wrongNumbers.Sum();
            });
        }

        protected override void ParseInput()
        {
            nearbyTickets = new List<long[]>();
            rules = new List<Rule>();
            var lines = ReadInput();
            var mine = true;
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }
                var rule = Regex.Match(line, "^(.*): ([0-9]*)-([0-9]*) or ([0-9]*)-([0-9]*)");
                var ticketRule = Regex.Match(line, "^([0-9]+,?)+");
                if (rule.Success)
                {
                    rules.Add(new Rule
                    {
                        Name = rule.Groups[1].Value,
                        LowerValues = (long.Parse(rule.Groups[2].Value), long.Parse(rule.Groups[3].Value)),
                        HigherValues = (long.Parse(rule.Groups[4].Value), long.Parse(rule.Groups[5].Value)),
                    });
                } else if (ticketRule.Success)
                {
                    if (mine)
                    {
                        myTicket = line.Split(',').Select(long.Parse).ToArray();
                        mine = false;
                    } else
                    {
                        nearbyTickets.Add(line.Split(',').Select(long.Parse).ToArray());
                    }
                }

            }
        }
    }

    class Rule
    {
        public string Name { get; set; }
        public (long, long) LowerValues { get; set; }
        public (long, long) HigherValues { get; set; }
    }
}
