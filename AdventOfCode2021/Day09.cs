using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2021
{
    public class Day09
    {
        public static long Part1(List<string> data)
        {
            var floor = new int[data.Count, data[0].Length];
            for (var i = 0; i < data.Count; i++)
            {
                var nums = data[i].ToCharArray();
                for (var j = 0; j < nums.Length; j++)
                {
                    floor[i, j] = int.Parse(nums[j].ToString());
                }
            }

            var knownFloorPaths = new Dictionary<FloorTile, FloorTile>();
            for (var i = 0; i < floor.GetLength(0); i++)
            {
                for (var j = 0; j < floor.GetLength(1); j++)
                {
                    RecursivelyTraverseFloor(new FloorTile(i, j, floor[i, j]), floor, knownFloorPaths);
                }
            }

            return knownFloorPaths.Values
                .Distinct()
                .Select(l => l.Height + 1)
                .Sum();
        }
        
        private static FloorTile RecursivelyTraverseFloor(FloorTile floorTile, int[,] floor, Dictionary<FloorTile, FloorTile> knownFloorPaths)
        {
            if (knownFloorPaths.ContainsKey(floorTile))
                return knownFloorPaths[floorTile];

            var adjacentFound = false;
            var bestAdjacent = new FloorTile(-1, -1, floorTile.Height);
            foreach (var adjacentFloorTiles in GetAdjacenctFloorTileCoords(floorTile.X, floorTile.Y, floor))
            {
                if (floor[adjacentFloorTiles.Item1, adjacentFloorTiles.Item2] < bestAdjacent.Height)
                {
                    bestAdjacent.Height = floor[adjacentFloorTiles.Item1, adjacentFloorTiles.Item2];
                    bestAdjacent.X = adjacentFloorTiles.Item1;
                    bestAdjacent.Y = adjacentFloorTiles.Item2;
                    adjacentFound = true;
                }
            }

            var lowest = floorTile;
            if (adjacentFound)
            {
                lowest = RecursivelyTraverseFloor(bestAdjacent, floor, knownFloorPaths);

                if (!knownFloorPaths.ContainsKey(floorTile))
                    knownFloorPaths.Add(floorTile, lowest);

                if (!knownFloorPaths.ContainsKey(lowest))
                    knownFloorPaths.Add(lowest, lowest);
            }
            
            return lowest;
        }

        public static List<Tuple<int, int>> GetAdjacenctFloorTileCoords(int x, int y, int[,] floor)
        {
            var xMax = floor.GetLength(0) - 1;
            var yMax = floor.GetLength(1) - 1;

            var adjacentFloorTiles = new List<Tuple<int, int>>();

            if (x > 0)
                adjacentFloorTiles.Add(new Tuple<int, int>(x - 1, y));
            if (y > 0)
                adjacentFloorTiles.Add(new Tuple<int, int>(x, y - 1));
            if (x < xMax)
                adjacentFloorTiles.Add(new Tuple<int, int>(x + 1, y));
            if (y < yMax)
                adjacentFloorTiles.Add(new Tuple<int, int>(x, y + 1));

            return adjacentFloorTiles;
        }

        public static long Part2(List<string> data)
        {
            return -1;
        }
    }

    class FloorTile : IEqualityComparer<FloorTile>, IEquatable<FloorTile>
    {
        public FloorTile(int x, int y, int height)
        {
            X = x;
            Y = y;
            Height = height;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }

        public bool Equals(FloorTile? x, FloorTile? y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x == null && y == null)
                return true;

            if (x == null)
                return false;

            if (y == null)
                return false;

            return x.X == y.X && x.Y == y.Y;
        }

        public bool Equals(FloorTile? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return X == other.X && Y == other.Y;
        }

        public int GetHashCode([DisallowNull] FloorTile obj) =>
            //multiply first hash by prime
            obj.X.GetHashCode() * 2147483647 + obj.Y.GetHashCode();

        public override bool Equals(object obj) => Equals(obj as FloorTile);

        public override int GetHashCode() => GetHashCode(this);
    }
}