namespace DocumentDbDemo.Models
{
    /// <summary>
    /// Base document model defined in the task description.
    /// </summary>
    public class StorageDocument
    {
        public string Id { get; set; } = null!;

        public string[] Tags { get; set; } = null!;

        public StorageDocumentData Data { get; set; } = null!;
    }
}
