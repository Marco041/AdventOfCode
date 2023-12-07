using static System.Linq.Enumerable;

public class Day4
{
    public static void Execute(string inputPath)
    {
        var input = File.ReadAllLines(inputPath);

        int total1 = 0, total2 = 0, gameNumber = 0;
        var copiedScratchcards = new int[input.Length];

        foreach (var line in input)
        {
            var numbers = line.Split(": ")[1].Split(" | ");
            var winningNumbers = numbers[0].Split(' ').Where(w => w != string.Empty);
            var currentNumber = numbers[1].Split(' ').Where(w => w != string.Empty);
            var matchedNumbers = winningNumbers.Where(w => currentNumber.Contains(w));
            var matchCount = matchedNumbers.Count();

            // part 1
            int cardResult = matchCount > 0 ? 1 : 0;
            foreach (var index in Range(0, matchCount > 0 ? matchCount - 1 : 0))
            {
                cardResult *= 2;
            }

            total1 += cardResult;

            // part 2
            total2 += 1 + copiedScratchcards[gameNumber];
            foreach (var index in Range(0, matchCount))
            {
                copiedScratchcards[gameNumber + 1 + index] +=
                    copiedScratchcards[gameNumber] + 1;
            }
            gameNumber++;
        }

        Console.WriteLine($"Day4 - Part1: {total1}");
        Console.WriteLine($"Day4 - Part2: {total2}");
    }
}