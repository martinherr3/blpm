using System;
using System.ServiceModel;
using System.Text;
using System.Web.UI;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Utilidades;


namespace EDUAR_UI.UserControls
{
    public partial class VentanaInfo : UserControl
    {
        #region --[Propiedades]--
        /// <summary>
        /// Indica que tipo de ventana se va a mostrar.
        /// </summary>
        public enumTipoVentanaInformacion TipoVentana { get; set; }

        /// <summary>
        /// Mensaje que se mostrará en la ventana emergente.
        /// </summary>
        public String Detalle
        {
            set { lblDetalleSys.Text = value; }
        }

        /// <summary>
        /// Titulo que tendrá la ventana emergente. 
        /// </summary>
        public String Titulo
        {
            set { lblTitulo.Text = value; }
        }

        #endregion

        #region --[Eventos]--
        /// <summary>
        /// Evento que se ejecuta al cargar el control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            btnAceptar.Click += (Aceptar);
            btnCancelar.Click += (Cancelar);
        }

        void Aceptar(object sender, EventArgs e)
        {
            OnVentanaClick(VentanaAceptarClick, e);
        }

        void Cancelar(object sender, EventArgs e)
        {
            OnVentanaClick(VentanaCancelarClick, e);
        }
        #endregion

        #region --[Métodos Publicos]--
        /// <summary>
        /// Oculta la ventana.
        /// </summary>
        public void OcultarMensaje()
        {
            try { Visible = false; }
            catch (Exception ex) { GestionExcepciones(ex); }
        }

        /// <summary>
        /// Mostrar la ventana.
        /// </summary>
        public void MostrarMensaje()
        {
            try { CargarPresentacion(); }
            catch (Exception ex) { GestionExcepciones(ex); }
        }

