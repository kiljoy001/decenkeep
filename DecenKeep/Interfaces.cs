using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Pqc.Crypto.Crystals.Kyber;

namespace DecenKeep;
using System.Collections.Generic;

public interface ITrezorService
{
    public Task InitializeAsync();
    public Task<string> GetPublicKeyAsync();
    public Task<string> SignTransactionAsync();
    
}
public interface ISecretHash
{
    Task<string> HashUserSecret(string input);
    Task<bool> VerifyUserSecret(string input, string hashedSecret);
}
public interface IDatabaseService
{
    Task<bool> InsertIntoDatabaseAsync<T>(string tableName, T data);
    Task<bool> UpdateDatabaseRecordAsync<T>(string tableName, string recordId, T updatedData);
    Task<bool> DeleteFromDatabaseAsync(string tableName, string recordId);
    Task<T> GetRecordByIdAsync<T>(string tableName, string recordId);
    Task<IEnumerable<T>> GetAllRecordsAsync<T>(string tableName);
    Task<string> EncryptDataAsync(string data, string key);
    Task<string> DecryptDataAsync(string encryptedData, string key);
    Task<bool> CreateDatabaseAsync();
}

public interface IFileService
{
    Task<bool> PinFileAsync();
    Task<bool> CreateBackupFolder(string location);
    Task<bool> AddFileToBackupAsync(string filePath);
    Task<bool> RemoveFileFromBackupAsync(string filePath);
    Task<IEnumerable<string>> ListBackedUpFilesAsync();
    Task<bool> EncryptAndBackupCidAsync(string cid, string userKey);
    Task<string> RetrieveEncryptedCidAsync(string cid, string userKey);
    Task<string> GenerateNewEncryptionKeyAsync();
    Task<bool> InitializeBackupDirectoryAsync(string location);
}

public interface IWalletService
{
    Task<bool> CheckBalanceAsync();
    Task<double> EstimateCosts();
}

public interface IBlockchainService
{
    Task<bool> PostToBlockchainAsync();
}

public interface ICostEstimator
{
    double EstimateCost(int rewardInCents, int leaseTimeInYears, int dataSizeInBytes);
}

public interface IAesService
{
    byte[] Decrypt(byte[] encryptedBytes, byte[] key);
    bool EncryptFile(string inputFile, string outputFile, string fileMetaData);
    bool DecryptFile(string inputFile, string outputFile, byte[] encryptionKey);
    byte[] Encrypt(byte[] plainBytes, string contextFileName);

    void SetKey(byte[]? key);
}

public interface IKyberService
{
    Org.BouncyCastle.Crypto.AsymmetricCipherKeyPair GenerateKyberKeys();
    (byte[] CipherText, byte[] AesKey) GenerateAesKey(KyberPublicKeyParameters decoderKyberPublicKey);
    byte[] DecryptData(byte[] cipherText, KyberPrivateKeyParameters privateKey);
}
