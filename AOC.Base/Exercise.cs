using System;
using System.Diagnostics;
using System.IO;

namespace AOC.Base {
    public abstract class Excercise<T>: object {

        public void Solve() {
            Console.WriteLine("Silver Answer: " + Silver());
            Console.WriteLine("Gold Answer: " + Gold());
        }

        public string[] ReadExample() {
            return File.ReadAllLines("./Inputs/EI.txt");
        }
        public string[] ReadInput() {
            return File.ReadAllLines("./Inputs/RI.txt");
        }

        private T Silver() {
            var stopwatch = Stopwatch.StartNew();
            var result = DoSilver();
            stopwatch.Stop();
            Console.WriteLine("T-total (millis): " + stopwatch.Elapsed.TotalMilliseconds);
            return result;
        }

        protected abstract T DoSilver();

        private T Gold() {
            var stopwatch = Stopwatch.StartNew();
            var result = DoGold();
            stopwatch.Stop();
            Console.WriteLine("T-total (millis): " + stopwatch.Elapsed.TotalMilliseconds);
            return result;
        }

        protected abstract T DoGold();

    }
}
