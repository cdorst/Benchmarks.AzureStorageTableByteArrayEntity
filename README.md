# Azure Storage Table byte[] ITableEntity implementation benchmarks
Run `./run.ps1` or `./run.sh` at the repository root to repeat the experiment

## Question

For Azure Storage Table entities with unique partition keys and `byte[]` content, what is the performance impact (if any) of using a custom `ITableEntity` type instead of a type derived from the `TableEntity` base class? What is the performance impact of using the `in` readonly-reference keyword when constructing new objects?

## Variables

Two implementations of `ITableEntity` are tested:

- `CustomTypeEntity` (implements `ITableEntity`)
- `DerivedTypeEntity` (extends `TableEntity`)

Performance impact is observed across these dimensions:

- Object instantiation (including use of the `in` keyword)
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
.NET Core SDK=2.1.300-rc1-008673
  [Host]     : .NET Core 2.1.0-rc1 (CoreCLR 4.6.26426.02, CoreFX 4.6.26426.04), 64bit RyuJIT
  Job-JMMNQF : .NET Core 2.1.0-rc1 (CoreCLR 4.6.26426.02, CoreFX 4.6.26426.04), 64bit RyuJIT

LaunchCount=10  

```
|                       Method |         Mean |      Error |     StdDev |       Median | Rank |
|----------------------------- |-------------:|-----------:|-----------:|-------------:|-----:|
|      CustomTypeInstantiation |    56.064 ns |  0.4707 ns |  2.3638 ns |    55.566 ns |    4 |
| CustomTypeInstantiationInRef |    56.640 ns |  0.5415 ns |  2.4580 ns |    55.956 ns |    4 |
|       CustomTypeKeyRetrieval |     4.714 ns |  0.1132 ns |  0.4514 ns |     4.861 ns |    1 |
|         CustomTypeReadEntity |    45.053 ns |  0.3175 ns |  1.2548 ns |    44.683 ns |    3 |
|        CustomTypeWriteEntity |   166.043 ns |  1.0401 ns |  4.0122 ns |   165.569 ns |    5 |
|     DerivedTypeInstantiation |    56.336 ns |  0.3947 ns |  1.5598 ns |    56.124 ns |    4 |
|      DerivedTypeKeyRetrieval |     5.012 ns |  0.0340 ns |  0.1236 ns |     4.999 ns |    2 |
|        DerivedTypeReadEntity | 3,143.487 ns | 20.2793 ns | 77.7395 ns | 3,134.838 ns |    7 |
|       DerivedTypeWriteEntity | 3,036.002 ns | 13.8699 ns | 51.2990 ns | 3,037.277 ns |    6 |

## Conclusion

Observed performance differences for object-instantiation and key-property access are negligible (less than 1 nanosecond).

`CustomTypeEntity`'s simpler `ReadEntity` and `WriteEntity` methods out-performed `DerivedTypeEntity`'s more complex, reflection-based methods.

