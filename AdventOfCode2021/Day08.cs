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
                            .Select(c => (int)c)))),
                    new List<HashSet<int>>(line
                        .Split(" | ")[1]
                        .Split(' ')
                        .Select(s => new HashSet<int>(s
                            .ToCharArray()
                            .Select(c => (int)c))))));
            }

            var sum = 0;
            
            foreach (var (input, output) in displays)
            {
                var segmentMap = new Dictionary<Segment, IEnumerable<int>>();
                var digitLookup = new Dictionary<int, HashSet<int>>();

                var fiveCounts = new List<HashSet<int>>(input.Where(i => i.Count == 5));
                var sixCounts = new List<HashSet<int>>(input.Where(i => i.Count == 6));

                digitLookup.Add(1, input.Where(i => i.Count == 2).First());
                digitLookup.Add(7, input.Where(i => i.Count == 3).First());
                digitLookup.Add(4, input.Where(i => i.Count == 4).First());
                digitLookup.Add(8, input.Where(i => i.Count == 7).First());

                segmentMap.Add(Segment.Top, digitLookup[7].Except(digitLookup[1]));

                var topLeftAndMiddle = digitLookup[4].Except(digitLookup[1]);
                var topMiddleAndBottom = fiveCounts.Aggregate((acc, x) => acc.Intersect(x).ToHashSet());

                segmentMap.Add(Segment.Middle, topMiddleAndBottom.Intersect(topLeftAndMiddle));
                segmentMap.Add(Segment.TopLeft, topLeftAndMiddle.Except(segmentMap[Segment.Middle]));
                segmentMap.Add(Segment.Bottom, topMiddleAndBottom.Except(segmentMap[Segment.Top]).Except(segmentMap[Segment.Middle]));

                digitLookup.Add(5, fiveCounts.First(f => f.Contains(segmentMap[Segment.TopLeft].First())));
                fiveCounts.Remove(digitLookup[5]);

                digitLookup.Add(3, fiveCounts.First(f => digitLookup[5].Except(f).ToHashSet().SetEquals(segmentMap[Segment.TopLeft])));
                fiveCounts.Remove(digitLookup[3]);

                digitLookup.Add(2, fiveCounts.First());

                segmentMap.Add(Segment.BottomRight, digitLookup[5].Except(topMiddleAndBottom).Except(segmentMap[Segment.TopLeft]));
                segmentMap.Add(Segment.TopRight, digitLookup[1].Except(segmentMap[Segment.BottomRight]));
                segmentMap.Add(Segment.BottomLeft, digitLookup[8].Except(digitLookup[5]).Except(segmentMap[Segment.TopRight]));

                digitLookup.Add(0, sixCounts.First(f => !f.Contains(segmentMap[Segment.Middle].First())));
                sixCounts.Remove(digitLookup[0]);

                digitLookup.Add(6, sixCounts.First(f => f.Contains(segmentMap[Segment.BottomLeft].First())));
                sixCounts.Remove(digitLookup[6]);

                digitLookup.Add(9, sixCounts.First(f => f.Contains(segmentMap[Segment.TopRight].First())));

                var hashToDigitMap = digitLookup.ToDictionary(k => k.Value, v => v.Key, HashSet<int>.CreateSetComparer());

                var currentNumber = 0;
                for (int i = 0; i < output.Count; i++)
                {
                    currentNumber += hashToDigitMap[output[i]] * (int)Math.Pow(10, 3 - i);
                }

                sum += currentNumber;
            }

            return sum;
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