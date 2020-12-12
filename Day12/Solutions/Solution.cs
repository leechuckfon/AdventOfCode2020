using AOC.Base;
using AOC.Base.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Template.Solutions
{
    class Solution : Excercise<long>
    {
        string[] AllCommands;
        protected override void DoGold()
        {
            PerfMon.Monitor("Calculate", () =>
            {
                Ship ship = new Ship();
                foreach (var command in AllCommands)
                {
                    ship.MoveWaypoint(command);
                }

                Result = ship.Coordinates.Sum(x => Math.Abs(x));
            });
        }

        protected override void DoSilver()
        {
            PerfMon.Monitor("Read", () => ParseInput());

            PerfMon.Monitor("Calculate", () =>
            {
                Ship ship = new Ship();
                foreach (var command in AllCommands)
                {
                    ship.Move(command);
                }

                Result = ship.Coordinates.Sum(x => Math.Abs(x));
            });
            
        }

        protected override void ParseInput()
        {
            AllCommands = ReadInput();
        }
    }

    class Ship
    {
        public int[] Coordinates { get; set; } = new int[2] { 0, 0 };
        public int[] Waypoint { get; set; } = new int[2] { 1, 10 };
        public float Angle { get; set; }
        public char Facing { get; set; } = 'E';
        public List<char> Directions = new List<char> { 'N','E','S','W'};

        public void Move(string command)
        {
            var direction = command[0];
            var value = command.Substring(1);

            switch (direction)
            {
                case 'N':
                    Coordinates[0] += int.Parse(value);
                    break;
                case 'S':
                    Coordinates[0] -= int.Parse(value);
                    break;
                case 'E':
                    Coordinates[1] += int.Parse(value);
                    break;
                case 'W':
                    Coordinates[1] -= int.Parse(value);
                    break;
                case 'R':
                    var turns = (int.Parse(value) / 90);
                    var cycles = 0;
                    while (cycles < turns)
                    {
                        DirectionalTurn(float.Parse(value)/turns);
                        cycles++;   
                    }
                    //Angle += float.Parse(value);
                    break;
                case 'L':
                    turns = (int.Parse(value) / 90);
                    cycles = 0;
                    while (cycles < turns)
                    {
                        DirectionalTurn(-float.Parse(value) / turns);
                        cycles++;   
                    }
                //Angle -= float.Parse(value);
                break;
                case 'F':
                    Move($"{Facing}{value}");
                    break;
            }
        }
        public void MoveWaypoint(string command)
        {
            var direction = command[0];
            var value = command.Substring(1);

            switch (direction)
            {
                case 'N':
                Waypoint[0] += int.Parse(value);
                break;
                case 'S':
                Waypoint[0] -= int.Parse(value);
                break;
                case 'E':
                Waypoint[1] += int.Parse(value);
                break;
                case 'W':
                Waypoint[1] -= int.Parse(value);
                break;
                case 'R':
                var turns = (int.Parse(value) / 90);
                var cycles = 0;
                while (cycles < turns)
                {
                    WaypointTurn(float.Parse(value) / turns);
                    cycles++;
                }
                break;
                case 'L':
                turns = (int.Parse(value) / 90);
                cycles = 0;
                while (cycles < turns)
                {
                    WaypointTurn(-float.Parse(value) / turns);
                    cycles++;
                }
                break;
                case 'F':
                    Coordinates[0] += int.Parse(value) * Waypoint[0];
                    Coordinates[1] += int.Parse(value) * Waypoint[1];
                break;
            }
        }
        private void WaypointTurn(float degrees)
        {
            switch (degrees)
            {
                case 90:
                    if (Facing == 'W')
                    {
                        Facing = 'N';
                    } else
                    {
                        Facing = Directions[Directions.IndexOf(Facing) + 1];
                    }
                    var temp = Waypoint[1];
                    Waypoint[1] = Waypoint[0];
                    Waypoint[0] = -temp;
                break;
                case -90:
                    if (Facing == 'N')
                    {
                        Facing = 'W';
                    } else
                    {
                        Facing = Directions[Directions.IndexOf(Facing) - 1];
                    }
                    temp = Waypoint[1];
                    Waypoint[1] = -Waypoint[0];
                    Waypoint[0] = temp;
                break;
            }
        }
        private void DirectionalTurn(float degrees)
        {
            switch (degrees)
            {
                case 90:
                    if (Facing == 'W')
                    {
                        Facing = 'N';
                    } else
                    {
                        Facing = Directions[Directions.IndexOf(Facing) + 1];
                    }
                break;
                case -90:
                    if (Facing == 'N')
                    {
                        Facing = 'W';
                    } else
                    {
                        Facing = Directions[Directions.IndexOf(Facing) - 1];
                    }
                break;
            }
        }
    }
}
