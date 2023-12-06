var input = File.ReadAllLines("Input.txt").ToList();
var times =
    input[0].Split(':')[1]
        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
        .Select(long.Parse)
        .ToArray();

var dists =
    input[1].Split(':')[1]
        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
        .Select(long.Parse)
        .ToArray();

long answer = 1;

var lockObj = new object();

for (var i = 0; i < times.Length; i++)
{
    var maxTime = times[i];
    var recordDist = dists[i];
    var waysToBeat = 0;

    Parallel.For(0, maxTime, timeHeld =>
    {
        var distanceTraveled = timeHeld * (maxTime - timeHeld);

        if (distanceTraveled > recordDist)
        {
            lock (lockObj)
            {
                waysToBeat++;
            }
        }
    });

    answer *= waysToBeat;
}

Console.WriteLine(answer);