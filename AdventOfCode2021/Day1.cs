using System.Collections.Generic;

namespace AdventOfCode2020
{
    public class Day1
    {
        public static long Part1(List<string> data)
        {
            var complementLookup = new Dictionary<int, int>();
            foreach (var num in data)
            {
                var numAsInt = int.Parse(num);
                complementLookup.Add(numAsInt, 2020 - numAsInt);
            }

            foreach (var (num, comp) in complementLookup)
            {
                if (complementLookup.ContainsKey(comp))
                {
                    return num * comp;
                }
            }

            return -1;
        }

        public static long Part2(List<string> data)
        {
            var numbers = new List<int>();
            foreach (var num in data)
            {
                numbers.Add(int.Parse(num));
            }

            for (int i = 0; i < numbers.Count; i++)
                for (int j = i + 1; j < numbers.Count; j++)
                    for (int k = j + 1; k < numbers.Count; k++)
                        if (numbers[i] + numbers[j] + numbers[k] == 2020)
                            return numbers[i] * numbers[j] * numbers[k];

            return -1;
        }
    }
}