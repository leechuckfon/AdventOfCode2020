using AOC.Base;
using System.Linq;

namespace Day1.Solutions
{
    class Solution : Excercise<long>
    {
        protected override long DoGold()
        {
            var lines = ReadInput();
            var allNumbers = lines.Select(x => int.Parse(x));
            foreach (var number in allNumbers)
            {
                foreach (var nu in allNumbers.Except(new int[] { number }))
                {
                    var result = allNumbers.Except(new int[] { number, nu }).Where(y => y + number + nu == 2020).FirstOrDefault();
                    if (result != default(int))
                    {
                        return result * number * nu;
                    }
                }
            }
            return 0;
        }

        protected override long DoGrey()
        {
            var lines = ReadInput();
            var allNumbers = lines.Select(x => int.Parse(x));

            foreach (var number in allNumbers)
            {
                var result = allNumbers.Except(new int[] { number }).Where(y => y + number == 2020).FirstOrDefault();
                if (result != default(int))
                {
                    return result * number;
                }
            }
            return 0;
        }
    }
}
