using AOC.Base;
using AOC.Base.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Template.Solutions
{
    class Solution : Excercise<long>
    {
        List<char[]> Seats;
        List<char[]> newSeatStatus ;
        int cycles;
        protected override void DoGold()
        {
            PerfMon.Monitor("Parse", () => ParseInput());

            PerfMon.Monitor("Calculate", () =>
            {
                newSeatStatus = Seats;
                cycles = 0;
                while (cycles == 0 || !StatusSame(Seats, newSeatStatus))
                {
                    cycles++;
                    Seats = newSeatStatus;
                    newSeatStatus = Seats.Select(x => new char[x.Count()]).ToList();
                    for (int row = 0; row < Seats.Count; row++)
                    {
                        for (int seat = 0; seat < Seats[row].Count(); seat++)
                        {
                            Evaluate(row, seat);
                        }
                    }
                }

                Console.WriteLine("Done in {0} cycles", cycles);
                Result = Seats.Sum(x => x.Count(y => y == '#'));
            });            
        }

        protected override void DoSilver()
        {
            PerfMon.Monitor("Parse", () => ParseInput());

            PerfMon.Monitor("Calculate", () =>
            {
                newSeatStatus = Seats;
                cycles = 0;
                while (cycles == 0 || !StatusSame(Seats, newSeatStatus))
                {
                    cycles++;
                    Seats = newSeatStatus;
                    newSeatStatus = Seats.Select(x => new char[x.Count()]).ToList();
                    for (int row = 0; row < Seats.Count; row++)
                    {
                        for (int seat = 0; seat < Seats[row].Count(); seat++)
                        {
                            Evaluate2(row, seat);
                        }
                    }
                }
                Console.WriteLine("Done in {0} cycles", cycles);
                Result = Seats.Sum(x => x.Count(y => y == '#'));
            });
        }
        protected override void ParseInput()
        {
            Seats = new List<char[]>();
            var lines = ReadInput();
            foreach (var line in lines)
            {
                Seats.Add(line.ToCharArray());
            }
            newSeatStatus = new List<char[]>();
        }

        private bool StatusSame(List<char[]> seats, List<char[]> newSeatStatus)
        {
            for (int i = 0; i < seats.Count; i++)
            {
                for (int j = 0; j < seats[i].Count(); j++)
                {
                    if (!(seats[i][j] == newSeatStatus[i][j]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        protected void Evaluate(int row, int seat)
        {
            var status = Seats[row][seat];
            switch (status)
            {
                case 'L':
                if ((ClosestRowOccupied(row, seat) == 0)
                    && 
                    (ClosestColumnOccupied(row, seat) == 0 )
                    && 
                    (ClosestLeftDiagonalOccupied(row, seat) == 0)
                    && 
                    (ClosestRightDiagonalOccupied(row, seat) == 0))
                {
                    newSeatStatus[row][seat] = '#';
                } else
                {
                    goto default;
                }
                break;
                case '#':
                var count = ClosestColumnOccupied(row, seat) + ClosestRowOccupied(row, seat) + ClosestLeftDiagonalOccupied(row, seat) + ClosestRightDiagonalOccupied(row, seat);
                if (count >= 5)
                {
                    newSeatStatus[row][seat] = 'L';
                } else
                {
                    goto default;
                }
                break;
                case '.':
                default:
                newSeatStatus[row][seat] = status;
                break;
            }
        }

        protected void Evaluate2(int row, int seat)
        {
            var status = Seats[row][seat];
            switch (status)
            {
                case 'L':
                if (AdjacentEmpty(row,seat))
                {
                    newSeatStatus[row][seat] = '#';
                } else
                {
                    goto default;
                }
                break;
                case '#':
                if (CountOccupied(row, seat))
                { 
                    newSeatStatus[row][seat] = 'L';
                } else
                {
                    goto default;
                }
                break;
                case '.':
                default:
                newSeatStatus[row][seat] = status;
                break;
            }
        }


        private int ClosestRightDiagonalOccupied(int row, int seat)
        {
            var leftSeat = '.';
            var rightSeat = '.';
            var currentSeat = 1;
            var currentRow = 1;
            while (currentRow < Seats.Count && currentSeat < Seats[0].Count())
            {
                if ((currentSeat + seat) < Seats[0].Count() && (currentRow + row) < Seats.Count())
                {
                    if (Seats[currentRow + row][currentSeat + seat] != '.')
                    {
                        if (rightSeat == '.')
                            rightSeat = Seats[currentRow + row][currentSeat + seat];
                    }
                }
                if ((seat - currentSeat) >= 0 && (row - currentRow) >= 0)
                {
                    if (Seats[row - currentRow][seat - currentSeat] != '.')
                    {
                        if (leftSeat == '.')
                        leftSeat = Seats[row - currentRow][seat - currentSeat];
                    }
                }
                currentRow++;
                currentSeat++;
            }

            if (rightSeat == '#' && leftSeat == '#')
            {
                return 2;
            } else if (rightSeat == '#' || leftSeat == '#')
            {
                return 1;
            }
            return 0;
        }

        private int ClosestLeftDiagonalOccupied(int row, int seat)
        {
            var leftSeat = '.';
            var rightSeat = '.';
            var currentSeat = 1;
            var currentRow = 1;
            while (currentRow <= Seats.Count && currentSeat <= Seats[0].Count())
            {
                if ((seat - currentSeat ) >= 0 && (currentRow + row) < Seats.Count())
                {
                    if (Seats[currentRow + row][seat - currentSeat] != '.')
                    {
                        if (rightSeat == '.')
                            rightSeat = Seats[currentRow + row][seat - currentSeat];
                    }
                }
                if ((currentSeat + seat) < Seats[0].Count() && (row - currentRow) >= 0)
                {
                    if (Seats[row - currentRow][currentSeat + seat] != '.')
                    {
                        if (leftSeat == '.')
                            leftSeat = Seats[row - currentRow][currentSeat + seat];
                    }
                }
                currentRow++;
                currentSeat++;
            }

            if (rightSeat == '#' && leftSeat == '#')
            {
                return 2;
            } else if (rightSeat == '#' || leftSeat == '#')
            {
                return 1;
            }
            return 0;
        }

        private int ClosestColumnOccupied(int row, int seat)
        {
            var upSeat = '.';
            var downSeat = '.';
            var rowCount = 1;
            while (rowCount <= Seats.Count())
            {
                if(row + rowCount < Seats.Count())
                {
                    if (Seats[row + rowCount][seat] != '.')
                    {
                        if (upSeat == '.')
                            upSeat = Seats[row + rowCount][seat];
                    }
                }

                if (row - rowCount >= 0)
                {
                    if (Seats[row - rowCount][seat] != '.')
                    {
                        if (downSeat == '.')
                            downSeat = Seats[row - rowCount][seat];
                    }
                }

                rowCount++;
            }


            if (downSeat == '#' && upSeat == '#')
            {
                return 2;
            } else if (downSeat == '#' || upSeat == '#')
            {
                return 1;
            }
            return 0;
        }

        private int ClosestRowOccupied(int row, int seat)
        {
            var leftSeat = '.';
            var rightSeat = '.';
            var rowCount = 1;
            while (rowCount <= Seats[0].Count())
            {
                if (seat + rowCount < Seats[0].Count())
                {
                    if (Seats[row][seat + rowCount] != '.')
                    {
                        if (rightSeat == '.')
                            rightSeat = Seats[row][rowCount + seat];
                    }
                }

                if (seat - rowCount >= 0)
                {
                    if (Seats[row][seat - rowCount] != '.')
                    {
                        if (leftSeat == '.')
                            leftSeat = Seats[row][seat - rowCount];
                    }
                }
                rowCount++;
            }
            if (rightSeat == '#' && leftSeat == '#')
            {
                return 2;
            } else if (rightSeat == '#' || leftSeat == '#')
            {
                return 1;
            }
            return 0;
        }
        private bool AdjacentEmpty(int row, int seat)
        {
            var max = Seats[row].Count() - 1;
            var maxRow = Seats.Count() - 1;

            var diagonalFree = ((row - 1 < 0 || seat - 1 < 0) || (Seats[row - 1][seat - 1] == '.' || Seats[row - 1][seat - 1] == 'L'))
                                &&
                                ((row - 1 < 0 || seat + 1 > max) || (Seats[row - 1][seat + 1] == '.' || Seats[row - 1][seat + 1] == 'L'))
                                &&
                                ((row + 1 > maxRow || seat - 1 < 0) || (Seats[row + 1][seat - 1] == '.' || Seats[row + 1][seat - 1] == 'L'))
                                &&
                                ((row + 1 > maxRow || seat + 1 > max) || Seats[row + 1][seat + 1] == '.' || Seats[row + 1][seat + 1] == 'L');

            if (!diagonalFree)
            {
                return diagonalFree;
            }

            var directionalFree = ((row - 1 < 0) || (Seats[row - 1][seat] == '.' || Seats[row - 1][seat] == 'L'))
                                    &&
                                    ((seat + 1 > max) || (Seats[row][seat + 1] == '.' || Seats[row][seat + 1] == 'L'))
                                    &&
                                    ((row + 1 > maxRow) || (Seats[row + 1][seat] == '.' || Seats[row + 1][seat] == 'L'))
                                    &&
                                    ((seat - 1 < 0) || (Seats[row][seat - 1] == '.' || Seats[row][seat - 1] == 'L'));

            return directionalFree && directionalFree;
        }

        private bool CountOccupied(int row, int seat)
        {
            var max = Seats[row].Count() - 1;
            var maxRow = Seats.Count() - 1;

            var occupiedCount = 0;
            if ((row > 0 && seat > 0) && Seats[row - 1][seat - 1] == '#')
            {
                occupiedCount++;
            }
            if ((row > 0 && seat < max) && Seats[row - 1][seat + 1] == '#')
            {
                occupiedCount++;
            }

            if ((row < maxRow && seat > 0) && Seats[row + 1][seat - 1] == '#')
            {
                occupiedCount++;

            }

            if ((row < maxRow && seat < max) && Seats[row + 1][seat + 1] == '#')
            {
                occupiedCount++;
            }

            if ((seat > 0) && Seats[row][seat - 1] == '#')
            {
                occupiedCount++;
            }
            if ((seat < max) && Seats[row][seat + 1] == '#')
            {
                occupiedCount++;
            }

            if ((row > 0) && Seats[row - 1][seat] == '#')
            {
                occupiedCount++;

            }

            if ((row < maxRow) && Seats[row + 1][seat] == '#')
            {
                occupiedCount++;
            }

            return occupiedCount >= 4;
        }
    }
}
