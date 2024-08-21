using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Pqc.Crypto.Crystals.Kyber;
using Org.BouncyCastle.Security;

namespace DecenKeep
{
    public class KyberCryptoServices: IKyberService
    {
        private readonly SecureRandom _secureRandom = new SecureRandom();
        private readonly KyberKeyGenerationParameters _kyber;

        public KyberCryptoServices()
        {
            _kyber = new KyberKeyGenerationParameters(_secureRandom, KyberParameters.kyber1024);
        }

        public AsymmetricCipherKeyPair GenerateKyberKeys()
        {
            var kyberGenerator = new KyberKeyPairGenerator();
            kyberGenerator.Init(_kyber);
            var kyberKeyPair = kyberGenerator.GenerateKeyPair();
            return kyberKeyPair;
        }

        public (byte[] CipherText, byte[] AesKey) GenerateAesKey(KyberPublicKeyParameters decoderKyberPublicKey)
        {
            var kemGenerator = new KyberKemGenerator(_secureRandom);
            var encapsulatedSecret = kemGenerator.GenerateEncapsulated(decoderKyberPublicKey);
            var cipherText = encapsulatedSecret.GetEncapsulation();
            var aesKey = encapsulatedSecret.GetSecret();
            return (cipherText, aesKey);
        }


        public byte[] DecryptData(byte[] cipherText, KyberPrivateKeyParameters privateKey)
        {
            var kemExtractor = new KyberKemExtractor(privateKey);
            return kemExtractor.ExtractSecret(cipherText);
        }
    }
}