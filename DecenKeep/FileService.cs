namespace DecenKeep;

public class FileService: IFileService
{
    public Task<bool> PinFileAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> CreateBackupFolder(string location)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddFileToBackupAsync(string filePath)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveFileFromBackupAsync(string filePath)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<string>> ListBackedUpFilesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> EncryptAndBackupCidAsync(string cid, string userKey)
    {
        throw new NotImplementedException();
    }

    public Task<string> RetrieveEncryptedCidAsync(string cid, string userKey)
    {
        throw new NotImplementedException();
    }

    public Task<bool> PostKeyToEmercoinAsync(string encryptedKey)
    {
        throw new NotImplementedException();
    }

    public Task<string> RestoreFromEmercoinAsync(string nvsEntry)
    {
        throw new NotImplementedException();
    }

    public Task<string> GenerateNewEncryptionKeyAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> InitializeBackupDirectoryAsync(string location)
    {
        throw new NotImplementedException();
    }
}