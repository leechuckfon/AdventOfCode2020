using AOC.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day3.Solutions
{
    class Solution : Excercise<long>
    {
        protected override long DoGold()
        {
            var trees = new List<int>();
            var lines = ReadInput();
            var area = new List<char[]>();
            for (int i = 0; i < lines.Count(); i++)
            {
                area.Add(lines[i].ToCharArray());
            }

            var positions = new List<Pos>()
            {
                new Pos
                {
                    Right = 1,
                    Down = 1
                },
                new Pos
                {
                    Right = 3,
                    Down = 1
                },
                new Pos
                {
                    Right = 5,
                    Down = 1
                },
                new Pos
                {
                    Right = 7,
                    Down = 1
                },
                new Pos
                {
                    Right = 1,
                    Down = 2
                }
            };
            Stopwatch s = Stopwatch.StartNew();

            foreach (var position in positions)
            {
                var t = 0;
                for (var i = 0; i * position.Down < area.Count; i++)
                {
                    var XCo = area[i * position.Down];
                    var lineLength = XCo.Length;
                    var skip = i * position.Right;
                    while (skip >= lineLength)
                    {
                        skip -= lineLength;
                    }

                    var thing = XCo[skip];

                    if (thing == '#')
                    {
                        t++;
                    }
                }
                trees.Add(t);

            }

            s.Stop();
            Console.WriteLine("T-CalculateTrees: " + s.Elapsed.TotalMilliseconds);
            return trees.Aggregate((x,y) => x*y);
        }

        protected override long DoSilver()
        {
            var trees = 0;
            var lines = ReadInput();
            var area = new List<char[]>();
            for (int i = 0; i < lines.Count(); i++)
            {
                area.Add(lines[i].ToCharArray());
            }

            var rightPos = 3;
            var downPos = 1;
            Stopwatch s = Stopwatch.StartNew();
            for (var i = 0; i < area.Count; i++)
            {
                var XCo = area[i * downPos];
                var lineLength = XCo.Length;
                var skip = i * rightPos;
                while (skip >= lineLength)
                {
                    skip -= lineLength;
                }

                var thing = XCo[skip];

                if (thing == '#')
                {
                    trees++;
                }
            }

            s.Stop();
            Console.WriteLine("T-CalculateTrees: " + s.Elapsed.TotalMilliseconds);
            return trees;
        }

        class Pos
        {
            public int Right { get; set; }
            public int Down { get; set; }
        }
    }
}
