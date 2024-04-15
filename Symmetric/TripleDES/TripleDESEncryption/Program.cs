using DataEncryption.Common;
using TripleDESEncryption;

var original = "Here is some data to encrypt!";

var publicKey = Convert.ToBase64String(KeyHelpers.GenerateRandomPublicKey(SymmetricAlgorithms.TRIPLE_DES_192));

var encrypted = EncryptionHelper.Encrypt(original, publicKey);

var roundtrip = EncryptionHelper.Decrypt(encrypted, publicKey);

Console.WriteLine("Original        : {0}", original);  // Here is some data to encrypt!
Console.WriteLine("Public Key      : {0}", publicKey); // ac9+kZjrhKo=
Console.WriteLine("Encrypted (b64) : {0}", encrypted); // iuwJZNkLgzD9ptD4TI1XpWXQE3nP2IjlqvccxkiemfI=
Console.WriteLine("Decrypted       : {0}", roundtrip); // Here is some data to encrypt!