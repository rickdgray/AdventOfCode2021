using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day15
    {
        public static long Part1(List<string> data)
        {
            var turns = data.First().Split(",").Select(n => int.Parse(n)).ToList();

            for (int i = turns.Count; i < 2020; i++)
            {
                var lastNumber = turns[i - 1];
                if (turns.FindAll(n => n == lastNumber).Count == 1)
                {
                    turns.Add(0);
                }
                else
                {
                    for (int j = turns.Count - 2; j >= 0; j--)
                    {
                        if (turns[j] == lastNumber)
                        {
                            turns.Add(turns.Count - j - 1);
                            break;
                        }
                    }
                }
            }

            return turns.Last();
        }

        public static long Part2(List<string> data)
        {
            var startingNumbers = data.First().Split(",");
            var numberLookup = new Dictionary<int, Queue<int>>();

            for (int i = 0; i < startingNumbers.Length; i++)
            {
                numberLookup.Add(int.Parse(startingNumbers[i]), new Queue<int>());
                numberLookup[int.Parse(startingNumbers[i])].Enqueue(i);
            }

            var lastNumber = int.Parse(startingNumbers.Last());
            for (int i = startingNumbers.Length; i < 30000000; i++)
            {
                if (numberLookup[lastNumber].Count < 2)
                {
                    lastNumber = 0;

                    if (!numberLookup.ContainsKey(0))
                    {
                        numberLookup.Add(0, new Queue<int>());
                    }
                    numberLookup[0].Enqueue(i);
                    if (numberLookup[0].Count > 2)
                        numberLookup[0].Dequeue();
                }
                else
                {
                    lastNumber = numberLookup[lastNumber].ElementAt(1) - numberLookup[lastNumber].ElementAt(0);

                    if (numberLookup.ContainsKey(lastNumber))
                    {
                        numberLookup[lastNumber].Enqueue(i);
                        if (numberLookup[lastNumber].Count > 2)
                            numberLookup[lastNumber].Dequeue();
                    }
                    else
                    {
                        numberLookup.Add(lastNumber, new Queue<int>());
                        numberLookup[lastNumber].Enqueue(i);
                    }
                }
            }

            return lastNumber;
        }
    }
}