using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC.Base.Models
{
    public class Passport
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
