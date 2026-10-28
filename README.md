# DotNetBoxingBenchmarks

**Exploring boxing, memory allocations, collection performance, and RyuJIT optimizations in .NET 9 and .NET 10.**

This repository contains reproducible [BenchmarkDotNet](https://benchmarkdotnet.org/) experiments developed for the technical article **"Where Do Value Types Actually Live in C#? The Stack Myth Explained"**, part of the *C# & .NET, from the Inside* series by Ayoub Edahlouli.

The goal is to investigate how value types behave at runtime, measure the cost of boxing, compare generic and non-generic collections, and examine how RyuJIT can eliminate unnecessary allocations.

## Experiments

### 1. Boxing vs. No Boxing

`BoxingBenchmarks.cs`

Compares returning an `int` directly with returning it as an `object`, which requires boxing when the object escapes.

| Method | .NET 9 | .NET 10 | Allocated |
|---|---:|---:|---:|
| WithoutBoxing | 0.0029 ns | 0.0202 ns | 0 B |
| WithBoxing | 3.9804 ns | 3.8452 ns | 24 B |

**Finding:** An escaping boxed integer allocated 24 bytes on both runtimes in the tested environment.

The non-boxing measurements were indistinguishable from benchmark overhead, so their reported means should not be interpreted as precise execution times.

### 2. ArrayList vs. List&lt;int&gt;

`CollectionBenchmarks.cs`

Compares inserting integers into the non-generic `ArrayList` and the generic `List<int>`.

| Elements | ArrayList (.NET 10) | List&lt;int&gt; (.NET 10) |
|---|---:|---:|
| 100 | 633.4 ns | 194.0 ns |
| 1,000 | 6,984.9 ns | 1,749.0 ns |
| 10,000 | 83,564.6 ns | 16,886.2 ns |

At 10,000 elements:

- `ArrayList` allocated 320,056 bytes.
- `List<int>` allocated 40,056 bytes.
- The generic collection reduced managed allocation by approximately **87.5%**.

These results include collection construction and insertion, rather than isolating boxing costs alone.

### 3. RyuJIT Boxing Elimination

`JitOptimizationBenchmarks.cs`

Investigates whether RyuJIT can eliminate a boxing operation when the intermediate object is unnecessary.

| Method | Mean (.NET 10) | Allocated | Native code |
|---|---:|---:|---:|
| DirectReturn | 0.0060 ns | 0 B | 4 B |
| BoxAndUnbox | 0.0406 ns | 0 B | 4 B |
| EscapingBox | 4.1574 ns | 24 B | 35 B |

The generated assembly for `DirectReturn` and `BoxAndUnbox` was identical:

```asm
mov eax,[rcx+8]
ret
```

RyuJIT completely eliminated the intermediate boxing operation.

In contrast, `EscapingBox` contained an allocation-helper call:

```asm
call CORINFO_HELP_NEWSFAST
```

**Key finding:** Allocation elimination is not the same as stack allocation. In this experiment, the unnecessary boxed object was eliminated entirely.

BenchmarkDotNet reported `ZeroMeasurement` warnings for the first two methods. Their execution-time estimates should not be used for meaningful performance ratios.

## Requirements

- .NET SDK supporting .NET 9 and .NET 10
- BenchmarkDotNet 0.15.8
- Windows, Linux, or macOS with a supported .NET runtime

The recorded results were collected on Windows x64. Results on other platforms may differ.

## Running the Benchmarks

Clone the repository:

```bash
git clone https://github.com/Ayoub-EDAHLOULI/DotNetBoxingBenchmarks.git
cd DotNetBoxingBenchmarks
```

Restore dependencies:

```bash
dotnet restore
```

Run all benchmarks on .NET 9:

```bash
dotnet run -c Release -f net9.0 -- --filter '*'
```

Run all benchmarks on .NET 10:

```bash
dotnet run -c Release -f net10.0 -- --filter '*'
```

Run only the JIT optimization benchmarks:

```bash
dotnet run -c Release -f net10.0 -- --filter '*JitOptimizationBenchmarks*'
```

For meaningful results, use a Release build and avoid running other CPU-intensive applications during measurement.

## Benchmark Environment

| Component | Specification |
|---|---|
| Operating system | Windows 10 22H2 |
| Processor | Intel Core i7-6820HQ |
| Physical cores | 4 |
| Logical processors | 8 |
| .NET SDK | 10.0.403 |
| Runtimes | .NET 9.0.22 / .NET 10.0.14 |
| BenchmarkDotNet | 0.15.8 |
| JIT | X64 RyuJIT |

Benchmark results are environment-specific observations, not universal performance guarantees.

## Repository Structure

```text
DotNetBoxingBenchmarks/
├── BoxingBenchmarks.cs
├── CollectionBenchmarks.cs
├── JitOptimizationBenchmarks.cs
├── Program.cs
├── DotNetBoxingBenchmarks.csproj
├── results/
│   ├── BoxingBenchmarks-report.csv
│   ├── BoxingBenchmarks-report.html
│   ├── BoxingBenchmarks-report-github.md
│   ├── CollectionBenchmarks-report.csv
│   ├── CollectionBenchmarks-report.html
│   ├── CollectionBenchmarks-report-github.md
│   ├── JitOptimizationBenchmarks-report.csv
│   ├── JitOptimizationBenchmarks-report.html
│   ├── JitOptimizationBenchmarks-report-github.md
│   └── JitOptimizationBenchmarks-asm.md
├── .gitignore
├── LICENSE.txt
└── README.md
```

The `results/` directory preserves the original benchmark reports in CSV, HTML, and Markdown formats, together with the RyuJIT disassembly.

## Related Article

**Where Do Value Types Actually Live in C#? The Stack Myth Explained**

Part 01 of *C# & .NET, from the Inside*.

The article explores:

- Value types versus reference types.
- Stack, heap, and inline storage.
- Boxing and unboxing.
- Garbage collection and allocation costs.
- Generic versus non-generic collections.
- JIT optimization and allocation elimination.
- Real benchmark results from .NET 9 and .NET 10.

Series: [C# & .NET, from the Inside](https://ayoubedahlouli.com)

## References

- [C# Language Specification](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/)
- [.NET Garbage Collection Fundamentals](https://learn.microsoft.com/en-us/dotnet/standard/garbage-collection/fundamentals)
- [BenchmarkDotNet](https://benchmarkdotnet.org/)
- [.NET Runtime Source Code](https://github.com/dotnet/runtime)

## License

Licensed under the MIT License. See [LICENSE.txt](LICENSE.txt) for details.

---

Created by **Ayoub Edahlouli** as part of the *C# & .NET, from the Inside* technical writing series.
