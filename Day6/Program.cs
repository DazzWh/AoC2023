var input = File.ReadAllLines("Input.txt").ToList();
var times =
    input[0].Split(':')[1]
        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToArray();

var dists =
    input[1].Split(':')[1]
        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToArray();

var answer = new int[times.Length];

for (var i = 0; i < times.Length; i++)
{
    var maxTime = times[i];
    var recordDist = dists[i];
    var waysToBeat = 0;

    for (var timeHeld = 0; timeHeld < maxTime; timeHeld++)
    {
        var distanceTraveled = timeHeld * (maxTime - timeHeld);
        
        if (distanceTraveled > recordDist)
        {
            waysToBeat++;
        }
    }

    answer[i] = waysToBeat;
}

Console.WriteLine(answer.Aggregate((x, y) => x * y));