public class Day11
{
    public static void Execute(string inputPath)
    {
        var input = File.ReadAllLines(inputPath);

        var rowsToAdd = Enumerable.Range(0, input.Length)
            .Where(row => !input[row].Contains('#'))
            .ToArray();

        var colsToAdd = Enumerable.Range(0, input[0].Length)
            .Where(col => input.All(l => l[col] == '.'))
            .ToArray();

        var galaxies = input
            .Select(
                (line, row) => line.ToCharArray()
                    .Select((c, col) => (c, (row, col)))
                    .Where(i => i.c != '.')
                    .Select(i => i.Item2)
            )
            .SelectMany((a,b) => a)
            .ToList();
        
        var result1 = CalculateDistance(galaxies, rowsToAdd, colsToAdd, 2);
        Console.WriteLine($"Day 11 - Part 1: {result1}");

        var result2 = CalculateDistance(galaxies, rowsToAdd, colsToAdd, 1_000_000);
        Console.WriteLine($"Day 11 - Part 2: {result2}");
    }

    private static long CalculateDistance(List<(int, int)> galaxies, int[] rowsToAdd, int[] colsToAdd, int expansion)
    {
        long distance = 0;
        for(int i = 0; i < galaxies.Count; i++)
        {
            for(int j = i + 1; j < galaxies.Count; j++)
            {
                distance += Distance(galaxies[i], galaxies[j]);
                var rowCorrection = rowsToAdd.Where(w =>
                    (galaxies[i].Item1 > w && galaxies[j].Item1 < w)
                    || (galaxies[i].Item1 < w && galaxies[j].Item1 > w));

                var colCorrection = colsToAdd.Where(w =>
                    (galaxies[i].Item2 > w && galaxies[j].Item2 < w)
                    || (galaxies[i].Item2 < w && galaxies[j].Item2 > w));

                distance += rowCorrection.Count() * expansion - rowCorrection.Count() 
                    + colCorrection.Count() * expansion - colCorrection.Count();
            }
        }
        return distance;
    }

    public static long Distance((int, int) p1, (int, int) p2) =>
        Math.Abs((long)p2.Item1 - p1.Item1) +
        Math.Abs((long)p2.Item2 - p1.Item2);
}