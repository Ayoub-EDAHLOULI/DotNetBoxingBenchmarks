using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

[MemoryDiagnoser]
[DisassemblyDiagnoser(
    maxDepth: 2,
    printSource: true,
    printInstructionAddresses: false)]
public class JitOptimizationBenchmarks
{
    private int _value = 42;

    [Benchmark]
    public int DirectReturn()
    {
        return _value;
    }

    [Benchmark]
    public int BoxAndUnbox()
    {
        object boxed = _value;

        return (int)boxed;
    }

    [Benchmark]
    public object EscapingBox()
    {
        return _value;
    }
}