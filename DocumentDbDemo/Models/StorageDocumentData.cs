namespace DocumentDbDemo.Models
{
    /// <summary>
    /// Model with arbitrary data fields, to be used in <see cref="StorageDocument"/>.
    /// </summary>
    public class StorageDocumentData
    {
        public string? ArbitraryStringField { get; set; }

        public int? ArbitraryIntField { get; set; }

        public bool? ArbitraryBoolField { get; set; }

    }
}
