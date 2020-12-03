using AOC.Base;
using AOC.Base.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day3.Solutions
{
    partial class Solution : Excercise<long>
    {

        /**
         * Make Area object with internal MoveSettings
         * Area will have a 2D char array
         * Area will be able to use the array and MoveSettings to traverse the map using Traverse()
         * **/
        protected override void DoGold()
        {
            List<Area> positions = new List<Area>();

            PerfMon.Monitor("Parse", () =>
            {
                var lines = ReadInput();
                var area = new List<char[]>();
                // Create Area
                for (int i = 0; i < lines.Count(); i++)
                {
                    area.Add(lines[i].ToCharArray());
                }

                // Create MoveSettings
                positions = new List<Area>()
                {
                    new Area
                    {
                        Map = area,
                        Settings = new MoveSettings { Right = 1, Down = 1 }
                    },
                    new Area
                    {
                        Map = area,
                        Settings = new MoveSettings { Right = 3, Down = 1 }
                    },
                    new Area
                    {
                        Map = area,
                        Settings = new MoveSettings { Right = 5, Down = 1 }
                    },
                    new Area
                    {
                        Map = area,
                        Settings = new MoveSettings { Right = 7, Down = 1 }
                    },
                    new Area
                    {
                        Map = area,
                        Settings = new MoveSettings { Right = 1, Down = 2 }
                    }
                };

            });

            PerfMon.Monitor("Calculate", () =>
            {
                Result = positions.Select(x => x.CalculateTrees()).Aggregate((x, y) => x * y);
            });
        }

        protected override void DoSilver()
        {
            var area = new Area();
            
            PerfMon.Monitor("Parse", () =>
            {
                var lines = ReadInput();
                for (int i = 0; i < lines.Count(); i++)
                {
                    area.Map.Add(lines[i].ToCharArray());
                }

                area.Settings = new MoveSettings()
                {
                    Down = 1,
                    Right = 3
                };

            });
           
            PerfMon.Monitor("Calculate", () =>
            {
                Result = area.CalculateTrees();
            });
        }
    }

}
