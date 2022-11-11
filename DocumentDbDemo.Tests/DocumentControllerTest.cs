using DocumentDbDemo.Controllers;
using DocumentDbDemo.Models;
using DocumentDbDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DocumentDbDemo.Tests
{
    public class DocumentControllerTest
    {
        private static StorageDocument TestingSingleDocument = new()
        {
            Id = "1",
            Tags = new[] { "mock", "" },
            Data = new StorageDocumentData()
            {
                ArbitraryStringField = "aaa",
                ArbitraryIntField = 42,
                ArbitraryBoolField = true
            }
        };

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

        [Fact]
        public async Task GetAsync_ReturnsDocument_WhenFound()
        {
            var mockData = TestingSingleDocument;
            var documentServiceMock = new Mock<IDocumentService>();
            documentServiceMock.Setup(service => service.GetAsync(mockData.Id)).ReturnsAsync(mockData);
            var testedController = new DocumentsController(documentServiceMock.Object);

            var result = await testedController.GetAsync(mockData.Id);

            Assert.Equal(mockData, result.Value);
        }

        [Fact]
        public async Task GetAsync_ReturnsNotFound_WhenNotFound()
        {
            var mockData = TestingSingleDocument;
            var documentServiceMock = new Mock<IDocumentService>();
            documentServiceMock.Setup(service => service.GetAsync(mockData.Id)).ReturnsAsync(null as StorageDocument);
            var testedController = new DocumentsController(documentServiceMock.Object);

            var result = await testedController.GetAsync(mockData.Id);

            Assert.IsAssignableFrom<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostAsync_CreatesDocument_WhenValid()
        {
            var mockData = TestingSingleDocument;
            var documentServiceMock = new Mock<IDocumentService>();
            documentServiceMock.Setup(service => service.CreateAsync(mockData)).Verifiable();
            var testedController = new DocumentsController(documentServiceMock.Object);

            await testedController.PostAsync(mockData);

            documentServiceMock.Verify();
        }

        [Fact]
        public async Task PostAsync_ReturnsBadRequest_OnArgumentException()
        {
            var mockData = TestingSingleDocument;
            var documentServiceMock = new Mock<IDocumentService>();
            var exceptionText = "Something wrong";
            documentServiceMock.Setup(service => service.CreateAsync(mockData)).Throws(() => new ArgumentException(exceptionText));
            var testedController = new DocumentsController(documentServiceMock.Object);

            var result = await testedController.PostAsync(mockData);

            Assert.True(result is BadRequestObjectResult);
            Assert.True(((BadRequestObjectResult)result).Value?.Equals(exceptionText));
        }

        [Fact]
        public async Task PutAsync_UpdatesDocument_WhenValid()
        {
            var id = "123456";
            var mockData = TestingSingleDocument;
            var documentServiceMock = new Mock<IDocumentService>();
            documentServiceMock.Setup(service => service.UpdateAsync(id, mockData)).Verifiable();
            var testedController = new DocumentsController(documentServiceMock.Object);

            await testedController.PutAsync(id, mockData);

            documentServiceMock.Verify();
        }

        [Fact]
        public async Task PutAsync_ReturnsBadRequest_OnArgumentException()
        {
            var id = "123456";
            var mockData = TestingSingleDocument;
            var documentServiceMock = new Mock<IDocumentService>();
            var exceptionText = "Something wrong";
            documentServiceMock.Setup(service => service.UpdateAsync(id, mockData)).Throws(() => new ArgumentException(exceptionText));
            var testedController = new DocumentsController(documentServiceMock.Object);

            var result = await testedController.PutAsync(id, mockData);

            Assert.True(result is BadRequestObjectResult);
            Assert.True(((BadRequestObjectResult)result).Value?.Equals(exceptionText));
        }
    }
}