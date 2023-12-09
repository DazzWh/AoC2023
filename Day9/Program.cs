var input = 
    File.ReadAllLines("Input.txt")
        .Select(s => s.Split(' ').Select(int.Parse).ToList())
        .ToList();

var nextValues = new List<int>();

foreach (var valueHistory in input)
{
    
    // Generate differences history
    var differences = new List<List<int>> { valueHistory };
    var currentDiff = differences.First();

    while (currentDiff.Any(i => i != 0))
    {
        differences.Add(currentDiff.Zip(currentDiff.Skip(1), (a, b) => b - a).ToList());
        currentDiff = differences.Last();
    }
    
    // Add a new X to the start of each line (except a 0 to the lowest)
    differences.Last().Insert(0, 0);
    
    foreach (var diffList in differences[..^1]) // All but the last
    {
        diffList.Insert(0, -1); // Should be irrelevant what this is.
    }
    
    // Fill in the added numbers
    for (var i = differences.Count - 2; i >= 0; i--)
    {
        var valueRight = differences[i][1];
        var valueBelow = differences[i + 1].First();
        var value = valueRight - valueBelow;

        differences[i][0] = value;
    }
    
    nextValues.Add(differences.First().First());
}

Console.WriteLine(string.Join(',', nextValues));
Console.WriteLine($"Sum: {nextValues.Sum()}");