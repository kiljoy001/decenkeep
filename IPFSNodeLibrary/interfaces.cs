using System.Net;
using System.Net.Http.Headers;

namespace IPFSNodeLibrary
{
    public interface interfaces
    {
        public interface IIPFSCommands
        {
            Task<(bool, HttpResponseMessage, IEnumerable<string>)> PinFileAsync(string cid);
            Task<(string, HttpResponseMessage, IEnumerable<string>)> AddFileAsync(string cid);
            Task<(bool, HttpResponseMessage,IEnumerable<string>)> GetFileAsync(string cid);
            Task<(bool, HttpResponseMessage, IEnumerable<string>)> RemoveFileAsync(string cid);
        }

        public interface IIPFSFileMetadata
        {
            Task<string> GetCid();
            Task<string> GetFileName();
            Task<string> GetSize();
            Task<string> GetData(HttpResponseMessage response);
        }
    
        public interface IHttpRepsonseMessageWrapper
        {
            HttpContent Content { get; }
            HttpResponseHeaders Headers { get; }
            bool IsSuccessStatusCode { get; }
            Task<string> ReadAsStringAsync();
            Task<IEnumerable<string>> GetResponseKeysAsync();
        }
    }
}