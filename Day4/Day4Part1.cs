using System.Text.RegularExpressions;

namespace Day4;

public static class Day4Part1
{
    public static void Run()
    {
        Console.WriteLine(
            File.ReadAllLines("Day4.txt")
                .Select(l => new Regex(@"(Card\s*\d*:)(.*)\|(.*)").Matches(l)[0].Groups)
                .Select(gc =>
                    (gc[2].Value.Split(' ', StringSplitOptions.RemoveEmptyEntries),
                     gc[3].Value.Split(' ', StringSplitOptions.RemoveEmptyEntries)))
                .Aggregate(0, (score, cards) =>
                    score + Math.Max(0, 1 << cards.Item1.Intersect(cards.Item2).Count() - 1)));
    }
}