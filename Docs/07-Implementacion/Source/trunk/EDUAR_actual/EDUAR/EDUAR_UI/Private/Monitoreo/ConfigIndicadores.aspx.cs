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
		#region --[Propiedades]--
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
				Master.BotonAvisoAceptar += (VentanaAceptar);
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

		/// <summary>
		/// Handles the Click event of the btnCancelar control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnCancelar_Click(object sender, EventArgs e)
		{
			try
			{
				divConfig.Visible = false;
				idIndicador = 0;
				mpeContenido.Hide();
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Ventanas the aceptar.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void VentanaAceptar(object sender, EventArgs e)
		{
			try
			{

			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the Click event of the btnGuardar control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnGuardar_Click(object sender, EventArgs e)
		{
			try
			{
				if (idIndicador != 0)
				{
					Indicador indSeleccion = new Indicador();
					indSeleccion = listaIndicadores.Find(p => p.idIndicador == idIndicador);
					indSeleccion.invertirEscala = chkInvertirEscala.Checked;
					indSeleccion.parametroCantidad = Convert.ToInt32(txtParametroCantidad.Text);
					indSeleccion.diasHastaPrincipal = Convert.ToInt32(txtHastaPrincipal.Text);
					indSeleccion.diasHastaIntermedio = Convert.ToInt32(txtHastaIntermedio.Text);
					indSeleccion.diasHastaSecundario = Convert.ToInt32(txtHastaSecundario.Text);
					indSeleccion.verdeNivelPrincipal = Convert.ToInt32(txtVerdePrincipal.Text);
					indSeleccion.verdeNivelIntermedio = Convert.ToInt32(txtVerdeIntermedio.Text);
					indSeleccion.verdeNivelSecundario = Convert.ToInt32(txtVerdeSecundario.Text);
					indSeleccion.rojoNivelPrincipal = Convert.ToInt32(txtRojoPrincipal.Text);
					indSeleccion.rojoNivelIntermedio = Convert.ToInt32(txtRojoIntermedio.Text);
					indSeleccion.rojoNivelSecundario = Convert.ToInt32(txtRojoSecundario.Text);

					BLIndicador objBLIndicador = new BLIndicador(indSeleccion);
					objBLIndicador.Save();
					CargarIndicadores();
					divConfig.Visible = false;
					idIndicador = 0;
					rfvTxtParametroCantidad.Enabled = false;
					cmvtxtParametroCantidad.Enabled = false;
					mpeContenido.Hide();
				}
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the PageIndexChanging event of the gvwIndicadores control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
		protected void gvwIndicadores_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{

		}

		/// <summary>
		/// Handles the RowCommand event of the gvwIndicadores control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
		protected void gvwIndicadores_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				switch (e.CommandName)
				{
					case "Editar":
						//lblTitulo.Text = "Editar Indicador";
						idIndicador = Convert.ToInt32(e.CommandArgument.ToString());
						CargarIndicador();
						divConfig.Visible = true;
						mpeContenido.Show();
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

		/// <summary>
		/// Cargars the indicador.
		/// </summary>
		private void CargarIndicador()
		{
			if (idIndicador != 0)
			{
				Indicador indSeleccion = new Indicador();
				indSeleccion = listaIndicadores.Find(p => p.idIndicador == idIndicador);
				lblTitulo.Text = indSeleccion.nombre;
				chkInvertirEscala.Checked = indSeleccion.invertirEscala;
				txtParametroCantidad.Text = indSeleccion.parametroCantidad.ToString();
				txtParametroCantidad.Visible = indSeleccion.parametroCantidad > 0;
				lblParametroCantidad.Visible = indSeleccion.parametroCantidad > 0;

				rfvTxtParametroCantidad.Enabled = indSeleccion.parametroCantidad > 0;
				cmvtxtParametroCantidad.Enabled = indSeleccion.parametroCantidad > 0;

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