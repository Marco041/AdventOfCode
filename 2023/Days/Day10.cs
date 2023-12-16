public class Day10
{
    public static void Execute(string inputPath)
    {
        var input = File.ReadAllLines(inputPath);

        var result1 = Part1(input);
        Console.WriteLine($"Day 10 - Part 1: {result1}");

        var result2 = Part2(input);
        Console.WriteLine($"Day 10 - Part 2: {result2}");
    }

    private static (int, int) startPosition;

    public static int Part1(string[] input)
    {
        var positions = new List<(int, int)>();
        for(int i = 0; i < input.Length; i++)
        {
            var sIndex = input[i].IndexOf('S');
            if(sIndex >= 0)
            {
                startPosition = (i, sIndex);
                positions.Add((i, sIndex));
            }
        }

        int distance = 0;
        while(true)
        {
            var nextPositions = new List<(int, int)>();
            foreach(var pos in positions){
                if(!part1PathPositions.Contains(pos))
                {                
                    part1PathPositions.Add(pos);
                }
                nextPositions.AddRange(GetNewPosition(input, pos));
            }

            if(nextPositions.Count == 0)
            {
                break;
            }

            positions = nextPositions;
            distance++;
        }

        return distance;
    }

    private static int Part2(string[] input)
    {
        List<List<char>> expandedInput = ExpandMatrix(input);
        
        var start = startPosition;
        
        if(expandedInput[start.Item1 * 3 + 3][start.Item2 * 3 + 1] == '*')
            expandedInput[start.Item1 * 3 + 2][start.Item2 * 3 + 1] = '*';

        if(expandedInput[start.Item1 * 3 - 1][start.Item2 * 3 + 1] == '*')
            expandedInput[start.Item1 * 3][start.Item2 * 3 +1] = '*';

        if(expandedInput[start.Item1 * 3 + 1][start.Item2 * 3 + 3] == '*')
            expandedInput[start.Item1 * 3 +1][start.Item2 * 3 + 2] = '*';

        if(expandedInput[start.Item1 * 3 + 1][start.Item2 * 3 - 1] == '*')
            expandedInput[start.Item1 * 3 + 1][start.Item2 * 3] = '*';

        var outsideLoop = new HashSet<(int, int)>{ (0, 0) };

        while(outsideLoop.Count > 0)
        {
            var newHashSet = new HashSet<(int, int)>();
            foreach(var item in outsideLoop)
            {
                var i = item.Item1;
                var j = item.Item2;
                var north = (Math.Max(i - 1, 0), j);
                var east = (i, Math.Min(j + 1, expandedInput[0].Count - 1));
                var west = (i, Math.Max(j - 1, 0));
                var south = (Math.Min(i + 1, expandedInput.Count - 1), j);

                if(expandedInput[north.Item1][north.Item2] == '.')
                {
                    expandedInput[north.Item1][north.Item2] = 'O';
                        newHashSet.Add(north);
                }

                if(expandedInput[east.Item1][east.Item2] == '.')
                {
                    expandedInput[east.Item1][east.Item2] = 'O';
                        newHashSet.Add(east);
                }

                if(expandedInput[west.Item1][west.Item2] == '.')
                {
                    expandedInput[west.Item1][west.Item2] = 'O';
                        newHashSet.Add(west);
                }

                if(expandedInput[south.Item1][south.Item2] == '.')
                {
                    expandedInput[south.Item1][south.Item2] = 'O';
                        newHashSet.Add(south);
                }
            }
            outsideLoop = new HashSet<(int, int)>(newHashSet);
        }

        var result = 0;
         for(int i = 0; i < expandedInput.Count; i += 3)
        {
            for(int j = 0; j < expandedInput[0].Count; j += 3)
            {
                if(expandedInput[i][j] == '.'
                    && expandedInput[i+1][j] == '.'
                    && expandedInput[i][j+1] == '.'
                    && expandedInput[i+1][j+1] == '.'
                    && expandedInput[i+2][j+2] == '.'
                    && expandedInput[i+2][j] == '.'
                    && expandedInput[i][j+2] == '.'
                    && expandedInput[i+1][j+2] == '.'
                    && expandedInput[i+2][j+1] == '.')
                {
                    result++;
                }
            }
        }
        return result;
    }

    private static List<List<char>> ExpandMatrix(string[] input)
    {
         List<List<char>> expandedInput = new List<List<char>>();

        var horizontalMapping = new Dictionary<char, string> {
            { '|', ".*." },
            { '-', "***" },
            { 'L', ".**" },
            { 'J', "**." },
            { '7', "**." },
            { 'F', ".**" },
            { '.', "..." },
            { 'S', ".S." }
        };

        var verticalMapping = new Dictionary<char, string> {
            { '|', "***" },
            { '-', ".*." },
            { 'L', "**." },
            { 'J', "**." },
            { '7', ".**" },
            { 'F', ".**" },
            { '.', "..." },
            { 'S', ".S." }
        };

        for(int i = 0; i < input.Length * 3; i++)
        {
            expandedInput.Add(new List<char>());
            for(int j = 0; j < input[0].Length * 3; j++)
            {
                expandedInput[i].Add('.');
            }
        }

        for(int i = 0; i < input.Length; i++)
        {
            for(int j = 0; j < input[0].Length; j++)
            {
                var symbol = input[i][j];
                if(!part1PathPositions.Contains((i,j)))
                {
                    symbol = '.';
                }
                var newHorizontral = horizontalMapping[symbol];
                var newVertical = verticalMapping[symbol];
                for(int z = 0; z < 3; z++)
                {
                    expandedInput[i * 3 + 1][j * 3 + z] = newHorizontral[z];
                }
                for(int z = 0; z < 3; z++)
                {
                    expandedInput[i * 3 + z][j * 3 + 1] = newVertical[z];
                }
            }
        }

        return expandedInput;
    }

    static HashSet<(int, int)> part1PathPositions = [];

    private static List<(int, int)> GetNewPosition(string[] input, (int, int) position)
    {
        var result = new List<(int, int)>();
        var north = (Math.Max(position.Item1 - 1, 0), position.Item2);
        var east = (position.Item1, Math.Min(position.Item2 + 1, input[0].Length - 1));
        var west = (position.Item1, Math.Max(position.Item2 - 1, 0));
        var south = (Math.Min(position.Item1 + 1, input.Length - 1), position.Item2);

        var northPosition = input[north.Item1][north.Item2];
        var estPosition = input[east.Item1][east.Item2];
        var westPosition = input[west.Item1][west.Item2];
        var southPosition = input[south.Item1][south.Item2];

        if(input[position.Item1][position.Item2] == '|' 
            || input[position.Item1][position.Item2] == 'L' 
            || input[position.Item1][position.Item2] == 'J' 
            || input[position.Item1][position.Item2] == 'S')
        {
            if(northPosition == '|' 
                || northPosition == '7' 
                || northPosition == 'F' 
                || (true && input[position.Item1][position.Item2] != 'S'))
            {
                result.Add((north.Item1, north.Item2));
            }
        }

        if(input[position.Item1][position.Item2] == '-' 
            || input[position.Item1][position.Item2] == 'L'  
            || input[position.Item1][position.Item2] == 'F' 
            || input[position.Item1][position.Item2] == 'S')
        {
            if(estPosition == '-' 
                || estPosition == '7' 
                || estPosition == 'J' 
                || (true && input[position.Item1][position.Item2] != 'S'))
            {
                result.Add((east.Item1, east.Item2));
            }
        }

        if(input[position.Item1][position.Item2] == '-' 
            || input[position.Item1][position.Item2] == 'J'  
            || input[position.Item1][position.Item2] == '7' 
            || input[position.Item1][position.Item2] == 'S')
        {
            if(westPosition == '-' 
                || westPosition == 'L' 
                || westPosition == 'F' 
                || (true && input[position.Item1][position.Item2] != 'S'))
            {
                result.Add((west.Item1, west.Item2));
            }
        }

        if(input[position.Item1][position.Item2] == '|' 
            || input[position.Item1][position.Item2] == 'F' 
            || input[position.Item1][position.Item2] == '7' 
            || input[position.Item1][position.Item2] == 'S')
        {
            if(southPosition == '|' 
                || southPosition == 'J' 
                || southPosition == 'L' 
                || (true && input[position.Item1][position.Item2] != 'S'))
            {
                result.Add((south.Item1, south.Item2));
            }
        }

        result.RemoveAll(r => part1PathPositions.Contains(r));
        return result;  
    }
}