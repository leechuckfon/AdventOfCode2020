using AOC.Base;
using AOC.Base.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Template.Solutions
{
    class Solution : Excercise<long>
    {
        XmasEncoder encoder = new XmasEncoder();

        protected override void DoGold()
        {
            PerfMon.Monitor("Calculate", () =>
            {
                Result = encoder.ContiguousSet();
            });
        }

        protected override void DoSilver()
        {
            ParseInput();
            encoder.Preamble = 25;

            PerfMon.Monitor("Calculate", () =>
            {
                Result = encoder.FindBadNumber();
            });

        }

        protected override void ParseInput()
        {
            var lines = ReadInput();

            PerfMon.Monitor("Parse", () =>
            {
                encoder.Cypher = lines.Select(x => long.Parse(x)).ToList();
            });
        }
    }

    class XmasEncoder
    {
        public List<long> Cypher { get; set; }
        public int Preamble { get; set; }
        public long BadNumber { get; set; }

        public long FindBadNumber()
        {
            for (int i=0; i<Cypher.Count-Preamble;i++)
            {
                var numbers = Cypher.Skip(i).Take(Preamble).ToList();
                var nextNumber = Cypher.Skip(i + Preamble).Take(1).FirstOrDefault();

                if (!numbers.Any(x =>
                {
                    var numberToMatch = nextNumber - x;
                    return numbers.Contains(numberToMatch);
                }))
                {
                    BadNumber = nextNumber;
                    return nextNumber;
                }
            }
            return 0;
        }

        public long ContiguousSet()
        {
            for (int i = 0; i < Cypher.Count - Preamble; i++)
            {
                var numbers = Cypher.Skip(i).ToList();
                var upperBound = (long)0;
                var index = 0;
                var result = numbers.Aggregate((a, b) =>
                {
                    if (a == 0 || (a + b) > BadNumber)
                    {
                        return 0;
                    }

                    if ((a + b) == BadNumber)
                    {
                        index+=2;
                        upperBound = b;
                    }

                    if(upperBound == 0)
                    {
                        index++;
                    }
                    return a + b;
                });

                if (upperBound != 0)
                {
                    var check = numbers.GetRange(0, index);
                    return check.Min() + check.Max();
                }
            }
            return 0;
        }
    }
}
