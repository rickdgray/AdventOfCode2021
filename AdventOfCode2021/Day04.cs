namespace AdventOfCode2021
{
    public class Day04
    {
        public static long Part1(List<string> data)
        {
            var (nums, boards) = PopulateData(data);

            int? winningNum = null;
            int? winningBoardId = null;

            foreach (var num in nums)
            {
                foreach (var board in boards)
                {
                    foreach (var line in board)
                    {
                        foreach (var cell in line)
                        {
                            if (cell.Number == num)
                            {
                                cell.Marked = true;
                            }
                        }
                    }
                }

                winningBoardId = GetFirstWinningBoardId(boards);

                if (winningBoardId != null)
                {
                    winningNum = num;
                    break;
                }
            }

            var sum = 0;
            foreach (var line in boards[winningBoardId ?? -1])
            {
                foreach (var cell in line)
                {
                    if (!cell.Marked)
                    {
                        sum += cell.Number;
                    }
                }
            }

            return sum * winningNum ?? -1;
        }

        public static long Part2(List<string> data)
        {
            var (nums, boards) = PopulateData(data);

            int? winningNum = null;
            int? winningBoardId = null;
            var lastWonBoards = new HashSet<int>();

            foreach (var num in nums)
            {
                foreach (var board in boards)
                {
                    foreach (var line in board)
                    {
                        foreach (var cell in line)
                        {
                            if (cell.Number == num)
                            {
                                cell.Marked = true;
                            }
                        }
                    }
                }

                var allWonBoards = GetAllWinningBoardIds(boards);

                if (allWonBoards?.Count == 100)
                {
                    //figure out which board won most recently
                    var newlyWonBoards = allWonBoards;
                    newlyWonBoards.ExceptWith(lastWonBoards);

                    winningBoardId = newlyWonBoards.First();
                    winningNum = num;
                    break;
                }

                lastWonBoards.UnionWith(allWonBoards ?? new HashSet<int>());
            }

            var sum = 0;
            foreach (var line in boards[winningBoardId ?? -1])
            {
                foreach (var cell in line)
                {
                    if (!cell.Marked)
                    {
                        sum += cell.Number;
                    }
                }
            }

            return sum * winningNum ?? -1;
        }

        static (List<int>, List<List<List<Cell>>>) PopulateData(List<string> data)
        {
            var nums = new List<int>(data[0].Split(',').Select(n => int.Parse(n)));
            var boards = new List<List<List<Cell>>>();
            var boardCount = 0;
            int i = 2;
            while (i < data.Count)
            {
                boards.Add(new List<List<Cell>>());

                for (int j = 0; j < 5; j++)
                {
                    boards[boardCount].Add(new List<Cell>(data[i + j]
                        .Split(' ')
                        .Where(n => !string.IsNullOrWhiteSpace(n))
                        .Select(n => new Cell(int.Parse(n)))));
                }

                i += 6;
                boardCount++;
            }

            return (nums, boards);
        }

        static int? GetFirstWinningBoardId(List<List<List<Cell>>> boards)
        {
            for (int i = 0; i < boards.Count; i++)
            {
                if (CheckBoard(boards[i]))
                {
                    return i;
                }
            }

            return null;
        }

        static HashSet<int>? GetAllWinningBoardIds(List<List<List<Cell>>> boards)
        {
            var winningBoards = new HashSet<int>();

            for (int i = 0; i < boards.Count; i++)
            {
                if (CheckBoard(boards[i]))
                {
                    winningBoards.Add(i);
                }
            }

            return winningBoards.Any() ? winningBoards : null;
        }

        static bool CheckBoard(List<List<Cell>> board)
        {
            foreach (var line in board)
            {
                if (CheckRow(line))
                {
                    return true;
                }
            }

            var transedBoard = Transpose(board);

            foreach (var line in transedBoard)
            {
                if (CheckRow(line))
                {
                    return true;
                }
            }

            return false;
        }

        static bool CheckRow(List<Cell> line)
        {
            return line.All(c => c.Marked);
        }

        static List<List<Cell>> Transpose(List<List<Cell>> board)
        {
            return board
                .SelectMany(inner => inner.Select((item, index) => new { item, index }))
                .GroupBy(i => i.index, i => i.item)
                .Select(g => g.ToList())
                .ToList();
        }
    }

    public class Cell
    {
        public Cell(int number)
        {
            Number = number;
        }

        public int Number { get; set; }
        public bool Marked { get; set; }
    }
}