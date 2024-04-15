using System.Security.Cryptography;
using DataEncryption.Abstraction;
using DataEncryption.Common;

namespace TripleDESEncryption
{
    public class EncryptionHelper : IEncryptionHelper
    {
        private static string PrivateKey
        {
            get
            {
                var key = "3fb7fe5dbb0643caa984f53d";

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
            return AlgorithmHelper.Decrypt(SymmetricAlgorithms.TRIPLE_DES_192, cipherText, publicKey);
        }

        public static string Encrypt(string plainText, string publicKey)
        {
            return AlgorithmHelper.Encrypt(SymmetricAlgorithms.TRIPLE_DES_192, plainText, publicKey);
        }
    }
}