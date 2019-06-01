using System;
using System.Text;
using System.Numerics;

namespace RSA
{
    public static class RSAEncryptor
    {

        public static ((long e, long n) publicKey, (long d, long n) privateKey) KeyInstances;

        public static bool IsPrime(int n)
        {
            for (int i = 2; i < Math.Sqrt(n); i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static int RandomPrime(int min, int max, Random randy)
        {
            while (true)
            {
                int number = randy.Next(min, max);
                if (IsPrime(number)) return number;
            }
        }

        public static long GCD(long x, long y)
        {
            return y == 0 ? x : GCD(y, x % y);
        }

        public static ((long e, long n) publicKey, (long d, long n) privateKey) GenerateKeys()
        {
            Random randy = new Random();

            long p = RandomPrime(100000, 500000, randy);
            long q = RandomPrime(100000, 500000, randy);
            
            Console.WriteLine("p: " + p.ToString());
            Console.WriteLine("q: " + q.ToString());
            long n = p * q;
            Console.WriteLine("n: " + n.ToString());
            long phi = (p - 1) * (q - 1);
            Console.WriteLine("phi: " + phi.ToString());
            long e;

            for (e = 2; e < phi - 1; e++)
            {
                if (GCD(e, phi) == 1) break;
            }
            Console.WriteLine("e: " + e.ToString());
            int k = 3;
            long d;
            while (true)
            {
                double dTemp = (double)(1 + (k * phi)) / e;
                if (dTemp % 1 == 0)
                {
                    d = (long)dTemp;
                    break;
                }
                k++;
            }

            Console.WriteLine("k: " + k.ToString());

            Console.WriteLine("d: " + d.ToString());
            KeyInstances = ((e, n), (d, n));
            return ((e, n), (d, n));
        }

        public static BigInteger Encrypt(string message)
        {
            return Encrypt(message, KeyInstances.publicKey);
        }



        public static BigInteger Encrypt(string message, (long e, long n) publicKey)
        {
        
            BigInteger result = new BigInteger(Encoding.Default.GetBytes(message));
            Console.WriteLine("Unencrypted bigint: " + result.ToString());
            result = BigInteger.ModPow(result, publicKey.e, publicKey.n);

            return result;

        }

        public static string Decrypt(BigInteger message)
        {
            return Decrypt(message, KeyInstances.privateKey);
        }


        public static string Decrypt(BigInteger message, (long d, long n) privateKey)
        {
        
            BigInteger result = BigInteger.ModPow(message, privateKey.d, privateKey.n);
            Console.WriteLine("Decrypted bigint: " + result.ToString());
            return Encoding.Default.GetString(result.ToByteArray());


        }
    }
}
