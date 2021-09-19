using System;
using System.Security.Cryptography;
using System.Text;

namespace GeekForLess_TestTask_Forum.Static
{
    public static class Hash
    {
        public static string ToSHA1(this string input)
        {
            using (var sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (var b in hash) sb.Append(b.ToString("X2"));

                return sb.ToString();
            }
        }

        public static string ToBase64(this string input)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string ToStringFromBase64(this string input)
        {
            var base64EncodedBytes = Convert.FromBase64String(input);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}