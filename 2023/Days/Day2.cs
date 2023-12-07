using System.Text.RegularExpressions;

public class Day2
{
    public static void Execute(string inputPath)
    {
        var lines = File.ReadAllLines(inputPath);

        var part1 = Part1(lines);
        Console.WriteLine($"Day 2 - Part 1: {part1}");

        var part2 = Part2(lines);
        Console.WriteLine($"Day 2 - Part 2: {part2}");
    }

    private static int Part1(string[] lines)
    {
        int result = 0;
        foreach(var line in lines)
        {
            var colorLimit = new Dictionary<string, int>()
                { { "red", 12 }, { "green", 13 }, { "blue", 14 } };

            var inLimit = true;
            foreach(var color in colorLimit.Keys)
            {
                var regexMatchResult = MatchCubeRegexInGame(color).Matches(line);
                if(regexMatchResult.Any(m => ParseCubeNumber(m) > colorLimit[color]))
                {
                    inLimit = false;
                }
            }
            result += inLimit? int.Parse(line.Split(' ')[1].Replace(":", "")) : 0;
        }
        return result;
    }

    private static int Part2(string[] lines)
    {
        int result = 0;
        foreach(var line in lines)
        {
            string[] colors = ["red", "green", "blue"];
            var gameResult = 1;
            foreach(var color in colors)
            {
                var regexMatchResult = MatchCubeRegexInGame(color).Matches(line);
                var maxCubeNumber = regexMatchResult.MaxBy(ParseCubeNumber);
                gameResult *= ParseCubeNumber(maxCubeNumber);
            }
            result += gameResult;
        }
        
        return result;
    }

    private static Regex MatchCubeRegexInGame(string color){
        return new Regex($"(0|[1-9][0-9]*) ({color})");
    }

    private static int ParseCubeNumber(Match? cubeColor){
        return int.Parse(cubeColor.Value.Split(' ')[0]);
    }
}