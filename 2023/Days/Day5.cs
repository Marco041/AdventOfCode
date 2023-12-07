using static System.Linq.Enumerable;

public class Day5
{
    public static void Execute(string inputPath)
    {
        var input = File.ReadAllLines(inputPath);

        var seeds = input[0].Replace("seeds: ", "").Split(' ').Select(long.Parse).ToList();

        var mappingResult1 = Part1(seeds, input);
        Console.WriteLine($"Day5 - Part1: {mappingResult1.Min()}");

        var mappingResult2 = Part2(seeds, input);
        Console.WriteLine($"Day5 - Part2: {mappingResult2.Min(m => m.MappingStart)}");
    }

    private static List<long> Part1(List<long> seeds, string[] input)
    {
        var mappingResult = new List<long>(seeds);
        foreach (var line in input)
        {
            if (!string.IsNullOrEmpty(line) && char.IsDigit(line[0]))
            {
                var cnt = 0;
                foreach (var number in seeds)
                {
                    var mapping = GetMapping(number, line);
                    if (mapping != null)
                    {
                        mappingResult[cnt] = mapping.Value;
                    }
                    cnt++;
                }
            }

            if (line.EndsWith("map:"))
            {
                seeds = new List<long>(mappingResult);
            }
        }
        return mappingResult;
    }

    private static long? GetMapping(long number, string mappingLine)
    {
        var mapping = mappingLine.Split(' ').Select(long.Parse).ToArray();
        if (number >= mapping[1] && number < mapping[1] + mapping[2])
        {
            return mapping[0] + Math.Abs(mapping[1] - number);
        }

        return null;
    }

    private static List<MappingResult> Part2(List<long> seeds, string[] input)
    {
        var result = new List<MappingResult>();
        var ranges = new List<MappingResult>();

        for (int i = 0; i < seeds.Count; i += 2)
        {
            ranges.Add(new MappingResult()
            {
                MappingStart = seeds[i],
                MappingEnd = seeds[i] + seeds[i + 1] - 1,
            });
        }

        foreach (var range in ranges)
        {
            var rangeToProcess = new List<MappingResult>() { range };
            var mappedRange = new List<MappingResult>();
            foreach (var line in input.Skip(3))
            {
                var noMappedRange = new List<MappingResult>();
                if (!string.IsNullOrEmpty(line) && char.IsDigit(line[0]))
                {
                    foreach (var item in rangeToProcess)
                    {
                        var mappingFound = CalculateMapping(item.MappingStart!.Value, item.MappingEnd!.Value, line);

                        foreach (var pair in mappingFound)
                        {
                            if (!pair.NoMatch)
                            {
                                mappedRange.Add(pair); 
                            }
                            else
                            {
                                noMappedRange.Add(pair);
                            }
                        }
                    }

                    rangeToProcess = new List<MappingResult>(noMappedRange);
                }

                if (line.EndsWith("map:"))
                {
                    rangeToProcess.AddRange(mappedRange);
                    mappedRange = new List<MappingResult>();
                }
            }
            result.AddRange(mappedRange);
        }

        return result;
    }

    private static List<MappingResult> CalculateMapping(long numberStart, long numberEnd, string mappingLine)
    {
        var mapping = mappingLine.Split(' ').Select(long.Parse).ToArray();
        var offset = mapping[0] - mapping[1];

        if (numberStart >= mapping[1] 
            && numberEnd <= mapping[1] + mapping[2]
            && numberStart < numberEnd)
        {
            return new List<MappingResult>()
            { 
                new MappingResult()
                {
                    MappingStart = numberStart + offset,
                    MappingEnd = numberEnd + offset,
                    NoMatch = false
                }
            };
        }

        if (numberStart >= mapping[1] 
            && numberStart < mapping[1] + mapping[2] 
            && numberEnd > mapping[1] + mapping[2])
        {
            return new List<MappingResult>()
            {  
                new MappingResult()
                {
                    MappingStart =  numberStart + offset,
                    MappingEnd = mapping[0] + mapping[2] - 1,
                    NoMatch = false,
                },
                new MappingResult()
                {
                    MappingStart = mapping[1] + mapping[2],
                    MappingEnd = numberEnd,
                    NoMatch = true
                }
            };
        }

        if (numberStart < mapping[1] 
            && numberEnd < mapping[1] + mapping[2]
            && numberEnd >= mapping[1])
        {
            return new List<MappingResult>()
            {
                new MappingResult()
                {
                    MappingStart = mapping[0],
                    MappingEnd = numberEnd + offset,
                    NoMatch = false
                },
                new MappingResult()
                {
                    MappingStart = numberStart,
                    MappingEnd = mapping[1],
                    NoMatch = true
                } 
            };
        }

        if (numberStart <= mapping[1] 
            && numberEnd > mapping[1] + mapping[2])
        {
            return new List<MappingResult>()
            {
                new MappingResult()
                {
                    MappingStart = mapping[0],
                    MappingEnd =   mapping[0] + mapping[2] - 1,
                    NoMatch = false
                },
                new MappingResult()
                {
                    MappingStart = mapping[1] + mapping[2],
                    MappingEnd = numberEnd,
                    NoMatch = true
                },
                new MappingResult()
                {
                    MappingStart = numberStart,
                    MappingEnd = mapping[1],
                    NoMatch = true,
                } 
            };
        }

        return new List<MappingResult>()
            {
                new MappingResult()
                {
                    MappingStart = numberStart,
                    MappingEnd = numberEnd,
                    NoMatch = true,
                }
            };
    }
}

class MappingResult()
{
    public long? MappingStart { get; set; }
    public long? MappingEnd { get; set; }
    public bool NoMatch { get; set; }
}
