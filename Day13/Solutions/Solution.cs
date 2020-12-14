using AOC.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Template.Solutions
{
    class Solution : Excercise<long>
    {
        BusSchedules schedule = new BusSchedules();
        protected override void DoGold()
        {
            Result = schedule.FindConsecutiveDepart();
        }

        protected override void DoSilver()
        {
            ParseInput();
            Result = schedule.FindEarliestDepart();
        }

        protected override void ParseInput()
        {
            var lines = ReadInput();
            schedule.EarliestDepartTime = long.Parse(lines[0]);
            schedule.Busses = lines[1].Split(',').Select(x => new Bus {
            DepartTime = long.TryParse(x, out long result) ? result: 1,
            Id = x,
            }).ToList();
        }
    }

    class BusSchedules
    {
        public List<Bus> Busses { get; set; }
        public long EarliestDepartTime { get; set; }
        public long StartingDepartTime { get; set; }

        public long FindEarliestDepart()
        {
            StartingDepartTime = EarliestDepartTime;
            while (!Busses.Any(x => x.Id != "x" && EarliestDepartTime % x.DepartTime == 0))
            {
                EarliestDepartTime++;
            }

            return (Busses.First(x => x.Id != "x" && EarliestDepartTime % x.DepartTime == 0).DepartTime) * (EarliestDepartTime - StartingDepartTime);
        }

        internal long FindConsecutiveDepart()
        {
            var departTime = (long)0;

            var departTest = Busses.Select(x => x.DepartTime % Busses.IndexOf(x));

            var chineseArithmeticSolution = Busses.Select(x =>
            {
                var start = Busses.Except(new Bus[] { x }).Select(y => y.DepartTime).Aggregate((a, b) => a * b);
                var invertedModulo = modInverse(start % x.DepartTime, x.DepartTime);
                var expectedModulo = (x.Id == "x" ? 0 : x.DepartTime - Busses.IndexOf(x));
                return start * invertedModulo * expectedModulo;
            });

            // A result, not THE result
            departTime = chineseArithmeticSolution.Sum();
            
            var commonDenominator = Busses.Select(x => x.DepartTime).Aggregate((a,b) => a*b);
            
            while (departTime - commonDenominator > 0)
            {
                // Keep going until you hit the lowest time
                departTime -= commonDenominator;
            }

            return departTime;
        }

        private long modInverse(long a, long m)
        {
            a = a % m;
            for (int x = 1; x < m; x++)
                if ((a * x) % m == 1)
                    return x;
            return 1;
        }
    }

    class Bus
    {
        public string Id { get; set; }
        public long DepartTime { get; set; }
    }
}
