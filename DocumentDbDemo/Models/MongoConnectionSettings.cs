namespace DocumentDbDemo.Models
{
    public class MongoConnectionSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string DocumentCollectionName { get; set; } = null!;
    }
}
