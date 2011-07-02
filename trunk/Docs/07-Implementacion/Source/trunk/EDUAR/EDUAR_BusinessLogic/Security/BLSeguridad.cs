using System;
using System.Text;
using System.Transactions;
using System.Web.Security;
using System.Configuration.Provider;
using EDUAR_BusinessLogic.Shared;
using EDUAR_DataAccess.Security;
using EDUAR_Entities.Security;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

namespace EDUAR_BusinessLogic.Security
{
    /// <summary>
    /// Clase que contienen toda la logica de seguridad de la Aplicación. 
    /// </summary>
    public class BLSeguridad
    {
        #region --[Constructores]--

        /// <summary>
        /// Constructor con DTO como parámetro.
        /// </summary>
        public BLSeguridad(DTSeguridad objDTUsuario)
        {
            Data = objDTUsuario;
            Data.Aplicacion = Membership.ApplicationName;
        }

        /// <summary>
        /// 
        /// </summary>
        public BLSeguridad()
        {
            Data = new DTSeguridad { Aplicacion = Membership.ApplicationName };
        }

        #endregion

        #region --[Constante]--
        private const String ClassName = "BLSeguridad";
        #endregion

        #region --[Propiedades]--
        /// <summary>
        /// Propiedad que contiene un objeto DTSeguridad que contiene la data.
        /// </summary>
        public DTSeguridad Data { get; set; }

        #endregion

        #region --[Métodos Privados]--
        /// <summary>
        /// Método que crea un usuario. 
        /// </summary>
        private void ObtenerRolesUsuario()
        {
            //Obtengo todos los roles del usuario y los cargo en el Data.
            String[] arrRoles = Roles.GetRolesForUser(Data.Usuario.Nombre);

            foreach (String rolUsuario in arrRoles)
            {
                DTRol objDTRol = new DTRol { Nombre = rolUsuario, NombreCorto = rolUsuario.ToLower() };
                //Obtiene el IDRol desde la enumeracion enumRoles
                objDTRol.ID = Enum.Parse(typeof(enumRoles), rolUsuario).GetHashCode();
                Data.Usuario.ListaRoles.Add(objDTRol);
            }
        }

        /// <summary>
        /// Método que desencripta un texto.
        /// </summary>
        /// <param name="textEncripted"></param>
        /// <returns></returns>
        private static String Desencriptar(String textEncripted)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            Decoder utf8Decode = encoder.GetDecoder();

            byte[] todecodeByte = Convert.FromBase64String(textEncripted);
            int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
            char[] decodedChar = new char[charCount];
            utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
            String result = new String(decodedChar);

            return result;
        }
        #endregion

