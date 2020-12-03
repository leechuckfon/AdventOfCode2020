using System.Collections.Generic;

namespace Day3.Solutions
{
    public class Area
    {
        public List<char[]> Map { get; set; } = new List<char[]>();
        public MoveSettings Settings { get; set; }

        public int CalculateTrees()
        {
            var trees = 0;

            for (var i = 0; i * Settings.Down < Map.Count; i++)
            {
                var yCoordinate = Map[i * Settings.Down];
                var lengthOfLineFromInput = yCoordinate.Length;
                var xCoordinate = i * Settings.Right;
                while (xCoordinate >= lengthOfLineFromInput)
                {
                    xCoordinate -= lengthOfLineFromInput;
                }

                var occupant = yCoordinate[xCoordinate];

                if (occupant == '#')
                {
                    trees++;
                }
            }

            return trees;
        }
    }

}
