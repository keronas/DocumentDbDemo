namespace DocumentDbDemo.Models
{
    public class StorageDocument
    {
        public string Id { get; set; } = null!;

        public string[] Tags { get; set; } = null!;

        public StorageDocumentData Data { get; set; } = null!;
    }
}
