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

            var lowestLocation = long.MaxValue;
            foreach (var seed in seeds)
            {
                var location = seed;
                foreach (var map in maps)
                {
                    var range = map.Ranges
                        .FirstOrDefault(r => location.IsBetween(r.SourceStart, r.SourceStart + r.Range));

                    if (range != null)
                    {
                        location = range.DestinationStart + location - range.SourceStart;
                    }
                }

                if (location < lowestLocation)
                {
                    lowestLocation = location;
                }
            }
            
            Console.WriteLine($"Solution answer: {lowestLocation}");
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