int GetDigit(string i)
{
    string[] words = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

    var lowest = int.MaxValue;
    var lowNum = int.MaxValue;
    for (var j = 0; j < words.Length; j++)
    {
        if (i.Contains(words[j]) && i.IndexOf(words[j]) < lowest)
        {
            lowest = i.IndexOf(words[j]);
            lowNum = j + 1;
        }
    }
    
    var highest = int.MinValue;
    var highNum = int.MinValue;
    for (var j = 0; j < words.Length; j++)
    {
        if (i.Contains(words[j]) && i.LastIndexOf(words[j]) > highest)
        {
            highest = i.LastIndexOf(words[j]);
            highNum = j + 1;
        }
    }

    if (!i.Any(c => char.IsDigit(c)))
    {
        Console.WriteLine($"{i} : {lowNum}{highNum} No Digits");
        return int.Parse($"{lowNum}{highNum}");
    }
    
    var first = 
        lowest < i.ToList().FindIndex(c => char.IsDigit(c)) ?
            lowNum : int.Parse(i.First(char.IsDigit).ToString());
    
    var second = 
        highest >= i.ToList().FindLastIndex(c => char.IsDigit(c)) ?
            highNum : int.Parse(i.Last(char.IsDigit).ToString());
    
    Console.WriteLine($"{i} : {first}{second}");
    return int.Parse($"{first}{second}");
}

Console.WriteLine(
    File.ReadAllLines("Day1Input.txt")
        .Select(GetDigit)
        .Sum()
);