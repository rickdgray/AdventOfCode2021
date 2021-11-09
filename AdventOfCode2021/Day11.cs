using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day11
    {
        public static long Part1(List<string> data)
        {
            var rowCount = data.Count + 2;
            var colCount = data.First().Length + 2;
            var directions = Enum
                .GetValues(typeof(Direction))
                .Cast<Direction>()
                .ToList();

            var seatingChart = new Cell[rowCount, colCount];

            for (int i = 0; i < rowCount - 2; i++)
            {
                var seats = data[i].ToArray();
                for (int j = 0; j < colCount - 2; j++)
                {
                    seatingChart[i + 1, j + 1] = (seats[j]) switch
                    {
                        '.' => Cell.Floor,
                        'L' => Cell.Empty,
                        '#' => Cell.Occupied,
                        _ => throw new Exception("Invalid input"),
                    };
                }
            }

            Cell[,] previousSeatingChart = null;

            while (!SeatingChartsAreEqual(rowCount, colCount, previousSeatingChart, seatingChart))
            {
                previousSeatingChart = (Cell[,])seatingChart.Clone();

                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        if (previousSeatingChart[i, j] == Cell.Floor)
                            continue;

                        var adjacentSeats = new List<Cell>();

                        foreach (var direction in directions)
                        {
                            var (adjacentCellRow, adjacentCellCol) = GetAdjacentCell(i, j, direction);
                            adjacentSeats.Add(previousSeatingChart[adjacentCellRow, adjacentCellCol]);
                        }

                        if (previousSeatingChart[i, j] == Cell.Empty
                            && adjacentSeats.All(s => s != Cell.Occupied))
                        {
                            seatingChart[i, j] = Cell.Occupied;
                        }
                        else if (previousSeatingChart[i, j] == Cell.Occupied
                            && adjacentSeats.FindAll(s => s == Cell.Occupied).Count >= 4)
                        {
                            seatingChart[i, j] = Cell.Empty;
                        }
                    }
                }
            }

            var finalCount = 0;
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    if (seatingChart[i, j] == Cell.Occupied)
                        finalCount++;
                }
            }

            return finalCount;
        }

        public static long Part2(List<string> data)
        {
            var rowCount = data.Count + 2;
            var colCount = data.First().Length + 2;
            var directions = Enum
                .GetValues(typeof(Direction))
                .Cast<Direction>()
                .ToList();

            var seatingChart = new Cell[rowCount, colCount];

            for (int i = 0; i < rowCount - 2; i++)
            {
                var seats = data[i].ToArray();
                for (int j = 0; j < colCount - 2; j++)
                {
                    seatingChart[i + 1, j + 1] = (seats[j]) switch
                    {
                        '.' => Cell.Floor,
                        'L' => Cell.Empty,
                        '#' => Cell.Occupied,
                        _ => throw new Exception("Invalid input"),
                    };
                }
            }

            Cell[,] previousSeatingChart = null;

            while (!SeatingChartsAreEqual(rowCount, colCount, previousSeatingChart, seatingChart))
            {
                previousSeatingChart = (Cell[,])seatingChart.Clone();

                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        if (previousSeatingChart[i, j] == Cell.Floor)
                            continue;

                        var adjacentSeats = new List<Cell>();

                        foreach (var direction in directions)
                        {
                            var currentAdjacentCell = Cell.Floor;

                            var adjacentCellRow = i;
                            var adjacentCellCol = j;

                            do
                            {
                                (adjacentCellRow, adjacentCellCol) = GetAdjacentCell(adjacentCellRow, adjacentCellCol, direction);
                                currentAdjacentCell = previousSeatingChart[adjacentCellRow, adjacentCellCol];
                            } while (currentAdjacentCell == Cell.Floor
                                && adjacentCellRow > 0
                                && adjacentCellCol > 0
                                && adjacentCellRow < rowCount - 1
                                && adjacentCellCol < colCount - 1);

                            adjacentSeats.Add(currentAdjacentCell);
                        }

                        if (previousSeatingChart[i, j] == Cell.Empty
                            && adjacentSeats.All(s => s != Cell.Occupied))
                        {
                            seatingChart[i, j] = Cell.Occupied;
                        }
                        else if (previousSeatingChart[i, j] == Cell.Occupied
                            && adjacentSeats.FindAll(s => s == Cell.Occupied).Count >= 5)
                        {
                            seatingChart[i, j] = Cell.Empty;
                        }
                    }
                }
            }

            var finalCount = 0;
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    if (seatingChart[i, j] == Cell.Occupied)
                        finalCount++;
                }
            }

            return finalCount;
        }

        public static bool SeatingChartsAreEqual(int rowCount, int colCount, Cell[,] a, Cell[,] b)
        {
            if (a == null)
                return false;

            if (b == null)
                return false;

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    if (a[i, j] != b[i, j])
                        return false;
                }
            }

            return true;
        }

        public static Tuple<int, int> GetAdjacentCell(int i, int j, Direction direction)
        {
            return direction switch
            {
                Direction.Left => Tuple.Create(i, j - 1),
                Direction.UpLeft => Tuple.Create(i - 1, j - 1),
                Direction.Up => Tuple.Create(i - 1, j),
                Direction.UpRight => Tuple.Create(i - 1, j + 1),
                Direction.Right => Tuple.Create(i, j + 1),
                Direction.DownRight => Tuple.Create(i + 1, j + 1),
                Direction.Down => Tuple.Create(i + 1, j),
                Direction.DownLeft => Tuple.Create(i + 1, j - 1),
                _ => throw new ArgumentException("Invalid Direction")
            };
        }

        public enum Cell
        {
            Floor,
            Empty,
            Occupied
        }

        public enum Direction
        {
            Left,
            UpLeft,
            Up,
            UpRight,
            Right,
            DownRight,
            Down,
            DownLeft
        }
    }
}