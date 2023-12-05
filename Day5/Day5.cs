using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Day5
{
    internal class Day5
    {
        public void Run()
        {
            Console.WriteLine("Running solution...");
            var timer = new Stopwatch();
            timer.Start();

            var input = File.ReadAllLines("Day5Input.txt").ToList();

            var seeds = input[0]
                .Remove(0, "seeds: ".Length)
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToArray();

            input.RemoveRange(0, 2);

            var maps = CompileMaps(input);

            var checkChunkSize = maps
                .Select(am => am.Ranges)
                .Select(rl => rl.Min(r => r.Range))
                .Min();

            checkChunkSize /= 6;

            // Optimization logging.
            var totalSeeds = seeds.Where((x, i) => i % 2 != 0).Sum();
            long seedsDone = 0;
            long seedCheckAmount = 200000000;
            var seedsNextCheck = seedCheckAmount;

            var lowestLocation = long.MaxValue;
            for (var i = 0; i < seeds.Length; i += 2)
            {
                var seed = seeds[i];
                var range = seeds[i + 1];

                Console.WriteLine($"Calculating seed location: {seed} for {range} range...");

                for (long j = 0; j < range; j++)
                {
                    long locationNumber;

                    if (j + checkChunkSize < range)
                    {
                        var firstLocationNumber = GetSeedLocationNumber(seed + j, maps);
                        var lastLocationNumber = GetSeedLocationNumber(seed + j + checkChunkSize, maps);

                        if (firstLocationNumber == lastLocationNumber - checkChunkSize)
                        {
                            j += checkChunkSize;
                            locationNumber = Math.Min(firstLocationNumber, lastLocationNumber);

                            seedsDone += checkChunkSize;
                        }
                        else
                        {
                            locationNumber = Math.Min(firstLocationNumber, lastLocationNumber);
                            seedsDone++;
                        }
                    }
                    else
                    {
                        locationNumber = GetSeedLocationNumber(seed + j, maps);

                        seedsDone++;
                    }


                    if (locationNumber < lowestLocation)
                    {
                        lowestLocation = locationNumber;
                    }


                    if (seedsDone > seedsNextCheck)
                    {
                        seedsNextCheck += seedCheckAmount;
                        Console.WriteLine($"Seeds completed: {seedsDone}/{totalSeeds} complete at {timer.Elapsed}...");
                    }
                }
            }

            Console.WriteLine($"Seeds completed: {seedsDone}/{totalSeeds}");

            Console.WriteLine($"Solution answer: {lowestLocation}");

            timer.Stop();
            Console.WriteLine($"Total time taken: {timer.Elapsed}");
        }

        private static long GetSeedLocationNumber(long seed, List<AlmanacMap> maps)
        {
            foreach (var map in maps)
            {
                var range = map.Ranges
                    .FirstOrDefault(r => seed.IsBetween(r.SourceStart, r.SourceStart + r.Range));

                if (range != null)
                {
                    seed = range.DestinationStart + seed - range.SourceStart;
                }
            }

            return seed;
        }

        private static List<AlmanacMap> CompileMaps(List<string> input)
        {
            var maps = new List<AlmanacMap>();
            var sourceAndDestRegex = new Regex(@"(.*)-to-(.*?)\s");

            foreach (var line in input.Where(line => !string.IsNullOrWhiteSpace(line)))
            {
                if (!char.IsDigit(line[0])) // Parse x -to- y map header.
                {
                    var sourceAndDest = sourceAndDestRegex.Matches(line)[0].Groups;
                    maps.Add(new AlmanacMap(sourceAndDest[1].Value, sourceAndDest[2].Value));
                }
                else // Add ranges to latest map
                {
                    var newMapRangeData = line
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Select(long.Parse)
                        .ToArray();

                    maps.Last().Ranges.Add(
                        new AlmanacRange(
                            newMapRangeData[0],
                            newMapRangeData[1],
                            newMapRangeData[2]));
                }
            }

            return maps;
        }
    }
}