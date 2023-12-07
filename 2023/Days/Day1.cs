public class Day1
{
    public static void Execute(string inputPath)
    {
        var lines = File.ReadAllLines(inputPath);
        
        var part1 = Part1(lines);
        Console.WriteLine($"Day 1 - Part 1: {part1}");

        var part2 = Part2(lines);
        Console.WriteLine($"Day 1 - Part 2: {part2}");
    }

    private static int Part1(string[] input)
    {
        int total = 0;

        foreach (var line in input)
        {
            char first_digit = ' ';
            char last_digit = ' ';
            for (int i = 0; i < line.Length; i++)
            {
                if (char.IsDigit(line[i]))
                {
                    if (first_digit == ' ')
                    {
                        first_digit = line[i];
                    }
                    last_digit = line[i];
                }
            }

            total += int.Parse(first_digit.ToString() + last_digit.ToString());
        }

        return total;
    }

    static Dictionary<string, int> number_word = new Dictionary<string, int> 
    { {"one", 1 }, {"two", 2 }, {"three", 3 }, {"four", 4 }, {"five", 5 }, {"six", 6 }, {"seven", 7 }, { "eight", 8 }, {"nine", 9 }, 
        {"1", 1 }, {"2", 2 }, {"3", 3 }, {"4", 4 }, {"5", 5 }, {"6", 6 }, {"7", 7 }, {"8", 8 }, {"9", 9 }};


    private static int Part2(string[] input)
    {
        int total = 0;
        foreach (var line in input)
        {
            int first_digit = 0;
            int last_digit = 0;

            var minNumIndex = int.MaxValue;
            var maxNumIndex = -1;
            foreach (var item in number_word)
            {
                var firstIndex = line.IndexOf(item.Key);
                if (firstIndex != -1)
                {
                    if (firstIndex < minNumIndex)
                    {
                        first_digit = number_word[item.Key];
                        minNumIndex = firstIndex;
                    }
                }

                var lastIndex = line.LastIndexOf(item.Key);
                if (lastIndex != -1)
                {
                    if (lastIndex > maxNumIndex)
                    {
                        last_digit = number_word[item.Key];
                        maxNumIndex = lastIndex;
                    }
                }
            }

            total += int.Parse(first_digit.ToString() + last_digit.ToString());
        }

        return total;
    }
}