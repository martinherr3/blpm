using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Promethee.Scripts
{
    public static class UIUtility
    {
        /// <summary>
        /// Eliminars the archivos session.
        /// </summary>
        /// <param name="sessionID">The session ID.</param>
        public static void EliminarArchivosSession(string sessionID)
        {
            GC.WaitForPendingFinalizers();
            GC.Collect();
            string TmpPath = System.Configuration.ConfigurationManager.AppSettings["oTmpPath"];
            string ImgPath = System.Configuration.ConfigurationManager.AppSettings["oImgPath"];
            var archivo = "*" + sessionID + "*.png";
            if (Directory.Exists(TmpPath))
            {
                foreach (string item in Directory.GetFiles(TmpPath, archivo, SearchOption.TopDirectoryOnly))
                {
                    File.Delete(item);
                }
                archivo = "Podio_" + sessionID + "*.png";
            }
            if (Directory.Exists(ImgPath))
            {
                foreach (string item in Directory.GetFiles(ImgPath, archivo, SearchOption.TopDirectoryOnly))
                {
                    File.Delete(item);
                }
                if (HttpContext.Current != null)
                {
                    ImgPath = "~/Images/TMP";
                    string miruta = HttpContext.Current.Server.MapPath(ImgPath).ToString();
                    foreach (string item in Directory.GetFiles(miruta, archivo, SearchOption.TopDirectoryOnly))
                    {
                        File.Delete(item);
                    }
                }
            }
        }
    }
}