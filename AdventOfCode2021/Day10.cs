using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day10
    {
        public static long Part1(List<string> data)
        {
            var adapters = new List<int>
            {
                0
            };

            foreach (var adapter in data)
            {
                adapters.Add(int.Parse(adapter));
            }

            adapters.Sort();

            adapters.Add(adapters.Last() + 3);

            var threeCount = 0;
            var oneCount = 0;
            for (var i = 0; i < adapters.Count - 1; i++)
            {
                if (adapters[i + 1] - adapters[i] == 3)
                {
                    threeCount++;
                }
                else if (adapters[i + 1] - adapters[i] == 1)
                {
                    oneCount++;
                }
            }

            return threeCount * oneCount;
        }

        public static long Part2(List<string> data)
        {
            var adapters = new List<int>
            {
                0
            };

            foreach (var adapter in data)
            {
                adapters.Add(int.Parse(adapter));
            }

            adapters.Sort();

            adapters.Add(adapters.Last() + 3);

            var total = 0;
            var cache = new Dictionary<int, int>();

            for (var i = adapters.Count - 1; i > 0; i--)
            {
                for (var j = 1; j < 4; j++)
                {
                    if (i + j >= adapters.Count)
                        break;

                    if (adapters[i + j] - adapters[i] > 3)
                        break;

                    if (cache.ContainsKey(i))
                    {
                        cache[i] = cache[i] + cache[i + j];
                        break;
                    }
                    else
                    {
                        cache.Add(i, 1);
                    }
                }
            }

            return total;
        }
    }
}