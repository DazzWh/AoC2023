using MoreLinq;
using MoreLinq.Extensions;

namespace Day2;

public static class Day2
{
    public static void Run()
    {
        // Limit = 12 red, 13 green, 14 blue.
        Console.WriteLine(
            File.ReadAllLines("Day2Input.txt")
            .Select(l => l.Split(':')[1])
            .Select(l => l.Split(';'))
            .Select(ParseGame)
            .Select(r => r.R * r.G * r.B)
            .Sum());
    }

    private static (int R, int G, int B) ParseGame(string[] game)
    {
        int r = 0, g = 0, b = 0;

        foreach (var show in game)
        {
            foreach (var num in show.Split(','))
            {
                var s = num.Trim().Split(' ');
                switch (s[1])
                {
                    case "red":
                        r = Math.Max(r, int.Parse(s[0]));
                        break;
                    case "green":
                        g = Math.Max(g, int.Parse(s[0]));
                        break;
                    case "blue":
                        b = Math.Max(b, int.Parse(s[0]));
                        break;
                }
            }
        }

        return (r, g, b);
    }
}