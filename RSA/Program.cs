using System;
using System.Numerics;
using System.Text;

namespace RSA
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var keys = RSAEncryptor.GenerateKeys();
            Console.WriteLine("Public: " + keys.publicKey);
            Console.WriteLine("Private: " + keys.privateKey);

            BigInteger encrypted = RSAEncryptor.Encrypt("hi");
            Console.WriteLine("Encrypted: " + encrypted.ToString());
            string decrypted = RSAEncryptor.Decrypt(encrypted);
            Console.WriteLine("Decrypted: " + decrypted);
        }
    }
}
