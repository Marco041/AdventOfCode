using static System.Linq.Enumerable;
public class Day6
{
    public static void Execute(string inputPath)
    {
        var input = File.ReadAllLines(inputPath);

        Func<int, long[]> parseInput = index =>
            input[index]
                .Split(" ")
                .Where(w => !string.IsNullOrWhiteSpace(w)
                    && char.IsDigit(w.Last()))
                .Select(s => long.Parse(s.Trim()))
                .ToArray();

        //Part 1
        var time = parseInput(0);
        var distance = parseInput(1);
        var part1Result = ResolveEquation(time, distance);
        Console.WriteLine($"Day6 - Part1: {part1Result}");

        //Part 2
        Func<long[], long> getPart2Input = inputList =>
            long.Parse(string.Join("", inputList.Select(s => s.ToString())));

        var part2Time = getPart2Input(time);
        var part2Distance = getPart2Input(distance);
        var part2Result = ResolveEquation([part2Time], [part2Distance]);
        Console.WriteLine($"Day6 - Part2: {part2Result}");
    }

    private static int ResolveEquation(long[] time, long[] distance)
    {
        var result = 1;
        //t_hold + t_drive = t_tot && t_hold * t_drive > t_record
        for (int i = 0; i < time.Length; i++)
        {
            var delta = Math.Sqrt(time[i] * time[i] - 4 * distance[i]);
            var x1 = (int)Math.Ceiling((time[i] + delta) / 2);
            var x2 = (int)(Math.Floor(time[i] - delta) / 2);
            result *= (x1 - x2 - 1);
        }

        return result;
    }
}