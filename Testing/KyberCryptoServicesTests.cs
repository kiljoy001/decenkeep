
using CustomGenerators;
using DecenKeep;
using FsCheck;
using FsCheck.FSharp;
using FsCheck.Xunit;

using Org.BouncyCastle.Pqc.Crypto.Crystals.Kyber;
using Prop = FsCheck.Fluent.Prop;


namespace Testing
{
    public class KyberCryptoServicesTests
    {
        [Property]
        public Property EncryptDecryptTest()
        {
        var byteArrayArb = Arb.From<byte[]>(ByteArrayGenerator.byteGenerator);
        return Prop.ForAll<byte[]>(byteArrayArb =>
        {
            var cryptoService = new KyberCryptoServices();
            var keyPair = cryptoService.GenerateKyberKeys();
            var publicKey = (KyberPublicKeyParameters)keyPair.Public;
            var privateKey = (KyberPrivateKeyParameters)keyPair.Private;

            var (cipherText, secret) = cryptoService.GenerateAesKey(publicKey);
            var decryptedSecret = cryptoService.DecryptData(cipherText, privateKey);

            return secret.Length == decryptedSecret.Length &&
                   secret.SequenceEqual(decryptedSecret);
        });
        }
    }
}