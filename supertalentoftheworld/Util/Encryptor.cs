//Downloaded from
//Visual C# Kicks - http://vcskicks.com/

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Encryptor
{
    internal class Encryptor
    {
        public static byte[] Encrypt(byte[] input, string password)
        {
            try
            {
                var service = new TripleDESCryptoServiceProvider();
                var md5 = new MD5CryptoServiceProvider();

                byte[] key = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
                byte[] iv = md5.ComputeHash(Encoding.ASCII.GetBytes(password));

                return Transform(input, service.CreateEncryptor(key, iv));
            }
            catch (Exception)
            {
                return new byte[0];
            }
        }

        public static byte[] Decrypt(byte[] input, string password)
        {
            try
            {
                var service = new TripleDESCryptoServiceProvider();
                var md5 = new MD5CryptoServiceProvider();

                byte[] key = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
                byte[] iv = md5.ComputeHash(Encoding.ASCII.GetBytes(password));

                return Transform(input, service.CreateDecryptor(key, iv));
            }
            catch (Exception)
            {
                return new byte[0];
            }
        }

        public static string Encrypt(string text, string password)
        {
            byte[] input = Encoding.UTF8.GetBytes(text);
            byte[] output = Encrypt(input, password);
            return Convert.ToBase64String(output);
        }

        public static string Decrypt(string text, string password)
        {
            byte[] input = Convert.FromBase64String(text);
            byte[] output = Decrypt(input, password);
            return Encoding.UTF8.GetString(output);
        }

        private static byte[] Transform(byte[] input, ICryptoTransform CryptoTransform)
        {
            var memStream = new MemoryStream();
            var cryptStream = new CryptoStream(memStream, CryptoTransform, CryptoStreamMode.Write);

            cryptStream.Write(input, 0, input.Length);
            cryptStream.FlushFinalBlock();

            memStream.Position = 0;
            var result = new byte[Convert.ToInt32(memStream.Length)];
            memStream.Read(result, 0, Convert.ToInt32(result.Length));

            memStream.Close();
            cryptStream.Close();

            return result;
        }
    }
}