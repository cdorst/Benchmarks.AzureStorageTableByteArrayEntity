using Microsoft.WindowsAzure.Storage.Table;

namespace Benchmarks
{
    public class DerivedTypeEntity : TableEntity
    {
        public DerivedTypeEntity()
        {
        }

        public DerivedTypeEntity(string rowKey, byte[] content = null)
        {
            PartitionKey = rowKey;
            RowKey = rowKey;
            Content = content;
        }

        public DerivedTypeEntity(long rowKey, byte[] content = null)
            : this(rowKey.ToString(), content)
        {
        }

        public byte[] Content { get; set; }
    }
}
