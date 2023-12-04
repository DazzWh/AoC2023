using System.Text.RegularExpressions;

namespace Day4;

public class Day4Part2
{
    public static void Run()
    {
        var input = File.ReadAllLines("Day4.txt")
            .Select(l => new Regex(@"(Card\s*\d*:)(.*)\|(.*)").Matches(l)[0].Groups)
            .Select(gc =>
                (gc[2].Value.Split(' ', StringSplitOptions.RemoveEmptyEntries),
                    gc[3].Value.Split(' ', StringSplitOptions.RemoveEmptyEntries), 1))
            .ToList();

        var counts = Enumerable.Repeat(1, input.Count).ToArray();
        
        for (var i = 0; i < input.Count; i++)
        {
            var winNums = input[i].Item2;
            var currCard = input[i].Item1;
            var matches = currCard.Intersect(winNums).Count();
            for (var j = 0; j < matches; j++)
            {
                counts[i + j + 1] += 1 * counts[i];
            }
        }

        Console.WriteLine(counts.Sum());
    }
}