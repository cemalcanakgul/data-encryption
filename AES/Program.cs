using AESEncryption;

var original = "Here is some data to encrypt!";

var publicKey = Convert.ToBase64String(AesExample.GenerateRandomPublicKey());

var encrypted = AesExample.Encrypt(original, publicKey);

var roundtrip = AesExample.Decrypt(encrypted, publicKey);

Console.WriteLine("Original        : {0}", original);  // Here is some data to encrypt!
Console.WriteLine("Public Key      : {0}", publicKey); // Xrzpl5vUxYvsXvfZ3pNpdQ==
Console.WriteLine("Encrypted (b64) : {0}", encrypted); // n1v/sPFxALgH0SS+b4XAXqbcROk5Ti2ILML+QrpVMS0=
Console.WriteLine("Decrypted       : {0}", roundtrip); // Here is some data to encrypt!
