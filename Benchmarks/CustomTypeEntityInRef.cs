using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;

namespace Benchmarks
{
    public class CustomTypeEntityInRef : ITableEntity
    {
        public CustomTypeEntityInRef()
        {
        }

        public CustomTypeEntityInRef(in string rowKey, in byte[] content = default)
        {
            RowKey = rowKey;
            Content = content;
        }

        public CustomTypeEntityInRef(in long rowKey, in byte[] content = default)
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
