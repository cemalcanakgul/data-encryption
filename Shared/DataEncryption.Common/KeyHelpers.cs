using System.Security.Cryptography;
using System.Text;

namespace DataEncryption.Common
{
    public static class KeyHelpers
    {
        public static byte[] GenerateRandomPublicKey(SymmetricAlgorithms cipherAlgorithm)
        {
            int length;
            switch (cipherAlgorithm)
            {
                case SymmetricAlgorithms.AES_128:
                case SymmetricAlgorithms.AES_192:
                case SymmetricAlgorithms.AES_256:
                    length = 16;
                    break;

                case SymmetricAlgorithms.DES:
                case SymmetricAlgorithms.TRIPLE_DES_112:
                case SymmetricAlgorithms.TRIPLE_DES_168:
                case SymmetricAlgorithms.TRIPLE_DES_192:
                case SymmetricAlgorithms.RC2:
                    length = 8;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(cipherAlgorithm), cipherAlgorithm, null);
            }

            var iv = new byte[length];
            iv = RandomNumberGenerator.GetBytes(iv.Length);
            return iv;
        }

        public static byte[] CreateKey(SymmetricAlgorithms cipherAlgorithm, string inputString)
        {
            int keySize;
            switch (cipherAlgorithm)
            {
                case SymmetricAlgorithms.AES_128:
                    keySize = 128;
                    break;

                case SymmetricAlgorithms.AES_192:
                    keySize = 192;
                    break;

                case SymmetricAlgorithms.AES_256:
                    keySize = 256;
                    break;

                case SymmetricAlgorithms.DES:
                    keySize = 64;
                    break;

                case SymmetricAlgorithms.TRIPLE_DES_112:
                    keySize = 112;
                    break;

                case SymmetricAlgorithms.TRIPLE_DES_168:
                    keySize = 168;
                    break;

                case SymmetricAlgorithms.TRIPLE_DES_192:
                    keySize = 192;
                    break;

                case SymmetricAlgorithms.RC2:
                    keySize = 128;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(cipherAlgorithm), cipherAlgorithm, null);
            }

            if (string.IsNullOrEmpty(inputString))
            {
                throw new ArgumentException("Input string cannot be null or empty.");
            }

            var keyBytes = Encoding.UTF8.GetBytes(inputString);
            var desiredByteLength = keySize / 8;
            var hashBytes = SHA256.Create().ComputeHash(keyBytes);

            if (desiredByteLength > hashBytes.Length)
            {
                throw new ArgumentException($"Input string is too short for {keySize}-bit key.");
            }

            var finalKey = new byte[desiredByteLength];
            Array.Copy(hashBytes, finalKey, desiredByteLength);

            if (cipherAlgorithm == SymmetricAlgorithms.TRIPLE_DES_112) // TripleDES_112
            {
                finalKey[14] = finalKey[0];
                finalKey[15] = finalKey[1];
            }

            return finalKey;
        }
    }
}