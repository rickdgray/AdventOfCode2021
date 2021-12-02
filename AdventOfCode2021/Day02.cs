namespace AdventOfCode2021
{
    public class Day02
    {
        public static long Part1(List<string> data)
        {
            var x = 0;
            var y = 0;
            foreach (var line in data)
            {
                var instruction = line.Split(" ")[0];
                var distance = int.Parse(line.Split(" ")[1]);

                switch (instruction)
                {
                    case "forward":
                        x += distance;
                        break;
                    case "up":
                        y -= distance;
                        break;
                    case "down":
                        y += distance;
                        break;
                }
            }

            return x * y;
        }

        public static long Part2(List<string> data)
        {
            var aim = 0;
            var x = 0;
            var y = 0;
            foreach (var line in data)
            {
                var instruction = line.Split(" ")[0];
                var value = int.Parse(line.Split(" ")[1]);

                switch (instruction)
                {
                    case "forward":
                        x += value;
                        y += aim * value;
                        break;
                    case "up":
                        aim -= value;
                        break;
                    case "down":
                        aim += value;
                        break;
                }
            }

            return x * y;
        }
    }
}