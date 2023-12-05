new Day5.Day5().Run();

public class AlmanacMap(string source, string destination)
{
    public string Source = source;
    public string Destination = destination;
    public List<AlmanacRange> Ranges = new();
}

public class AlmanacRange(long destinationStart, long sourceStart, long range)
{
    public long DestinationStart = destinationStart;
    public long SourceStart = sourceStart;
    public long Range = range;
}


