# Azure Storage Table byte[] ITableEntity implementation benchmarks
Run `./run.ps1` or `./run.sh` at the repository root to repeat the experiment

## Question

For Azure Storage Table entities with unique partition keys and `byte[]` content, what is the performance impact (if any) of using a custom `ITableEntity` type instead of a type derived from the `TableEntity` base class?

## Variables

Two implementations of `ITableEntity` are tested:

- `CustomTypeEntity` (implements `ITableEntity`)
- `DerivedTypeEntity` (extends `TableEntity`)

Performance impact is observed across these dimensions:

- Object instantiation
- Key-property access
- `ReadEntity` method invocation
- `WriteEntity` method invocation

## Hypothesis

`CustomTypeEntity` is expected to have more-performant object instantiation runtime given that its constructor has one less assignment statement than `DerivedTypeEntity`. Key-retrieval operations should perform equally well for both types.

`CustomTypeEntity` is expected to have more-performant `ReadEntity` and `WriteEntity` method-invocation runtimes given that the `DerivedTypeEntity` uses reflection.

## Results

``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17134
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.1.300-preview2-008533
  [Host]     : .NET Core 2.0.7 (CoreCLR 4.6.26328.01, CoreFX 4.6.26403.03), 64bit RyuJIT
  Job-WRSVCX : .NET Core 2.0.7 (CoreCLR 4.6.26328.01, CoreFX 4.6.26403.03), 64bit RyuJIT

LaunchCount=10  

```
|                   Method |         Mean |      Error |     StdDev |       Median | Rank |
|------------------------- |-------------:|-----------:|-----------:|-------------:|-----:|
|  CustomTypeInstantiation |    96.956 ns |  0.8461 ns |  3.0438 ns |    96.429 ns |    4 |
|   CustomTypeKeyRetrieval |     4.172 ns |  0.0155 ns |  0.0559 ns |     4.161 ns |    2 |
|     CustomTypeReadEntity |    41.239 ns |  0.4262 ns |  1.5442 ns |    40.597 ns |    3 |
|    CustomTypeWriteEntity |   170.269 ns |  0.7253 ns |  2.6091 ns |   169.622 ns |    5 |
| DerivedTypeInstantiation |    96.707 ns |  0.6051 ns |  2.1845 ns |    96.202 ns |    4 |
|  DerivedTypeKeyRetrieval |     3.449 ns |  0.0145 ns |  0.0523 ns |     3.438 ns |    1 |
|    DerivedTypeReadEntity | 3,582.633 ns | 14.9283 ns | 54.0837 ns | 3,572.175 ns |    6 |
|   DerivedTypeWriteEntity | 3,590.722 ns | 10.9776 ns | 39.3489 ns | 3,587.621 ns |    6 |

## Conclusion

Observed performance differences for object-instantiation and key-property access are negligible (less than 1 nanosecond).

`CustomTypeEntity`'s simpler `ReadEntity` and `WriteEntity` methods out-performed `DerivedTypeEntity`'s more complex, reflection-based methods.

