using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace ttpClient.Helpers
{
    public class CryptoHelper
    {
        public static AsymmetricCipherKeyPair CreateRSAKey()
        {
            var generator = new RsaKeyPairGenerator();

            var exponent = new Org.BouncyCastle.Math.BigInteger("65537");

            var param = new RsaKeyGenerationParameters(exponent, new SecureRandom(), 4096, 5);

            generator.Init(param);

            return generator.GenerateKeyPair();
        }

    }
}
