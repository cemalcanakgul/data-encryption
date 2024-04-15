namespace DataEncryption.Abstraction
{
    public interface IEncryptionHelper
    {
        static abstract string Decrypt(string cipherText, string publicKey);

        static abstract string Encrypt(string plainText, string publicKey);
    }
}