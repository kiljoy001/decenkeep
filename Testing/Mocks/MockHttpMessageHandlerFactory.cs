using System.Net;
using Moq;
using Moq.Protected;

namespace Testing.Mocks
{
    public static class MockHttpMessageHandlerFactory
    {
        public static HttpClient CreateHttpClient(HttpStatusCode statusCode, string responseContent)
        {
            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = new StringContent(responseContent)
                });

            return new HttpClient(handlerMock.Object);
        }

        public static HttpClient CreateHttpClient(Dictionary<HttpRequestMessage, HttpResponseMessage> responses)
        {
            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync((HttpRequestMessage request, CancellationToken cancellationToken) =>
                {
                    if (responses.TryGetValue(request, out var response))
                    {
                        return response;
                    }

                    return new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        RequestMessage = request
                    };
                });

            return new HttpClient(handlerMock.Object);
        }
    }
}