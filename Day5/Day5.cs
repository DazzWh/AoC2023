using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Day5
{
    internal class Day5
    {
        public void Run()
        {
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

            checkChunkSize /= 6; // If this is too high, then it's often out of range and actually goes slower.

            var lowestLocation = long.MaxValue;
            for (var i = 0; i < seeds.Length; i += 2)
            {
                var seed = seeds[i];
                var range = seeds[i + 1];

                for (long j = 0; j < range; j++)
                {
                    long locationNumber;
                    
                    // Check a whole chunk if we can.
                    if (j + checkChunkSize < range)
                    {
                        var firstLocationNumber = GetSeedLocationNumber(seed + j, maps);
                        var lastLocationNumber = GetSeedLocationNumber(seed + j + checkChunkSize, maps);

                        // If the start of the chunk, and the end of the chunk
                        // Have the "same" value
                        // (follows the same route and ends up being lastLocationNumber - checkChunkSize)
                        if (firstLocationNumber == lastLocationNumber - checkChunkSize)
                        {
                            // Then we don't need to check anything in between these values, 
                            // and can skip j up a whole chunk size.
                            j += checkChunkSize;
                            locationNumber = Math.Min(firstLocationNumber, lastLocationNumber);
                        }
                        else
                        {
                            // If they are not the same though, we need to not move up, and check incrementally again.
                            locationNumber = Math.Min(firstLocationNumber, lastLocationNumber);
                        }
                    }
                    else
                    {
                        // And if we're near the end of the range, check incrementally.
                        locationNumber = GetSeedLocationNumber(seed + j, maps);
                    }


                    if (locationNumber < lowestLocation)
                    {
                        lowestLocation = locationNumber;
                    }
                }
            }

            Console.WriteLine($"Solution answer: {lowestLocation}");
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