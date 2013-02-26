using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Constantes;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Entities.DEC;

namespace EDUAR_UI
{
	public partial class ManageIndicadores : EDUARBasePage
	{
		#region --[Atributos]--
		private BLIndicador objBLIndicador;
		#endregion

		#region --[Propiedades]--
		/// <summary>
		/// Gets or sets the obj indicador.
		/// </summary>
		/// <value>
		/// The obj indicador.
		/// </value>
		public Indicador objIndicador
		{
			get
			{
				if (ViewState["objIndicador"] == null)
					objIndicador = new Indicador();

				return (Indicador)ViewState["objIndicador"];
			}
			set { ViewState["objIndicador"] = value; }
		}

		/// <summary>
		/// Gets or sets the lista indicadores.
		/// </summary>
		/// <value>
		/// The lista indicadores.
		/// </value>
		public List<Indicador> listaIndicadores
		{
			get
			{
				if (ViewState["listaIndicadores"] == null)
					listaIndicadores = new List<Indicador>();

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
				if (!Page.IsPostBack)
				{
					CargarPresentacion();
				}
			}
			catch (Exception ex)
			{
				AvisoMostrar = true;
				AvisoExcepcion = ex;
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
				switch (AccionPagina)
				{
					case enumAcciones.Limpiar:
						CargarPresentacion();
						break;
					case enumAcciones.Guardar:
						AccionPagina = enumAcciones.Limpiar;
						GuardarEntidad(ObtenerValoresDePantalla());
						Master.MostrarMensaje(enumTipoVentanaInformacion.Satisfactorio.ToString(), UIConstantesGenerales.MensajeGuardadoOk, enumTipoVentanaInformacion.Satisfactorio);
						break;
					case enumAcciones.Error:
						break;
					default:
						break;
				}
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the Click event of the btnNuevo control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnVolver_Click(object sender, EventArgs e)
		{
			try
			{
				AccionPagina = enumAcciones.Limpiar;
				LimpiarCampos();
				CargarPresentacion();
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the Click event of the btnAsignarRol control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnGuardar_Click(object sender, EventArgs e)
		{
			try
			{
				string mensaje = ValidarPagina();
				if (mensaje == string.Empty)
				{
					if (Page.IsValid)
					{
						AccionPagina = enumAcciones.Guardar;
						Master.MostrarMensaje(enumTipoVentanaInformacion.Confirmación.ToString(), UIConstantesGenerales.MensajeConfirmarCambios, enumTipoVentanaInformacion.Confirmación);
					}
				}
				else
				{
					AccionPagina = enumAcciones.Error;
					Master.MostrarMensaje(enumTipoVentanaInformacion.Advertencia.ToString(), UIConstantesGenerales.MensajeDatosFaltantes + mensaje, enumTipoVentanaInformacion.Advertencia);
				}
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Método que se llama al hacer click sobre las acciones de la grilla
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
		protected void gvwReporte_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				switch (e.CommandName)
				{
					case "Editar":
						objIndicador.idIndicador = Convert.ToInt32(e.CommandArgument.ToString());
						CargaIndicador();
						break;
				}
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the PageIndexChanging event of the gvwReporte control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
		protected void gvwReporte_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			try
			{
				gvwReporte.PageIndex = e.NewPageIndex;
				CargarGrilla();
			}
			catch (Exception ex) { Master.ManageExceptions(ex); }
		}
		#endregion

		#region --[Métodos Privados]--
		/// <summary>
		/// Cargars the presentacion.
		/// </summary>
		private void CargarPresentacion()
		{
			AccionPagina = enumAcciones.Limpiar;
			LimpiarCampos();
			BuscarIndicadores(new Indicador());
			udpEdit.Visible = false;
			btnVolver.Visible = false;
			btnGuardar.Visible = false;
			gvwReporte.Visible = true;
			udpGrilla.Update();
			udpBotonera.Update();
		}

		/// <summary>
		/// Limpiars the campos.
		/// </summary>
		private void LimpiarCampos()
		{
			txtEscala.Text = string.Empty;
			txtPesoDefault.Text = string.Empty;
			txtPesoMaximo.Text = string.Empty;
			txtPesoMinimo.Text = string.Empty;
			chkMaximiza.Checked = false;
		}

		/// Cargars the grilla.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="lista">The lista.</param>
		private void CargarGrilla()
		{
			gvwReporte.DataSource = UIUtilidades.BuildDataTable<Indicador>(listaIndicadores).DefaultView;
			gvwReporte.DataBind();
			udpEdit.Visible = false;
			udpGrilla.Update();
		}

		/// <summary>
		/// Buscars the entidads.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		private void BuscarIndicadores(Indicador entidad)
		{
			CargarLista(entidad);

			CargarGrilla();
		}

		/// <summary>
		/// Cargars the lista.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		private void CargarLista(Indicador entidad)
		{
			objBLIndicador = new BLIndicador();
			listaIndicadores = objBLIndicador.GetIndicadores(entidad);
		}

		/// <summary>
		/// Obteners the valores pantalla.
		/// </summary>
		/// <returns></returns>
		private Indicador ObtenerValoresDePantalla()
		{
			Indicador entidad = new Indicador();
			entidad = objIndicador;

			entidad.pesoDefault = Convert.ToDecimal(txtPesoDefault.Text);
			entidad.pesoMinimo = Convert.ToDecimal(txtPesoMinimo.Text);
			entidad.pesoMaximo = Convert.ToDecimal(txtPesoMaximo.Text);

			entidad.escala = txtEscala.Text.Trim();
			entidad.maximiza = chkMaximiza.Checked;

			foreach (ConfigFuncionPreferencia item in entidad.listaConfig)
			{
				switch (item.idFuncionPreferencia)
				{
					case 2:
						//Cuasi Criterio
						item.valorDefault = Convert.ToDecimal(txtCCLimIndiferencia.Text);
						break;
					case 3:
						//Pseudo Criterio con Preferencia Lineal
						item.valorDefault = Convert.ToDecimal(txtPCLimPreferencia.Text);
						break;
					case 4:
						//Level Criterio
						if (item.idValorFuncionPreferencia == 1)
							item.valorDefault = Convert.ToDecimal(txtLCLimIndiferencia.Text);
						else
							item.valorDefault = Convert.ToDecimal(txtLCLimPreferencia.Text);
						break;
					case 5:
						//Preferencia Lineal y Área de Indiferencia
						if (item.idValorFuncionPreferencia == 1)
							item.valorDefault = Convert.ToDecimal(txtPLLimIndiferencia.Text);
						else
							item.valorDefault = Convert.ToDecimal(txtPLLimPreferencia.Text);
						break;
					case 6:
						//Gaussiano
						item.valorDefault = Convert.ToDecimal(txtGSigma.Text);
						break;
					default:
						break;
				}
			}
			return entidad;
		}

		/// <summary>
		/// Guardars the agenda.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		private void GuardarEntidad(Indicador entidad)
		{
			objBLIndicador = new BLIndicador(entidad);
			objBLIndicador.Save();
		}

		/// <summary>
		/// Cargars the entidad.
		/// </summary>
		private void CargarValoresEnPantalla(int idIndicador)
		{
			Indicador entidad = listaIndicadores.Find(c => c.idIndicador == idIndicador);
			objIndicador = entidad;
			if (entidad != null)
			{
				litEditar.Text = entidad.nombre;
				txtEscala.Text = entidad.escala;
				txtPesoDefault.Text = entidad.pesoDefault.ToString();
				txtPesoMaximo.Text = entidad.pesoMaximo.ToString();
				txtPesoMinimo.Text = entidad.pesoMinimo.ToString();
				chkMaximiza.Checked = entidad.maximiza;

				foreach (ConfigFuncionPreferencia item in entidad.listaConfig)
				{
					switch (item.idFuncionPreferencia)
					{
						case 2:
							//Cuasi Criterio
							txtCCLimIndiferencia.Text = item.valorDefault.ToString("##.00");
							break;
						case 3:
							//Pseudo Criterio con Preferencia Lineal
							txtPCLimPreferencia.Text = item.valorDefault.ToString("##.00");
							break;
						case 4:
							//Level Criterio
							if (item.idValorFuncionPreferencia == 1)
								txtLCLimIndiferencia.Text = item.valorDefault.ToString("##.00");
							else
								txtLCLimPreferencia.Text = item.valorDefault.ToString("##.00");
							break;
						case 5:
							//Preferencia Lineal y Área de Indiferencia
							if (item.idValorFuncionPreferencia == 1)
								txtPLLimIndiferencia.Text = item.valorDefault.ToString("##.00");
							else
								txtPLLimPreferencia.Text = item.valorDefault.ToString("##.00");
							break;
						case 6:
							//Gaussiano
							txtGSigma.Text = item.valorDefault.ToString("##.00");
							break;
						default:
							break;
					}
				}
			}
		}

		/// <summary>
		/// Validars the pagina.
		/// </summary>
		/// <returns></returns>
		private string ValidarPagina()
		{
			string mensaje = string.Empty;
			decimal prueba = 0;

			decimal.TryParse(txtPesoDefault.Text, out prueba);
			if (!(prueba > 0))
				mensaje = " - Peso Predeterminado<br />";
			prueba = 0;
			decimal.TryParse(txtPesoMinimo.Text, out prueba);
			if (!(prueba > 0))
				mensaje += " - Peso Mínimo<br />";
			prueba = 0; 
			decimal.TryParse(txtPesoMaximo.Text, out prueba);
			if (!(prueba > 0))
				mensaje += " - Peso Máximo<br />";

			prueba = 0;
			decimal.TryParse(txtCCLimIndiferencia.Text, out prueba);
			if (!(prueba > 0))
				mensaje += " - Cuasi Criterio: Límite de Indiferencia<br />";

			prueba = 0;
			decimal.TryParse(txtPCLimPreferencia.Text, out prueba);
			if (!(prueba > 0))
				mensaje += " - Pseudo Criterio con Preferencia Lineal: Límite de Preferencia<br />";

			prueba = 0;
			decimal.TryParse(txtLCLimIndiferencia.Text, out prueba);
			if (!(prueba > 0))
				mensaje += " - Level Criterio: Límite de Indiferencia<br />";
			prueba = 0;
			decimal.TryParse(txtLCLimPreferencia.Text, out prueba);
			if (!(prueba > 0))
				mensaje += " - Level Criterio: Límite de Preferencia<br />";

			prueba = 0;
			decimal.TryParse(txtPLLimIndiferencia.Text, out prueba);
			if (!(prueba > 0))
				mensaje += " - Preferencia Lineal y Área de Indiferencia: Límite de Indiferencia<br />";
			prueba = 0;
			decimal.TryParse(txtPLLimPreferencia.Text, out prueba);
			if (!(prueba > 0))
				mensaje += " - Preferencia Lineal y Área de Indiferencia: Límite de Preferencia<br />";

			prueba = 0;
			decimal.TryParse(txtGSigma.Text, out prueba);
			if (!(prueba > 0))
				mensaje += " - Criterio Gaussiano: Sigma<br />";

			return mensaje;
		}

		/// <summary>
		/// Cargas the agenda.
		/// </summary>
		private void CargaIndicador()
		{
			AccionPagina = enumAcciones.Modificar;
			esNuevo = false;
			CargarValoresEnPantalla(objIndicador.idIndicador);
			litEditar.Visible = true;
			btnVolver.Visible = true;
			btnGuardar.Visible = true;
			gvwReporte.Visible = false;
			udpEdit.Visible = true;
			udpEdit.Update();
			udpBotonera.Update();
		}

		#endregion
	}
}