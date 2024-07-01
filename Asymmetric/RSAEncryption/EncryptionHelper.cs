using System.Security.Cryptography;
using System.Text;
using DataEncryption.Common;

namespace RSAEncryption
{
    public class EncryptionHelper
    {
        private static readonly AsymmetricAlgorithmHelper AlgorithmHelper;

        static EncryptionHelper()
        {
            AlgorithmHelper = new AsymmetricAlgorithmHelper();
        }

        public static string Decrypt(string cipherText)
        {
            return AlgorithmHelper.Decrypt(AsymmetricAlgorithms.RSA, cipherText);
        }

        public static string Encrypt(string plainText)
        {
            return AlgorithmHelper.Encrypt(AsymmetricAlgorithms.RSA, plainText);
        }
    }
}