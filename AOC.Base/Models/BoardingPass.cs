using System.Text.RegularExpressions;

namespace AOC.Base.Models
{
    public class BoardingPass
    {
        public int TopBound { get; set; } = 127;
        public int LowerBound { get; set; } = 0;
        public int TopSubstraction { get; set; } = 128;
        public int RightBound { get; set; } = 7;
        public int LeftBound { get; set; } = 0;
        public int RightSubstraction { get; set; } = 8;
        public int SeatId { get; set; }
        public string Pass { get; set; }

        public int GetSeatId()
        {
            var match = Regex.Match(Pass, "^([FB]{7})([LR]{3})$");

            foreach (var binder in match.Groups[1].Value)
            {
                switch (binder)
                {
                    case 'F':
                    TopBound -= TopSubstraction / 2;
                    TopSubstraction -= TopSubstraction / 2;
                    break;
                    case 'B':
                    LowerBound += TopSubstraction / 2;
                    TopSubstraction -= TopSubstraction / 2;
                    break;
                }
            }
            foreach (var binder in match.Groups[2].Value)
            {
                switch (binder)
                {
                    case 'R':
                    LeftBound += RightSubstraction / 2;
                    RightSubstraction -= RightSubstraction / 2;
                    break;
                    case 'L':
                    RightBound -= RightSubstraction / 2;
                    RightSubstraction -= RightSubstraction / 2;
                    break;
                }
            }
            SeatId = TopBound * 8 + LeftBound;
            return SeatId;
        }
    }
}
