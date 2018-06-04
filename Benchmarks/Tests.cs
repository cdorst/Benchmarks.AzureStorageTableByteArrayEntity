using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Exporters;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Running;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using static Benchmarks.Constants;

namespace Benchmarks
{
    [SimpleJob(10), MemoryDiagnoser]
    [RPlotExporter, RankColumn]
    public class Tests
    {
        [Benchmark]
        public CustomTypeEntity CustomTypeInstantiation()
            => new CustomTypeEntity(Id, Value);

        [Benchmark]
        public CustomTypeEntityInRef CustomTypeInstantiationInRef()
            => new CustomTypeEntityInRef(Id, Value);

        [Benchmark]
        public (string, string) CustomTypeKeyRetrieval()
            => (CustomEntity.PartitionKey, CustomEntity.RowKey);

        [Benchmark]
        public void CustomTypeReadEntity()
            => CustomEntity.ReadEntity(Properties, OperationContext);

        [Benchmark]
        public IDictionary<string, EntityProperty> CustomTypeWriteEntity()
            => CustomEntity.WriteEntity(OperationContext);

        [Benchmark]
        public DerivedTypeEntity DerivedTypeInstantiation()
            => new DerivedTypeEntity(Id, Value);

        [Benchmark]
        public (string, string) DerivedTypeKeyRetrieval()
            => (DerivedEntity.PartitionKey, DerivedEntity.RowKey);

        [Benchmark]
        public void DerivedTypeReadEntity()
            => DerivedEntity.ReadEntity(Properties, OperationContext);

        [Benchmark]
        public IDictionary<string, EntityProperty> DerivedTypeWriteEntity()
            => DerivedEntity.WriteEntity(OperationContext);
    }
}
