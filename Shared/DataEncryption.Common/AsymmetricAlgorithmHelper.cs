using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataEncryption.Common
{
    public class AsymmetricAlgorithmHelper
    {
        public string Decrypt(AsymmetricAlgorithms cipherAlgorithm, string cipherText)
        {
            if (cipherText is not { Length: > 0 })
                throw new ArgumentNullException(nameof(cipherText));

            using var algorithm = CipherAlgorithmHelper.GetAsymmetricAlgorithm(cipherAlgorithm);

            var cipherTextBytes = Convert.FromBase64String(cipherText);

            var plaintext = String.Empty;
            if (algorithm is RSA rsa)
            {
                var decryptedData = rsa.Decrypt(cipherTextBytes, RSAEncryptionPadding.Pkcs1);
                plaintext = Encoding.UTF8.GetString(decryptedData);
            }

            return plaintext;
        }

        public string Encrypt(AsymmetricAlgorithms cipherAlgorithm, string plainText)
        {
            if (plainText is not { Length: > 0 })
                throw new ArgumentNullException(nameof(plainText));

            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            using var algorithm = CipherAlgorithmHelper.GetAsymmetricAlgorithm(cipherAlgorithm);
            byte[] encrypted = new byte[] { };
            if (algorithm is RSA rsa)
            {
                encrypted = rsa.Encrypt(plainTextBytes, RSAEncryptionPadding.Pkcs1);
            }

            return Convert.ToBase64String(encrypted);
        }
    }
}