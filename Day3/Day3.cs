namespace Day3;

public static class Day3
{
    public static void Run()
    {
        var grid = File.ReadAllLines("Day3.txt");

        var total = 0;
        var currNum = string.Empty;
        var addNum = false;

        for (var x = 0; x < grid.Length; x++)
        {
            var row = grid[x];
            for (var y = 0; y < row.Length; y++)
            {
                var curr = row[y];
                
                if (!char.IsDigit(curr) || y == 0)
                {
                    if (addNum)
                    {
                        total += int.Parse(currNum);
                        addNum = false;
                        currNum = string.Empty;
                        continue;
                    }
                    
                    currNum = string.Empty;
                }

                if (!char.IsDigit(curr))
                {
                    continue;
                }
                
                if (char.IsDigit(curr))
                {
                    currNum += curr;

                    if (addNum)
                    {
                        continue;
                    }
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

                foreach (var dir in dirs)
                {
                    // Break if out of bounds
                    if (x + dir.Item1 < 0 || x + dir.Item1 >= grid.Length ||
                        y + dir.Item2 < 0 || y + dir.Item2 >= row.Length)
                    {
                        continue;
                    }

                    var checkPos = grid[x + dir.Item1][y + dir.Item2];
                    
                    if (!char.IsDigit(checkPos) && checkPos != '.')
                    {
                        addNum = true;
                    }
                }
            }
        }
        
        Console.WriteLine(total);
    }
}