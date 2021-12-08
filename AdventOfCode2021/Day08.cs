namespace AdventOfCode2021
{
    public class Day08
    {
        public static long Part1(List<string> data)
        {
            var displays = new List<Tuple<List<string>, List<string>>>();
            foreach (var line in data)
            {
                displays.Add(new Tuple<List<string>, List<string>>(
                    new List<string>(line.Split(" | ")[0].Split(' ')),
                    new List<string>(line.Split(" | ")[1].Split(' '))));
            }

            var count = 0;
            foreach (var (_, display) in displays)
            {
                foreach (var digit in display)
                {
                    if (digit.Length == 2 ||
                        digit.Length == 3 ||
                        digit.Length == 4 ||
                        digit.Length == 7)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public static long Part2(List<string> data)
        {
            var displays = new List<Tuple<List<HashSet<int>>, List<HashSet<int>>>>();
            foreach (var line in data)
            {
                //I am not proud of this
                displays.Add(new Tuple<List<HashSet<int>>, List<HashSet<int>>>(
                    new List<HashSet<int>>(line
                        .Split(" | ")[0]
                        .Split(' ')
                        .Select(s => new HashSet<int>(s
                            .ToCharArray()
                            .Select(c => (int)c))))
                        .OrderBy(d => d.Count)
                        .ToList(),
                    new List<HashSet<int>>(line
                        .Split(" | ")[1]
                        .Split(' ')
                        .Select(s => new HashSet<int>(s
                            .ToCharArray()
                            .Select(c => (int)c)))
                        .OrderBy(d => d.Count)
                        .ToList())));
            }

            var digitLookup = new Dictionary<int, Segment>();

            foreach (var (input, output) in displays)
            {
                digitLookup.Add(input[1].Except(input[0]).First(), Segment.Top);
            }

            //test

            throw new NotImplementedException();
        }

        [Flags]
        enum Segment
        {
            Top             = 1,
            TopLeft         = 2,
            TopRight        = 4,
            Middle          = 8,
            BottomLeft      = 16,
            BottomRight     = 32,
            Bottom          = 64,

            Zero            = Top | TopLeft | TopRight | BottomLeft | BottomRight | Bottom,
            One             = TopRight | BottomRight,
            Two             = Top | TopRight | Middle | BottomLeft | Bottom,
            Three           = Top | TopRight | Middle | BottomRight | Bottom,
            Four            = TopLeft | TopRight | Middle | BottomRight,
            Five            = Top | TopLeft | Middle | BottomRight | Bottom,
            Six             = Top | TopLeft | Middle | BottomLeft | BottomRight | Bottom,
            Seven           = Top | TopRight | BottomRight,
            Eight           = Top | TopLeft | TopRight | Middle | BottomLeft | BottomRight | Bottom,
            Nine            = Top | TopLeft | TopRight | Middle | BottomRight | Bottom,

            Invalid         = 0
        }
    }
}