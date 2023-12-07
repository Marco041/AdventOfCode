using System.Diagnostics;

var executeWithStopWatch = (Action f) => 
{
    Stopwatch sw = new Stopwatch();
    sw.Start();
    f();
    sw.Stop();
    Console.WriteLine($"Execution time: {sw.ElapsedMilliseconds} ms"+Environment.NewLine);
};

executeWithStopWatch(() => Day1.Execute("Input/Day1.txt"));
executeWithStopWatch(() => Day2.Execute("Input/Day2.txt"));
executeWithStopWatch(() => Day3.Execute("Input/Day3.txt"));
executeWithStopWatch(() => Day4.Execute("Input/Day4.txt"));
executeWithStopWatch(() => Day5.Execute("Input/Day5.txt"));
executeWithStopWatch(() => Day6.Execute("Input/Day6.txt"));
executeWithStopWatch(() => Day7.Execute("Input/Day7.txt"));