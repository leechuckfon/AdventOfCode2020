using System;
using System.Diagnostics;
using System.IO;

namespace AOC.Base {
    public abstract class Excercise<T>: object {

        public void Solve() {
            Console.WriteLine("Answer: " + Grey());
            Console.WriteLine("Answer: " + Gold());
        }

        public string[] ReadExample() {
            return File.ReadAllLines("./Inputs/EI.txt");
        }
        public string[] ReadInput() {
            return File.ReadAllLines("./Inputs/RI.txt");
        }

        private T Grey() {
            var stopwatch = Stopwatch.StartNew();
            var result = DoGrey();
            stopwatch.Stop();
            Console.WriteLine("T (tick): " + stopwatch.ElapsedTicks);
            return result;
        }

        protected abstract T DoGrey();

        private T Gold() {
            var stopwatch = Stopwatch.StartNew();
            var result = DoGold();
            stopwatch.Stop();
            Console.WriteLine("T (tick): " + stopwatch.ElapsedTicks);
            return result;
        }

        protected abstract T DoGold();

    }
}
