```

BenchmarkDotNet v0.15.8, Windows 10 (10.0.19045.6466/22H2/2022Update)
Intel Core i7-6820HQ CPU 2.70GHz (Max: 2.71GHz) (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 10.0.403
  [Host]     : .NET 10.0.14 (10.0.14, 10.0.1426.52326), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.14 (10.0.14, 10.0.1426.52326), X64 RyuJIT x86-64-v3


```
| Method        | Mean      | Error     | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|-------------- |----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| WithoutBoxing | 0.0202 ns | 0.0228 ns | 0.0178 ns |     ? |       ? |      - |         - |           ? |
| WithBoxing    | 3.8452 ns | 0.1215 ns | 0.0949 ns |     ? |       ? | 0.0057 |      24 B |           ? |
