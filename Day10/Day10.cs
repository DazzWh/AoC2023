namespace Day10;

public static class Day10
{
    public static void Run()
    {
        var grid = File.ReadAllLines("Input.txt");
        var tileMap = ParseMap(grid).ToArray();

        // Find start tile.
        var startTile = GetStartTile(tileMap);
        
        // Get the tiles around the start tile that connect to start tile.
        var tilesConnectedToStart = startTile
            .ConnectedPositions()
            .Where(p => 
                p.X >= 0 && p.X < tileMap.First().Length &&
                p.Y >= 0 && p.Y < tileMap.Length)
            // Start is connected to 4 directions, find the 2 tiles in these directions that claim connection to start. 
            .Select(p => tileMap.TileAt(p.X, p.Y))
            .Where(t => t.ConnectedPositions().Any(p => tileMap.TileAt(p) == startTile));
        
        // Follow one, taking note of the points as we go
        var currTile = tilesConnectedToStart.First();
        var previousTile = startTile; // Store previous tile, to not go backwards when we find connected tiles
        var loop = new List<Tile>();
        do
        {
            var nextPos = currTile
                .ConnectedPositions()
                .First(p => tileMap.TileAt(p) != previousTile);
                
            loop.Add(currTile);
            previousTile = currTile;
            currTile = tileMap.TileAt(nextPos);
        } while (currTile.Type != TileType.Start);

        // When we reach the start again find the middle of the loop
        Console.WriteLine((loop.Count + 1) / 2);
    }

    private static Tile[][] ParseMap(string[] grid)
    {
        var tilemap = new Tile[grid.Length][];
        for (var y = 0; y < grid.Length; y++)
        {
            var newRow = new List<Tile>();
            for (var x = 0; x < grid[y].Length; x++)
            {
                newRow.Add(new Tile(new Position(x, y), grid[y][x].ToTileType()));
            }

            tilemap[y] = newRow.ToArray();
        }

        return tilemap;
    }

    private static Tile GetStartTile(IEnumerable<Tile[]> tileMap)
    {
        foreach (var row in tileMap)
        {
            foreach (var tile in row)
            {
                if (tile.Type == TileType.Start)
                {
                    return tile;
                }
            }
        }

        throw new Exception();
    }
}