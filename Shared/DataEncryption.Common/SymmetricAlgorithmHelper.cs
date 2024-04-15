using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataEncryption.Common
{
    public class SymmetricAlgorithmHelper(string privateKey)
    {
        public string Decrypt(SymmetricAlgorithms cipherAlgorithm, string cipherText, string publicKey)
        {
            if (cipherText is not { Length: > 0 })
                throw new ArgumentNullException(nameof(cipherText));
            if (privateKey is not { Length: > 0 })
                throw new ArgumentNullException(nameof(privateKey));
            if (publicKey is not { Length: > 0 })
                throw new ArgumentNullException(nameof(publicKey));

            using var algorithm = CipherAlgorithmHelper.GetSymmetricAlgorithm(cipherAlgorithm);

            algorithm.Mode = CipherMode.CBC;
            algorithm.Key = KeyHelpers.CreateKey(cipherAlgorithm, privateKey);
            algorithm.IV = Convert.FromBase64String(publicKey);

            var decryptor = algorithm.CreateDecryptor();

            using var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText));
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);
            var plaintext = srDecrypt.ReadToEnd();

            return plaintext;
        }

        public string Encrypt(SymmetricAlgorithms cipherAlgorithm, string plainText, string publicKey)
        {
            if (plainText is not { Length: > 0 })
                throw new ArgumentNullException(nameof(plainText));
            if (privateKey is not { Length: > 0 })
                throw new ArgumentNullException(nameof(privateKey));
            if (publicKey is not { Length: > 0 })
                throw new ArgumentNullException(nameof(publicKey));

            byte[] encrypted;

            using (var algorithm = CipherAlgorithmHelper.GetSymmetricAlgorithm(cipherAlgorithm))
            {
                algorithm.Mode = CipherMode.CBC;
                algorithm.Key = KeyHelpers.CreateKey(cipherAlgorithm, privateKey);
                algorithm.IV = Convert.FromBase64String(publicKey);

                var encryptor = algorithm.CreateEncryptor();

                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encrypted);
        }
    }
}