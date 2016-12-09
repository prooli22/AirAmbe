// Nom : Olivier Provost.
// Date : 2016-12-09.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AirAmbe
{
    public static class MD5
    {
        /// <summary>
        /// Permet d'encoder une String en MD5.
        /// </summary>
        /// <param name="input"> Mot de passe à encoder en MD5. </param>
        /// <returns> Retourne le mot de passe encodé en MD5. </returns>
        public static string Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }

            return hash.ToString();
        }
    }
}
