using System.Globalization;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;


namespace DecenKeep
{
    public class AesCryptoService: IAesService
    {
        public KeyParameter Key { get; private set; }
        private readonly byte[] _iv;
        private const int NonceSize = 12;
        private const int IntSize = sizeof(int);
        private const int LengthFieldSize = 2 * IntSize;
        private const int BitsPerByte = 8;
        private const int KeySize = 256;
        private const int BufferSize = 4096;
        public AesCryptoService(byte[] iv, byte[]? key = null)
        {
            _iv = iv ?? throw new ArgumentNullException(nameof(iv));
            if (_iv.Length < NonceSize)
            {
                throw new ArgumentException($"IV length must be at least {NonceSize}!");
            }

            if (key != null)
            {
                SetKey(key);
            }
        }
        public void SetKey(byte[]? key)
        {
            if (key == null || key.Length * 8 != KeySize)
            {
                throw new ArgumentException($"Key length must be {KeySize / 8} bytes.");
            }

            Key = new KeyParameter(key);
        }
        private byte[] GenerateAad(string context)
        {
            List<byte> combinedBytes = new List<byte>();
            var convertStringBytes = Encoding.UTF8.GetBytes(context);
            var convertDateTimeBytes = Encoding.UTF8.GetBytes(
                DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));
            combinedBytes.AddRange(convertStringBytes);
            combinedBytes.AddRange(convertDateTimeBytes);
            return combinedBytes.ToArray();
        }

        private static byte[] AppendAadDataBytes(byte[] encrypted, byte[] nonce, byte[] aadData)
        {
            var combinedBytes = new List<byte>();
            combinedBytes.AddRange(BitConverter.GetBytes(nonce.Length));
            combinedBytes.AddRange(BitConverter.GetBytes(aadData.Length));
            combinedBytes.AddRange(nonce);
            combinedBytes.AddRange(aadData);
            combinedBytes.AddRange(encrypted);
            return combinedBytes.ToArray();
        }         

        public byte[] Encrypt(byte[] plainBytes, string contextFileName)
        {
            const int keySize = 256;

            IBlockCipher cipher = new AesEngine();
            var macSize = BitsPerByte * cipher.GetBlockSize();
            var aadData = GenerateAad(contextFileName);

            var keyGen = new CipherKeyGenerator();
            keyGen.Init(new KeyGenerationParameters(new SecureRandom(), keySize));
            var keyBytes = keyGen.GenerateKey();
            Key = new KeyParameter(keyBytes);

            var keyParamAead = new AeadParameters(Key, macSize, _iv, aadData);
            var cipherMode = new GcmBlockCipher(cipher);
            cipherMode.Init(true, keyParamAead);

            var encryptedBytes = new byte[cipherMode.GetOutputSize(plainBytes.Length)];
            int length = cipherMode.ProcessBytes(plainBytes, 0, plainBytes.Length, encryptedBytes, 0);
            cipherMode.DoFinal(encryptedBytes, length);
            return AppendAadDataBytes(encryptedBytes, _iv, aadData);
        }

        public byte[] Decrypt(byte[] encryptedBytes, byte[] key)
        {
            IBlockCipher cipher = new AesEngine();
            var macSize = BitsPerByte * cipher.GetBlockSize();

            // Read lengths
            int ivLength = BitConverter.ToInt32(encryptedBytes, 0);
            int aadLength = BitConverter.ToInt32(encryptedBytes, IntSize);
            //validate IV Length
            if (ivLength < NonceSize)
            {
                throw new ArgumentException($"IV Length must be at least {NonceSize}!");
            }
            // Allocate buffers
            var iv = new byte[ivLength];
            var aadData = new byte[aadLength];
            var cipherText = new byte[encryptedBytes.Length - LengthFieldSize - iv.Length - aadData.Length];

            // Extract data
            Array.Copy(encryptedBytes, 
                LengthFieldSize, iv, 0, iv.Length);
            Array.Copy(encryptedBytes,
                LengthFieldSize + iv.Length, aadData, 0, aadData.Length);
            Array.Copy(encryptedBytes,
                LengthFieldSize + iv.Length + aadData.Length,
                cipherText, 0, cipherText.Length);

            var keyParamAead = new AeadParameters(new KeyParameter(key), macSize, iv, aadData);
            var cipherMode = new GcmBlockCipher(cipher);
            cipherMode.Init(false, keyParamAead);

            var decryptedBytes = new byte[cipherMode.GetOutputSize(cipherText.Length)];
            int len = cipherMode.ProcessBytes(cipherText, 0, cipherText.Length, decryptedBytes, 0);
            cipherMode.DoFinal(decryptedBytes, len);

            return decryptedBytes;
        }

        public bool EncryptFile(string inputFile, string outputFile, string fileMetaData)
        {
            try
            {
                using (var inputStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
                using (var outputStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                {
                    byte[] buffer = new byte[BufferSize];
                    int bytesRead;
                    while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        byte[] encryptedBytes = Encrypt(buffer.AsSpan(0, bytesRead).ToArray(),
                            fileMetaData);
                        outputStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool DecryptFile(string inputFile, string outputFile, byte[] encryptionKey)
        {
            try
            {
                using (var inputStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
                using (var outputStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                {
                    byte[] buffer = new byte[BufferSize];
                    int bytesRead;
                    while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        byte[] decryptedBytes = Decrypt(buffer.AsSpan(0, bytesRead).ToArray(),
                            encryptionKey);
                        outputStream.Write(decryptedBytes, 0, decryptedBytes.Length);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

