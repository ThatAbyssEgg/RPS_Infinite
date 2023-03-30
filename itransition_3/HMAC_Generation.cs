using System;
using System.Security.Cryptography;
using System.Text;

namespace itransition_3
{
    internal class HMAC_Generation
    {
        public static byte[] GenerateKey()
        {
            return RandomNumberGenerator.GetBytes(256);
        }
        public static string GetHMAC(string text, byte[] key)
        {
            using (var hmacsha256 = new HMACSHA256(key))
            {
                var hash = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                string convertedHash = Convert.ToHexString(hash);
                Console.WriteLine($"HMAC: {convertedHash}");
                return convertedHash;
            }
        }
        
    }
}
