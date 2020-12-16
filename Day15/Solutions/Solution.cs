using AOC.Base;
using AOC.Base.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Template.Solutions
{
    class Solution : Excercise<long>
    {
        List<long> StartingNumbers = new List<long>();
        Dictionary<long, (long, long)> GameHistory;
        protected override void DoGold()
        {
            PerfMon.Monitor("Parse", () => ParseInput());
            PerfMon.Monitor("Calculate", () =>
            {
                long turns = 1;
                long lastNumber = 0;
                foreach (var number in StartingNumbers)
                {
                    GameHistory.Add(number, (turns, 0));
                    lastNumber = number;
                    turns++;
                }

                while (turns <= 30000000)
                {
                    lastNumber = NextTurn(lastNumber, turns);
                    turns++;
                }

                Result = lastNumber;
            });
        }

        protected override void DoSilver()
        {
            PerfMon.Monitor("Parse", () => ParseInput());
            PerfMon.Monitor("Calculate", () =>
            {
                long turns = 1;
                long lastNumber = 0;
                foreach (var number in StartingNumbers)
                {
                    GameHistory.Add(number, (turns, 0));
                    lastNumber = number;
                    turns++;
                }

                while (turns <= 2020)
                {
                    lastNumber = NextTurn(lastNumber, turns);
                    turns++;
                }

                Result = lastNumber;
            });
        }

        private long NextTurn(long lastNumber, long turns)
        {
            long spokenNumber = 0;

            if (GameHistory.ContainsKey(lastNumber))
            {
                if (GameHistory[lastNumber].Item2 == 0)
                {
                    GameHistory[spokenNumber] = (turns, GameHistory[spokenNumber].Item1);
                    return spokenNumber;
                } else
                {
                    spokenNumber = GameHistory[lastNumber].Item1 - GameHistory[lastNumber].Item2;
                    if(GameHistory.ContainsKey(spokenNumber))
                    {
                        GameHistory[spokenNumber] = (turns, GameHistory[spokenNumber].Item1);
                    } else
                    {
                        GameHistory.Add(spokenNumber, (turns, 0));
                    }
                    return spokenNumber;
                }
            } else
            {
                GameHistory.Add(lastNumber, (turns, 0));
                return spokenNumber;
            }
        }

        protected override void ParseInput()
        {
            GameHistory = new Dictionary<long, (long, long)>();
            StartingNumbers = ReadInput()[0].Split(',').Select(long.Parse).ToList();
        }
    }
}
