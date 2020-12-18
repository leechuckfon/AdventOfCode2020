using AOC.Base;
using AOC.Base.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AOC.Template.Solutions
{
    class Solution : Excercise<long>
    {
        string[] lines = new string[1];
        protected override void DoGold()
        {
            PerfMon.Monitor("Calculate", () =>
            {
                var res = new List<long>();
                foreach (var line in lines)
                {
                    var trimmedLine = line.Replace(" ", "");
                    Stack<string> calculation = new Stack<string>();
                    foreach (var c in trimmedLine)
                    {
                        calculation.Push(c.ToString());
                        if (c == ')')
                        {
                            var poppedChar = default(string);
                            StringBuilder a = new StringBuilder();
                            while (poppedChar != "(")
                            {
                                calculation.TryPop(out poppedChar);
                                a.Insert(0, poppedChar);
                                if (poppedChar == "(")
                                {
                                    var result = CalcString2(a.ToString().Substring(1, a.Length - 2));
                                    calculation.Push(result.ToString());
                                }
                            }
                        }
                    }

                    res.Add(CalcString2(calculation.Reverse().Aggregate((a, b) => a + b)));
                }

                Result = res.Sum();
            });
        }

        protected override void DoSilver()
        {
            PerfMon.Monitor("Read", () => ParseInput());
            PerfMon.Monitor("Calculate", () =>
            {

                var res = new List<long>();
                foreach (var line in lines)
                {
                    var trimmedLine = line.Replace(" ", "");
                    Stack<string> calculation = new Stack<string>();
                    foreach (var c in trimmedLine)
                    {
                        calculation.Push(c.ToString());
                        if (c == ')')
                        {
                            var poppedChar = default(string);
                            StringBuilder a = new StringBuilder();
                            while (poppedChar != "(")
                            {
                                calculation.TryPop(out poppedChar);
                                a.Insert(0, poppedChar);
                                if (poppedChar == "(")
                                {
                                    var result = CalcString(a.ToString().Substring(1, a.Length - 2));
                                    calculation.Push(result.ToString());
                                }
                            }
                        }
                    }

                    res.Add(CalcString(calculation.Reverse().Aggregate((a, b) => a + b)));
                }

                Result = res.Sum();
            });
        }

        protected long CalcString(string test)
        {
            long result = 0;

            var match = Regex.Matches(test, "(\\*|\\+)?([\\d]+)");
            foreach (var m in match.ToArray())
            {
                if (m.Value[0] != '+' && m.Value[0] != '*')
                {
                    result = long.Parse(m.Value);
                } else
                {
                    switch (m.Value[0])
                    {
                        case '+':
                        result += long.Parse(m.Value.Substring(1));
                        break;
                        case '*':
                        result *= long.Parse(m.Value.Substring(1));
                        break;
                    }
                }
            }
            return result;
        }

        protected long CalcString2(string test)
        {
            var additions = test.Split('*');
            var intermediate = new List<long>();
            foreach (var addition in additions)
            {
                if (addition.IndexOf("+") != -1)
                {
                    intermediate.Add(addition.Split('+').Select(long.Parse).Sum());
                } else
                {
                    intermediate.Add(long.Parse(addition));
                }
            }
            return intermediate.Aggregate((a, b) => a * b);
        }

        protected override void ParseInput()
        {
            lines = ReadInput();

        }
    }
}
