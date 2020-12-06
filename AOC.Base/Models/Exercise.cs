using AOC.Base.Helpers;
using System;
using System.Diagnostics;
using System.IO;

namespace AOC.Base {
    public abstract class Excercise<T> {

        public T Result { get; set; } = default(T);

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
            PerfMon.Monitor("SilverTotal", () => DoSilver());
            return Result;
        }

        protected abstract void DoSilver();

        private T Gold() {
            PerfMon.Monitor("GoldTotal", () => DoGold());
            return Result;
        }

        protected abstract void DoGold();

        protected abstract void ParseInput();

    }
}
