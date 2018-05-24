``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17134
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.1.300-rc1-008673
  [Host]     : .NET Core 2.1.0-rc1 (CoreCLR 4.6.26426.02, CoreFX 4.6.26426.04), 64bit RyuJIT
  Job-EEIOGG : .NET Core 2.1.0-rc1 (CoreCLR 4.6.26426.02, CoreFX 4.6.26426.04), 64bit RyuJIT

LaunchCount=10  

```
|                       Method |         Mean |      Error |     StdDev |       Median | Rank |  Gen 0 | Allocated |
|----------------------------- |-------------:|-----------:|-----------:|-------------:|-----:|-------:|----------:|
|      CustomTypeInstantiation |    54.468 ns |  0.5124 ns |  2.2465 ns |    54.023 ns |    5 | 0.0330 |     104 B |
| CustomTypeInstantiationInRef |    53.375 ns |  0.2950 ns |  1.1309 ns |    53.345 ns |    4 | 0.0330 |     104 B |
|       CustomTypeKeyRetrieval |     4.576 ns |  0.0234 ns |  0.0846 ns |     4.577 ns |    2 |      - |       0 B |
|         CustomTypeReadEntity |    44.716 ns |  0.4932 ns |  1.7680 ns |    44.165 ns |    3 |      - |       0 B |
|        CustomTypeWriteEntity |   163.864 ns |  0.8354 ns |  3.0158 ns |   163.422 ns |    7 | 0.0787 |     248 B |
|     DerivedTypeInstantiation |    55.898 ns |  0.2775 ns |  1.0089 ns |    55.846 ns |    6 | 0.0355 |     112 B |
|      DerivedTypeKeyRetrieval |     4.325 ns |  0.0285 ns |  0.1027 ns |     4.315 ns |    1 |      - |       0 B |
|        DerivedTypeReadEntity | 3,096.046 ns | 14.9035 ns | 56.0441 ns | 3,089.750 ns |    9 | 0.5493 |    1736 B |
|       DerivedTypeWriteEntity | 3,002.374 ns | 12.4190 ns | 44.8345 ns | 3,000.432 ns |    8 | 0.6104 |    1920 B |