        #region --[Métodos Publicos]--
        /// <summary>
        /// 
        /// </summary>
        public void GetUsuario()
        {
            try
            {
                MembershipUser user = Membership.GetUser(Data.Usuario.Nombre);
                Data.Usuario.Password = user.GetPassword();
                Data.Usuario.Aprobado = user.IsApproved;
                ObtenerRolesUsuario();
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - GetUsuario", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Método que obtiene todos los roles. 
        /// </summary>
        public void GetRoles()
        {
            try
            {
                DASeguridad dataAcces = new DASeguridad();
                Data.ListaRoles = dataAcces.GetRoles(Data);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - GetRoles", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Metodo que obtiene usuarios.
        /// </summary>
        public void GetUsuarios()
        {
            try
            {
                DASeguridad dataAcces = new DASeguridad();
                Data = dataAcces.GetUsuarios(Data, true);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - GetUsuarios", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Método que valida usuario y password, en caso de ser exitoso carga la Data.
        /// </summary>
        public void ValidarUsuario()
        {
            try
            {
                Data.Usuario.UsuarioValido = Membership.ValidateUser(Data.Usuario.Nombre, Data.Usuario.Password);

                if (Data.Usuario.UsuarioValido)
                {
                    ObtenerRolesUsuario();
                    //ObtenerIntervalosActualizacionUsuario(Data.Usuario);
                    MembershipUser us = Membership.GetUser(Data.Usuario.Nombre);

                    if (us.CreationDate == us.LastPasswordChangedDate)
                        Data.Usuario.EsUsuarioInicial = true;
                }

            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - ValidarUsuario()", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Método que cambia el password de un usuario. 
        /// </summary>
        public void CambiarPassword()
        {
            try
            {
                MembershipUser user = Membership.GetUser(Data.Usuario.Nombre);
                user.ChangePassword(Data.Usuario.Password, Data.Usuario.PasswordNuevo);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - CambiarPassword", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Método que crea un usuario. 
        /// </summary>
        public void CrearUsuario()
        {
            try
            {
                //Obtener el password por defecto
                BLConfiguracionGlobal objBLConfiguracionGlobal = new BLConfiguracionGlobal();
                //String passwordEncriptado = objBLConfiguracionGlobal.ObtenerConfiguracion(enumConfiguraciones.PasswordInicial);
                Data.Usuario.Password = objBLConfiguracionGlobal.ObtenerConfiguracion(enumConfiguraciones.PasswordInicial);
                //Data.Usuario.Password = Desencriptar(passwordEncriptado);
                Data.Usuario.Aprobado = Data.Usuario.Aprobado;

                //Inicia la transaccion.
                //using (TransactionScope txScope = new TransactionScope())
                //{

                //Crea el nuevo usuario
                MembershipCreateStatus status;
                MembershipUser newUser = Membership.CreateUser(Data.Usuario.Nombre, Data.Usuario.Password, Data.Usuario.Email, Data.Usuario.PaswordPregunta, Data.Usuario.PaswordRespuesta, Data.Usuario.Aprobado, out status);

                //Valida el estado del usuario creado.
                if (newUser == null)
                {
                    switch (status)
                    {
                        case MembershipCreateStatus.DuplicateUserName:
                            throw new CustomizedException("El usuario ya existe", null, enuExceptionType.SecurityException);
                        case MembershipCreateStatus.InvalidPassword:
                            throw new CustomizedException("La Contraseña es invalida.", null,
                                                          enuExceptionType.SecurityException);
                        case MembershipCreateStatus.InvalidUserName:
                            throw new Exception("El nombre del usuario es invalido.");

                        #region Excepciones no controladas

                        //case MembershipCreateStatus.InvalidEmail:
                        //    throw new Exception("The e-mail address provided is invalid. Please check the value and try again.");
                        //case MembershipCreateStatus.InvalidAnswer:
                        //    throw new Exception("The password retrieval answer provided is invalid. Please check the value and try again.");
                        //case MembershipCreateStatus.InvalidQuestion:
                        //    throw new Exception("The password retrieval question provided is invalid. Please check the value and try again.");
                        //case MembershipCreateStatus.ProviderError:
                        //    throw new Exception("The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.");
                        //case MembershipCreateStatus.UserRejected:
                        //    throw new Exception("The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.");
                        //case MembershipCreateStatus.DuplicateEmail:
                        //    throw new Exception("A username for that e-mail address already exists. Please enter a different e-mail address.");

                        #endregion

                        default:
                            throw new CustomizedException("No se pudo crear el usuario.", null,
                                                          enuExceptionType.SecurityException);

                    }
                }

                //Agrega el usuario a los Roles que se le definieron.
                foreach (DTRol rolUsuario in Data.Usuario.ListaRoles)
                    Roles.AddUserToRole(Data.Usuario.Nombre, rolUsuario.Nombre);


                //    //Completa la transaccion.
                //    txScope.Complete();
                //}
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - CrearUsuario", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Método que crea los usuarios en forma masiva. 
        /// </summary>
        public void CrearUsuarios(DTSeguridad objDTSeguridad)
        {
            DASeguridad dataAcces = new DASeguridad();
            try
            {

                //Abre la transaccion que se va a utilizar
                //dataAcces.transaction.OpenTransaction();
                dataAcces.CrearUsuarios(objDTSeguridad);
                //Se da el OK para la transaccion.
                //dataAcces.transaction.CommitTransaction();
            }
            catch (CustomizedException ex)
            {
                //dataAcces.transaction.RollbackTransaction();
                throw ex;
            }
            catch (Exception ex)
            {
                //dataAcces.transaction.RollbackTransaction();
                throw new CustomizedException(String.Format("Fallo en {0} - CrearUsuarios()", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Agregars the roles.
        /// </summary>
        /// <param name="objUsuario">The obj usuario.</param>
        /// <param name="txScope">The tx scope.</param>
        void AgregarRoles(DTUsuario objUsuario, TransactionScope txScope)
        {
            string sTodosRoles = String.Empty;
            string[] sRoles = new string[objUsuario.ListaRoles.Count];
            int i = 0;
            using (TransactionScope txScope1 = txScope)
            {
                //Agrega el usuario a los Roles que se le definieron.
                foreach (DTRol rolUsuario in objUsuario.ListaRoles)
                {
                    sRoles[i] = rolUsuario.Nombre;
                    i++;
                }

                Roles.AddUserToRoles(objUsuario.Nombre, sRoles);
                //txScope1.Complete();
            }
        }

        /// <summary>
        /// Método que crea un usuario. 
        /// </summary>
        public void EliminarUsuario()
        {
            try
            {
                Membership.DeleteUser(Data.Usuario.Nombre);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - EliminarUsuario", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Método que crea un usuario. 
        /// </summary>
        public void ActualizarUsuario()
        {
            try
            {
                //Inicia la transaccion.
                //using (TransactionScope txScope = new TransactionScope())
                //{
                #region Habilita o Bloquea un usuario
                MembershipUser user = Membership.GetUser(Data.Usuario.Nombre);
                user.IsApproved = Data.Usuario.Aprobado;

                Membership.UpdateUser(user);
                #endregion

                #region Elimina la asignacion de roles al usuario
                String[] rolesAplicacion = Roles.GetAllRoles();

                foreach (String rolActual in rolesAplicacion)
                {
                    if (Roles.IsUserInRole(Data.Usuario.Nombre, rolActual))
                        Roles.RemoveUserFromRole(Data.Usuario.Nombre, rolActual);
                    else
                        continue;
                }
                #endregion

                //Asigna los roles al usuario
                foreach (DTRol rolUsuario in Data.Usuario.ListaRoles)
                    Roles.AddUserToRole(Data.Usuario.Nombre, rolUsuario.Nombre);


                //    //Completa la transaccion.
                //    txScope.Complete();
                //}
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - ActualizarUsuario", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Guardars the rol.
        /// </summary>
        public void GuardarRol()
        {
            try
            {

                DASeguridad dataAccess = new DASeguridad();
                // si Data.Rol.RoleId = string.Empty, es un rol nuevo, sino, existe y debo actualizar la descripción
                if (string.IsNullOrEmpty(Data.Rol.RoleId))
                {
                    if (!Roles.RoleExists(Data.Rol.Nombre))
                    {
                        Roles.CreateRole(Data.Rol.Nombre);
                        Data.Rol = dataAccess.GetRol(Data.Rol);
                    }
                    else
                    {
                        throw new CustomizedException(String.Format("Fallo en {0} - GuardarRol - el rol {1} ya existe.", ClassName, Data.Rol.Nombre), null,
                                                     enuExceptionType.BusinessLogicException);
                    }
                }
                dataAccess.UpdateRol(Data.Rol);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - GuardarRol", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Gets the rol.
        /// </summary>
        public void GetRol()
        {
            try
            {
                DASeguridad dataAcces = new DASeguridad();
                Data.Rol = dataAcces.GetRol(Data.Rol);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - GetRol", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Método que crea un rol. 
        /// </summary>
        public void CrearRol()
        {
            try
            {
                //Inicia la transaccion.
                using (TransactionScope txScope = new TransactionScope())
                {
                    DASeguridad dataAcces = new DASeguridad();
                    if (Data.Rol.ID == 0)
                        dataAcces.CrearRol(Data);
                    //else
                    //    dataAcces.Update(Data);

                    //Completa la transaccion.
                    txScope.Complete();
                }
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - CrearRol()", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }

        /// <summary>
        /// Eliminars the rol.
        /// </summary>
        public void EliminarRol()
        {
            try
            {
                Roles.DeleteRole(Data.Rol.Nombre, true);
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (ProviderException ex)
            {
                throw new CustomizedException(string.Format("No puede elminarse el perfil {0} ya que tiene asociados usuarios.", Data.Rol.Nombre), ex,
                                              enuExceptionType.BusinessLogicException); ;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - EliminarRol", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }
        }
        #endregion
    }
}
