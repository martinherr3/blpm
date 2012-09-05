using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Reports;
using EDUAR_Entities.Reports;
using EDUAR_UI.Shared;

namespace EDUAR_UI
{
	public partial class ConfigIndicadores : EDUARBasePage
	{
		#region --[Propiedades]
		/// <summary>
		/// Gets or sets the id indicador.
		/// </summary>
		/// <value>
		/// The id indicador.
		/// </value>
		public int idIndicador
		{
			get
			{
				if (ViewState["idIndicador"] != null)
					return Convert.ToInt32(ViewState["idIndicador"].ToString());
				return 0;
			}
			set { ViewState["idIndicador"] = value; }
		}

        public List<Indicador> listaIndicadores
        {
            get
            {
                if (ViewState["listaIndicadores"] == null)
                    ViewState["listaIndicadores"] = new List<Indicador>();

                return (List<Indicador>)ViewState["listaIndicadores"];
            }
            set { ViewState["listaIndicadores"] = value; }
        }
		#endregion

		#region --[Eventos]--
		/// <summary>
		/// Método que se ejecuta al dibujar los controles de la página.
		/// Se utiliza para gestionar las excepciones del método Page_Load().
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (AvisoMostrar)
			{
				AvisoMostrar = false;

				try
				{
					Master.ManageExceptions(AvisoExcepcion);
				}
				catch (Exception ex) { Master.ManageExceptions(ex); }
			}
		}

		/// <summary>
		/// Handles the Load event of the Page control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				if (!IsPostBack)
				{
					CargarIndicadores();
				}
			}
			catch (Exception ex)
			{
				AvisoMostrar = true;
				AvisoExcepcion = ex;
			}
		}

		protected void btnCancelar_Click(object sender, EventArgs e)
		{
			divConfig.Visible = false;
			//IdIndicador = 0;
		}

		protected void btnGuardar_Click(object sender, EventArgs e)
		{
			//if (IdIndicador != 0)
			//{
			//    Indicador i = new Indicador();
			//    DB.IndicadoresRow indi = i.getIndicador(IdIndicador);
			//    indi.invertirEscala = chkInvertirEscala.Checked;
			//    indi.diasHastaPrincipal = Convert.ToInt32(txtHastaPrincipal.Text);
			//    indi.diasHastaIntermedio = Convert.ToInt32(txtHastaIntermedio.Text);
			//    indi.diasHastaSecundario = Convert.ToInt32(txtHastaSecundario.Text);
			//    indi.verdePrincipal = Convert.ToInt32(txtVerdePrincipal.Text);
			//    indi.verdeIntermedio = Convert.ToInt32(txtVerdeIntermedio.Text);
			//    indi.verdeSecundario = Convert.ToInt32(txtVerdeSecundario.Text);
			//    indi.rojoPrincipal = Convert.ToInt32(txtRojoPrincipal.Text);
			//    indi.rojoIntermedio = Convert.ToInt32(txtRojoIntermedio.Text);
			//    indi.rojoSecundario = Convert.ToInt32(txtRojoSecundario.Text);

			//    i.Actualizar(indi);
			//}

			//divConfig.Visible = false;
			//IdIndicador = 0;
		}

		protected void LinkButton1_Click(object sender, EventArgs e)
		{
			divConfig.Visible = true;
			limpiarABMIndicador();
			string id = ((LinkButton)sender).CommandArgument;
			if (id != "")
			{
				//IdIndicador = Convert.ToInt32(id);
				CargarIndicador();
			}
		}

        protected void gvwIndicadores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvwIndicadores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Editar":
                        lblTitulo.Text = "Editar Indicador";
                        idIndicador = Convert.ToInt32(e.CommandArgument.ToString());
                        CargarIndicador();
                        divConfig.Visible = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }
		#endregion

		#region --[Métodos Privados]--
		/// <summary>
		/// Cargars the links.
		/// </summary>
        private void CargarIndicadores()
		{
			BLIndicador objBLIndicador = new BLIndicador();
            listaIndicadores = objBLIndicador.GetIndicadores(null);
            gvwIndicadores.DataSource = listaIndicadores;
            gvwIndicadores.DataBind();
            udpGrilla.Update();
		}

		private void limpiarABMIndicador()
		{
			chkInvertirEscala.Checked = false;
			txtHastaPrincipal.Text = "";
			txtHastaIntermedio.Text = "";
			txtHastaSecundario.Text = "";
			txtVerdePrincipal.Text = "";
			txtVerdeIntermedio.Text = "";
			txtVerdeSecundario.Text = "";
			txtRojoPrincipal.Text = "";
			txtRojoIntermedio.Text = "";
			txtRojoSecundario.Text = "";
            txtParametroCantdiad.Text = "";
            lblTitulo.Text = "";
		}

		private void CargarIndicador()
		{
            if (idIndicador != 0)
            {
                Indicador indSeleccion = new Indicador();
                indSeleccion = listaIndicadores.Find(p => p.idIndicador == idIndicador);
                lblTitulo.Text = indSeleccion.nombre;
                chkInvertirEscala.Checked = indSeleccion.invertirEscala;
                txtParametroCantdiad.Text = indSeleccion.parametroCantidad.ToString();
                txtHastaPrincipal.Text = indSeleccion.diasHastaPrincipal.ToString();
                txtHastaIntermedio.Text = indSeleccion.diasHastaIntermedio.ToString();
                txtHastaSecundario.Text = indSeleccion.diasHastaSecundario.ToString();
                txtVerdePrincipal.Text = indSeleccion.verdeNivelPrincipal.ToString();
                txtVerdeIntermedio.Text = indSeleccion.verdeNivelIntermedio.ToString();
                txtVerdeSecundario.Text = indSeleccion.verdeNivelSecundario.ToString();
                txtRojoPrincipal.Text = indSeleccion.rojoNivelPrincipal.ToString();
                txtRojoIntermedio.Text = indSeleccion.rojoNivelIntermedio.ToString();
                txtRojoSecundario.Text = indSeleccion.rojoNivelSecundario.ToString();
            }
		}
		#endregion

        
	}
}