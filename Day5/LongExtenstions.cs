namespace Day5;

public static class LongExtenstions
{
    public static bool IsBetween(this long num, long start, long end)
    {
        return num >= start && num < end;
    }
}