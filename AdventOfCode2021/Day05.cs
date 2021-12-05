namespace AdventOfCode2021
{
    public class Day05
    {
        public static long Part1(List<string> data)
        {
            var lines = PopulateData(data);
            var map = InitializeMap();

            foreach (var line in lines)
            {
                if (line.First.X == line.Second.X)
                {
                    //vertical
                    for (var i = Math.Min(line.First.Y, line.Second.Y); i <= Math.Max(line.First.Y, line.Second.Y); i++)
                    {
                        map[line.First.X, i]++;
                    }
                }
                else
                {
                    //horizontal
                    for (var i = Math.Min(line.First.X, line.Second.X); i <= Math.Max(line.First.X, line.Second.X); i++)
                    {
                        map[i, line.First.Y]++;
                    }
                }
            }

            var count = 0;
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    if (map[j, i] > 1)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public static long Part2(List<string> data)
        {
            var lines = PopulateData(data, true);
            var map = InitializeMap();

            foreach (var line in lines)
            {
                if (line.First.X == line.Second.X)
                {
                    //vertical
                    for (var i = Math.Min(line.First.Y, line.Second.Y); i <= Math.Max(line.First.Y, line.Second.Y); i++)
                    {
                        map[line.First.X, i]++;
                    }
                }
                else if (line.First.Y == line.Second.Y)
                {
                    //horizontal
                    for (var i = Math.Min(line.First.X, line.Second.X); i <= Math.Max(line.First.X, line.Second.X); i++)
                    {
                        map[i, line.First.Y]++;
                    }
                }
                else if ((line.First.Y - line.Second.Y) / (line.First.X - line.Second.X) > 0)
                {
                    //negative diagonal
                    var startingX = Math.Min(line.First.X, line.Second.X);
                    var startingY = Math.Min(line.First.Y, line.Second.Y);
                    var distance = Math.Abs(line.First.Y - line.Second.Y);
                    for (var i = 0; i <= distance; i++)
                    {
                        map[startingX + i, startingY + i]++;
                    }
                }
                else
                {
                    //positive diagonal
                    var startingX = Math.Min(line.First.X, line.Second.X);
                    var startingY = Math.Max(line.First.Y, line.Second.Y);
                    var distance = Math.Abs(line.First.Y - line.Second.Y);
                    for (var i = 0; i <= distance; i++)
                    {
                        map[startingX + i, startingY - i]++;
                    }
                }
            }

            var count = 0;
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    if (map[j, i] > 1)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private static List<Line> PopulateData(List<string> data, bool withDiags = false)
        {
            var lines = new List<Line>();
            foreach (var item in data)
            {
                var line = item.Split(" -> ");

                var x1 = int.Parse(line[0].Split(',')[0]);
                var y1 = int.Parse(line[0].Split(',')[1]);

                var x2 = int.Parse(line[1].Split(',')[0]);
                var y2 = int.Parse(line[1].Split(',')[1]);

                if (!withDiags && x1 != x2 && y1 != y2)
                    continue;

                lines.Add(new Line(new Coord(x1, y1), new Coord(x2, y2)));
            }

            return lines;
        }

        private static int[,] InitializeMap()
        {
            var map = new int[1000, 1000];
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    map[j, i] = 0;
                }
            }

            return map;
        }
    }

    public class Line
    {
        public Line(Coord first, Coord second)
        {
            First = first;
            Second = second;
        }

        public Coord First { get; set; }
        public Coord Second { get; set; }
    }

    public class Coord
    {
        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}