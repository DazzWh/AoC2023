using MoreLinq;
using MoreLinq.Extensions;

namespace Day2;

public static class Day2
{
    public static void Run()
    {
        // Limit = 12 red, 13 green, 14 blue.
        var lines = File.ReadAllLines("Day2Input.txt").ToList();

        // Split into games
        lines = lines.Select(l => l.Split(':')[1]).ToList();

        var shown = lines.Select(l => l.Split(';'));
        var maxInEachGame = shown.Select(ParseGame).ToList();


        var total = 0;
        foreach (var (r, i) in maxInEachGame.Select((item, index) => (item, index)))
        {
            if (r.R <= 12 && r.G <= 13 && r.B <= 14)
            {
                total += i + 1;
            }
        }
        
        Console.WriteLine(total);
    }

    private static (int R, int G, int B) ParseGame(string[] shows)
    {
        int r = 0, g = 0, b = 0;

        foreach (var show in shows)
        {
            var numbers = show.Split(',');
            foreach (var num in numbers)
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