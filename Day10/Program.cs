Day10.Day10.Run();
return;

public static class Extenstions
{
    public static TileType ToTileType(this char c)
    {
        return c switch
        {
            '|' => TileType.Vertical,
            '-' => TileType.Horizontal,
            'L' => TileType.NorthEast,
            'J' => TileType.NorthWest,
            '7' => TileType.SouthWest,
            'F' => TileType.SouthEast,
            '.' => TileType.Gound,
            'S' => TileType.Start,
            _ => throw new Exception()
        };
    }

    public static Tile TileAt(this Tile[][] map, int x, int y)
    {
        return map[y][x];
    }
    
    public static Tile TileAt(this Tile[][] map, Position p)
    {
        return map[p.Y][p.X];
    }
}

public enum TileType
{
    Vertical,
    Horizontal,
    NorthEast,
    NorthWest,
    SouthWest,
    SouthEast,
    Gound,
    Start
}

static class Direction
{
    public static Position North = new (0, -1);
    public static Position South = new (0,1);
    public static Position East = new (1, 0);
    public static Position West = new (-1,0);
}

public class Position(int x, int y)
{
    public int X = x;
    public int Y = y;

    public override string ToString()
    {
        return $"({X},{Y})";
    }

    public Position Copy()
    {
        return new Position(X, Y);
    }
    
    public override bool Equals(object? obj)
    {
        var other = obj as Position;

        if (other == null)
        {
            return false;
        }

        return other.X == X && other.Y == Y;
    }

    protected bool Equals(Position other)
    {
        return X == other.X && Y == other.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}

public class Tile(Position position, TileType type)
{
    public Position Pos = position;
    public TileType Type = type;

    public List<Position> ConnectedPositions()
    {
        var positions = new List<Position>();
        
        switch (type)
        {
            case TileType.Vertical:
                positions.Add(Direction.North);
                positions.Add(Direction.South);
                break;
            case TileType.Horizontal:
                positions.Add(Direction.East);
                positions.Add(Direction.West);
                break;
            case TileType.NorthEast:
                positions.Add(Direction.North);
                positions.Add(Direction.East);
                break;
            case TileType.NorthWest:
                positions.Add(Direction.North);
                positions.Add(Direction.West);
                break;
            case TileType.SouthWest:
                positions.Add(Direction.South);
                positions.Add(Direction.West);
                break;
            case TileType.SouthEast:
                positions.Add(Direction.South);
                positions.Add(Direction.East);
                break;
            case TileType.Start:
                positions.Add(Direction.North);
                positions.Add(Direction.East);
                positions.Add(Direction.South);
                positions.Add(Direction.West);
                break;
            case TileType.Gound:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        return positions.Select(p => new Position(p.X + Pos.X, p.Y + Pos.Y)).ToList();
    }

    public override string ToString()
    {
        return $"({Type} at {Pos})";
    }

    protected bool Equals(Tile other)
    {
        return Equals(other.Pos, Pos);
    }
}

