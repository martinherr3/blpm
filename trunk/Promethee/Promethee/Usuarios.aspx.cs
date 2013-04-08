using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;

namespace Promethee
{
	public partial class Usuarios : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}
		protected void btnAceptar_Click(object sender, EventArgs e)
		{
			UsuarioEntity usuario = new UsuarioEntity();

			usuario.nombre = txtNombre.Text;
			usuario.apellido = txtApellido.Text;
			usuario.username = txtLogin.Text;
			usuario.password = txtPassword.Text;

			usuario = LoginService.Insert(usuario);

			ClearControls();
			lblMessage.InnerHtml = string.Format("Se ha creado el usuario, ID: {0}", usuario.idUsuario);

		}

		protected void btnCancelar_Click(object sender, EventArgs e)
		{
			Response.Redirect("~/Default.aspx");
		}

		private void ClearControls()
		{
			txtNombre.Text = "";
			txtApellido.Text = "";
			txtLogin.Text = "";
			txtPassword.Text = "";
		}
	}
}