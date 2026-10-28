```

BenchmarkDotNet v0.15.8, Windows 10 (10.0.19045.6466/22H2/2022Update)
Intel Core i7-6820HQ CPU 2.70GHz (Max: 2.71GHz) (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 10.0.403
  [Host]     : .NET 10.0.14 (10.0.14, 10.0.1426.52326), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.14 (10.0.14, 10.0.1426.52326), X64 RyuJIT x86-64-v3


```
| Method       | Mean      | Error     | StdDev    | Median    | Gen0   | Code Size | Allocated |
|------------- |----------:|----------:|----------:|----------:|-------:|----------:|----------:|
| DirectReturn | 0.0060 ns | 0.0115 ns | 0.0102 ns | 0.0000 ns |      - |       4 B |         - |
| BoxAndUnbox  | 0.0406 ns | 0.0287 ns | 0.0670 ns | 0.0000 ns |      - |       4 B |         - |
| EscapingBox  | 4.1574 ns | 0.0846 ns | 0.0750 ns | 4.1138 ns | 0.0057 |      35 B |      24 B |
