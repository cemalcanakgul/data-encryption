using System.Security.Cryptography;
using System.Text;

namespace AESEncryption
{
    public static class AesExample
    {
        private static string PrivateKey
        {
            get
            {
                var key = "3fb7fe5dbb0643caa984f53de6fffd0f";

                const string envVarName = "APP_ENCRYPTION_SECRET_KEY";

                var envKeyValue = Environment.GetEnvironmentVariable(envVarName);

                if (envKeyValue != null)
                {
                    key = envKeyValue;
                }
                return key;
            }
        }

        public static string Decrypt(string cipherText, string publicKey)
        {
            if (cipherText is not { Length: > 0 })
                throw new ArgumentNullException(nameof(cipherText));
            if (PrivateKey is not { Length: > 0 })
                throw new ArgumentNullException(nameof(PrivateKey));
            if (publicKey is not { Length: > 0 })
                throw new ArgumentNullException(nameof(publicKey));

            using var aesAlg = Aes.Create();
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Key = CreateAesKey(PrivateKey);
            aesAlg.IV = Convert.FromBase64String(publicKey);

            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText));
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);
            var plaintext = srDecrypt.ReadToEnd();

            return plaintext;
        }

        public static string Encrypt(string plainText, string publicKey)
        {
            if (plainText is not { Length: > 0 })
                throw new ArgumentNullException(nameof(plainText));
            if (PrivateKey is not { Length: > 0 })
                throw new ArgumentNullException(nameof(PrivateKey));
            if (publicKey is not { Length: > 0 })
                throw new ArgumentNullException(nameof(publicKey));

            byte[] encrypted;

            using (var aesAlg = Aes.Create())
            {
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Key = CreateAesKey(PrivateKey);
                aesAlg.IV = Convert.FromBase64String(publicKey);
                //aesAlg.GenerateKey();
                //aesAlg.GenerateIV();

                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

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

        public static byte[] GenerateRandomPublicKey()
        {
            var iv = new byte[16]; // AES > IV > 128 bit
            iv = RandomNumberGenerator.GetBytes(iv.Length);
            return iv;
        }

        private static byte[] CreateAesKey(string inputString)

        {
            return Encoding.UTF8.GetByteCount(inputString) == 32 ? Encoding.UTF8.GetBytes(inputString) : SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }
    }
}