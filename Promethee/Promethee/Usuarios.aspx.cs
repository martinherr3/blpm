using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using System.Text;

namespace Promethee
{
    public partial class Usuarios : System.Web.UI.Page
    {
        public int idUsuario
        {
            get
            {
                if (ViewState["idUsuario"] == null)
                    idUsuario = 0;

                return (int)ViewState["idUsuario"];
            }
            set { ViewState["idUsuario"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    CargarGrilla();
                }
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarDatos()) GuardarUsuario();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        private bool ValidarDatos()
        {
            StringBuilder mensaje = new StringBuilder();
            if (string.IsNullOrEmpty(txtNombre.Text.Trim()))
                mensaje.AppendLine("<br />Nombre");
            if (string.IsNullOrEmpty(txtApellido.Text.Trim()))
                mensaje.AppendLine("<br />Apellido");
            if (string.IsNullOrEmpty(txtLogin.Text.Trim()))
                mensaje.AppendLine("<br />Username");
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
                mensaje.AppendLine("<br />Password");
            if (!string.IsNullOrEmpty(mensaje.ToString()))
                mensaje.Insert(0, "Los siguientes datos no han sido ingresados: <br />", 1); ;
            lblMessage.InnerHtml = mensaje.ToString();
            return mensaje.ToString() == string.Empty;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Default.aspx");
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        protected void gvwUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int idUsuarioTest = 0;
                int.TryParse(e.CommandArgument.ToString(), out idUsuarioTest);
                switch (e.CommandName)
                {
                    case "eliminar":
                        idUsuario = idUsuarioTest;
                        mpeEliminar.Show();
                        break;
                    default:
                        idUsuario = 0;
                        break;
                }
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        protected void btnEliminar_OnClick(object sender, EventArgs e)
        {
            try
            {
                EliminarUsuario();
                CargarGrilla();
                mpeEliminar.Hide();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        protected void btnCerrarPopUp_Click(object sender, EventArgs e)
        {
            try
            {
                mpeEliminar.Hide();
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        private void ClearControls()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtLogin.Text = "";
            txtPassword.Text = "";
        }

        private void CargarGrilla()
        {
            List<UsuarioEntity> listaUsuarios = UsuariosDA.Select();
            gvwUsuarios.DataSource = listaUsuarios;
            gvwUsuarios.DataBind();
            udpGrilla.Update();
        }

        private void EliminarUsuario()
        {
            UsuariosDA.Delete(idUsuario);
        }

        private void GuardarUsuario()
        {
            UsuarioEntity usuario = new UsuarioEntity();

            usuario.nombre = txtNombre.Text;
            usuario.apellido = txtApellido.Text;
            usuario.username = txtLogin.Text;
            usuario.password = txtPassword.Text;

            usuario = LoginService.Insert(usuario);

            ClearControls();
            lblMessage.InnerHtml = string.Format("Se ha creado el usuario, ID: {0}", usuario.idUsuario);
            CargarGrilla();
        }
    }
}