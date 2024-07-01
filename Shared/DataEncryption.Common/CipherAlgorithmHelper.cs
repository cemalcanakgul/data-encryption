using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataEncryption.Common
{
    public class CipherAlgorithmHelper
    {
        public static SymmetricAlgorithm GetSymmetricAlgorithm(SymmetricAlgorithms encryptionKeyEncryptor)
        {
            switch (encryptionKeyEncryptor)
            {
                case SymmetricAlgorithms.AES_128:
                case SymmetricAlgorithms.AES_192:
                case SymmetricAlgorithms.AES_256:
                    return new RijndaelManaged();

                case SymmetricAlgorithms.DES:
                    return new DESCryptoServiceProvider();

                case SymmetricAlgorithms.TRIPLE_DES_112:
                case SymmetricAlgorithms.TRIPLE_DES_168:
                case SymmetricAlgorithms.TRIPLE_DES_192:
                    return new TripleDESCryptoServiceProvider();

                case SymmetricAlgorithms.RC2:
                    return new RC2CryptoServiceProvider();

                default:
                    throw (new NotSupportedException($"Unsupported Cipher Algorithm: {encryptionKeyEncryptor}"));
            }
        }

        public static AsymmetricAlgorithm GetAsymmetricAlgorithm(AsymmetricAlgorithms encryptionKeyEncryptor)
        {
            switch (encryptionKeyEncryptor)
            {
                case AsymmetricAlgorithms.RSA:
                    return RSA.Create();

                case AsymmetricAlgorithms.DSA:
                    return DSA.Create();

                default:
                    throw (new NotSupportedException($"Unsupported Cipher Algorithm: {encryptionKeyEncryptor}"));
            }
        }
    }
}