public class Day7
{
    public static void Execute(string inputPath)
    {
        var input = File.ReadAllLines(inputPath);

        var result1 = GetScore(input);
        Console.WriteLine($"Day 7 - Part 1: {result1}");

        var result2 = GetScore(input, true);
        Console.WriteLine($"Day 7 - Part 2: {result2}");
    }

    static Dictionary<char, int> cardPoints = new Dictionary<char, int>()
    { 
        { 'A', 0 }, { 'K', 1 },{ 'Q', 2 },{ 'J', 3 },{ 'T', 4 },
        { '9', 5 },{ '8', 6 },{ '7', 7 },{ '6', 8 },{ '5', 9 }, { '4', 10 }, { '3', 11 }, { '2', 12 }
    };

    public static int GetScore(string[] input, bool withJolly = false)
    {
        if(withJolly)
        {
            cardPoints['J'] = cardPoints['2'] + 1;
        }

        var sortedInput = new List<string>(input);
        sortedInput.Sort((h1, h2) => SortByScore(h1, h2, withJolly));
        int cnt = input.Length;
        var part1Result = 0;
        sortedInput.ForEach(r => part1Result += int.Parse(r.Split(' ')[1]) * cnt--);
        return part1Result;
    }

    private static int SortByScore(string item1, string item2, bool withJolly = false)
    {
        var hand1 = item1.Split(' ')[0];
        var hand2 = item2.Split(' ')[0];
        var h1Type = GetHandType(hand1, withJolly);
        var h2Type = GetHandType(hand2, withJolly);

        if(h1Type < h2Type)
        {
            return -1;
        }

        if(h1Type > h2Type)
        {
            return 1;
        }

        for(int i = 0; i < hand1.Length; i++)
        {
            if(cardPoints[hand1[i]] < cardPoints[hand2[i]])
            {
                return -1;
            }

            if(cardPoints[hand1[i]] > cardPoints[hand2[i]])
            {
                return 1;
            }
        }

        return 0;
    }

    private static int GetHandType(string hand, bool withJolly = false)
    {
        if(withJolly && hand.Contains('J') && hand != "JJJJJ")
        {
            hand = hand.Replace("J", "");
            var charToUse = hand
                .GroupBy(x => x)
                .OrderByDescending(x => x.Count())
                .First()
                .Key;
            hand = hand.PadLeft(5, charToUse);
        }

        var d = hand
            .GroupBy(x => x)
            .Select(y => new { Word = y.Key, Count = y.Count() })
            .OrderByDescending(o => o.Count)
            .ToList();

        if(d.Count == 1)
        {
            return 0;
        }

        if(d.Count == 2 && d[0].Count == 4)
        {
            return 1;
        }

         if(d.Count == 2 && d[0].Count == 3)
        {
            return 2;
        }

        if(d.Count == 3 && d[0].Count == 3)
        {
            return 3;
        }

        if(d.Count == 3 && d[0].Count == 2 && d[1].Count == 2)
        {
            return 4;
        }

        if(d.Count == 4 && d[0].Count == 2)
        {
            return 5;
        }

        if(d.Count == 5)
        {
            return 6;
        }

        throw new Exception();
    }
}