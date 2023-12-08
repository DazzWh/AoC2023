using System.Text.RegularExpressions;

var input = File.ReadAllLines("Input.txt").ToList();

var pathToFollow = input[0];
input.RemoveRange(0, 2);

var nodes = input
    .Select(s => new Regex(@"([A-Z]{3})\s=\s\(([A-Z]{3}),\s([A-Z]{3})\)").Matches(s)[0].Groups)
    .Select(gc => new { Pos = gc[1].Value, L = gc[2].Value, R = gc[3].Value })
    .ToArray();

var currentNodes = nodes.Where(n => n.Pos.Last() == 'A').ToArray();

// Get smallest steps for each node
var smallestSteps = new List<long>();

foreach (var node in currentNodes)
{
    var currentNode = node;
    long steps = 0;

    while (currentNode.Pos.Last() != 'Z')
    {
        var nextLocation = pathToFollow[(int)steps % pathToFollow.Length] == 'R' ? currentNode.R : currentNode.L;
        steps++;

        currentNode = nodes.First(n => n.Pos == nextLocation);
    }
    
    smallestSteps.Add(steps);
}

// Find lowest they all divide into...?
var answer = lowestCommonMultipleInArray(smallestSteps.ToArray());

Console.WriteLine(answer);

return;

// Thanks maths.
long lowestCommonMultipleInArray(IEnumerable<long> numbers)
{
    return numbers.Aggregate(lowestCommonMultiple);
}

long lowestCommonMultiple(long a, long b)
{
    return Math.Abs(a * b) / greatestCommonDivider(a, b);
}

long greatestCommonDivider(long a, long b)
{
    return b == 0 ? a : greatestCommonDivider(b, a % b);
}