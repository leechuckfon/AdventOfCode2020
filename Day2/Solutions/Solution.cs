using AOC.Base;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day2.Solutions
{
    class Solution : Excercise<long>
    {
        protected override long DoGold()
        {
            var lines = ReadInput();
            var policies = new List<Policy>();
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
            return policies.Select(x => CheckOccurPolicy(x)).Count(x => x == true);

        }

        protected override long DoGrey()
        {
            var lines = ReadInput();
            var policies = new List<Policy>();
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

            return policies.Select(x => CheckPolicy(x)).Count(x => x == true);
        }

        public bool CheckPolicy(Policy p)
        {
            var occur = p.Password.Where(x => x == p.Sequence).Count();
            if (p.MinOccur > occur || p.MaxOccur < occur)
            {
                return false;
            }
            return true;
        }

        public bool CheckOccurPolicy(Policy p)
        {
            var check1 = p.Password[p.MinOccur - 1];
            var check2 = p.Password[p.MaxOccur - 1];

            var onlya = check1 != p.Sequence && check2 == p.Sequence;
            var onlyb = check1 == p.Sequence && check2 != p.Sequence;

            if (onlya || onlyb)
            {
                return true;
            }

            return false;
        }
    }

    class Policy
    {
        public string Password { get; set; }
        public int MinOccur { get; set; }
        public int MaxOccur { get; set; }
        public char Sequence { get; set; }
    }
}
