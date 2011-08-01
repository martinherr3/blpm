using System;
using System.Text.RegularExpressions;

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

        //public static bool esHoraValida(string cadena)
        //{
        //    string[] arr = cadena.Split(':');
        //    if (arr.GetLength(0) < 2)
        //    {
        //        return false;
        //    }
        //    else if (!.IsNumeric(arr[0]))
        //    {
        //        return false;
        //    }
        //    else if (!Information.IsNumeric(arr[1]))
        //    {
        //        return false;
        //    }
        //    else if (!Information.IsNumeric(arr[2]))
        //    {
        //        return false;
        //    }
        //    else if (arr[0] < 0 | arr[0] > 23)
        //    {
        //        return false;
        //    }
        //    else if (arr[1] < 0 | arr[0] > 59)
        //    {
        //        return false;
        //    }
        //    else if (arr[2] < 0 | arr[0] > 59)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
    }
}