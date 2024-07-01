using RSAEncryption;

var original = "Here is some data to encrypt!";

var encrypted = EncryptionHelper.Encrypt(original);

var roundtrip = EncryptionHelper.Decrypt(encrypted);

Console.WriteLine("Original        : {0}", original);  // Here is some data to encrypt!
Console.WriteLine("Encrypted (b64) : {0}", encrypted); // FgO5ik5OiZPSrRwXhgKOuLorVH2YhlV8gOP/XNrdTy27SPh9GqB4NpBQUYzBf/RSmviVxUSKUQOrtsrdV+WDwX7diblmQc28/Ick0+wRPMNs6Y76QGXI047VBvHdq68FZewsxOD2QAHl0qQjEnIFKXfqKjDG8zqVq2bHCA7PUJ/kZp0rSjMTU3AG771wyYvlEgFNNbg0LzJcHd8LJ5G+MpEgKvKuz1r6vl1ufOWrHp/AcrSxYJGxn5LQfAfuObIPPxArxgDFaAXMh5PkIRCqwb10WzfdZvuD5ZIiPDuMq6ryAZPDxdAi2KGN0d7ECkrJUDrFSjKR3vqVlVxxOZIYqQ==
Console.WriteLine("Decrypted       : {0}", roundtrip); // Here is some data to encrypt!