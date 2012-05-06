using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using System.Web.Security;
using System.Configuration.Provider;
using System.Security.Cryptography;
using EDUAR_BusinessLogic.Shared;
using EDUAR_DataAccess.Security;
using EDUAR_DataAccess.Common;
using EDUAR_Entities.Security;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;
using EDUAR_Utility.Utilidades;

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
		private const string ClassName = "BLSeguridad";
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
			GetRoles();
			string[] arrRoles = Roles.GetRolesForUser(Data.Usuario.Nombre);

			foreach (string rolUsuario in arrRoles)
			{
                foreach (DTRol rol in Data.ListaRoles)
                {
                    if (rolUsuario == rol.Nombre || rolUsuario == rol.Nombre.ToLower())
                    {
                        Data.Usuario.ListaRoles.Add(rol);
                    }
                }
			}
		}

		/// <summary>
		/// Método que desencripta un texto.
		/// </summary>
		/// <param name="textEncripted"></param>
		/// <returns></returns>
		private static string Desencriptar(string textEncripted)
		{
			UTF8Encoding encoder = new UTF8Encoding();
			Decoder utf8Decode = encoder.GetDecoder();

			byte[] todecodeByte = Convert.FromBase64String(textEncripted);
			int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
			char[] decodedChar = new char[charCount];
			utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
			string result = new string(decodedChar);

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

				//Data.Usuario.Password = user.GetPassword();
				Data.Usuario.Aprobado = user.IsApproved;
				Data.Usuario.PaswordPregunta = user.PasswordQuestion;
				Data.Usuario.Email = user.Email;

				ObtenerRolesUsuario();
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetUsuario", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public void GetUsuarioByEmail()
		{
			try
			{
				DASeguridad dataAcces = new DASeguridad();
				DTUsuario usuario = dataAcces.GetUsuarioByEmail(Data.Usuario.Email);
				if (usuario == null)
					throw new CustomizedException(string.Format("El email {0} no se encuentra registrado.", Data.Usuario.Email), null,
														 enuExceptionType.ValidationException);
				Data.Usuario = usuario;

				ObtenerRolesUsuario();
			}
			catch (CustomizedException ex)
			{ throw ex; }
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetUsuarioByEmail", ClassName), ex,
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
				throw new CustomizedException(string.Format("Fallo en {0} - GetRoles", ClassName), ex,
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
				throw new CustomizedException(string.Format("Fallo en {0} - GetUsuarios", ClassName), ex,
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
				//TODO: para guardar password encriptada con hash
				//DASeguridad dataAcces = new DASeguridad();
				//Data.Usuario.UsuarioValido = dataAcces.Autenticar(Data.Usuario.Nombre, Data.Usuario.Password);


				if (Data.Usuario.UsuarioValido)
				{
					MembershipUser usuario = Membership.GetUser(Data.Usuario.Nombre);

					ObtenerRolesUsuario();
					//ObtenerIntervalosActualizacionUsuario(Data.Usuario);
					MembershipUser us = Membership.GetUser(Data.Usuario.Nombre);

					if (us.CreationDate == us.LastPasswordChangedDate
						 || us.GetPassword() == BLConfiguracionGlobal.ObtenerConfiguracion(enumConfiguraciones.PasswordInicial))
						Data.Usuario.EsUsuarioInicial = true;
				}
				else
				{
					MembershipUser usuario = Membership.GetUser(Data.Usuario.Nombre);
					if (usuario != null && usuario.IsLockedOut)
						throw new CustomizedException("El usuario se encuentra bloqueado.", null,
														  enuExceptionType.SecurityException);
					else
						throw new CustomizedException("No se encuentra el usuario.", null,
														  enuExceptionType.SecurityException);
				}
			}
			catch (CustomizedException ex)
			{ throw ex; }
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - ValidarUsuario()", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Validars the respuesta.
		/// </summary>
		public void ValidarRespuesta()
		{
			try
			{
				Data.Usuario.Nombre = Membership.GetUserNameByEmail(Data.Usuario.Email);
				MembershipUser user = Membership.GetUser(Data.Usuario.Nombre);
				Data.Usuario.PaswordRespuesta = user.GetPassword(Data.Usuario.PaswordRespuesta);
			}
			catch (MembershipPasswordException ex)
			{
				throw new CustomizedException(string.Format("La respuesta proporcionada no es válida.", ClassName), ex,
											  enuExceptionType.SecurityException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - CambiarPassword", ClassName), ex,
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
				user.ChangePassword(user.GetPassword(Data.Usuario.PaswordRespuesta), Data.Usuario.PasswordNuevo);
			}
			catch (ArgumentException)
			{
				throw new CustomizedException("La Contraseña debe tener al menos 5 caracteres, de los cuales uno debe ser numérico.", null,
															  enuExceptionType.SecurityException);
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - CambiarPassword", ClassName), ex,
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
				//string passwordEncriptado = objBLConfiguracionGlobal.ObtenerConfiguracion(enumConfiguraciones.PasswordInicial);
				Data.Usuario.Password = BLConfiguracionGlobal.ObtenerConfiguracion(enumConfiguraciones.PasswordInicial);
				//Data.Usuario.Password = Desencriptar(passwordEncriptado);
				Data.Usuario.Aprobado = Data.Usuario.Aprobado;

				//Inicia la transaccion.
				//using (TransactionScope txScope = new TransactionScope())
				//{

				//Crea el nuevo usuario
				MembershipCreateStatus status;
				//TODO: para guardar la password encriptada con hasH
				//string password = EDUARUtilidades.Helper.EncodePassword(string.Concat(Data.Usuario.Nombre, Data.Usuario.Password));
				MembershipUser newUser = Membership.CreateUser(Data.Usuario.Nombre, Data.Usuario.Password, Data.Usuario.Email, Data.Usuario.PaswordPregunta, Data.Usuario.PaswordRespuesta, Data.Usuario.Aprobado, out status);

				//Valida el estado del usuario creado.
				if (newUser == null)
				{
					switch (status)
					{
						case MembershipCreateStatus.DuplicateUserName:
							throw new CustomizedException("El usuario ya existe", null, enuExceptionType.SecurityException);
						case MembershipCreateStatus.InvalidPassword:
							throw new CustomizedException("La Contraseña es inválida.", null,
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
						case MembershipCreateStatus.DuplicateEmail:
							throw new CustomizedException("El email ya se encuentra registrado.", null, enuExceptionType.SecurityException);

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
			{ throw ex; }
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - CrearUsuario", ClassName), ex,
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
				throw new CustomizedException(string.Format("Fallo en {0} - CrearUsuarios()", ClassName), ex,
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
			try
			{
				string sTodosRoles = string.Empty;
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
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - AgregarRoles", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
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
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - EliminarUsuario", ClassName), ex,
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
				string[] rolesAplicacion = Roles.GetAllRoles();

				foreach (string rolActual in rolesAplicacion)
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
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - ActualizarUsuario", ClassName), ex,
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
						throw new CustomizedException(string.Format("El rol {1} ya existe.", Data.Rol.Nombre), null,
													 enuExceptionType.ValidationException);
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
				throw new CustomizedException(string.Format("Fallo en {0} - GuardarRol", ClassName), ex,
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
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - GetRol", ClassName), ex,
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
				throw new CustomizedException(string.Format("Fallo en {0} - CrearRol()", ClassName), ex,
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
											  enuExceptionType.IntegrityDataException); ;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - EliminarRol", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Recuperars the password.
		/// </summary>
		/// <param name="urlHost">The URL host.</param>
		public void RecuperarPassword(Uri urlHost)
		{
			try
			{
				string emailFrom = BLConfiguracionGlobal.ObtenerConfiguracion(enumConfiguraciones.emailFrom);
				string servidorSMTP = BLConfiguracionGlobal.ObtenerConfiguracion(enumConfiguraciones.servidorSMTP);
				string displayName = BLConfiguracionGlobal.ObtenerConfiguracion(enumConfiguraciones.displayName);
				Int32? puertoSMTP = Convert.ToInt32(BLConfiguracionGlobal.ObtenerConfiguracion(enumConfiguraciones.puertoSMTP));
				Boolean? enableSSL = Convert.ToBoolean(BLConfiguracionGlobal.ObtenerConfiguracion(enumConfiguraciones.enableSSL));

				EDUAREmail email = new EDUAREmail(emailFrom, servidorSMTP, puertoSMTP, true, displayName);

				string usuario = BLConfiguracionGlobal.ObtenerConfiguracion(enumConfiguraciones.SendUserName);
				string password = BLConfiguracionGlobal.ObtenerConfiguracion(enumConfiguraciones.SendUserPass);

				email.CargarCredenciales(usuario, password);
				email.AgregarDestinatario(Data.Usuario.Email);

				StringBuilder mensaje = new StringBuilder();
				mensaje.AppendLine("Gracias por utilizar EDU@R 2.0");
				mensaje.AppendLine("<br /><br />");
				mensaje.AppendLine("Hemos recibido un pedido solicitando tus datos de acceso, para restablecer tu contraseña, <br />");
				mensaje.Append("haz click en el siguiente enlace, de lo contrario, ignora este correo.");
				mensaje.AppendLine("<br /><br />");
				mensaje.AppendLine("<a href='" + urlHost.ToString() + "?const=" + BLEncriptacion.Encrypt(Data.Usuario.Nombre.Trim()) + "'>Acceder</a>");
				mensaje.AppendLine("<br /><br />");
				mensaje.AppendLine("<br /><br />");
				mensaje.AppendLine();
				mensaje.AppendLine("EDU@R 2.0 - Educación Argentina del Nuevo Milenio");

				email.EnviarMail("EDU@R 2.0 - Datos de Acceso - " + DateTime.Now.Date.ToShortDateString(), mensaje.ToString(), true);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - RecuperarPassword", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Changes the question.
		/// </summary>
		public void CambiarPregunta()
		{
			try
			{
				MembershipUser user = Membership.GetUser(Data.Usuario.Nombre);
				user.ChangePasswordQuestionAndAnswer(user.GetPassword(), Data.Usuario.PaswordPregunta, Data.Usuario.PaswordRespuesta);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - RecuperarPassword", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

		/// <summary>
		/// Actualizars the email.
		/// </summary>
		public void ActualizarEmail()
		{
			try
			{
				MembershipUser user = Membership.GetUser(Data.Usuario.Nombre);
				user.Email = Data.Usuario.Email;
				Membership.UpdateUser(user);
			}
			catch (ProviderException ex)
			{
				throw new CustomizedException(string.Format("El email {0} ya se encuentra registrado", Data.Usuario.Email), ex,
												  enuExceptionType.ValidationException);
			}
			catch (CustomizedException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new CustomizedException(string.Format("Fallo en {0} - ActualizarEmail", ClassName), ex,
											  enuExceptionType.BusinessLogicException);
			}
		}

        public List<DTRol> GetRolesByTipoPersona(int tipoPersona)
        {
            List<DTRol> retListRoles = new List<DTRol>();
            List<String> roles = new List<String>();

            this.GetRoles();

            try
            {
                //DASeguridad dataAcces = new DASeguridad();
                //D = dataAcces.GetRoles(Data);
                DAPersona dataAccessPersona = new DAPersona();
                roles = dataAccessPersona.GetRolesByTipoPersona(tipoPersona);


            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetRoles", ClassName), ex,
                                              enuExceptionType.BusinessLogicException);
            }

            foreach (DTRol rol in Data.ListaRoles)
            {
                foreach (String rolUser in roles)
                {
                    if (rolUser.ToLower() == rol.NombreCorto || rolUser == rol.NombreCorto)
                    {
                        retListRoles.Add(rol);
                    }

                }

            }

            return (retListRoles);

        }
        
		#endregion
	}
}
