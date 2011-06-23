using System;
using System.Diagnostics;
using System.IO;

namespace EDUAR_Utility.Utilidades
{
    /// <summary>
    /// Clase encargada de manejar los logs de la Aplicacion.
    /// </summary>
    public class EDUARLog
    {
        ///<summary>
        ///Ruta donde se guardará el log. 
        ///</summary>
        private String oPath;
        ///<summary>
        ///Permite activar/desactivar el registro de sucesos.
        ///</summary>
        ///<remarks></remarks>
        public Boolean grabacionActivada;

        #region --[Constructor]--
        /// <summary>
        /// Constructor con la ruta donde se guardarán los logs.
        ///</summary>
        /// <param name="path">No tiene ninguna función. Se ha dejado por temas de compatibilidad.</param>
        public EDUARLog(String path)
        {
            new EDUARLog(path, true);
        }

        /// <summary>
        /// Constructor que permite activar/desactivar el registro de logs y la ruta donde se guardarán los logs.
        /// </summary>
        /// <param name="path">Ubicación donde se grabará el log.</param>
        /// <param name="grabaLog">Permite activar/desactivar el Log.</param>
        public EDUARLog(String path, Boolean grabaLog)
        {
            oPath = path;
            grabacionActivada = grabaLog;
        }
        #endregion

        /// <summary>
        /// Método que guarda la cadena en un log.
        /// </summary>
        /// <param name="cadena">Cadena que vamos a guardar en el log.</param>
        /// <remarks></remarks>
        public void write(String cadena)
        {
            write(cadena, FileMode.Append);
        }

        /// <summary>
        /// Método que guarda la cadena en un log indicando el modo de acceso al fichero.
        /// </summary>
        /// <param name="cadena">Cadena que vamos a guardar en el log.</param>
        /// <param name="pFileMode">No tiene ninguna función. Se ha dejado por temas de compatibilidad.</param>
        /// <remarks>GrupoIberostar.Facilities no permite seleccionar el modo de acceso al fichero, se mantiene por temas de compatibilidad.</remarks>
        public void write(string cadena, FileMode pFileMode)
        {
            try
            {
                FileStream fs;
                StreamWriter swWriter;

                fs = new FileStream(oPath, pFileMode, FileAccess.Write);
                swWriter = new StreamWriter(fs);

                //Escritura dentro del log
                swWriter.WriteLine(cadena);
                //Cerramos los objetos
                swWriter.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                //No podemos guardar este error, ya que está fallando al guardar el error!
                Debug.WriteLine("Se ha producido el siguiente error al guardar el log " + ex.Message);
            }
        }
    }
}
