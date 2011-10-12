using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Security;
using EDUAR_DataAccess.Shared;
using EDUAR_Entities.Security;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

namespace EDUAR_DataAccess.Security
{
    /// <summary>
    /// Clase que contiene la logica de acceso a datos de Seguridad. 
    /// </summary>
    public class DASeguridad
    {
        #region --[Atributos]--
        public DATransaction transaction;
        private const string ClassName = "DASeguridad";
        #endregion

        #region --[Constructores]--
        /// <summary>
        /// Contructor que setea la conexion a 
        /// </summary>
        public DASeguridad()
        {
            transaction = new DATransaction("ApplicationServices");
        }

        protected DASeguridad(DATransaction objDATransaction)
        {
            transaction = objDATransaction;
        }
        #endregion

        #region --[Métodos Publicos]--
        public void CrearUsuarios(DTSeguridad objDTSeguridad)
        {
            string Nombre = string.Empty;
            try
            {
                //Obtener el password por defecto
                DAConfiguracionGlobal objDAConfiguracionGlobal = new DAConfiguracionGlobal();
                string password = objDAConfiguracionGlobal.GetConfiguracion(enumConfiguraciones.PasswordInicial);
                int i = 0;

                foreach (DTUsuario objUsuarios in objDTSeguridad.ListaUsuarios)
                {
                    objDTSeguridad.ListaUsuarios[i].Password = password;
                    objDTSeguridad.ListaUsuarios[i].Aprobado = true;
                    i++;
                }

                try
                {
                    foreach (DTUsuario objUsuario in objDTSeguridad.ListaUsuarios)
                    {
                        #region [Usuarios]

                        //Crea el nuevo usuario
                        System.Web.Security.MembershipCreateStatus status;
                        System.Web.Security.MembershipUser newUser;

                        newUser = System.Web.Security.Membership.CreateUser(objUsuario.Nombre, objUsuario.Password, objUsuario.Email, objUsuario.PaswordPregunta, objUsuario.PaswordRespuesta, objUsuario.Aprobado, out status);

                        //Valida el estado del usuario creado.
                        if (newUser == null)
                        {
                            switch (status)
                            {
                                case System.Web.Security.MembershipCreateStatus.DuplicateUserName:
                                    EliminarUsuarios(objDTSeguridad, Nombre);
                                    throw new CustomizedException("El usuario " + objUsuario.Nombre + " ya existe, la operación ha sido cancelada.", null, enuExceptionType.ValidationException);
                                case System.Web.Security.MembershipCreateStatus.InvalidPassword:
                                    EliminarUsuarios(objDTSeguridad, Nombre);
                                    throw new CustomizedException("La Contraseña para el usuario " + objUsuario.Nombre + " es invalida, la operación ha sido cancelada.", null,
                                                                  enuExceptionType.ValidationException);
                                case System.Web.Security.MembershipCreateStatus.InvalidUserName:
                                    EliminarUsuarios(objDTSeguridad, Nombre);
                                    throw new Exception("El nombre del usuario " + objUsuario.Nombre + " es invalido, la operación ha sido cancelada.");
                                default:
                                    EliminarUsuarios(objDTSeguridad, Nombre);
                                    throw new CustomizedException("No se pudo crear el usuario " + objUsuario.Nombre + ", la operación ha sido cancelada.", null,
                                                                  enuExceptionType.ValidationException);
                            }
                        }

                        #endregion

                        Nombre = objUsuario.Nombre.ToString();

                        #region [Roles]
                        string sTodosRoles = string.Empty;
                        string[] sRoles = new string[objUsuario.ListaRoles.Count];
                        try
                        {
                            //Agrega el usuario a los Roles que se le definieron.
                            foreach (DTRol rolUsuario in objUsuario.ListaRoles)
                            {
                                if (rolUsuario.Nombre.ToString() != string.Empty && Roles.RoleExists(rolUsuario.Nombre.ToString()))
                                    Roles.AddUserToRole(objUsuario.Nombre, rolUsuario.Nombre);
                                else
                                {
                                    EliminarUsuarios(objDTSeguridad, Nombre);
                                    Exception ex = new CustomizedException("No se pudieron agregar los roles para El usuario " + Nombre + ", la operación ha sido cancelada.", null, enuExceptionType.ValidationException);
                                    throw ex;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (CustomizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomizedException(ex.Message, null,
                                              enuExceptionType.ValidationException);
            }
        }

        private void EliminarUsuarios(DTSeguridad objDTSeguridad, string Nombre)
        {
            try
            {
                foreach (DTUsuario objUsuarioDelete in objDTSeguridad.ListaUsuarios)
                {

                    if (objUsuarioDelete.Nombre.ToString() != string.Empty && Membership.FindUsersByName(objUsuarioDelete.Nombre.ToString()).Count > 0)
                    {
                        foreach (DTRol rolUsuario in objUsuarioDelete.ListaRoles)
                        {
                            if (rolUsuario.Nombre.ToString() != string.Empty && Roles.RoleExists(rolUsuario.Nombre.ToString()))
                                Roles.RemoveUserFromRole(objUsuarioDelete.Nombre, rolUsuario.Nombre);
                        }
                        Membership.DeleteUser(objUsuarioDelete.Nombre, true);
                    }
                    if (Nombre == objUsuarioDelete.Nombre)
                        return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Método que obtiene la lista de Roles de la aplicacion
        /// </summary>  
        /// <param name="objDTSeguridad">DTO con los parametros dentro</param>
        /// <returns>Lista con los roles</returns>
        public List<DTRol> GetRoles(DTSeguridad objDTSeguridad)
        {
            string rolesParam = string.Empty;
            try
            {
                string query = @"SELECT 
                                             ROL.RoleId
                                            ,ROL.RoleName
                                            ,ROL.LoweredRoleName
                                            ,ROL.Description
                                        FROM
                                            aspnet_Roles			AS ROL
                                            INNER JOIN
                                            aspnet_Applications AS APP
                                            ON 
                                            ROL.ApplicationId = APP.ApplicationId 
                                        WHERE 
	                                        APP.ApplicationName = @ApplicationName";

                if (objDTSeguridad.ListaRoles.Count != 0)
                {
                    foreach (DTRol rol in objDTSeguridad.ListaRoles)
                        rolesParam += string.Format("'{0}',", rol.Nombre);

                    rolesParam = rolesParam.Substring(0, rolesParam.Length - 1);
                    query = string.Format("{0} AND ROL.RoleName IN ({1})", query, rolesParam);
                }

                transaction.DBcomand = transaction.DataBase.GetSqlStringCommand(query);

                // Añadir parámetros
                transaction.DataBase.AddInParameter(transaction.DBcomand, "@ApplicationName", DbType.String, objDTSeguridad.Aplicacion);

                IDataReader reader = transaction.DataBase.ExecuteReader(transaction.DBcomand);
                List<DTRol> listaRoles = new List<DTRol>();
                while (reader.Read())
                {
                    DTRol rol = new DTRol()
                    {
                        RoleId = reader["RoleId"].ToString(),
                        Descripcion = reader["Description"].ToString(),
                        Nombre = reader["RoleName"].ToString(),
                        NombreCorto = reader["LoweredRoleName"].ToString()
                    };
                    listaRoles.Add(rol);
                }
                return listaRoles;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetRoles()", ClassName),
                                                       ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetRoles()", ClassName),
                                                       ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Método que obtiene la lista de usuarios de la aplicacion
        /// </summary>  
        /// <param name="objDTSeguridad">DTO con los parametros dentro</param>
        /// <param name="paginar"></param>
        /// <returns>Lista de usuarios.</returns>
        public DTSeguridad GetUsuarios(DTSeguridad objDTSeguridad, Boolean paginar)
        {
            // DSUsuarios.UsersDataTable dt = new DSUsuarios.UsersDataTable();
            string rolesParam = string.Empty;
            try
            {
                string query = @"SELECT DISTINCT
                                            US.ApplicationId
                                            ,US.UserId
                                            ,US.UserName
                                            ,US.LoweredUserName
                                            ,US.MobileAlias
                                            ,US.IsAnonymous
                                            ,US.LastActivityDate
                                            ,MEM.IsApproved
                                        FROM 
	                                        aspnet_Users AS US 
	                                        INNER JOIN 
	                                        aspnet_Applications AS APP
	                                        ON US.ApplicationId = APP.ApplicationId
	                                        INNER JOIN aspnet_Membership AS MEM
	                                        ON US.UserId = MEM.UserId
                                            LEFT JOIN aspnet_UsersInRoles AS USR
	                                        ON US.UserId = USR.UserId
	                                        LEFT JOIN aspnet_Roles AS R
	                                        ON USR.RoleId = R.RoleId
                                        WHERE
	                                        APP.ApplicationName = @ApplicationName
                                            AND
                                            (@UserName IS NULL OR @UserName = '' OR UserName LIKE @UserName )
                                            AND
                                            (@IsApproved IS NULL OR @IsApproved = IsApproved )";

                if (objDTSeguridad.ListaRoles.Count != 0)
                {
                    foreach (DTRol rol in objDTSeguridad.ListaRoles)
                        rolesParam += string.Format("'{0}',", rol.Nombre);

                    rolesParam = rolesParam.Substring(0, rolesParam.Length - 1);
                    query = string.Format("{0} AND R.RoleName IN ({1})", query, rolesParam);
                }

                transaction.DBcomand = transaction.DataBase.GetSqlStringCommand(query);

                // Añadir parámetros
                transaction.DataBase.AddInParameter(transaction.DBcomand, "@ApplicationName", DbType.String, objDTSeguridad.Aplicacion);
                transaction.DataBase.AddInParameter(transaction.DBcomand, "@UserName", DbType.String, objDTSeguridad.Usuario.Nombre);
                transaction.DataBase.AddInParameter(transaction.DBcomand, "@IsApproved", DbType.Boolean, objDTSeguridad.Usuario.Aprobado);
                IDataReader reader = transaction.DataBase.ExecuteReader(transaction.DBcomand);
                objDTSeguridad = new DTSeguridad();
                DTUsuario usuario;
                while (reader.Read())
                {
                    usuario = new DTUsuario()
                    {
                        Nombre = reader["UserName"].ToString(),
                        Aprobado = (bool)reader["IsApproved"]
                    };
                    objDTSeguridad.ListaUsuarios.Add(usuario);
                }
                return objDTSeguridad;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetUsuarios()", ClassName),
                                                       ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetUsuarios()", ClassName),
                                                       ex, enuExceptionType.DataAccesException);
            }
        }

        public DTUsuario GetUsuarioByEmail(string email)
        {
            // DSUsuarios.UsersDataTable dt = new DSUsuarios.UsersDataTable();
            string rolesParam = string.Empty;
            try
            {
                string query = @"SELECT aspnet_Membership.UserId, 
                                        aspnet_Membership.Password, 
                                        aspnet_Membership.PasswordQuestion, 
                                        aspnet_Membership.PasswordAnswer, 
                                        aspnet_Membership.IsApproved, 
                                        aspnet_Membership.IsLockedOut, 
                                        aspnet_Membership.ApplicationId, 
                                        aspnet_Membership.PasswordFormat, 
                                        aspnet_Membership.PasswordSalt, 
                                        aspnet_Membership.MobilePIN, 
                                        aspnet_Membership.Email, 
                                        aspnet_Membership.LoweredEmail, 
                                        aspnet_Users.UserName
                                        FROM aspnet_Membership 
                                        INNER JOIN
	                                        aspnet_Users ON aspnet_Membership.UserId = aspnet_Users.UserId
                                        WHERE
                                           aspnet_Membership.Email LIKE @Email";


                transaction.DBcomand = transaction.DataBase.GetSqlStringCommand(query);

                // Añadir parámetros
                transaction.DataBase.AddInParameter(transaction.DBcomand, "@Email", DbType.String, email);
                IDataReader reader = transaction.DataBase.ExecuteReader(transaction.DBcomand);
                DTUsuario usuario = null;
                while (reader.Read())
                {
                    usuario = new DTUsuario();
                    usuario.Nombre = reader["UserName"].ToString();
                    usuario.Aprobado = (bool)reader["IsApproved"];
                    usuario.PaswordPregunta = reader["PasswordQuestion"].ToString();
                    usuario.PaswordRespuesta = reader["PasswordAnswer"].ToString();
                    usuario.Password = reader["Password"].ToString();
                    usuario.Email = reader["Email"].ToString();
                }
                return usuario;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetUsuarioByEmail()", ClassName),
                                                       ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetUsuarioByEmail()", ClassName),
                                                       ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Método que obtiene un rol filtrado por el ID
        /// </summary>  
        /// <param name="objRol">DTO con los parametros dentro</param>
        /// <returns>Rol</returns>
        public DTRol GetRol(DTRol objRol)
        {
            try
            {
                const string query = @"SELECT 
                                             ROL.RoleId
                                            ,ROL.RoleName
                                            ,ROL.LoweredRoleName
                                            ,ROL.Description
                                        FROM
                                            aspnet_Roles AS ROL
                                        WHERE 
	                                        ROL.RoleId = @RoleId";

                transaction.DBcomand = transaction.DataBase.GetSqlStringCommand(query);

                // Añadir parámetros
                transaction.DataBase.AddInParameter(transaction.DBcomand, "@RoleId", DbType.String, objRol.RoleId);

                IDataReader reader = transaction.DataBase.ExecuteReader(transaction.DBcomand);

                while (reader.Read())
                {
                    objRol.RoleId = reader["RoleId"].ToString();
                    objRol.Nombre = reader["RoleName"].ToString();
                    objRol.NombreCorto = reader["LoweredRoleName"].ToString();
                    objRol.Descripcion = reader["Description"].ToString();
                }
                return objRol;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetRol()", ClassName),
                                                       ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - GetRol()", ClassName),
                                                       ex, enuExceptionType.DataAccesException);
            }
        }

        public void CrearRol(DTSeguridad objSeguridad)
        {

            try
            {
                transaction.DBcomand = transaction.DataBase.GetStoredProcCommand("Roles_Insert");

                // Añadir parámetros
                transaction.DataBase.AddInParameter(transaction.DBcomand, "@ApplicationName", DbType.String, objSeguridad.Aplicacion);
                transaction.DataBase.AddInParameter(transaction.DBcomand, "@RoleName", DbType.String, objSeguridad.Rol.Nombre);
                transaction.DataBase.AddInParameter(transaction.DBcomand, "@LoweredRoleName", DbType.String, objSeguridad.Rol.Nombre.ToLower());
                transaction.DataBase.AddInParameter(transaction.DBcomand, "@Description", DbType.String, objSeguridad.Rol.Descripcion);

                transaction.DataBase.ExecuteNonQuery(transaction.DBcomand);

            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - CrearRol()", ClassName),
                                    ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - CrearRol()", ClassName),
                                    ex, enuExceptionType.DataAccesException);
            }
        }

        public void UpdateRol(DTRol objRol)
        {
            try
            {
                const string query = @"UPDATE aspnet_Roles
                                        SET Description = @Descripcion
                                        WHERE RoleId = @RoleId";

                transaction.DBcomand = transaction.DataBase.GetSqlStringCommand(query);

                // Añadir parámetros
                transaction.DataBase.AddInParameter(transaction.DBcomand, "@RoleId", DbType.String, objRol.RoleId);
                transaction.DataBase.AddInParameter(transaction.DBcomand, "@Descripcion", DbType.String, objRol.Descripcion);

                transaction.DataBase.ExecuteNonQuery(transaction.DBcomand);
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - UpdateRol()", ClassName),
                                                       ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(string.Format("Fallo en {0} - UpdateRol()", ClassName),
                                                       ex, enuExceptionType.DataAccesException);
            }
        }
        #endregion

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
    }
}
