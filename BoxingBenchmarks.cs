using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

[MemoryDiagnoser]
public class BoxingBenchmarks
{
    private int _number = 42;

    [Benchmark(Baseline = true)]
    public int WithoutBoxing()
    {
        return _number;
    }

    [Benchmark]
    public object WithBoxing()
    {
        return _number;
    }
}

