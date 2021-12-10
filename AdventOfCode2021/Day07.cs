namespace AdventOfCode2021
{
    public class Day07
    {
        public static long Part1(List<string> data)
        {
            var crabPositions = new List<long>(data[0].Split(',').Select(f => long.Parse(f)));
            crabPositions.Sort();
            var median = Median(crabPositions);

            return crabPositions.Select(c => Math.Abs(median - c)).Sum();
        }

        public static long Part2(List<string> data)
        {
            var crabPositions = new List<int>(data[0].Split(',').Select(f => int.Parse(f)));
            crabPositions.Sort();

            //Started with median and counted up and down to find optimal number
            //var median = Median(crabPositions);

            var possibleAnswers = new List<int>();
            for (int i = 583; i < 609; i++)
            {
                possibleAnswers.Add(crabPositions.Select(c => Summation1ToN(Math.Abs(crabPositions[i] - c))).Sum());
            }

            return possibleAnswers.Min();
        }

        public static long Median(List<long> list)
        {
            long median;
            if (list.Count % 2 == 0)
            {
                var leftIndex = (int)(list.Count / 2d);
                var rightIndex = leftIndex - 1;
                median = (long)Math.Round((list[leftIndex] + list[rightIndex]) / 2d, MidpointRounding.AwayFromZero);
            }
            else
            {
                median = list[list.Count / 2];
            }

            return median;
        }

        private static int Summation1ToN(int n)
        {
            return n * (n + 1) / 2;
        }
    }
}