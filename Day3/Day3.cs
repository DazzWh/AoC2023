namespace Day3;

public static class Day3
{
    public static void Run()
    {
        var grid = File.ReadAllLines("Day3.txt");

        var total = 0;

        for (var x = 0; x < grid.Length; x++)
        {
            var row = grid[x];
            for (var y = 0; y < row.Length; y++)
            {
                var curr = row[y];

                if (curr != '*')
                {
                    continue;
                }
                
                // Check each direction
                var dirs = new[]
                {
                    (1, 1), // Down Right
                    (-1, 0), // Up
                    (1, 0), // Down
                    (0, -1), // Left
                    (0, 1), // Right
                    (-1, -1), // Up Left
                    (-1, 1), // Up Right
                    (1, -1), // Down Left
                };

                var foundDigits = new List<(int, int)>();
                
                foreach (var dir in dirs)
                {
                    // Break if out of bounds
                    if (x + dir.Item1 < 0 || x + dir.Item1 >= grid.Length ||
                        y + dir.Item2 < 0 || y + dir.Item2 >= row.Length)
                    {
                        continue;
                    }

                    var checkPos = grid[x + dir.Item1][y + dir.Item2];
                    
                    if (char.IsDigit(checkPos))
                    {
                        foundDigits.Add((x + dir.Item1, y + dir.Item2));
                    }
                }

                if (foundDigits.Any())
                {
                    total += CalculateGearRatio(foundDigits, grid);
                }
            }
        }
        
        Console.WriteLine(total);
    }

    private static int CalculateGearRatio(List<(int, int)> foundDigits, string[] grid)
    {
        var adjacentParts = new List<Part>();
        
        foreach (var digits in foundDigits)
        {
            adjacentParts.Add(CreatePartAt(digits.Item1, digits.Item2, grid[digits.Item1]));
        }

        var unique = adjacentParts.Distinct().ToArray();

        if (unique.Length == 2)
        {
            return unique[0].Value * unique[1].Value;
        }
        
        return 0;
    }

    private static Part CreatePartAt(int x, int y, string row)
    {
        // Find index of left most num from y.
        var leftMost = y;
        while (true)
        {
            leftMost--;

            if (leftMost < 0 || !char.IsDigit(row[leftMost]))
            {
                leftMost++;
                break;
            }
        }
        
        // Find index of right most num from y.
        var rightMost = y;
        while (true)
        {
            rightMost++;

            if (rightMost > row.Length - 1 || !char.IsDigit(row[rightMost]))
            {
                rightMost--;
                break;
            }
        }
        
        // Create number from these parts.
        var value = int.Parse(row.Substring(leftMost, rightMost - leftMost + 1));
        
        return new Part(x, leftMost, rightMost, value);
    }

    private class Part
    {
        public int Row;
        public int Start;
        public int End;
        public int Value;

        public Part(int row, int start, int end, int value)
        {
            Row = row;
            Start = start;
            End = end;
            Value = value;
        }

        public override bool Equals(object? obj)
        {
            var other = obj as Part;

            if (other == null)
            {
                return false;
            }

            return other.Row == Row && other.Start == Start && other.End == End;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Start, End, Value);
        }
    }
}