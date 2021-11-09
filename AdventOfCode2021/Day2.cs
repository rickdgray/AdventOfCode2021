using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day2
    {
        public static long Part1(List<string> data)
        {
            var count = 0;

            foreach (var item in data)
            {
                var tokens = item.Split(" ");

                if (tokens.Length != 3)
                    throw new Exception("fucked");

                var numberOfLettersTokens = tokens[0].Split("-");

                var policyMinimumNumber = int.Parse(numberOfLettersTokens[0]);
                var policyMaximumNumber = int.Parse(numberOfLettersTokens[1]);
                var policyLetter = tokens[1].ToCharArray().First();
                var policyPassword = tokens[2];

                var concordance = new Dictionary<char, int>();
                foreach (var letter in policyPassword)
                {
                    if (!concordance.ContainsKey(letter))
                    {
                        concordance.Add(letter, 0);
                    }

                    concordance[letter] = concordance[letter] + 1;
                }

                if (concordance.ContainsKey(policyLetter))
                {
                    var numberOfInstances = concordance[policyLetter];

                    if (numberOfInstances >= policyMinimumNumber && numberOfInstances <= policyMaximumNumber)
                        count++;
                }
            }

            return count;
        }

        public static long Part2(List<string> data)
        {
            var count = 0;

            foreach (var item in data)
            {
                var tokens = item.Split(" ");

                if (tokens.Length != 3)
                    throw new Exception("fucked");

                var positions = tokens[0].Split("-");

                var letter = tokens[1].ToCharArray().First();
                var password = tokens[2];

                var firstLetterMatches = password.ElementAt(int.Parse(positions[0]) - 1).Equals(letter);
                var secondLetterMatches = password.ElementAt(int.Parse(positions[1]) - 1).Equals(letter);

                if (firstLetterMatches ^ secondLetterMatches)
                    count++;
            }

            return count;
        }
    }
}