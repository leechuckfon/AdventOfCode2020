namespace AOC.Base.Models
{
    public class Policy
    {
        public string Password { get; set; }
        public int MinOccur { get; set; }
        public int MaxOccur { get; set; }
        public char Sequence { get; set; }

        public bool CheckPolicy()
        {
            var occur = Password.Count(x => x == Sequence);
            if (MinOccur > occur || MaxOccur < occur)
            {
                return false;
            }
            return true;
        }

        public bool CheckOccurPolicy()
        {
            var check1 = Password[MinOccur - 1];
            var check2 = Password[MaxOccur - 1];

            var onlya = check1 != Sequence && check2 == Sequence;
            var onlyb = check1 == Sequence && check2 != Sequence;

            if (onlya || onlyb)
            {
                return true;
            }

            return false;
        }
    }
}
