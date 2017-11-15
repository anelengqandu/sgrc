using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sgrc.Encrypt
{
    public class HashCreator
    {
        public static string genHashFromGuid(string prefix, int? limit = null)
        {
            string plainText = prefix + Guid.NewGuid().ToString();
            string hash = SimpleHash.ComputeHash(plainText, SimpleHashAlgorithm.MD5, null);

            if (limit.HasValue && limit > 0)
                if (hash.Length > 50)
                    hash = hash.Remove(49);
            return hash;
        }

        public static string genHash(string UniqueValue)
        {
            string plainText = UniqueValue;
            string hash = SimpleHash.ComputeHash(plainText, SimpleHashAlgorithm.MD5, null);
            return hash;
        }

        public static string getUserVerifyEmailHash()
        {
            string plainText = "ve" + Guid.NewGuid().ToString();
            string hash = SimpleHash.ComputeHash(plainText, SimpleHashAlgorithm.MD5, null);

            if (hash.Length > 50)
                hash = hash.Remove(49);
            return hash;
        }

        /// <summary>
        /// Gets a hash value for use in invite user email
        /// </summary>
        /// <returns>a unique string</returns>
        /// <author>MI Laher</author>
        /// <created>2013-12-08</created>
        public static string getInviteUserEmailHash()
        {
            string plainText = "iu" + Guid.NewGuid().ToString();
            string hash = SimpleHash.ComputeHash(plainText, SimpleHashAlgorithm.MD5, null);

            if (hash.Length > 50)
                hash = hash.Remove(49);
            return hash;
        }

        /// <summary>
        /// Gets a hash2 value for use in invite user email (smaller hash used for verification)
        /// </summary>
        /// <returns>a unique string</returns>
        /// <author>MI Laher</author>
        /// <created>2013-12-08</created>
        public static string getInviteUserEmailHash2()
        {
            string plainText = "iv" + Guid.NewGuid().ToString();
            string hash = SimpleHash.ComputeHash(plainText, SimpleHashAlgorithm.MD5, null);

            if (hash.Length > 20)
                hash = hash.Remove(19);
            return hash;
        }

        public static bool verifyHash(string UniqueValue, string UniqueValueHashed)
        {
            return SimpleHash.VerifyHash(UniqueValue, SimpleHashAlgorithm.MD5, UniqueValueHashed);
        }
    }
}
