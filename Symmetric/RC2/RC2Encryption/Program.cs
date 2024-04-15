using DataEncryption.Common;
using RC2Encryption;

var original = "Here is some data to encrypt!";

var publicKey = Convert.ToBase64String(KeyHelpers.GenerateRandomPublicKey(SymmetricAlgorithms.RC2));

var encrypted = EncryptionHelper.Encrypt(original, publicKey);

var roundtrip = EncryptionHelper.Decrypt(encrypted, publicKey);

Console.WriteLine("Original        : {0}", original);  // Here is some data to encrypt!
Console.WriteLine("Public Key      : {0}", publicKey); // LGf2a6zw28g=
Console.WriteLine("Encrypted (b64) : {0}", encrypted); // PejbFUSCf/oWJIxz0lfxK2LOQkp5H/cn6cs+IT04iGY=
Console.WriteLine("Decrypted       : {0}", roundtrip); // Here is some data to encrypt!