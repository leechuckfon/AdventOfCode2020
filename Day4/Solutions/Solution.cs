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
            var year = Regex.Match(v, "^([0-9]{4})$");
            if (year.Success)
            {
                var actualYear = year.Groups[0].Value;
                return int.Parse(actualYear) >= 1920 && int.Parse(actualYear) <= 2002;
            }

            return false;
        }

        private bool CheckIyr(string v)
        {
            var year = Regex.Match(v, "^([0-9]{4})$");
            if (year.Success)
            {
                var actualYear = year.Groups[0].Value;
                return int.Parse(actualYear) >= 2010 && int.Parse(actualYear) <= 2020;
            }

            return false;
        }

        private bool CheckEyr(string v)
        {
            var year = Regex.Match(v, "^([0-9]{4})$");
            if (year.Success)
            {
                var actualYear = year.Groups[0].Value;
                return int.Parse(actualYear) >= 2020 && int.Parse(actualYear) <= 2030;
            }

            return false;
        }

        private bool CheckHgt(string v)
        {
            var height = Regex.Match(v, "^([0-9]*)(cm|in)$");
            if (height.Success)
            {
                if (height.Groups[2].Value == "cm")
                {
                    var n = int.Parse(height.Groups[1].Value);
                    return n >= 150 && n <= 193;
                } else
                {
                    var n = int.Parse(height.Groups[1].Value);
                    return n >= 59 && n <= 76;
                }
            }
            return false;
        }

        private bool CheckHcl(string v)
        {
            return Regex.Match(v, "^#([a-f]|[0-9]){6}$").Success;
        }

        private bool CheckEcl(string v)
        {
            return Regex.Match(v, "^amb|blu|brn|gry|grn|hzl|oth$").Success;
        }

        private bool CheckPid(string v)
        {
            return Regex.Match(v, "^[0-9]{9}$").Success;
        }
    }
}
