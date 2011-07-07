using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_Utility.Utilidades
{
    public class EDUAREmail
    {
        #region --[Atributos]--
        /// <summary>
        /// Atributo que contiene el cliente SMTP para el envio de mail.
        /// </summary>
        private readonly SmtpClient ObjClienteSmtp;
        /// <summary>
        /// Atributo que contiene el mail a enviar.
        /// </summary>
        private readonly MailMessage ObjMailMessage;

        /// <summary>
        /// nombre clase
        /// </summary>
        private const String ClassName = "EDUAREmail";
        #endregion

        #region --[Constructor]--

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="emailFrom">Dirección de correo del from del mail. Se almacena en ObjMailMessage.From</param>
        /// <param name="servidorSMTP">Nombre del Servidor SMTP. Se almacena en ObjClienteSmtp.Host</param>
        /// <param name="puertoSMTP">Número de puerto para la conexión al servidor SMTP. Por default contiene el puerto 25. </param>
        /// <param name="enableSSL">Boolean que contiene verdadero si se utiliza SSL. Por default es falso. </param>
        /// <param name="displayName">Nombre que se mostrará en el From al enviarse el correo.</param>
        public EDUAREmail(String emailFrom, String servidorSMTP, Int32? puertoSMTP, Boolean? enableSSL, String displayName)
        {
            ObjMailMessage = new MailMessage
                                 {
                                     From = displayName.Trim() != String.Empty
                                             ? new MailAddress(emailFrom, displayName)
                                             : new MailAddress(emailFrom)
                                 };


            //Seteo de Cliente SMTP
            ObjClienteSmtp = new SmtpClient(servidorSMTP);
            if (puertoSMTP != null)
                ObjClienteSmtp.Port = (Int32)puertoSMTP;
            if (enableSSL != null)
                ObjClienteSmtp.EnableSsl = (Boolean)enableSSL;
        }

        #endregion

        #region --[Métodos Publicos]--

        /// <summary>
        /// Llama al método validarDatosNecesarios() y si es true envía un correo con al
        /// asunto y el cuerpo que se pasa como parámetro.
        /// Pasos:
        /// - valida los datos necesarios.
        /// - carga el asunto: ObjMailMessage.Subject = asunto.
        /// - carga el cuerpo del mail: ObjMailMessage.Body = cuerpo.
        /// - envia el correo: ObjClienteSmtp.Send(ObjMailMessage)
        /// </summary>
        /// <param name="cuerpo">Cuerpo del mail</param>
        /// <param name="asunto">Asunto del Mail</param>
        public void EnviarMail(String asunto, String cuerpo, bool isBodyHtml)
        {
            try
            {
                if (!ValidarDatosNecesarios())
                    throw new Exception();
                ObjMailMessage.Subject = asunto;
                ObjMailMessage.Body = cuerpo;
                ObjMailMessage.IsBodyHtml = isBodyHtml;

                ObjClienteSmtp.Send(ObjMailMessage);
                ObjMailMessage.To.Clear();
                ObjMailMessage.Attachments.Dispose();
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - EnviarMail()", ClassName),
                                                        ex, enuExceptionType.Exception);
            }

        }

        /// <summary>
        /// Adjunta un fichero al correo que se enviará. Objeto ObjMailMessage.Attachments
        /// método Add(new Attachment(string pathArchivo))
        /// </summary>
        /// <param name="pathAdjunto">Path del fichero a adjuntar</param>
        public void AdjuntarArchivo(String pathAdjunto)
        {
            ObjMailMessage.Attachments.Add(new Attachment(pathAdjunto));
        }

        /// <summary>
        /// Adjunta un fichero al correo que se enviará. Objeto ObjMailMessage.Attachments
        /// método Add(new Attachment(contentetStream, nombreArchivo))
        /// </summary>
        /// <param name="contentetStream">Contenido del Fichero.</param>
        /// <param name="nombreArchivo">Path del fichero a adjuntar</param>
        public void AdjuntarArchivo(Stream contentetStream, String nombreArchivo)
        {
            ObjMailMessage.Attachments.Add(new Attachment(contentetStream, nombreArchivo));
        }

        /// <summary>
        /// Método que agrega un detinatario a ObjMailMessage.To con el método Add(new
        /// MailAddress(string destinatario)).
        /// </summary>
        /// <param name="email"></param>
        public void AgregarDestinatario(String email)
        {
            ObjMailMessage.To.Add(new MailAddress(email));
        }

        /// <summary>
        /// Crea un objeto NetworkCredential(usuarioAutenticacion, passwordAutenticacion) y
        /// se lo asigna a ObjClienteSmtp.Credentials.
        /// </summary>
        /// <param name="passwordSMTP">Contraseña del usuario para conectarse al servidor
        /// SMTP.</param>
        /// <param name="usuarioSMTP">Nombre de usuario para conectarse al servidor SMTP.
        /// </param>
        public void CargarCredenciales(String usuarioSMTP, String passwordSMTP)
        {
            ObjClienteSmtp.Credentials = new NetworkCredential(usuarioSMTP, passwordSMTP);
        }

        #endregion

        #region --[Métodos Privados]--
        /// <summary>
        /// Método que valida que al menos estén los datos mínimos para el envío del mail
        /// Email destinatario, servidor SMTP, usuario y password si
        /// requiere autenticación).
        /// </summary>
        private bool ValidarDatosNecesarios()
        {
            return ObjMailMessage.To.Count != 0;
        }
        #endregion
    }
}
