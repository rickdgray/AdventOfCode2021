using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day3
    {
        public static long Part1(List<string> data)
        {
            var rowCount = data.Count;
            var colCount = data.First().Length;
            var map = new bool[rowCount, colCount];

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    map[i, j] = data[i].ElementAt(j).Equals('#');
                }
            }

            var currentRow = 0;
            var currentCol = 0;
            var treeCount = 0;

            while (currentRow < rowCount - 1)
            {
                currentCol = (currentCol + 3) % colCount;
                currentRow++;

                if (map[currentRow, currentCol])
                    treeCount++;
            }

            return treeCount;
        }

        public static long Part2(List<string> data)
        {
            var rowCount = data.Count;
            var colCount = data.First().Length;
            var map = new bool[rowCount, colCount];

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    map[i, j] = data[i].ElementAt(j).Equals('#');
                }
            }

            var slopes = new List<Tuple<int, int>>
            {
                Tuple.Create(1, 1),
                Tuple.Create(3, 1),
                Tuple.Create(5, 1),
                Tuple.Create(7, 1),
                Tuple.Create(1, 2)
            };

            var treeCounts = new List<long>();

            foreach (var slope in slopes)
            {
                var currentRow = 0;
                var currentCol = 0;
                var treeCount = 0L;

                while (currentRow < rowCount - 1)
                {
                    currentCol = (currentCol + slope.Item1) % colCount;
                    currentRow += slope.Item2;

                    if (map[currentRow, currentCol])
                        treeCount++;
                }

                treeCounts.Add(treeCount);
            }

            return treeCounts.Aggregate((total, next) => total * next);
        }
    }
}