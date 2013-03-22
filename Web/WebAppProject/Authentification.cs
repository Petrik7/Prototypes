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
        public static byte[] MakeEncriptedSoltedPassword(string password, byte[] solt)
        {
            Validator.ThrowIfNullOrEmpty<ArgumentNullException>(password, "password cannot be null o empty");
            Validator.ThrowIfNull<ArgumentNullException>(password, "solt cannot be null o empty");

            byte[] soltedPassword = new byte[solt.Length + password.Length];
            byte[] encriptedSoltedPassword = null;

            using (MemoryStream soltedPasswordStream = new MemoryStream())
            {
                using (StreamWriter streamWriter = new StreamWriter(soltedPasswordStream, System.Text.Encoding.BigEndianUnicode))
                {
                    streamWriter.Write(password);
                    streamWriter.Write(solt);
                    streamWriter.Flush();
                    soltedPasswordStream.Seek(0, SeekOrigin.Begin);

                    SHA1 sha = new SHA1CryptoServiceProvider();
                    encriptedSoltedPassword = sha.ComputeHash(soltedPasswordStream);
                }
            }
            return encriptedSoltedPassword;
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