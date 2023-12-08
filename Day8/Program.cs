
using System.Text.RegularExpressions;

var input = File.ReadAllLines("Input.txt").ToList();

var pathToFollow = input[0];
input.RemoveRange(0, 2);

var nodes = input
    .Select(s => new Regex(@"([A-Z]{3})\s=\s\(([A-Z]{3}),\s([A-Z]{3})\)").Matches(s)[0].Groups)
    .Select(gc => new { Pos = gc[1].Value, L = gc[2].Value, R = gc[3].Value })
    .ToArray();

var currentNode = nodes.First(n => n.Pos == "AAA");

var steps = 0;
while (currentNode.Pos != "ZZZ")
{
    var nextLocation = pathToFollow[steps % pathToFollow.Length] == 'R' ? currentNode.R : currentNode.L;
    steps++;

    currentNode = nodes.First(n => n.Pos == nextLocation);
}

Console.WriteLine(steps);
