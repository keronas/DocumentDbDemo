using MessagePack;

namespace DocumentDbDemo.Models
{
    /// <summary>
    /// Base document model defined in the task description.
    /// </summary>
    [MessagePackObject]
    public class StorageDocument
    {
        [Key(0)]
        public string Id { get; set; } = null!;

        [Key(1)]
        public string[] Tags { get; set; } = null!;

        [Key(2)]
        public StorageDocumentData Data { get; set; } = null!;
    }
}
