using System;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;

namespace EDUAR_Utility.Utilidades
{
    [Serializable]
    public class EDUARUtilidades
    {
        /// <summary>
        /// Función que devuelve un true o false si la cadena enviada por parámetro es un email o no.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool EsEmailValido(string email)
        {
            string expresion;
            expresion = @"^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            { return false; }
        }

		/// <summary>
		/// 
		/// </summary>
		public static class Helper
		{
			/// <summary>
			/// Encodes the password.
			/// </summary>
			/// <param name="originalPassword">The original password.</param>
			/// <returns></returns>
			public static string EncodePassword(string originalPassword)
			{
				SHA1 sha1 = new SHA1CryptoServiceProvider();

				byte[] inputBytes = (new UnicodeEncoding()).GetBytes(originalPassword);
				byte[] hash = sha1.ComputeHash(inputBytes);

				return Convert.ToBase64String(hash);
			}
		}
    }
}