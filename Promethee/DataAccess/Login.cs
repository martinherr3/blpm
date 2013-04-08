using System;
using System.Configuration;
using System.Data.SqlClient;

namespace DataAccess
{
    public static class LoginService
    {
		/// <summary>
		/// Autenticars the specified usuario.
		/// </summary>
		/// <param name="usuario">The usuario.</param>
		/// <param name="password">The password.</param>
		/// <returns></returns>
        public static bool Autenticar(string usuario, string password)
        {
            string sql = @"SELECT COUNT(*)
                              FROM Usuarios
                              WHERE username = @nombre AND password = @password";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {
                conn.Open();

                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@nombre", usuario);

                string hash = Helper.EncodePassword(string.Concat(usuario, password));
                command.Parameters.AddWithValue("@password", hash);

                int count = Convert.ToInt32(command.ExecuteScalar());

                if (count == 0)
                    return false;
                else
                    return true;

            }
        }

		/// <summary>
		/// Inserts the specified nombre.
		/// </summary>
		/// <param name="nombre">The nombre.</param>
		/// <param name="apellido">The apellido.</param>
		/// <param name="nombreLogin">The nombre login.</param>
		/// <param name="password">The password.</param>
		/// <returns></returns>
        public static UsuarioEntity Insert(string nombre, string apellido, string nombreLogin, string password)
        {
            UsuarioEntity usuario = new UsuarioEntity();

            usuario.nombre = nombre;
            usuario.apellido = apellido;
            usuario.username = nombreLogin;
            usuario.password = password;

            return Insert(usuario);
        }

		/// <summary>
		/// Inserts the specified usuario.
		/// </summary>
		/// <param name="usuario">The usuario.</param>
		/// <returns></returns>
        public static UsuarioEntity Insert(UsuarioEntity usuario)
        {

            string sql = @"INSERT INTO Usuarios (
                                   nombre
                                  ,apellido
                                  ,username
                                  ,password)
                              VALUES (
                                    @nombre, 
                                    @apellido, 
                                    @username,
                                    @password)
                            SELECT SCOPE_IDENTITY()";


            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["default"].ToString()))
            {

                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("nombre", usuario.nombre);
                command.Parameters.AddWithValue("apellido", usuario.apellido);
                command.Parameters.AddWithValue("username", usuario.username);

                string password = Helper.EncodePassword(string.Concat(usuario.username, usuario.password));
                command.Parameters.AddWithValue("password", password);

                conn.Open();

                usuario.idUsuario = Convert.ToInt32(command.ExecuteScalar());

                return usuario;
            }
        }
    }
}
