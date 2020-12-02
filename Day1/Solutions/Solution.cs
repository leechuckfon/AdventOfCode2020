using AOC.Base;
using System.Linq;

namespace Day1.Solutions {
    class Solution : Excercise<long> {
        protected override long DoGold() {
            var lines = ReadInput();
            var allNumbers = lines.Select(x => int.Parse(x));
            var a = 0;
            var b = 0;
            var c = 0;
            foreach (var number in allNumbers) {
                if (a == 0 && b == 0 && c == 0) {
                    foreach (var nu in allNumbers.Except(new int[] { number })) {
                        var result = allNumbers.Except(new int[] { number, nu }).Where(y => y + number + nu == 2020).FirstOrDefault();
                        if (result != default(int)) {
                            a = number;
                            b = result;
                            c = nu;
                        }
                    }
                }
            }

            return a * b * c;
        }

        protected override long DoGrey() {
            var lines = ReadInput();
            var allNumbers = lines.Select(x => int.Parse(x));
            var a = 0;
            var b = 0;

            foreach (var number in allNumbers) {
                if (a == 0 && b == 0) {
                    var result = allNumbers.Except(new int[] { number }).Where(y => y + number == 2020).FirstOrDefault();
                    if (result != default(int)) {
                        a = number;
                        b = result;
                    }
                }
            }

            return a * b;
        }
    }
}
