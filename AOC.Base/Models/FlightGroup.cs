using System.Linq;

namespace AOC.Base.Models
{
    public class FlightGroup
    {
        public string Answers { get; set; }
        public int PeopleCount { get; set; }

        public int SameAnswerCount()
        {
            var distinctAnswers = Answers.Distinct();
            var allAnsweredSameCount = 0;
            foreach (var answer in distinctAnswers)
            {
                if (Answers.Count(x => x == answer) == PeopleCount)
                {
                    allAnsweredSameCount++;
                }
            }
            return allAnsweredSameCount;
        }


    }
}
