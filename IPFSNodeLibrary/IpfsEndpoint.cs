namespace IPFSNodeLibrary
{
    public struct IpfsEndpoint
    {
        public string Name { get; }
        public string Url { get; }

        public IpfsEndpoint(string name, string url)
        {
            Name = name;
            Url = url;
        }

        public static readonly IpfsEndpoint AddFile = new IpfsEndpoint("AddFile", "api/v0/add");
        public static readonly IpfsEndpoint CatFile = new IpfsEndpoint("CatFile", "api/v0/cat");
        public static readonly IpfsEndpoint PinFile = new IpfsEndpoint("PinFile", "api/v0/pin/add");
        public static readonly IpfsEndpoint RemoveFile = new IpfsEndpoint("RemoveFile", "api/v0/files/rm");
    }
}