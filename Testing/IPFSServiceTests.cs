using System.Net;
using CustomGenerators;
using FsCheck.Xunit;
using IPFSNodeLibrary;
using Testing.Mocks;
using Xunit;

namespace Testing
{
    public class IPFSServiceTests
    {
        private readonly string _baseUrl = "http://localhost:5001/api/v0";
        private readonly HttpClient _httpClient;

        public IPFSServiceTests()
        {
            // Set up the mocked HttpClient with default behavior
            _httpClient = MockHttpMessageHandlerFactory.CreateHttpClient(HttpStatusCode.OK, "{}");
        }

        [Property(Arbitrary = new[] { typeof(CidGenerator) })]
        public async Task PinFileAsync_ShouldReturnSuccess(string cid)
        {
            // Arrange
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{}")
            };
            var httpClient = MockHttpMessageHandlerFactory.CreateHttpClient(new Dictionary<HttpRequestMessage, HttpResponseMessage>
            {
                { new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/pin/add?arg={cid}"), expectedResponse }
            });
            var ipfsService = new IPFSService(httpClient, _baseUrl);

            // Act
            var (success, response, responseKeys) = await ipfsService.PinFileAsync(cid);

            // Assert
            Assert.True(success);
            Assert.NotNull(response);
            Assert.NotEmpty(responseKeys);
        }

        [Property(Arbitrary = new[] { typeof(CidGenerator) })]
        public async Task GetFileAsync_ShouldReturnSuccess(string cid)
        {
            // Arrange
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{}")
            };
            var httpClient = MockHttpMessageHandlerFactory.CreateHttpClient(new Dictionary<HttpRequestMessage, HttpResponseMessage>
            {
                { new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/cat?arg={cid}"), expectedResponse }
            });
            var ipfsService = new IPFSService(httpClient, _baseUrl);

            // Act
            var (success, response, responseKeys) = await ipfsService.GetFileAsync(cid);

            // Assert
            Assert.True(success);
            Assert.NotNull(response);
            Assert.NotEmpty(responseKeys);
        }

        [Property(Arbitrary = new[] { typeof(CidGenerator) })]
        public async Task RemoveFileAsync_ShouldReturnSuccess(string cid)
        {
            // Arrange
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{}")
            };
            var httpClient = MockHttpMessageHandlerFactory.CreateHttpClient(new Dictionary<HttpRequestMessage, HttpResponseMessage>
            {
                { new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/pin/rm?arg={cid}"), expectedResponse }
            });
            var ipfsService = new IPFSService(httpClient, _baseUrl);

            // Act
            var (success, response, responseKeys) = await ipfsService.RemoveFileAsync(cid);

            // Assert
            Assert.True(success);
            Assert.NotNull(response);
            Assert.NotEmpty(responseKeys);
        }
    }
}
