using System.Net.Http.Headers;
using static IPFSNodeLibrary.interfaces;

namespace IPFSNodeLibrary
{
    public class IPFSService: IIPFSCommands
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private HttpResponseMessage _response;

        public IPFSService(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl.TrimEnd('/');
        }
        public async Task<(bool, HttpResponseMessage, IEnumerable<string>)>  PinFileAsync(string cid)
        {
            var url = $"{_baseUrl}/{IpfsEndpoint.PinFile.Url}?arg={cid}"; 
            var response = await _httpClient.PostAsync(url, null);
            var wrapper = new HttpResponseMessageWrapper(response);
            var responseKeys = await wrapper.GetResponseKeysAsync();
            return (response.IsSuccessStatusCode, response, responseKeys);
        }

        public async Task<(string, HttpResponseMessage, IEnumerable<string>)> AddFileAsync(string filePath)
        {
            var url = $"{_baseUrl}/{IpfsEndpoint.AddFile.Url}?arg={filePath}";
            using var content = new MultipartContent();
            await using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            content.Add(fileContent);
        
            var response = await _httpClient.PostAsync(url, content);
            var wrapper = new HttpResponseMessageWrapper(response);
            var responseKeys = await wrapper.GetResponseKeysAsync();
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return (responseString, response, responseKeys);
        }

        public async Task<(bool, HttpResponseMessage, IEnumerable<string>)> GetFileAsync(string cid)
        {
            var url = $"{_baseUrl}/{IpfsEndpoint.CatFile.Url}?arg={cid}";
            var response = await _httpClient.GetAsync(url);
            var wrapper = new HttpResponseMessageWrapper(response);
            var responseKeys = await wrapper.GetResponseKeysAsync();
            return (response.IsSuccessStatusCode, response, responseKeys);
        }

        public async Task<(bool, HttpResponseMessage, IEnumerable<string>)> RemoveFileAsync(string cid)
        {
            var url = $"{_baseUrl}/{IpfsEndpoint.RemoveFile.Url}?arg={cid}";
            var response = await _httpClient.PostAsync(url, null);
            var wrapper = new HttpResponseMessageWrapper(response);
            var responseKeys = await wrapper.GetResponseKeysAsync();
            return (response.IsSuccessStatusCode, response, responseKeys);
        }
    }
}