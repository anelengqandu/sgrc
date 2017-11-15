using System;

namespace sgrc.DikizaCS.DAL.Utils
{
    public class PasswordGenerator
    {

        public static int PasswordLength(Random number)
        {
            return number.Next(4, 8);
        }

        public static string GeneratePassword(int length)
        {
            string stringsAllowed = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string otherAllowed = "123456897";
            char[] chars = new char[length];
            Random ran = new Random();
            bool useLetters = true;
            for (int i = 0; i < length; i++)
            {
                if (useLetters)
                {
                    chars[i] = stringsAllowed[ran.Next(0, stringsAllowed.Length)];
                    useLetters = false;
                }
                else
                {
                    chars[i] = otherAllowed[ran.Next(0, otherAllowed.Length)];
                    useLetters = true;
                }
            }
            return new string(chars);
        }

    }
}
