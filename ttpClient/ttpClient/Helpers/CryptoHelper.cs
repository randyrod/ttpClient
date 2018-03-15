using System;
using System.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace ttpClient.Helpers
{
    public class CryptoHelper
    {
        public static Tuple<string,string> CreateRSAKey()
        {
            var generator = new RsaKeyPairGenerator();

            var exponent = new Org.BouncyCastle.Math.BigInteger("65537");

            var param = new RsaKeyGenerationParameters(exponent, new SecureRandom(), 4096, 5);

            generator.Init(param);

            var keyPair = generator.GenerateKeyPair();

            return new Tuple<string, string>(KeyToString(keyPair.Private), KeyToString(keyPair.Public));

        }

        private static string KeyToString(AsymmetricKeyParameter key)
        {
            var writer = new StringWriter();
            var pemWriter = new PemWriter(writer);

            pemWriter.WriteObject(key);

            pemWriter.Writer.Flush();

            var keyString = writer.ToString();
            
            return keyString;
        }

    }
}
