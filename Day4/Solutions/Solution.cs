using AOC.Base;
using AOC.Base.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Day4.Solutions
{
    class Solution : Excercise<long>
    {
        protected override void DoGold()
        {
            var passports = new List<Passport>();
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

            PerfMon.Monitor("Calculate", () => Result = passports.Select(x => x.CheckFields(true)).Count(x => x == true));
            
        }

        protected override void DoSilver()
        {
            var passports = new List<Passport>();
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
           

            PerfMon.Monitor("Calculate", () => Result = passports.Select(x => x.CheckFields(false)).Count(x => x == true));
        }
    }

    class Passport
    {
        public Dictionary<string, string> Fields { get; set; } = new Dictionary<string, string>();

        public bool CheckFields(bool checkFields)
        {
            //"cid",
            var neededFields = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid", };

            if (neededFields.Any(x => !Fields.ContainsKey(x)))
            {
                return false;
            }

            if (checkFields)
            {
                var requirementsMet = new bool[]{
                    CheckByr(Fields["byr"]),
                    CheckIyr(Fields["iyr"]),
                    CheckEyr(Fields["eyr"]),
                    CheckHgt(Fields["hgt"]),
                    CheckHcl(Fields["hcl"]),
                    CheckEcl(Fields["ecl"]),
                    CheckPid(Fields["pid"]),
                };
                return requirementsMet.All(x => x);
            }

            return true;
        }
        // change all to regex
        private bool CheckByr(string v)
        {
            return v.Length == 4 && int.Parse(v) >= 1920 && int.Parse(v) <= 2002;
        }

        private bool CheckIyr(string v)
        {
            return v.Length == 4 && int.Parse(v) >= 2010 && int.Parse(v) <= 2020;
        }

        private bool CheckEyr(string v)
        {
            return v.Length == 4 && int.Parse(v) >= 2020 && int.Parse(v) <= 2030;
        }

        private bool CheckHgt(string v)
        {
            if (v.IndexOf("cm") > 0)
            {
                var n = int.Parse(v.Substring(0, v.Length - 2));
                return n >= 150 && n <= 193;

            } else if (v.IndexOf("in") > 0)
            {
                var n = int.Parse(v.Substring(0, v.Length - 2));
                return n >= 59 && n <= 76;

            }
            return false;
        }

        private bool CheckHcl(string v)
        {
            var yes = Regex.Match(v, "^#([a-f]|[0-9]){6}$").Success;
            return yes;
        }

        private bool CheckEcl(string v)
        {
            var m = new List<string>() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            return m.Contains(v.ToLower());
        }

        private bool CheckPid(string v)
        {
            var yes = Regex.Match(v, "^[0-9]{9}$").Success;
            return yes;
        }
    }
}
