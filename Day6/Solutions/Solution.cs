using AOC.Base;
using AOC.Base.Helpers;
using AOC.Base.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC.Template.Solutions
{
    class Solution : Excercise<long>
    {
        List<FlightGroup> groups = new List<FlightGroup>();

        protected override void DoGold()
        {
            PerfMon.Monitor("Calculate", () =>
            {
                Result = groups.Select(x => x.SameAnswerCount()).Sum();
            });
        }

        protected override void DoSilver()
        {
            ParseInput();
            PerfMon.Monitor("Calculate", () =>
            {
                Result = groups.Select(x => x.Answers.Distinct().Count()).Sum();
            });
        }

        protected override void ParseInput()
        {
            PerfMon.Monitor("Read", () =>
            {
                var input = ReadInput();
                StringBuilder builder = new StringBuilder();
                int count = 0;
                for (int i = 0; i < input.Length; i++)
                {
                    if (string.IsNullOrEmpty(input[i]) || i == input.Length - 1)
                    {
                        if (i == input.Length - 1)
                        {
                            count++;
                            builder.Append(input[i]);
                        }

                        groups.Add(new FlightGroup
                        {
                            PeopleCount = count,
                            Answers = builder.ToString()
                        });
                        count = 0;
                        builder.Clear();
                    } else
                    {
                        count++;
                        builder.Append(input[i]);
                    }
                }
            });
        }
    }
}
