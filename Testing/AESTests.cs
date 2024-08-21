using System.Text;
using FsCheck;
using FsCheck.Xunit;
using DecenKeep;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;

namespace Testing
{
    public class AesTests
    {
        public class AesCryptoServiceTests
        {
            private readonly byte[] _iv = new byte[12];
            private readonly SecureRandom _random = new SecureRandom();

            public AesCryptoServiceTests()
            {
                _random.NextBytes(_iv);
            }

            [Property]
            public bool EncryptDecryptShouldReturnOriginalData(NonEmptyString plainText, NonEmptyString context)
            {
                // Arrange
                var plainBytes = Encoding.UTF8.GetBytes(plainText.Get);
                var aesCryptoService = new AesCryptoService(_iv);

                // Act
                var encryptedBytes = aesCryptoService.Encrypt(plainBytes, context.Get);
                var decryptedBytes = aesCryptoService.Decrypt(encryptedBytes, aesCryptoService.Key.GetKey());

                // Assert
                var decryptedText = Encoding.UTF8.GetString(decryptedBytes);
                return plainText.Get == decryptedText;
            }

            [Property]
            public bool EncryptDecryptFileShouldReturnOriginalData(NonEmptyString fileContent, NonEmptyString context)
            {
                // Arrange
                var plainBytes = Encoding.UTF8.GetBytes(fileContent.Get);
                var aesCryptoService = new AesCryptoService(_iv);
                var inputFile = "input.txt";
                var encryptedFile = "encrypted.txt";
                var decryptedFile = "decrypted.txt";

                try
                {
                    // Write plain text to input file
                    File.WriteAllBytes(inputFile, plainBytes);

                    // Act
                    var encryptResult = aesCryptoService.EncryptFile(inputFile, encryptedFile, context.Get);
                    var decryptResult = aesCryptoService.DecryptFile(encryptedFile, decryptedFile, aesCryptoService.Key.GetKey());

                    // Read decrypted text from decrypted file
                    var decryptedBytes = File.ReadAllBytes(decryptedFile);
                    var decryptedText = Encoding.UTF8.GetString(decryptedBytes);

                    // Assert
                    return encryptResult && decryptResult && fileContent.Get == decryptedText;
                }
                finally
                {
                    // Clean up temporary files
                    if (File.Exists(inputFile)) File.Delete(inputFile);
                    if (File.Exists(encryptedFile)) File.Delete(encryptedFile);
                    if (File.Exists(decryptedFile)) File.Delete(decryptedFile);
                }
            }
        }
    }
}
