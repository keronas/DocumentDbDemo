using MessagePack;

namespace DocumentDbDemo.Models
{
    /// <summary>
    /// Model with arbitrary data fields, to be used in <see cref="StorageDocument"/>.
    /// </summary>
    [MessagePackObject]
    public class StorageDocumentData
    {
        [Key(0)]
        public string? ArbitraryStringField { get; set; }

        [Key(1)]
        public int? ArbitraryIntField { get; set; }

        [Key(2)]
        public bool? ArbitraryBoolField { get; set; }

    }
}
