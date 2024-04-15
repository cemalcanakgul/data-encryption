using DataEncryption.Common;
using DESEncryption;

var original = "Here is some data to encrypt!";

var publicKey = Convert.ToBase64String(KeyHelpers.GenerateRandomPublicKey(SymmetricAlgorithms.DES));

var encrypted = EncryptionHelper.Encrypt(original, publicKey);

var roundtrip = EncryptionHelper.Decrypt(encrypted, publicKey);

Console.WriteLine("Original        : {0}", original);  // Here is some data to encrypt!
Console.WriteLine("Public Key      : {0}", publicKey); // J8T9+LgVwME=
Console.WriteLine("Encrypted (b64) : {0}", encrypted); // 9TgfAxHVs7wxt1+4WyiUk1yEH70Ohg12RBFpBmsA/Og=
Console.WriteLine("Decrypted       : {0}", roundtrip); // Here is some data to encrypt!