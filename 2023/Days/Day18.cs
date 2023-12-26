using System.Globalization;

public static class Day18
{
    record class Point(long x, long y);
    record class Bounds(Point min, Point max);

    public static void Execute(string inputPath)
    {
        var input = File.ReadAllLines(inputPath);

        var position = new Point(0, 0);
        var vertices = new List<Point>();
        long borderLength = 0;
        foreach (var line in input)
        {
            var instruction = Instruction.ParseInput1(line);
            var newPosition = NewCoordinate(position, instruction);
            borderLength += instruction.Distance;
            vertices.Add(newPosition);
            position = newPosition;
        }
        
        var result1 = ShoelaceArea(vertices) + borderLength / 2 + 1;
        Console.WriteLine($"Day 18 - Part 1: {result1}");

        vertices = new List<Point>();
        borderLength = 0;
        foreach (var line in input)
        {
            var instruction = Instruction.ParseInput2(line);
            var newPosition = NewCoordinate(position, instruction);
            borderLength += instruction.Distance;
            vertices.Add(newPosition);
            position = newPosition;
        }
       var result2 = ShoelaceArea(vertices) + borderLength / 2 + 1;
        Console.WriteLine($"Day 18 - Part 2: {result2}");
    }

     private static Point NewCoordinate(Point point, Instruction instruction)
    {
        switch(instruction.Direction)
        {
            case 'U': return new Point(point.x, point.y - instruction.Distance);
            case 'D': return new Point(point.x, point.y + instruction.Distance);
            case 'L': return new Point(point.x - instruction.Distance, point.y);
            case 'R': return new Point(point.x + instruction.Distance, point.y);
        }

        throw new Exception();
    }

    private record Instruction(char Direction, long Distance)
    {
        public static Instruction ParseInput1(string str)
        {
            var parts = str.Split(' ');
            var direction = parts[0][0];
            var distance = int.Parse(parts[1]);
            return new Instruction(direction, distance);
        }

        public static Instruction ParseInput2(string str)
        {
            var parts = str.Split(' ');
            var n = Int32.Parse(parts[2].Substring(2, 5), NumberStyles.HexNumber);
            var direction = parts[2].Substring(7, 1) switch
            {
                "0" => 'R',
                "1" => 'D',
                "2" => 'L',
                _ => 'U',
            };
            return new Instruction(direction, n);
        }
    }
    
    private static double ShoelaceArea(this List<Point> vertices)
    {
        long sum = 0;
        Enumerable.Range(0, vertices.Count).Sum(i => 
        { 
            if(i>=vertices.Count-1) return 0;
            var a = vertices[i];
            var b = vertices[(i + 1) % vertices.Count];
            sum += a.x*b.y - b.x*a.y;
            return 0;
        });

        return sum / 2;
    }
}