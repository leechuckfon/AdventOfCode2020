using AOC.Base;
using AOC.Base.Helpers;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Template.Solutions
{
    class Solution : Excercise<long>
    {
        JoltageAdapters adapters = new JoltageAdapters();
        protected override void DoGold()
        {
            PerfMon.Monitor("Calculate", () =>
            {
                Result = adapters.SolveCombinations(0, 0);
            });
        }

        protected override void DoSilver()
        {
            PerfMon.Monitor("Parse", () =>
            {
                ParseInput();
            });

            PerfMon.Monitor("Calculate", () =>
            {
                Result = adapters.CountDifferences();
            });
        }

        protected override void ParseInput()
        {
            adapters.Adapters = ReadInput().Select(x => long.Parse(x)).ToList();
            adapters.Adapters.Sort();
        }
    }

    class JoltageAdapters
    {
        public List<long> Adapters { get; set; }
        private long currentAdapter = 0;
        MemoryCache _cache = new MemoryCache(optionsAccessor: new MemoryCacheOptions()
        {
        });
        public long CountDifferences()
        {
            var enumerator = Adapters.GetEnumerator();
            var a = (long)0;
            var b = (long)0;
            while (enumerator.MoveNext())
            {
                if (enumerator.Current - currentAdapter >= 4 )
                {
                    return a*b;
                }


                if (enumerator.Current - currentAdapter ==1)
                {
                    a++;
                }
                if (enumerator.Current - currentAdapter ==3)
                {
                    b++;
                }
                currentAdapter = enumerator.Current;
            }

            return a * (b+1); 
        }

        public long SolveCombinations(long value, long index)
        {
            return _cache.GetOrCreate($"{value}_{index}", (e) =>
            {
                if (index == Adapters.Count)
                {
                    return 1;
                }

                var result = (long)0;
                var possibleChoices = Adapters.Skip((int)index).Where(x => x <= value + 3);
                foreach (var choice in possibleChoices)
                {
                    result += SolveCombinations(choice, Adapters.IndexOf(choice)+1);
                }
                e.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                return result;
            });
        }
    }
}
