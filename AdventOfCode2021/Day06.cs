namespace AdventOfCode2021
{
    public class Day06
    {
        public static long Part1(List<string> data)
        {
            var school = new List<int>(data[0].Split(',').Select(f => int.Parse(f)));

            for (int i = 1; i <= 80; i++)
            {
                for (int j = 0; j < school.Count; j++)
                {
                    if (school[j] == 0)
                    {
                        school[j] = 6;
                        school.Add(9);
                    }
                    else
                    {
                        school[j]--;
                    }
                }
            }

            return school.Count;
        }

        public static long Part2(List<string> data)
        {
            var fishAgeCountsList = new List<long>();
            for (int i = 0; i < 7; i++)
            {
                fishAgeCountsList.Add(0);
            }
            foreach (var fishAge in new List<int>(data[0].Split(',').Select(f => int.Parse(f))))
            {
                fishAgeCountsList[fishAge]++;
            }
            var fishAdultsAgeCounts = new Queue<long>(fishAgeCountsList);

            var fishKidsAgeCounts = new Queue<long>();
            for (int i = 0; i < 2; i++)
            {
                fishKidsAgeCounts.Enqueue(0);
            }

            for (int i = 1; i <= 256; i++)
            {
                fishKidsAgeCounts.Enqueue(fishAdultsAgeCounts.Peek());
                fishAdultsAgeCounts.Enqueue(fishAdultsAgeCounts.Dequeue() + fishKidsAgeCounts.Dequeue());
            }

            return fishAdultsAgeCounts.Sum() + fishKidsAgeCounts.Sum();
        }
    }
}