        /// <summary>
        /// Guarda un log con la Excepcion y luego levanta una ventana emergente. 
        /// </summary>
        /// <param name="ex"></param>
        public void GestionExcepciones(Exception ex)
        {
            try
            {
                #region Manejo de Directorio de LOG
                Boolean oLogActivo = Boolean.Parse(System.Configuration.ConfigurationManager.AppSettings["oLogActivo"]);

                if (!oLogActivo)
                    return;

                String logPath = System.Configuration.ConfigurationManager.AppSettings["oLogPath"];
                String logNombre = System.Configuration.ConfigurationManager.AppSettings["oLogNombre"];

                //Crea el directorio.
                if (!System.IO.Directory.Exists(logPath))
                    System.IO.Directory.CreateDirectory(logPath);

                String oLogPath = String.Format("{0}\\{1}", logPath, logNombre);

                //Crea el archivo.
                if (!System.IO.File.Exists(oLogPath))
                    System.IO.File.CreateText(oLogPath);

                #endregion

                //Variables
                string informacion = String.Empty;
                string message;
                string source;
                string stackTrace;

                //Log
                StringBuilder msgLog = new StringBuilder();
                msgLog.AppendLine("*********************************************************************************");

                //Validacion sobre GenericException
                string exceptionName = ex.GetType().FullName;
                if ((exceptionName.Contains("GenericException")))
                {
                    #region "GenericException"
                    GenericException genericEx = ((FaultException<GenericException>)ex).Detail;
                    switch (genericEx.ExceptionType)
                    {
                        case enuExceptionType.SqlException:
                            Titulo = "Error en Base de Datos";
                            Detalle = "Se ha producido un error al realizar una acción en la Base de Datos.";
                            break;
                        case enuExceptionType.ServicesException:
                            Titulo = "Error en Servicio";
                            Detalle = "Se ha producido un error al realizar la consulta al Servicio.";
                            break;
                        case enuExceptionType.BusinessLogicException:
                            Titulo = "Error en Negocio";
                            Detalle = "Se ha producido un error al realizar una acción en el negocio.";
                            break;
                        default:
                            Titulo = "Error en la Aplicación";
                            Detalle = "Se ha producido un error interno en la aplicación.";
                            break;
                    }

                    informacion = String.Format("{0} {1}", genericEx.Message, genericEx.Informacion);
                    message = genericEx.Message;
                    source = genericEx.Source;
                    stackTrace = genericEx.StackTrace;
                    #endregion

                    msgLog.AppendFormat("{0} - {1}", DateTime.Now, genericEx.ExceptionType);
                }
                else
                {
                    #region Excepcion no controlada
                    message = ex.Message;
                    source = ex.Source;
                    stackTrace = ex.StackTrace;

                    Titulo = "Error en la Aplicación";
                    Detalle = "Se ha producido un error interno en la aplicación.";
                    #endregion
                    msgLog.AppendFormat("{0} - {1}", DateTime.Now, enuExceptionType.Exception);
                }

                //Borrar al realizar la entraga
                //Detalle = message + "<br/>" + source + "<br/>" + stackTrace;
                //FIN Borrar al realizar la entraga

                #region Guarda la Info en el Log
                //Informacion
                msgLog.AppendLine();
                msgLog.Append(String.Format("Informacion: {0}", informacion));
                msgLog.AppendLine();
                //Message
                msgLog.Append(String.Format("Message: {0}", message));
                msgLog.AppendLine();
                //Source
                msgLog.Append(String.Format("Source: {0}", source));
                msgLog.AppendLine();
                //StackTrace
                msgLog.Append(String.Format("StackTrace: {0}", stackTrace));
                msgLog.AppendLine();

                msgLog.AppendLine();
                msgLog.AppendLine("*********************************************************************************");

                EDUARLog objLog = new EDUARLog(oLogPath, true);
                objLog.write(msgLog.ToString());
                #endregion
                CargarPresentacion();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Guarda un log con la informacion recibida por parametro.
        /// </summary>
        /// <param name="exepcionControlada"></param>
        public void GestionExcepcionesLog(Exception exepcionControlada)
        {
            try
            {
                #region Manejo de Directorio de LOG

                Boolean oLogActivo = Boolean.Parse(System.Configuration.ConfigurationManager.AppSettings["oLogActivo"]);

                if (!oLogActivo)
                    return;

                String logPath = System.Configuration.ConfigurationManager.AppSettings["oLogPath"];
                String logNombre = System.Configuration.ConfigurationManager.AppSettings["oLogNombre"];

                //Crea el directorio.
                if (!System.IO.Directory.Exists(logPath))
                    System.IO.Directory.CreateDirectory(logPath);

                String oLogPath = String.Format("{0}\\{1}", logPath, logNombre);

                //Crea el archivo.
                if (!System.IO.File.Exists(oLogPath))
                    System.IO.File.CreateText(oLogPath);

                #endregion

                //Log
                StringBuilder msgLog = new StringBuilder();
                msgLog.AppendLine("*********************************************************************************");
                msgLog.AppendFormat("{0} - {1}", DateTime.Now, enuExceptionType.Exception);
                //Message
                msgLog.Append(String.Format("Message: {0}", exepcionControlada.Message));
                msgLog.AppendLine();
                //Source
                msgLog.Append(String.Format("Source: {0}", exepcionControlada.Source));
                msgLog.AppendLine();
                //StackTrace
                msgLog.Append(String.Format("StackTrace: {0}", exepcionControlada.StackTrace));
                msgLog.AppendLine();

                msgLog.AppendLine();
                msgLog.AppendLine("*********************************************************************************");

                EDUARLog objLog = new EDUARLog(oLogPath, true);
                objLog.write(msgLog.ToString());

            }
            catch (Exception ex) { GestionExcepciones(ex); }
        }
        #endregion

        #region --[Métodos Privados]--
        /// <summary>
        /// Método que dependiendo el tipo de ventana presentará los controles adecuados.
        /// </summary>
        private void CargarPresentacion()
        {
            try
            {
                switch (TipoVentana)
                {
                    case enumTipoVentanaInformacion.Satisfactorio:
                    case enumTipoVentanaInformacion.Advertencia:
                    case enumTipoVentanaInformacion.Error:
                        TDEncabezado.Attributes.Add("class", String.Format("Encabezado{0}", TipoVentana));
                        TablaInterna.Attributes.Add("class", String.Format("tablaInterna{0}", TipoVentana));
                        TDEtiquetas.Attributes.Add("class", String.Format("Eti{0}", TipoVentana));
                        TDEtiquetas2.Attributes.Add("class", String.Format("Eti{0}", TipoVentana));
                        TDEtiquetas3.Attributes.Add("class", String.Format("Eti{0}", TipoVentana));
                        imgIconoVentana.ImageUrl = String.Format("~/Images/ventana{0}.png", TipoVentana);
                        btnCancelar.Visible = false;
                        btnAceptar.Visible = true;
                        imgIconoVentana.Visible = true;
                        break;
                    case enumTipoVentanaInformacion.Confirmación:
                        TDEncabezado.Attributes.Add("class", String.Format("Encabezado{0}", TipoVentana));
                        TablaInterna.Attributes.Add("class", String.Format("tablaInterna{0}", TipoVentana));
                        TDEtiquetas.Attributes.Add("class", String.Format("Eti{0}", TipoVentana));
                        TDEtiquetas2.Attributes.Add("class", String.Format("Eti{0}", TipoVentana));
                        TDEtiquetas3.Attributes.Add("class", String.Format("Eti{0}", TipoVentana));
                        imgIconoVentana.ImageUrl = "~/Images/ventanaConfirmacion.png";
                        btnCancelar.Visible = true;
                        btnAceptar.Visible = true;
                        imgIconoVentana.Visible = true;
                        break;
                    default:
                        TDEncabezado.Attributes.Add("class", "EncabezadoConfirmación");
                        TablaInterna.Attributes.Add("class", "tablaInternaConfirmación");
                        TDEtiquetas.Attributes.Add("class", "EtiConfirmación");
                        TDEtiquetas2.Attributes.Add("class", "EtiConfirmación");
                        TDEtiquetas3.Attributes.Add("class", "EtiConfirmación");

                        imgIconoVentana.Visible = false;
                        btnCancelar.Visible = false;
                        btnAceptar.Visible = false;
                        break;
                }

                Visible = true;
            }
            catch (Exception ex)
            {
                GestionExcepciones(ex);
            }
        }
        #endregion

        #region --[Delegados ]--

        public delegate void VentanaBotonClickHandler(object sender, EventArgs e);

        public event VentanaBotonClickHandler VentanaAceptarClick;
        public event VentanaBotonClickHandler VentanaCancelarClick;

        public virtual void OnVentanaClick(VentanaBotonClickHandler sender, EventArgs e)
        {
            if (sender != null)
            {
                //Invoca el delegados
                sender(this, e);
            }
        }
        #endregion
    }
}