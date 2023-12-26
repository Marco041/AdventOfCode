using System.Data;

public class Day17
{
    public static void Execute(string inputPath)
    {
        var input = File.ReadAllLines(inputPath);

       var matrix = input
            .Select(s => s
                .Select(c => int.Parse(c.ToString()))
                .ToList())
            .ToList();
        
        Console.WriteLine($"Day 17 - Part 1: {FindPath(matrix, GetNewNodesPart1)}");
        Console.WriteLine($"Day 17 - Part 2: {FindPath(matrix, GetNewNodesPart2)}");

    }

    private static int FindPath(List<List<int>> matrix, Func<Node, List<Node>> getNextNodes)
    {
        PriorityQueue<Node, int> nodeQueue = new PriorityQueue<Node, int>();
        HashSet<Node> visitedNodes = new HashSet<Node>();

        var startNode1 = new Node(0, 0, Direction.S, 0);
        var startNode2 = new Node(0, 0, Direction.E, 0);

        Dictionary<Node, int> distances = new Dictionary<Node, int>()
        {
            {startNode1, 0}, {startNode2, 0}
        };

        nodeQueue.Enqueue(startNode1, 0);
        nodeQueue.Enqueue(startNode2, 0);

        var result = 0;

        while(true)
        {
            var current = nodeQueue.Dequeue();

            visitedNodes.Add(current);

            foreach(var node in getNextNodes(current))
            {
                if(!visitedNodes.Contains(node) 
                    && node.X >= 0 && node.X < matrix[0].Count
                    && node.Y >= 0 && node.Y < matrix.Count)
                {
                    var newDistance = distances[current] + matrix[node.Y][node.X];

                    if(!distances.ContainsKey(node))
                    {
                        distances.Add(node, newDistance);
                        nodeQueue.Enqueue(node, newDistance);
                    }
                    else if(distances[node] > newDistance)
                    {
                        distances[node] = newDistance;
                        nodeQueue.Enqueue(node, newDistance);
                    }  
                }
            }

            if(current.X == matrix[0].Count - 1 
                && current.Y == matrix.Count - 1)
            {
                result = distances[current];
                break;
            }
        }

        return result;
    }

    private static List<Node> GetNewNodesPart1(Node current)
    {
        var result = new List<Node>();
        
        if(current.StepCount < 2)
        {
            result.Add(GetNewNode(current, current.Direction, current.StepCount + 1));
        }

        if(current.Direction == Direction.N || current.Direction == Direction.S)
        {
            result.Add(GetNewNode(current, Direction.W, 0));
            result.Add(GetNewNode(current, Direction.E, 0));
        }

        if(current.Direction == Direction.E || current.Direction == Direction.W)
        {
            result.Add(GetNewNode(current, Direction.N, 0));
            result.Add(GetNewNode(current, Direction.S, 0));
        }

        return result;
    }


    private static List<Node> GetNewNodesPart2(Node current)
    {
        var result = new List<Node>();
        
        if(current.StepCount < 9)
        {
            result.Add(GetNewNode(current, current.Direction, current.StepCount + 1));
        }

        if(current.StepCount > 2){
            if(current.Direction == Direction.N || current.Direction == Direction.S)
            {
                result.Add(GetNewNode(current, Direction.W, 0));
                result.Add(GetNewNode(current, Direction.E, 0));
            }

            if(current.Direction == Direction.E || current.Direction == Direction.W)
            {
                result.Add(GetNewNode(current, Direction.N, 0));
                result.Add(GetNewNode(current, Direction.S, 0));
            }
        }
        return result;
    }

    private static Node GetNewNode(Node current, Direction direction, int step)
    {
        var cordinate = CalculateNewCoordinate(current.X, current.Y, direction);
        return new Node(cordinate.Item1, cordinate.Item2, direction, step);
    }

    private static (int, int) CalculateNewCoordinate(int x, int y, Direction dir)
    {
        switch(dir)
        {
            case Direction.N: return (x, y - 1);
            case Direction.S: return (x, y + 1);
            case Direction.W: return (x - 1, y);
            case Direction.E: return (x + 1, y);
        }

        throw new Exception();
    }


    public struct Node
    {    
        public Node(int x, int y, Direction direction, int stepCount)
        {
            X = x;
            Y = y;
            Direction = direction;
            StepCount = stepCount;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public Direction Direction { get; set; }
        public int StepCount { get; set; }
        public override int GetHashCode() => HashCode.Combine(X, Y, Direction, StepCount);
        public override bool Equals(object? obj) => obj is Node o && Equals(o);
            public bool Equals(Node other) => 
            X == other.X 
            && Y == other.Y 
            && other.Direction == Direction 
            && StepCount == other.StepCount;
    }

    public enum Direction
    {
        N,
        S,
        W,
        E
    }
}