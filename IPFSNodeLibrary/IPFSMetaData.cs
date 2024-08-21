namespace IPFSNodeLibrary;

using static interfaces;

public class IPFSMetaData: IIPFSFileMetadata
{
    private readonly IIPFSCommands _ipfsService;

    public IPFSMetaData(IIPFSCommands commands)
    {
        _ipfsService = commands;
    }
    
    public Task<string> GetCid()
    {
        throw new NotImplementedException();
    }

    public Task<string> GetFileName()
    {
        throw new NotImplementedException();
    }

    public Task<string> GetSize()
    {
        throw new NotImplementedException();
    }

    public Task<string> GetData(HttpResponseMessage response)
    {
        throw new NotImplementedException();
    }
}