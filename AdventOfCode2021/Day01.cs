namespace AdventOfCode2021
{
    public class Day01
    {
        public static long Part1(List<string> data)
        {
            var count = 0;
            for (var i = 1; i < data.Count; i++)
            {
                int.TryParse(data[i-1], out var lower);
                int.TryParse(data[i], out var higher);

                if (higher > lower)
                {
                    count++;
                }
            }

            return count;
        }

        public static long Part2(List<string> data)
        {
            var count = 0;
            for (var i = 3; i < data.Count; i++)
            {
                int.TryParse(data[i - 3], out var first);
                int.TryParse(data[i - 2], out var second);
                int.TryParse(data[i - 1], out var third);
                int.TryParse(data[i], out var forth);

                if (forth + third + second > third + second + first)
                {
                    count++;
                }
            }

            return count;
        }
    }
}