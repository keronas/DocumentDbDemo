using DocumentDbDemo.Controllers;
using DocumentDbDemo.Models;
using DocumentDbDemo.Services;
using Moq;

namespace DocumentDbDemo.Tests
{
    public class DocumentControllerTest
    {
        private static List<IEnumerable<StorageDocument>> TestingDocumentCollections => new List<IEnumerable<StorageDocument>>
        {
            new[] 
            {
                new StorageDocument()
                {
                    Id = "1",
                    Tags = new[] { "mock", "" },
                    Data = new StorageDocumentData()
                    {
                        ArbitraryStringField = "aaa",
                        ArbitraryIntField = 42,
                        ArbitraryBoolField = true
                    }
                }
            },
            new List<StorageDocument>
            {
                new StorageDocument()
                {
                    Id = "1",
                    Tags = new[] { "mock", "" },
                    Data = new StorageDocumentData()
                    {
                        ArbitraryStringField = "aaa",
                        ArbitraryIntField = 42,
                        ArbitraryBoolField = true
                    }
                }
            },
            new[]
            {
                new StorageDocument()
            },
            new[]
            {
                new StorageDocument()
                {
                    Id = "1",
                },
                new StorageDocument()
                {
                    Id = "2",
                },
                new StorageDocument()
                {
                    Id = "3",
                }
            },
            new List<StorageDocument>(),
        };


        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task GetAsync_ReturnsData_AsReceivedFromService(int testingDataIndex)
        {
            // I hope there's a better way to get the test parameters than the InlineData with indexes, but after some time I gave up finding it.
            // I Tried MemberData, but it looked even worse.

            var mockData = TestingDocumentCollections[testingDataIndex];

            var documentServiceMock = new Mock<IDocumentService>();
            documentServiceMock.Setup(service => service.GetAsync()).ReturnsAsync(mockData);
            var testedController = new DocumentsController(documentServiceMock.Object);

            var result = await testedController.GetAsync();

            Assert.Equal(mockData, result);
        }
    }
}