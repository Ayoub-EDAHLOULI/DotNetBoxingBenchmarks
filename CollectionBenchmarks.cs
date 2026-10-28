using System.Collections;
using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
public class CollectionBenchmarks
{
    [Params(100, 1_000, 10_000)]
    public int Count { get; set; }

    [Benchmark(Baseline = true)]
    public ArrayList NonGenericCollection()
    {
        var list = new ArrayList(Count);

        for (int i = 0; i < Count; i++)
        {
            list.Add(i);
        }

        return list;
    }

    [Benchmark]
    public List<int> GenericCollection()
    {
        var list = new List<int>(Count);

        for (int i = 0; i < Count; i++)
        {
            list.Add(i);
        }

        return list;
    }
}