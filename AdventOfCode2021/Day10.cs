namespace AdventOfCode2021
{
    public class Day10
    {
        public static long Part1(List<string> data)
        {
            var score = 0;
            var scoringTable = GetPart1ScoringTable();

            var stack = new Stack<char>();
            foreach (var line in data)
            {
                var violatingChar = '_';
                foreach (var c in line.ToCharArray())
                {
                    if (violatingChar != '_')
                        break;

                    switch (c)
                    {
                        case '(':
                        case '[':
                        case '{':
                        case '<':
                            stack.Push(c);
                            break;
                        case ')':
                            if (stack.Pop() != '(')
                                violatingChar = c;
                            break;
                        case ']':
                            if (stack.Pop() != '[')
                                violatingChar = c;
                            break;
                        case '}':
                            if (stack.Pop() != '{')
                                violatingChar = c;
                            break;
                        case '>':
                            if (stack.Pop() != '<')
                                violatingChar = c;
                            break;
                    }
                }

                if (violatingChar != '_')
                {
                    score += scoringTable[violatingChar];
                }
            }

            return score;
        }

        public static long Part2(List<string> data)
        {
            var incompleteLines = new List<string>();
            var stack = new Stack<char>();
            foreach (var line in data)
            {
                var violatingChar = '_';
                foreach (var c in line.ToCharArray())
                {
                    if (violatingChar != '_')
                        break;

                    switch (c)
                    {
                        case '(':
                        case '[':
                        case '{':
                        case '<':
                            stack.Push(c);
                            break;
                        case ')':
                            if (stack.Pop() != '(')
                                violatingChar = c;
                            break;
                        case ']':
                            if (stack.Pop() != '[')
                                violatingChar = c;
                            break;
                        case '}':
                            if (stack.Pop() != '{')
                                violatingChar = c;
                            break;
                        case '>':
                            if (stack.Pop() != '<')
                                violatingChar = c;
                            break;
                    }
                }

                if (violatingChar == '_')
                {
                    incompleteLines.Add(line);
                }
            }

            stack.Clear();
            var scoringTable = GetPart2ScoringTable();
            var matchingClosingCharMap = GetMatchingClosingCharMap();
            var scores = new List<long>();
            foreach (var line in incompleteLines)
            {
                foreach (var c in line.ToCharArray())
                {
                    switch (c)
                    {
                        case '(':
                        case '[':
                        case '{':
                        case '<':
                            stack.Push(c);
                            break;
                        case ')':
                        case ']':
                        case '}':
                        case '>':
                            stack.Pop();
                            break;
                    }
                }

                var score = 0L;
                while (stack.Any())
                {
                    score = score * 5 + scoringTable[matchingClosingCharMap[stack.Pop()]];
                }

                scores.Add(score);
            }

            scores.Sort();

            return Day07.Median(scores);
        }

        private static Dictionary<char, int> GetPart1ScoringTable()
        {
            return new Dictionary<char, int>
            {
                { ')', 3 },
                { ']', 57 },
                { '}', 1197 },
                { '>', 25137 }
            };
        }

        private static Dictionary<char, int> GetPart2ScoringTable()
        {
            return new Dictionary<char, int>
            {
                { ')', 1 },
                { ']', 2 },
                { '}', 3 },
                { '>', 4 }
            };
        }

        private static Dictionary<char, char> GetMatchingClosingCharMap()
        {
            return new Dictionary<char, char>
            {
                { '(', ')' },
                { '[', ']' },
                { '{', '}' },
                { '<', '>' }
            };
        }
    }
}