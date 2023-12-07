using MoreLinq.Extensions;

var cardOrder = new List<char> { 'A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J' };

Console.WriteLine(
    File.ReadAllLines("input.txt")
        .Select(l =>
            new
            {
                cards = l.Split(' ')[0],
                bid = int.Parse(l.Split(' ')[1]),
                strength = calculateHandStrength(l.Split(' ')[0])
            })
        .OrderBy(h => h.strength)
        .ThenByDescending(h => cardOrder.IndexOf(h.cards[0]))
        .ThenByDescending(h => cardOrder.IndexOf(h.cards[1]))
        .ThenByDescending(h => cardOrder.IndexOf(h.cards[2]))
        .ThenByDescending(h => cardOrder.IndexOf(h.cards[3]))
        .ThenByDescending(h => cardOrder.IndexOf(h.cards[4]))
        .Select((h, i) => h.bid * (i + 1))
        .Sum()
);

return;

int calculateHandStrength(string cards)
{
    if (cards.Contains('J'))
    {
        return cardOrder[..^1]
            .Select(possibleCard => cards.Replace('J', possibleCard))
            .Max(calculateHandStrength);
    }

    var uniqueCardValues = cards.Distinct().Count();

    var cardCounts = cards
        .GroupBy(c => c)
        .Select(g => new { Card = g.Key, Count = g.Count() })
        .ToArray();

    return cards switch
    {
        var fiveOfAKind when uniqueCardValues == 1 => 6,

        var fourOfAKind when uniqueCardValues == 2 &&
                             (cardCounts.First().Count, cardCounts.Last().Count) is (1, 4) or (4, 1) => 5,

        var fullHouse when uniqueCardValues == 2 &&
                           (cardCounts.First().Count, cardCounts.Last().Count) is (3, 2) or (2, 3) => 4,

        var threeOfAKind when cardCounts.Any(c => c.Count == 3) => 3,

        var twoPair when cardCounts.Count(c => c.Count == 2) == 2 => 2,

        var onePair when cardCounts.Count(c => c.Count == 2) == 1 => 1,

        var highCard when uniqueCardValues == 5 => 0,

        _ => throw new ArgumentOutOfRangeException(nameof(cards), cards, null)
    };
}