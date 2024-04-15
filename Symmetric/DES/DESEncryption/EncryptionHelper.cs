using System.Security.Cryptography;
using DataEncryption.Abstraction;
using DataEncryption.Common;

namespace DESEncryption
{
    public class EncryptionHelper : IEncryptionHelper
    {
        private static string PrivateKey
        {
            get
            {
                var key = "3fb7fe5";

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
            return AlgorithmHelper.Decrypt(SymmetricAlgorithms.DES, cipherText, publicKey);
        }

        public static string Encrypt(string plainText, string publicKey)
        {
            return AlgorithmHelper.Encrypt(SymmetricAlgorithms.DES, plainText, publicKey);
        }
    }
}