using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day17
    {
        public static long Part1(List<string> data)
        {
            var iterations = 6;
            var rowCount = data.Count + 2 * iterations + 2;
            var colCount = data.First().Length + 2 * iterations + 2;
            var layCount = 1 + 2 * iterations + 2;
            var directions = Enum
                .GetValues(typeof(Direction))
                .Cast<Direction>()
                .ToList();

            var pocketDim = new Cell[rowCount, colCount, layCount];

            for (int i = 0; i < data.Count; i++)
            {
                var rows = data[i].ToArray();
                for (int j = 0; j < data.First().Length; j++)
                {
                    pocketDim[i + iterations + 1, j + iterations + 1, iterations + 1] = (rows[j]) switch
                    {
                        '.' => Cell.Inactive,
                        '#' => Cell.Active,
                        _ => throw new Exception("Invalid input"),
                    };
                }
            }
            
            Cell[,,] previousPocketDim = null;

            for (int iteration = 0; iteration < iterations; iteration++)
            {
                previousPocketDim = (Cell[,,])pocketDim.Clone();

                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        for (int k = 0; k < layCount; k++)
                        {
                            var adjacentCells = new List<Cell>();

                            foreach (var direction in directions)
                            {
                                var (adjacentCellRow, adjacentCellCol, adjacentCellLay) = GetAdjacentCell(i, j, k, direction);

                                if (adjacentCellRow < 0
                                    || adjacentCellCol < 0
                                    || adjacentCellLay < 0
                                    || adjacentCellRow > rowCount - 1
                                    || adjacentCellCol > colCount - 1
                                    || adjacentCellLay > layCount - 1)
                                    continue;

                                adjacentCells.Add(previousPocketDim[adjacentCellRow, adjacentCellCol, adjacentCellLay]);
                            }

                            if (previousPocketDim[i, j, k] == Cell.Active)
                            {
                                var activeNeighborCount = adjacentCells.FindAll(s => s == Cell.Active).Count;
                                if (activeNeighborCount < 2 || activeNeighborCount > 3)
                                {
                                    pocketDim[i, j, k] = Cell.Inactive;
                                }
                            }
                            else
                            {
                                if (adjacentCells.FindAll(s => s == Cell.Active).Count == 3)
                                {
                                    pocketDim[i, j, k] = Cell.Active;
                                }
                            }
                        }
                    }
                }
            }

            var finalCount = 0;
            for (int i = 0; i < rowCount; i++)
                for (int j = 0; j < colCount; j++)
                    for (int k = 0; k < layCount; k++)
                        if (pocketDim[i, j, k] == Cell.Active)
                            finalCount++;

            return finalCount;
        }

        public static long Part2(List<string> data)
        {
            var iterations = 6;
            var rowCount = data.Count + 2 * iterations + 2;
            var colCount = data.First().Length + 2 * iterations + 2;
            var layCount = 1 + 2 * iterations + 2;
            var moodCount = 1 + 2 * iterations + 2;
            var directions = Enum
                .GetValues(typeof(Direction4D))
                .Cast<Direction4D>()
                .ToList();

            var pocketDim = new Cell[rowCount, colCount, layCount, moodCount];

            for (int i = 0; i < data.Count; i++)
            {
                var rows = data[i].ToArray();
                for (int j = 0; j < data.First().Length; j++)
                {
                    pocketDim[i + iterations + 1, j + iterations + 1, iterations + 1, iterations + 1] = (rows[j]) switch
                    {
                        '.' => Cell.Inactive,
                        '#' => Cell.Active,
                        _ => throw new Exception("Invalid input"),
                    };
                }
            }

            Cell[,,,] previousPocketDim = null;

            for (int iteration = 0; iteration < iterations; iteration++)
            {
                previousPocketDim = (Cell[,,,])pocketDim.Clone();

                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        for (int k = 0; k < layCount; k++)
                        {
                            for (int l = 0; l < moodCount; l++)
                            {
                                var adjacentCells = new List<Cell>();

                                foreach (var direction in directions)
                                {
                                    var (adjacentCellRow, adjacentCellCol, adjacentCellLay, adjacentCellMood) = GetAdjacentCell4D(i, j, k, l, direction);

                                    if (adjacentCellRow < 0
                                        || adjacentCellCol < 0
                                        || adjacentCellLay < 0
                                        || adjacentCellMood < 0
                                        || adjacentCellRow > rowCount - 1
                                        || adjacentCellCol > colCount - 1
                                        || adjacentCellLay > layCount - 1
                                        || adjacentCellMood > moodCount - 1)
                                        continue;

                                    adjacentCells.Add(previousPocketDim[adjacentCellRow, adjacentCellCol, adjacentCellLay, adjacentCellMood]);
                                }

                                if (previousPocketDim[i, j, k, l] == Cell.Active)
                                {
                                    var activeNeighborCount = adjacentCells.FindAll(s => s == Cell.Active).Count;
                                    if (activeNeighborCount < 2 || activeNeighborCount > 3)
                                    {
                                        pocketDim[i, j, k, l] = Cell.Inactive;
                                    }
                                }
                                else
                                {
                                    if (adjacentCells.FindAll(s => s == Cell.Active).Count == 3)
                                    {
                                        pocketDim[i, j, k, l] = Cell.Active;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            var finalCount = 0;
            for (int i = 0; i < rowCount; i++)
                for (int j = 0; j < colCount; j++)
                    for (int k = 0; k < layCount; k++)
                        for (int l = 0; l < moodCount; l++)
                            if (pocketDim[i, j, k, l] == Cell.Active)
                            finalCount++;

            return finalCount;
        }

        public static Tuple<int, int, int> GetAdjacentCell(int i, int j, int k, Direction direction)
        {
            return direction switch
            {
                Direction.Left => Tuple.Create(i, j - 1, k),
                Direction.UpLeft => Tuple.Create(i - 1, j - 1, k),
                Direction.Up => Tuple.Create(i - 1, j, k),
                Direction.UpRight => Tuple.Create(i - 1, j + 1, k),
                Direction.Right => Tuple.Create(i, j + 1, k),
                Direction.DownRight => Tuple.Create(i + 1, j + 1, k),
                Direction.Down => Tuple.Create(i + 1, j, k),
                Direction.DownLeft => Tuple.Create(i + 1, j - 1, k),
                Direction.Back => Tuple.Create(i, j, k - 1),
                Direction.BackLeft => Tuple.Create(i, j - 1, k - 1),
                Direction.BackUpLeft => Tuple.Create(i - 1, j - 1, k - 1),
                Direction.BackUp => Tuple.Create(i - 1, j, k - 1),
                Direction.BackUpRight => Tuple.Create(i - 1, j + 1, k - 1),
                Direction.BackRight => Tuple.Create(i, j + 1, k - 1),
                Direction.BackDownRight => Tuple.Create(i + 1, j + 1, k - 1),
                Direction.BackDown => Tuple.Create(i + 1, j, k - 1),
                Direction.BackDownLeft => Tuple.Create(i + 1, j - 1, k - 1),
                Direction.Front => Tuple.Create(i, j, k + 1),
                Direction.FrontLeft => Tuple.Create(i, j - 1, k + 1),
                Direction.FrontUpLeft => Tuple.Create(i - 1, j - 1, k + 1),
                Direction.FrontUp => Tuple.Create(i - 1, j, k + 1),
                Direction.FrontUpRight => Tuple.Create(i - 1, j + 1, k + 1),
                Direction.FrontRight => Tuple.Create(i, j + 1, k + 1),
                Direction.FrontDownRight => Tuple.Create(i + 1, j + 1, k + 1),
                Direction.FrontDown => Tuple.Create(i + 1, j, k + 1),
                Direction.FrontDownLeft => Tuple.Create(i + 1, j - 1, k + 1),
                _ => throw new ArgumentException("Invalid Direction")
            };
        }

        public static Tuple<int, int, int, int> GetAdjacentCell4D(int i, int j, int k, int l, Direction4D direction)
        {
            return direction switch
            {
                Direction4D.Left => Tuple.Create(i, j - 1, k, l),
                Direction4D.UpLeft => Tuple.Create(i - 1, j - 1, k, l),
                Direction4D.Up => Tuple.Create(i - 1, j, k, l),
                Direction4D.UpRight => Tuple.Create(i - 1, j + 1, k, l),
                Direction4D.Right => Tuple.Create(i, j + 1, k, l),
                Direction4D.DownRight => Tuple.Create(i + 1, j + 1, k, l),
                Direction4D.Down => Tuple.Create(i + 1, j, k, l),
                Direction4D.DownLeft => Tuple.Create(i + 1, j - 1, k, l),
                Direction4D.Back => Tuple.Create(i, j, k - 1, l),
                Direction4D.BackLeft => Tuple.Create(i, j - 1, k - 1, l),
                Direction4D.BackUpLeft => Tuple.Create(i - 1, j - 1, k - 1, l),
                Direction4D.BackUp => Tuple.Create(i - 1, j, k - 1, l),
                Direction4D.BackUpRight => Tuple.Create(i - 1, j + 1, k - 1, l),
                Direction4D.BackRight => Tuple.Create(i, j + 1, k - 1, l),
                Direction4D.BackDownRight => Tuple.Create(i + 1, j + 1, k - 1, l),
                Direction4D.BackDown => Tuple.Create(i + 1, j, k - 1, l),
                Direction4D.BackDownLeft => Tuple.Create(i + 1, j - 1, k - 1, l),
                Direction4D.Front => Tuple.Create(i, j, k + 1, l),
                Direction4D.FrontLeft => Tuple.Create(i, j - 1, k + 1, l),
                Direction4D.FrontUpLeft => Tuple.Create(i - 1, j - 1, k + 1, l),
                Direction4D.FrontUp => Tuple.Create(i - 1, j, k + 1, l),
                Direction4D.FrontUpRight => Tuple.Create(i - 1, j + 1, k + 1, l),
                Direction4D.FrontRight => Tuple.Create(i, j + 1, k + 1, l),
                Direction4D.FrontDownRight => Tuple.Create(i + 1, j + 1, k + 1, l),
                Direction4D.FrontDown => Tuple.Create(i + 1, j, k + 1, l),
                Direction4D.FrontDownLeft => Tuple.Create(i + 1, j - 1, k + 1, l),
                Direction4D.Charm => Tuple.Create(i, j, k, l - 1),
                Direction4D.CharmLeft => Tuple.Create(i, j - 1, k, l - 1),
                Direction4D.CharmUpLeft => Tuple.Create(i - 1, j - 1, k, l - 1),
                Direction4D.CharmUp => Tuple.Create(i - 1, j, k, l - 1),
                Direction4D.CharmUpRight => Tuple.Create(i - 1, j + 1, k, l - 1),
                Direction4D.CharmRight => Tuple.Create(i, j + 1, k, l - 1),
                Direction4D.CharmDownRight => Tuple.Create(i + 1, j + 1, k, l - 1),
                Direction4D.CharmDown => Tuple.Create(i + 1, j, k, l - 1),
                Direction4D.CharmDownLeft => Tuple.Create(i + 1, j - 1, k, l - 1),
                Direction4D.CharmBack => Tuple.Create(i, j, k - 1, l - 1),
                Direction4D.CharmBackLeft => Tuple.Create(i, j - 1, k - 1, l - 1),
                Direction4D.CharmBackUpLeft => Tuple.Create(i - 1, j - 1, k - 1, l - 1),
                Direction4D.CharmBackUp => Tuple.Create(i - 1, j, k - 1, l - 1),
                Direction4D.CharmBackUpRight => Tuple.Create(i - 1, j + 1, k - 1, l - 1),
                Direction4D.CharmBackRight => Tuple.Create(i, j + 1, k - 1, l - 1),
                Direction4D.CharmBackDownRight => Tuple.Create(i + 1, j + 1, k - 1, l - 1),
                Direction4D.CharmBackDown => Tuple.Create(i + 1, j, k - 1, l - 1),
                Direction4D.CharmBackDownLeft => Tuple.Create(i + 1, j - 1, k - 1, l - 1),
                Direction4D.CharmFront => Tuple.Create(i, j, k + 1, l - 1),
                Direction4D.CharmFrontLeft => Tuple.Create(i, j - 1, k + 1, l - 1),
                Direction4D.CharmFrontUpLeft => Tuple.Create(i - 1, j - 1, k + 1, l - 1),
                Direction4D.CharmFrontUp => Tuple.Create(i - 1, j, k + 1, l - 1),
                Direction4D.CharmFrontUpRight => Tuple.Create(i - 1, j + 1, k + 1, l - 1),
                Direction4D.CharmFrontRight => Tuple.Create(i, j + 1, k + 1, l - 1),
                Direction4D.CharmFrontDownRight => Tuple.Create(i + 1, j + 1, k + 1, l - 1),
                Direction4D.CharmFrontDown => Tuple.Create(i + 1, j, k + 1, l - 1),
                Direction4D.CharmFrontDownLeft => Tuple.Create(i + 1, j - 1, k + 1, l - 1),
                Direction4D.Strange => Tuple.Create(i, j, k, l + 1),
                Direction4D.StrangeLeft => Tuple.Create(i, j - 1, k, l + 1),
                Direction4D.StrangeUpLeft => Tuple.Create(i - 1, j - 1, k, l + 1),
                Direction4D.StrangeUp => Tuple.Create(i - 1, j, k, l + 1),
                Direction4D.StrangeUpRight => Tuple.Create(i - 1, j + 1, k, l + 1),
                Direction4D.StrangeRight => Tuple.Create(i, j + 1, k, l + 1),
                Direction4D.StrangeDownRight => Tuple.Create(i + 1, j + 1, k, l + 1),
                Direction4D.StrangeDown => Tuple.Create(i + 1, j, k, l + 1),
                Direction4D.StrangeDownLeft => Tuple.Create(i + 1, j - 1, k, l + 1),
                Direction4D.StrangeBack => Tuple.Create(i, j, k - 1, l + 1),
                Direction4D.StrangeBackLeft => Tuple.Create(i, j - 1, k - 1, l + 1),
                Direction4D.StrangeBackUpLeft => Tuple.Create(i - 1, j - 1, k - 1, l + 1),
                Direction4D.StrangeBackUp => Tuple.Create(i - 1, j, k - 1, l + 1),
                Direction4D.StrangeBackUpRight => Tuple.Create(i - 1, j + 1, k - 1, l + 1),
                Direction4D.StrangeBackRight => Tuple.Create(i, j + 1, k - 1, l + 1),
                Direction4D.StrangeBackDownRight => Tuple.Create(i + 1, j + 1, k - 1, l + 1),
                Direction4D.StrangeBackDown => Tuple.Create(i + 1, j, k - 1, l + 1),
                Direction4D.StrangeBackDownLeft => Tuple.Create(i + 1, j - 1, k - 1, l + 1),
                Direction4D.StrangeFront => Tuple.Create(i, j, k + 1, l + 1),
                Direction4D.StrangeFrontLeft => Tuple.Create(i, j - 1, k + 1, l + 1),
                Direction4D.StrangeFrontUpLeft => Tuple.Create(i - 1, j - 1, k + 1, l + 1),
                Direction4D.StrangeFrontUp => Tuple.Create(i - 1, j, k + 1, l + 1),
                Direction4D.StrangeFrontUpRight => Tuple.Create(i - 1, j + 1, k + 1, l + 1),
                Direction4D.StrangeFrontRight => Tuple.Create(i, j + 1, k + 1, l + 1),
                Direction4D.StrangeFrontDownRight => Tuple.Create(i + 1, j + 1, k + 1, l + 1),
                Direction4D.StrangeFrontDown => Tuple.Create(i + 1, j, k + 1, l + 1),
                Direction4D.StrangeFrontDownLeft => Tuple.Create(i + 1, j - 1, k + 1, l + 1),
                _ => throw new ArgumentException("Invalid Direction")
            };
        }

        public enum Cell
        {
            Inactive,
            Active
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
            DownLeft,
            Back,
            BackLeft,
            BackUpLeft,
            BackUp,
            BackUpRight,
            BackRight,
            BackDownRight,
            BackDown,
            BackDownLeft,
            Front,
            FrontLeft,
            FrontUpLeft,
            FrontUp,
            FrontUpRight,
            FrontRight,
            FrontDownRight,
            FrontDown,
            FrontDownLeft
        }

        public enum Direction4D
        {
            Left,
            UpLeft,
            Up,
            UpRight,
            Right,
            DownRight,
            Down,
            DownLeft,
            Back,
            BackLeft,
            BackUpLeft,
            BackUp,
            BackUpRight,
            BackRight,
            BackDownRight,
            BackDown,
            BackDownLeft,
            Front,
            FrontLeft,
            FrontUpLeft,
            FrontUp,
            FrontUpRight,
            FrontRight,
            FrontDownRight,
            FrontDown,
            FrontDownLeft,
            Charm,
            CharmLeft,
            CharmUpLeft,
            CharmUp,
            CharmUpRight,
            CharmRight,
            CharmDownRight,
            CharmDown,
            CharmDownLeft,
            CharmBack,
            CharmBackLeft,
            CharmBackUpLeft,
            CharmBackUp,
            CharmBackUpRight,
            CharmBackRight,
            CharmBackDownRight,
            CharmBackDown,
            CharmBackDownLeft,
            CharmFront,
            CharmFrontLeft,
            CharmFrontUpLeft,
            CharmFrontUp,
            CharmFrontUpRight,
            CharmFrontRight,
            CharmFrontDownRight,
            CharmFrontDown,
            CharmFrontDownLeft,
            Strange,
            StrangeLeft,
            StrangeUpLeft,
            StrangeUp,
            StrangeUpRight,
            StrangeRight,
            StrangeDownRight,
            StrangeDown,
            StrangeDownLeft,
            StrangeBack,
            StrangeBackLeft,
            StrangeBackUpLeft,
            StrangeBackUp,
            StrangeBackUpRight,
            StrangeBackRight,
            StrangeBackDownRight,
            StrangeBackDown,
            StrangeBackDownLeft,
            StrangeFront,
            StrangeFrontLeft,
            StrangeFrontUpLeft,
            StrangeFrontUp,
            StrangeFrontUpRight,
            StrangeFrontRight,
            StrangeFrontDownRight,
            StrangeFrontDown,
            StrangeFrontDownLeft
        }
    }
}