using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;

namespace Benchmarks
{
    internal static class Constants
    {
        public const long Id = int.MaxValue;
        public static readonly byte[] Value = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        public static readonly OperationContext OperationContext = new OperationContext();
        public static readonly IDictionary<string, EntityProperty> Properties = new Dictionary<string, EntityProperty>()
        {
            { "Content", new EntityProperty(Value) },
            { "PartitionKey", new EntityProperty(Id.ToString()) },
            { "RowKey", new EntityProperty(Id.ToString()) },
            { "Timestamp", new EntityProperty(DateTimeOffset.UtcNow) },
            { "ETag", new EntityProperty(string.Empty) },
        };

        public static readonly CustomTypeEntity CustomEntity = new CustomTypeEntity(Id, Value);
        public static readonly DerivedTypeEntity DerivedEntity = new DerivedTypeEntity(Id, Value);
    }
}
