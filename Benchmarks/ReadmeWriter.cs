﻿using System;
using System.IO;
using System.Text;

namespace Benchmarks
{
    internal static class ReadmeWriter
    {
        public static void WriteReadme()
        {
            var dataTable = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "BenchmarkDotNet.Artifacts", "results", "Tests-report-github.md"));
            var readme = new StringBuilder()
                .Append("# Azure Storage Table byte[] ITableEntity implementation benchmarks")
                .AppendLine()
                .AppendLine("Run `.\run.ps1` or `./run.sh` at the repository root to repeat the experiment")
                .AppendLine()
                .AppendLine("## Question")
                .AppendLine()
                .AppendLine("For Azure Storage Table entities with unique partition keys and `byte[]` content, what is the performance impact (if any) of using a custom `ITableEntity` type instead of a type derived from the `TableEntity` base class? What is the performance impact of using the `in` readonly-reference keyword when constructing new objects?")
                .AppendLine()
                .AppendLine("## Variables")
                .AppendLine()
                .AppendLine("Two implementations of `ITableEntity` are tested:")
                .AppendLine()
                .AppendLine("- `CustomTypeEntity` (implements `ITableEntity`)")
                .AppendLine("- `DerivedTypeEntity` (extends `TableEntity`)")
                .AppendLine()
                .AppendLine("Performance impact is observed across these dimensions:")
                .AppendLine()
                .AppendLine("- Object instantiation (including use of the `in` keyword)")
                .AppendLine("- Key-property access")
                .AppendLine("- `ReadEntity` method invocation")
                .AppendLine("- `WriteEntity` method invocation")
                .AppendLine()
                .AppendLine("## Hypothesis")
                .AppendLine()
                .AppendLine("`CustomTypeEntity` is expected to have more-performant object instantiation runtime given that its constructor has one less assignment statement than `DerivedTypeEntity`. Key-retrieval operations should perform equally well for both types.")
                .AppendLine()
                .AppendLine("`CustomTypeEntity` is expected to have more-performant `ReadEntity` and `WriteEntity` method-invocation runtimes given that the `DerivedTypeEntity` uses reflection.")
                .AppendLine()
                .AppendLine("## Results")
                .AppendLine();
            foreach (var line in dataTable) readme.AppendLine(line);
            readme.AppendLine();
            readme
                .AppendLine("## Conclusion")
                .AppendLine()
                .AppendLine("Observed performance differences for object-instantiation and key-property access are negligible (less than 1 nanosecond).")
                .AppendLine()
                .AppendLine("`CustomTypeEntity`'s simpler `ReadEntity` and `WriteEntity` methods out-performed `DerivedTypeEntity`'s more complex, reflection-based methods.")
                .AppendLine();
            File.WriteAllText("../README.md", readme.ToString());
        }
    }
}
