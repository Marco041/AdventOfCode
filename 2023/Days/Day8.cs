public class Day8
{
    public static void Execute(string inputPath)
    {
        var input = File.ReadAllLines(inputPath);

        var instruction = input[0];
        Dictionary<string, (string, string)> nodes = new();
        foreach(var item in input.Skip(2))
        {
            var parsedLine = item.Split(" = ");
            var newCoordinate = parsedLine[1].Split(", ");
            nodes.Add(
                parsedLine[0], 
                (newCoordinate[0].Replace("(", ""), 
                    newCoordinate[1].Replace(")", "")));
        }

        var result1 = Part1(nodes, instruction);
        Console.WriteLine($"Day 8 - Part 1: {result1}");

        var result2 = Part2(nodes, instruction);
        Console.WriteLine($"Day 8 - Part 2: {result2}");
    }


    public static int Part1(Dictionary<string, (string, string)> nodes, string instruction)
    {
        int step = 0;
        int currentInstruction = 0;
        string currentNode = "AAA";

        while(currentNode != "ZZZ")
        {
            if(instruction[currentInstruction] == 'R'){
                currentNode = nodes[currentNode].Item2;
            }
            else{
                currentNode = nodes[currentNode].Item1;
            }

            currentInstruction++;

            if(currentInstruction >= instruction.Length){
                currentInstruction = 0;
            }

            step++;
        }

        return step;
    }

    public static long Part2(Dictionary<string, (string, string)> nodes, string instruction)
    {
        int currentInstruction = 0;
        var currentNodes = nodes
            .Where(w => w.Key.EndsWith("A"))
            .Select(s => s.Key)
            .ToList();

        List<int> steps = new List<int>();
        currentNodes.ForEach(f => steps.Add(0));

        foreach(var i in Enumerable.Range(0, currentNodes.Count()))
        {
            while(true)
            {
                if(currentNodes[i].EndsWith("Z"))
                {
                    break;
                }

                if(instruction[currentInstruction] == 'R'){
                    currentNodes[i] = nodes[currentNodes[i]].Item2;
                }
                else{
                    currentNodes[i] = nodes[currentNodes[i]].Item1;
                }
                

                currentInstruction++;
                if(currentInstruction >= instruction.Length){
                    currentInstruction = 0;
                }

                steps[i]++;
            }
        }

        return lcm(steps);
    }

    public static long lcm(List<int> inputArray)
    {
        long lcArray = 1;
        int divisor = 2;
         
        while (true) {
             
            int counter = 0;
            bool divisible = false;
            for (int i = 0; i < inputArray.Count; i++) 
            {
                if (inputArray[i] == 0) {
                    return 0;
                }
                else if (inputArray[i] < 0) {
                    inputArray[i] = inputArray[i] * (-1);
                }
                if (inputArray[i] == 1) {
                    counter++;
                }
 
                if (inputArray[i] % divisor == 0) {
                    divisible = true;
                    inputArray[i] = inputArray[i] / divisor;
                }
            }
 
            if (divisible) {
                lcArray = lcArray * divisor;
            }
            else {
                divisor++;
            }

            if (counter == inputArray.Count) {
                return lcArray;
            }
        }
    }
}