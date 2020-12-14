using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC.Base.Helpers
{
    public static class HelperMethods
    {

        public static string ConvertToBinary(string value)
        {
            var actualValue = long.Parse(value);

            var binary = Convert.ToString(actualValue, 2);
            StringBuilder valueAsBinary = new StringBuilder(binary);
            while (valueAsBinary.Length < 36)
            {
                valueAsBinary.Insert(0, 0);
            }
            return valueAsBinary.ToString();
        }


        public static long ConvertBinaryFromString(string value)
        {
            var test = value.Reverse();
            long result = 0;
            int cycle = 0;
            foreach (var thing in test)
            {
                switch (thing)
                {
                    case '1':
                    if (cycle == 0)
                    {
                        result += 1;
                    } else
                    {

                        var intermediate = (long)2;
                        for (int i = 1; i < cycle; i++)
                        {
                            intermediate *= 2;
                        }
                        result += intermediate;
                    }
                    break;
                    case '0':
                    break;
                }
                cycle++;
            }
            return result;

        }
    }
}
