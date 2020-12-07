using AOC.Base;
using AOC.Base.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC.Template.Solutions
{
    class Solution : Excercise<long>
    {
        List<Luggage> bags = new List<Luggage>();
        protected override void DoGold()
        {
            PerfMon.Monitor("Calculate", () =>
            {
                Result = CountGold(bags.First(x => x.LuggageKind == "shiny gold"));
            });
        }

        private int CountGold(Luggage l)
        {
            var result = 0;
            foreach (var bag in l.ContainsKind)
            {
                result += bag.Value + (bag.Value * CountGold(bags.FirstOrDefault(x => x.LuggageKind == bag.Key)));
            }
            return result;
        }

        protected override void DoSilver()
        {
            PerfMon.Monitor("Parse", () =>
            {
                ParseInput();
            });

            PerfMon.Monitor("Calculate", () => {
                Result = bags.Select(x => FindGold(x)).Count(x => x == true);
            });
        }
        // God this is slow
        private bool FindGold(Luggage x)
        {
            if (x.ContainsKind.ContainsKey("shiny gold"))
            {
                return true;
            } else
            {
                foreach (var kind in x.ContainsKind)
                {
                    if(FindGold(bags.FirstOrDefault(y => y.LuggageKind == kind.Key)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        protected override void ParseInput()
        {
            var lines = ReadInput();

            foreach (var line in lines)
            {
                var match = Regex.Match(line, "^(.*) bags contain (( ?([0-9]*) (([a-z ]*) (bags?)),?)*)");
                var kind = match.Groups[1].Value;
                var contains = match.Groups[2];
                var split = contains.Value.Split(',');

                var luggage = new Luggage()
                {
                    LuggageKind = kind
                };

                foreach (var inside in split)
                {
                    var splitKind = Regex.Match(inside.Trim(), "^([0-9]*) (.*) (bags?)$");
                    if (splitKind.Success)
                    {
                        var amount = splitKind.Groups[1].Value;
                        var bagKing = splitKind.Groups[2].Value;

                        luggage.ContainsKind.Add(bagKing, int.Parse(amount));
                    }

                }
                bags.Add(luggage);
            }
        }
    }

    class Luggage
    {
        public string LuggageKind { get; set; }
        public Dictionary<string, int> ContainsKind { get; set; } = new Dictionary<string, int>();
    }
}