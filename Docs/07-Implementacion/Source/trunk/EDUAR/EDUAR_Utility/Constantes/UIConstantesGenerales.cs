using System;

namespace EDUAR_Utility.Constantes
{
    [Serializable]
    public class UIConstantesGenerales
    {
        #region Mensajes Login
        public const string MensajeLoginFallido = "El usuario o contraseña ingresada no son validos.";
        public const string MensajeLoginNoExisteUsuario = "El Usuario no existe.";
        public const string MensajeNuevoPassword = "El nuevo password ha sido creado con éxito.";
        public const string MensajeErrorPreguntaSeguridad = "Debe ingresar la respuesta a la pregunta de seguridad.";
        public const string MensajeUsuarioExiste = "Ya posee un usuario para el sistema, <br />será redirigido a la página de recuperación de contraseña.";
        #endregion

        #region Validaciones
        public const string MensajeEmailInvalido = "Por favor, ingrese un email válido.";
        public const string MensajeDatosRequeridos = "Por favor, verifique los datos ingresados.";
        #endregion

        #region Confirmaciones
        public const string MensajeConfirmarCambios = "¿Desea guardar los cambios?";
        public const string MensajeEliminar = "¿Desea eliminar el registro?";
        #endregion

        #region Satisfactorio
        public const string MensajeCheckearCorreo = "Por favor, verifique su correo electrónico.";
        public const string MensajeGuardadoOk = "Los cambios se guardaron con éxito.";
        #endregion

        #region MensajesGenericos
        public const string MensajeSinResultados = "La consulta no produjo resultados.";
        #endregion
    }
}
