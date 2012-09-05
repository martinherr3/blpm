using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_UI.Shared;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using System.Web.UI.DataVisualization.Charting;
using EDUAR_Entities.Reports;
using EDUAR_BusinessLogic.Reports;

namespace EDUAR_UI
{
	public partial class MntGeneral : EDUARBasePage
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
					cargarLinks();
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
			//    indi.diasHastaSecundario1 = Convert.ToInt32(txtHastaSecundario1.Text);
			//    indi.diasHastaSecundario2 = Convert.ToInt32(txtHastaSecundario2.Text);
			//    indi.verdePrincipal = Convert.ToInt32(txtVerdePrincipal.Text);
			//    indi.verdeSecundario1 = Convert.ToInt32(txtVerdeSecundario1.Text);
			//    indi.verdeSecundario2 = Convert.ToInt32(txtVerdeSecundario2.Text);
			//    indi.rojoPrincipal = Convert.ToInt32(txtRojoPrincipal.Text);
			//    indi.rojoSecundario1 = Convert.ToInt32(txtRojoSecundario1.Text);
			//    indi.rojoSecundario2 = Convert.ToInt32(txtRojoSecundario2.Text);

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
				cargarIndicador();
			}
		}
		#endregion

		#region --[Métodos Privados]--
		/// <summary>
		/// Cargars the links.
		/// </summary>
		private void cargarLinks()
		{
			int contador = 1;

			BLIndicador objBLIndicador = new BLIndicador();
			List<Indicador> listaIndicadores = objBLIndicador.GetIndicadores(null);
			foreach (Indicador item in listaIndicadores)
			{
				switch (contador)
				{
					case 1:
						LinkButton1.Text = item.nombre;
						LinkButton1.CommandArgument = item.idIndicador.ToString();
						break;
					case 2:
						LinkButton2.Text = item.nombre;
						LinkButton2.CommandArgument = item.idIndicador.ToString();
						break;
					case 3:
						LinkButton3.Text = item.nombre;
						LinkButton3.CommandArgument = item.idIndicador.ToString();
						break;
					case 4:
						LinkButton4.Text = item.nombre;
						LinkButton4.CommandArgument = item.idIndicador.ToString();
						break;
					case 5:
						LinkButton5.Text = item.nombre;
						LinkButton5.CommandArgument = item.idIndicador.ToString();
						break;
					case 6:
						LinkButton6.Text = item.nombre;
						LinkButton6.CommandArgument = item.idIndicador.ToString();
						break;
					default:
						break;
				}
				contador++;
			}
		}

		private void limpiarABMIndicador()
		{
			chkInvertirEscala.Checked = false;
			txtHastaPrincipal.Text = "";
			txtHastaSecundario1.Text = "";
			txtHastaSecundario2.Text = "";
			txtVerdePrincipal.Text = "";
			txtVerdeSecundario1.Text = "";
			txtVerdeSecundario2.Text = "";
			txtRojoPrincipal.Text = "";
			txtRojoSecundario1.Text = "";
			txtRojoSecundario2.Text = "";
		}

		private void cargarIndicador()
		{
			//if (IdIndicador != 0)
			//{
			//    //Indicador i = new Indicador();
			//    DB.IndicadoresRow indi = i.getIndicador(IdIndicador);
			//    lblTitulo.Text = "Indicador: " + indi.nombreIndicador;
			//    chkInvertirEscala.Checked = indi.invertirEscala;
			//    txtHastaPrincipal.Text = indi.diasHastaPrincipal.ToString();
			//    txtHastaSecundario1.Text = indi.diasHastaSecundario1.ToString();
			//    txtHastaSecundario2.Text = indi.diasHastaSecundario2.ToString();
			//    txtVerdePrincipal.Text = indi.verdePrincipal.ToString();
			//    txtVerdeSecundario1.Text = indi.verdeSecundario1.ToString();
			//    txtVerdeSecundario2.Text = indi.verdeSecundario2.ToString();
			//    txtRojoPrincipal.Text = indi.rojoPrincipal.ToString();
			//    txtRojoSecundario1.Text = indi.rojoSecundario1.ToString();
			//    txtRojoSecundario2.Text = indi.rojoSecundario2.ToString();
			//}
		}
		#endregion
	}
}