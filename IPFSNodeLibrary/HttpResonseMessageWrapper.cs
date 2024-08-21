using System.Net.Http.Headers;
using System.Text.Json;

namespace IPFSNodeLibrary
{
    public class HttpResponseMessageWrapper: interfaces.IHttpRepsonseMessageWrapper
    {
        private readonly HttpResponseMessage _responseMessage;
        public HttpContent Content { get; }
        public HttpResponseHeaders Headers { get; }
        public bool IsSuccessStatusCode { get; }

        public HttpResponseMessageWrapper(HttpResponseMessage responseMessage)
        {
            _responseMessage = responseMessage ?? throw new ArgumentNullException(nameof(responseMessage));
        }
        public Task<string> ReadAsStringAsync()
        {
            return _responseMessage.Content.ReadAsStringAsync();
        }

        public async Task<IEnumerable<string>> GetResponseKeysAsync()
        {
            var jsonString = await _responseMessage.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                return Enumerable.Empty<string>();
            }

            var resultDictionary = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonString);
            return resultDictionary?.Keys ?? Enumerable.Empty<string>();
        }
    }
}