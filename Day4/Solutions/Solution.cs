using AOC.Base;
using AOC.Base.Helpers;
using AOC.Base.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day4.Solutions
{
    class Solution : Excercise<long>
    {
        List<Passport> passports = new List<Passport>();
        
        protected override void DoGold()
        {
            PerfMon.Monitor("Calculate", () => Result = passports.Select(x => x.CheckFields(true)).Count(x => x == true));
            
        }

        protected override void DoSilver()
        {
            ParseInput();
            PerfMon.Monitor("Calculate", () => Result = passports.Select(x => x.CheckFields(false)).Count(x => x == true));
        }

        protected override void ParseInput()
        {
            StringBuilder builder = new StringBuilder();
            PerfMon.Monitor("Parse", () => {
                var lines = ReadInput();
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        var passport = new Passport();
                        var properties = builder.ToString().Split(' ').SelectMany(x => x.Split(':')).ToList();
                        for (int i = 0; i < properties.Count(); i += 2)
                        {
                            passport.Fields.Add(properties[i], properties[i + 1]);
                        }
                        passports.Add(passport);
                        builder = new StringBuilder();
                    } else
                    {
                        if (builder.Length > 0)
                        {
                            builder.Append(" " + line);
                        } else
                        {
                            builder.Append(line);
                        }
                    }


                }
            });
        }
    }
}
