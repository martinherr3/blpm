using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
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
        private const String ClassName = "DASeguridad";
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
            string Nombre = String.Empty;
            try
            {
                //Obtener el password por defecto
                DAConfiguracionGlobal objDAConfiguracionGlobal = new DAConfiguracionGlobal();
                String passwordEncriptado = objDAConfiguracionGlobal.GetConfiguracion(enumConfiguraciones.PasswordInicial);
                int i = 0;

                foreach (DTUsuario objUsuarios in objDTSeguridad.ListaUsuarios)
                {
                    objDTSeguridad.ListaUsuarios[i].Password = Desencriptar(passwordEncriptado);
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
                                    EliminarUsaurios(objDTSeguridad, Nombre);
                                    throw new CustomizedException("El usuario " + objUsuario.Nombre + " ya existe, la operación ha sido cancelada.", null, enuExceptionType.ValidationException);
                                case System.Web.Security.MembershipCreateStatus.InvalidPassword:
                                    EliminarUsaurios(objDTSeguridad, Nombre);
                                    throw new CustomizedException("La Contraseña para el usuario " + objUsuario.Nombre + " es invalida, la operación ha sido cancelada.", null,
                                                                  enuExceptionType.ValidationException);
                                case System.Web.Security.MembershipCreateStatus.InvalidUserName:
                                    EliminarUsaurios(objDTSeguridad, Nombre);
                                    throw new Exception("El nombre del usuario " + objUsuario.Nombre + " es invalido, la operación ha sido cancelada.");
                                default:
                                    EliminarUsaurios(objDTSeguridad, Nombre);
                                    throw new CustomizedException("No se pudo crear el usuario " + objUsuario.Nombre + ", la operación ha sido cancelada.", null,
                                                                  enuExceptionType.ValidationException);
                            }
                        }

                        #endregion

                        Nombre = objUsuario.Nombre.ToString();

                        #region [Roles]
                        string sTodosRoles = String.Empty;
                        string[] sRoles = new string[objUsuario.ListaRoles.Count];
                        try
                        {
                            //Agrega el usuario a los Roles que se le definieron.
                            foreach (DTRol rolUsuario in objUsuario.ListaRoles)
                            {
                                if (rolUsuario.Nombre.ToString() != String.Empty && Roles.RoleExists(rolUsuario.Nombre.ToString()))
                                    Roles.AddUserToRole(objUsuario.Nombre, rolUsuario.Nombre);
                                else
                                {
                                    EliminarUsaurios(objDTSeguridad, Nombre);
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

        private void EliminarUsaurios(DTSeguridad objDTSeguridad, String Nombre)
        {
            try
            {
                foreach (DTUsuario objUsuarioDelete in objDTSeguridad.ListaUsuarios)
                {

                    if (objUsuarioDelete.Nombre.ToString() != String.Empty && Membership.FindUsersByName(objUsuarioDelete.Nombre.ToString()).Count > 0)
                    {
                        foreach (DTRol rolUsuario in objUsuarioDelete.ListaRoles)
                        {
                            if (rolUsuario.Nombre.ToString() != String.Empty && Roles.RoleExists(rolUsuario.Nombre.ToString()))
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
            try
            {
                const String query = @"SELECT 
                                            ROL.RoleName
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


                transaction.DBcomand = transaction.DataBase.GetSqlStringCommand(query);

                // Añadir parámetros
                transaction.DataBase.AddInParameter(transaction.DBcomand, "@ApplicationName", DbType.String, objDTSeguridad.Aplicacion);

                DataSet ds = transaction.DataBase.ExecuteDataSet(transaction.DBcomand);

                List<DTRol> listaDTRol = new List<DTRol>();
                DTRol objDTRol;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    objDTRol = new DTRol { Nombre = (row["RoleName"]).ToString(), NombreCorto = (row["LoweredRoleName"]).ToString() };

                    if (row["Description"] != null)
                        objDTRol.Descripcion = (row["Description"]).ToString();

                    listaDTRol.Add(objDTRol);
                }

                return listaDTRol;
            }
            catch (SqlException ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - GetRoles()", ClassName),
                                                       ex, enuExceptionType.SqlException);
            }
            catch (Exception ex)
            {
                throw new CustomizedException(String.Format("Fallo en {0} - GetRoles()", ClassName),
                                                       ex, enuExceptionType.DataAccesException);
            }
        }

        /// <summary>
        /// Método que obtiene la lista de usuarios de la aplicacion
        /// </summary>  
        /// <param name="objDTSeguridad">DTO con los parametros dentro</param>
        /// <param name="paginar"></param>
        /// <returns>Lista de usuarios.</returns>
//        public DTSeguridad GetUsuarios(DTSeguridad objDTSeguridad, Boolean paginar)
//        {
//            DSUsuarios.UsersDataTable dt = new DSUsuarios.UsersDataTable();
//            String rolesParam = String.Empty;
//            try
//            {

//                String query = @"SELECT DISTINCT
//                                            US.ApplicationId
//                                            ,US.UserId
//                                            ,US.UserName
//                                            ,US.LoweredUserName
//                                            ,US.MobileAlias
//                                            ,US.IsAnonymous
//                                            ,US.LastActivityDate
//                                        FROM 
//	                                        aspnet_Users AS US 
//	                                        INNER JOIN 
//	                                        aspnet_Applications AS APP
//	                                        ON US.ApplicationId = APP.ApplicationId
//                                            LEFT JOIN aspnet_UsersInRoles AS USR
//	                                        ON US.UserId = USR.UserId
//	                                        LEFT JOIN aspnet_Roles AS R
//	                                        ON USR.RoleId = R.RoleId
//
//                                        WHERE
//	                                        APP.ApplicationName = @ApplicationName
//                                            AND
//                                            (@UserName IS NULL OR @UserName = '' OR UserName LIKE @UserName )";



//                if (objDTSeguridad.ListaRoles.Count != 0)
//                {
//                    foreach (DTRol rol in objDTSeguridad.ListaRoles)
//                        rolesParam += String.Format("'{0}',", rol.Nombre);

//                    rolesParam = rolesParam.Substring(0, rolesParam.Length - 1);
//                    query = String.Format("{0} AND R.RoleName IN ({1})", query, rolesParam);
//                }

//                transaction.DBcomand = transaction.DataBase.GetSqlStringCommand(query);

//                // Añadir parámetros
//                transaction.DataBase.AddInParameter(transaction.DBcomand, "@ApplicationName", DbType.String, objDTSeguridad.Aplicacion);
//                transaction.DataBase.AddInParameter(transaction.DBcomand, "@UserName", DbType.String, objDTSeguridad.Usuario.Nombre);

//                DataSet ds = transaction.DataBase.ExecuteDataSet(transaction.DBcomand);


//                DataTable dtResultado = paginar
//                                           ? DAHelper.ObtenerTablaPaginada(ds.Tables[0], objDTSeguridad.PagPaginaActual,
//                                                                           objDTSeguridad.PagSize, "UserId DESC")
//                                           : ds.Tables[0];

//                dt.Merge(dtResultado, true, MissingSchemaAction.Ignore);
//                objDTSeguridad = new DTSeguridad { UsersDT = dt, PagCantidadTotalReg = ds.Tables[0].Rows.Count };

//            }
//            catch (SqlException ex)
//            {
//                throw new CustomizedException(String.Format("Fallo en {0} - GetUsuarios()", ClassName),
//                                                       ex, enuExceptionType.SqlException);
//            }
//            catch (Exception ex)
//            {
//                throw new CustomizedException(String.Format("Fallo en {0} - GetUsuarios()", ClassName),
//                                                       ex, enuExceptionType.DataAccesException);
//            }

//            return objDTSeguridad;
//        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listaDTRol"></param>
        /// <returns></returns>
//        public DSUsuarios.UsersDataTable GetTutores()
//        {
//            DSUsuarios.UsersDataTable dt = new DSUsuarios.UsersDataTable();
//            String rolesParam = String.Empty;
//            try
//            {

//                String query = @"SELECT			DISTINCT
//                                                Usuario.ApplicationId, 
//				                                Usuario.UserId, 
//				                                Usuario.UserName, 
//				                                Usuario.LoweredUserName, 
//				                                Usuario.MobileAlias, 
//				                                Usuario.IsAnonymous, 
//                                                Usuario.LastActivityDate
//                                FROM			aspnet_Users AS Usuario 
//                                INNER JOIN		aspnet_UsersInRoles AS UsuarioRol ON Usuario.UserId = UsuarioRol.UserId 
//                                INNER JOIN		aspnet_Roles AS Rol ON UsuarioRol.RoleId = Rol.RoleId
//                                WHERE			Rol.RoleName IN ('TUAD', 'TUTO')";

//                transaction.DBcomand = transaction.DataBase.GetSqlStringCommand(query);

//                DataSet ds = transaction.DataBase.ExecuteDataSet(transaction.DBcomand);
//                dt.Merge(ds.Tables[0], true, MissingSchemaAction.Ignore);
//            }
//            catch (SqlException ex)
//            {
//                throw new CustomizedException(String.Format("Fallo en {0} - GetUsuarios()", ClassName),
//                                                       ex, enuExceptionType.SqlException);
//            }
//            catch (Exception ex)
//            {
//                throw new CustomizedException(String.Format("Fallo en {0} - GetUsuarios()", ClassName),
//                                                       ex, enuExceptionType.DataAccesException);
//            }

//            return dt;
//        }
        #endregion
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
    }
}
