using System.Security.Cryptography;
using DataEncryption.Abstraction;
using DataEncryption.Common;

namespace AESEncryption
{
    public class EncryptionHelper : IEncryptionHelper
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

        private static readonly SymmetricAlgorithmHelper AlgorithmHelper;

        static EncryptionHelper()
        {
            AlgorithmHelper = new SymmetricAlgorithmHelper(PrivateKey);
        }

        public static string Decrypt(string cipherText, string publicKey)
        {
            return AlgorithmHelper.Decrypt(SymmetricAlgorithms.AES_256, cipherText, publicKey);
        }

        public static string Encrypt(string plainText, string publicKey)
        {
            return AlgorithmHelper.Encrypt(SymmetricAlgorithms.AES_256, plainText, publicKey);
        }
    }
}