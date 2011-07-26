using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
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

        /// <summary>
        /// Devuelve todas las propiedades de un objeto en formato string
        /// como un diccionario donde la clave es el nombre de la propiedad.
        /// </summary>
        /// <param name=”o”>Cualquier objeto.</param>
        /// <returns>Diccionario con la colección de propiedades.</returns>
        public static Dictionary<string, string> GetPropiedades(object o)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            foreach (MemberInfo mi in o.GetType().GetMembers())
            {
                if (mi.MemberType == MemberTypes.Property)
                {
                    PropertyInfo pi = mi as PropertyInfo;
                    if (pi != null)
                    {
                        result.Add(pi.Name, pi.GetValue(o, null).ToString());
                    }
                }
            }
            return result;
        }
    }
}