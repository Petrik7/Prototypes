using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Security.Cryptography;

namespace WebAppProject
{
    public class Authentification
    {
        public static byte[] MakeEncriptedSaltedPassword(string password, byte[] salt)
        {
            Validator.ThrowIfNullOrEmpty<ArgumentNullException>(password, "password cannot be null o empty");
            Validator.ThrowIfNull<ArgumentNullException>(password, "salt cannot be null o empty");

            byte[] saltedPassword = new byte[salt.Length + password.Length];
            byte[] encriptedSaltedPassword = null;

            using (MemoryStream saltedPasswordStream = new MemoryStream())
            {
                using (StreamWriter streamWriter = new StreamWriter(saltedPasswordStream, System.Text.Encoding.BigEndianUnicode))
                {
                    streamWriter.Write(password);
                    streamWriter.Write(salt);
                    streamWriter.Flush();
                    saltedPasswordStream.Seek(0, SeekOrigin.Begin);

                    SHA1 sha = new SHA1CryptoServiceProvider();
                    encriptedSaltedPassword = sha.ComputeHash(saltedPasswordStream);
                }
            }
            return encriptedSaltedPassword;
        }

        public static bool PassworsAreEqual(byte[] password1, byte[] password2)
        {
            if (password1.Length != password2.Length)
                return false;

            for (int i = 0; i < password1.Length; ++i)
                if (password1[i] != password2[i])
                    return false;

            return true;
        }
    }
}