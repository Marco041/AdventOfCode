public class Day9
{
    public static void Execute(string inputPath)
    {
        var input = File.ReadAllLines(inputPath);

        var result1 = GetResult(input, Part1);
        Console.WriteLine($"Day 9 - Part 1: {result1}");

        var result2 = GetResult(input, Part2);
        Console.WriteLine($"Day 9 - Part 2: {result2}");
    }

    public static int GetResult(string[] input, Func<List<int>, int, int> getResult)
    {
        var result = 0;
        foreach(var item in input)
        {
            var numbers = item.Split(' ').Select(int.Parse).ToList();
            var history = new List<List<int>> { numbers };
            var allZero = false;

            while(!allZero){
                allZero = true;
                var currentSequence = history.Last();
                history.Add([]);

                for(int i = 1; i < currentSequence.Count; i++)
                {
                    var newValue = currentSequence[i] - currentSequence[i-1];
                    history.Last().Add(newValue);
                    if(newValue != 0)
                    {
                        allZero = false;
                    }
                }
            } 
            
            history.Reverse();
            var nextHistoryValue = 0;
            history.ForEach(i => nextHistoryValue = getResult(i, nextHistoryValue));
            result += nextHistoryValue;
       }
       return result;
    }

    private static int Part1(List<int> historyItem, int nextHistoryValue) 
        => historyItem.Last() + nextHistoryValue;

    private static int Part2(List<int> historyItem, int nextHistoryValue) 
        => historyItem.First() - nextHistoryValue;

}