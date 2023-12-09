

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
    
    // Add a new X on the end of each line (except a 0 to the lowest)
    
    differences.Last().Add(0);
    
    foreach (var diffList in differences[..^1]) // All but the last
    {
        diffList.Add(-1); // Should be irrelevant what this is.
    }
    
    // Fill in the added numbers
    for (var i = differences.Count - 2; i >= 0; i--)
    {
        var valueLeft = differences[i][^2];
        var valueBelow = differences[i + 1].Last();
        var value = valueLeft + valueBelow;

        differences[i][^1] = value;
    }
    
    nextValues.Add(differences.First().Last());
}

Console.WriteLine(string.Join(',', nextValues));
Console.WriteLine($"Sum: {nextValues.Sum()}");