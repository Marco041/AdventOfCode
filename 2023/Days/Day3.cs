using System.Text.RegularExpressions;

public class Day3
{
    public static void Execute(string inputPath)
    {
        var input = File.ReadAllText(inputPath);

        var result = GetResult(input);
        Console.WriteLine($"Day 3 - Part 1: {result.Item1}");
        Console.WriteLine($"Day 3 - Part 2: {result.Item2}");
    }

    public static (int, int) GetResult(string input)
    {
        int lineSize = 140;
        var resultPart1 = 0;
        var resultPart2 = 0;
        var gearIndexMatch = new Dictionary<int, List<int>>();

        input = input.Replace(Environment.NewLine, "");
        var symbolMatch = new Regex("[0-9]+").Matches(input);

        foreach(Match match in symbolMatch)
        {
            var currentIndexStart = match.Index;
            var currentIndexEnd = match.Index + match.Length - 1;
            var itemW = currentIndexStart - 1;
            var itemE = currentIndexEnd + 1;
            var itemNW = currentIndexStart - lineSize - 1 ;
            var itemNE = currentIndexEnd - lineSize + 1;
            var itemSW = currentIndexStart + lineSize - 1;
            var itemSE = currentIndexEnd + lineSize + 1;

            var currentLineMatchResult = SymbolMatchBetween(itemW, itemE, input);
            var northLineMatchResult = SymbolMatchBetween(itemNW, itemNE, input);
            var southLineMatchResult = SymbolMatchBetween(itemSW, itemSE, input);

            if(currentLineMatchResult != null
                || northLineMatchResult != null
                || southLineMatchResult != null)
            {
                var number = int.Parse(match.Value);
                resultPart1 += number;

                AddGearNumberMatched(currentLineMatchResult, gearIndexMatch, number);
                AddGearNumberMatched(northLineMatchResult, gearIndexMatch, number);
                AddGearNumberMatched(southLineMatchResult, gearIndexMatch, number);
            }
        }

        resultPart2 = gearIndexMatch.Sum(i => 
            i.Value.Count == 2 
            ? i.Value[0]*i.Value[1] 
            : 0);

        return (resultPart1, resultPart2);
    }

    private static CheckSymbolResult? SymbolMatchBetween(int indexStart, int indexEnd, string input)
    {
        if(indexStart <= 0 || indexStart > input.Length 
            || indexEnd <= 0 || indexEnd > input.Length)
        {
            return null;
        }

        for(int i = indexStart; i <= indexEnd; i++)
        {
            var result = SymbolMatch(i, input);
            if(result != null)
            {
                return result;
            }
        }

        return null;
    }

    private static CheckSymbolResult? SymbolMatch(int index, string input)
    {
        if(input[index] != '.' && !char.IsDigit(input[index]))
        {
            return new CheckSymbolResult()
            {
                IsGear = input[index] == '*',
                GearIndex = index
            };
        }
        
        return null;
    }

    private static void AddGearNumberMatched(
        CheckSymbolResult? lineMatchResult, 
        Dictionary<int, List<int>> gearDict,
        int number)
    {
        if(lineMatchResult != null 
            && lineMatchResult.IsGear)
        {
            var gearIndex = lineMatchResult.GearIndex;
            if(gearDict.ContainsKey(gearIndex))
            {
                gearDict[gearIndex].Add(number);
            }
            else
            {
                gearDict[gearIndex] = [number];
            }
        }
    }
}

class CheckSymbolResult()
{
    public bool IsGear { get; set; }
    public int GearIndex { get; set; }
}