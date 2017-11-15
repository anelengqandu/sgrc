using System.Web.Security;

namespace sgrc.Encrypt
{
    public class PasswordManager
    {
        public static string Encrypt(string password)
        {
            return SimpleHash.ComputeHash(password, SimpleHashAlgorithm.SHA512, null);
        }

        public static string SecurityToken(string token)
        {
            return SimpleHash.ComputeHash(token, SimpleHashAlgorithm.SHA512, null).Replace("/","");
        }

        public static bool Verify(string password, string passwordHashed)
        {
            return SimpleHash.VerifyHash(password, SimpleHashAlgorithm.SHA512, passwordHashed);
        }

        public static string Generate(int characters, int nonAlphanumericCharacter)
        {
            var password = Membership.GeneratePassword(characters, nonAlphanumericCharacter);
            return Encrypt(password);
        }
    }
}
