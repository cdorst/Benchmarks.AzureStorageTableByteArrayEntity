using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;

namespace Benchmarks
{
    public class CustomTypeEntity : ITableEntity
    {
        public CustomTypeEntity()
        {
        }

        public CustomTypeEntity(string rowKey, byte[] content = default)
        {
            RowKey = rowKey;
            Content = content;
        }

        public CustomTypeEntity(long rowKey, byte[] content = default)
            : this(rowKey.ToString(), content)
        {
        }

        public byte[] Content { get; set; }
        public string PartitionKey { get => RowKey; set => RowKey = value; }
        public string RowKey { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string ETag { get; set; }

        public void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
        {
            if (properties != null
                && properties.TryGetValue(nameof(Content), out EntityProperty value))
            {
                Content = value.BinaryValue;
            }
        }

        public IDictionary<string, EntityProperty> WriteEntity(OperationContext operationContext)
            => new Dictionary<string, EntityProperty>
            {
                { nameof(Content), new EntityProperty(Content) }
            };
    }
}
