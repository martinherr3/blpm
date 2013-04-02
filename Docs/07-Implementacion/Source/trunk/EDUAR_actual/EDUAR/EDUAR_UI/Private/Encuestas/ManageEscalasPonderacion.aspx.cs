using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_BusinessLogic.Encuestas;
using EDUAR_Entities;
using EDUAR_UI.Shared;
using EDUAR_UI.Utilidades;
using EDUAR_Utility.Constantes;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_UI
{
    public partial class ManageEscalasPonderacion : EDUARBasePage
	{
		#region --[Atributos]--
		private BLEscala objBLEscalaPonderacion;
		#endregion

		#region --[Propiedades]--
		/// <summary>
		/// Mantiene la encuesta seleccionada en la grilla.
		/// </summary>
		/// <value>
		/// The prop encuesta.
		/// </value>
		public EscalaMedicion propEscala
		{
			get
			{
                if (ViewState["propEscala"] == null)
                    propEscala = new EscalaMedicion();

                return (EscalaMedicion)ViewState["propEscala"];
			}
            set { ViewState["propEscala"] = value; }
		}

        public EscalaMedicion propFiltroEscala
        {
            get
            {
                if (ViewState["propFiltroEscala"] == null)
                    propFiltroEscala = new EscalaMedicion();

                return (EscalaMedicion)ViewState["propFiltroEscala"];
            }
            set { ViewState["propFiltroEscala"] = value; }
        }

		/// <summary>
		/// Gets or sets the lista encuesta.
		/// </summary>
		/// <value>
		/// The lista agenda.
		/// </value>
        public List<EscalaMedicion> listaEscalas
		{
			get
			{
                if (ViewState["listaEscalas"] == null)
                {
                    listaEscalas = new List<EscalaMedicion>();
                    listaEscalas = objBLEscalaPonderacion.GetEscalasMedicion(null);
                }
                return (List<EscalaMedicion>)ViewState["listaEscalas"];
			}
            set { ViewState["listaEscalas"] = value; }
		}

		public int idEscala
		{
			get
			{
                if (ViewState["idEscala"] == null) idEscala = 0;

                return (int)ViewState["idEscala"];
			}
            set { ViewState["idEscala"] = value; }
		}

		/// <summary>
		/// Gets or sets the encuesta sesion.
		/// </summary>
		/// <value>
		/// The encuesta sesion.
		/// </value>
        public EscalaMedicion escalaMedicionSesion
		{
			get
			{
                if (Session["escalaMedicionSesion"] == null)
                    escalaMedicionSesion = new EscalaMedicion();

                return (EscalaMedicion)Session["escalaMedicionSesion"];
			}
            set { Session["escalaMedicionSesion"] = value; }
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
                    //CargarLista(null);
					BuscarEscalaPonderacion(null);

					if (Request.UrlReferrer.AbsolutePath.Contains("ItemsEscalaPonderacion.aspx"))
					{
						//Lo que se hace en este bloque es restablecer los elementos a su estado anterior, dado que está volviendo desde otra página
					}
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
					case enumAcciones.Eliminar:
						AccionPagina = enumAcciones.Limpiar;
						EliminarEscala();
                        BuscarEscalaPonderacion(propFiltroEscala);
                        //CargarLista(null);
						//BuscarFiltrando();
						break;
					case enumAcciones.Limpiar:
						CargarPresentacion();
                        CargarLista(null);
						BuscarEscalaPonderacion(propFiltroEscala);
						break;
					case enumAcciones.Guardar:
						AccionPagina = enumAcciones.Limpiar;
						GuardarEscala(ObtenerValoresDePantalla());
						Master.MostrarMensaje(enumTipoVentanaInformacion.Satisfactorio.ToString(), UIConstantesGenerales.MensajeGuardadoOk, enumTipoVentanaInformacion.Satisfactorio);
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
		protected void btnNuevo_Click(object sender, EventArgs e)
		{
			try
			{
				AccionPagina = enumAcciones.Nuevo;
				LimpiarCampos();
				esNuevo = true;
				btnGuardar.Visible = true;
				//btnBuscar.Visible = false;
				btnVolver.Visible = true;
				btnNuevo.Visible = false;
                gvwEscalasPonderacion.Visible = false;
				litEditar.Visible = false;
				litNuevo.Visible = true;
				udpEdit.Visible = true;
				//udpFiltrosBusqueda.Visible = false;
				udpFiltros.Update();
				udpGrilla.Update();
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
					Master.MostrarMensaje(enumTipoVentanaInformacion.Advertencia.ToString(), UIConstantesGenerales.MensajeDatosFaltantes + mensaje, enumTipoVentanaInformacion.Advertencia);
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the Click event of the btnVolver control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void btnVolver_Click(object sender, EventArgs e)
		{
			try
			{
				CargarPresentacion();
                //CargarLista(new EscalaMedicion());
				BuscarEscalaPonderacion(propFiltroEscala);
				propEscala = new EscalaMedicion();
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
        protected void gvwEscalasPonderacion_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				switch (e.CommandName)
				{
					case "Editar":
						propEscala.idEscala = Convert.ToInt32(e.CommandArgument.ToString());
						CargaEscala();
						break;
					case "Eliminar":
                        propEscala.idEscala = Convert.ToInt32(e.CommandArgument.ToString());
						AccionPagina = enumAcciones.Eliminar;
						Master.MostrarMensaje("Eliminar Escala", "¿Desea <b>eliminar</b> la escala?", enumTipoVentanaInformacion.Confirmación);
						break;
					case "VerValoresEscala":
						AccionPagina = enumAcciones.Redirect;
						idEscala = Convert.ToInt32(e.CommandArgument.ToString());
						escalaMedicionSesion = listaEscalas.Find(p => p.idEscala == idEscala);
						Response.Redirect("ItemsEscalaPonderacion.aspx", false);
						break;
				}
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the PageIndexChanging event of the gvwEncuesta control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
        protected void gvwEscalasPonderacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			try
			{
                gvwEscalasPonderacion.PageIndex = e.NewPageIndex;
				CargarGrilla();
				//btnBuscar.Visible = false;
				btnVolver.Visible = true;
                gvwEscalasPonderacion.Visible = false;
				//udpFiltrosBusqueda.Visible = false;
				udpEdit.Visible = true;
				udpEdit.Update();
			}
			catch (Exception ex) { Master.ManageExceptions(ex); }
		}
		#endregion

		#region --[Métodos Privados]--
		/// <summary>
		/// Cargars the grilla.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="lista">The lista.</param>
		private void CargarGrilla()
		{
            gvwEscalasPonderacion.DataSource = UIUtilidades.BuildDataTable<EscalaMedicion>(listaEscalas).DefaultView;
            gvwEscalasPonderacion.DataBind();
			udpEdit.Visible = false;
			udpGrilla.Update();
		}

		/// <summary>
		/// Carga el contenido de la grilla de encuestas.
		/// </summary>
		private void CargarPresentacion()
		{
			LimpiarCampos();
			lblTitulo.Text = "Escalas de Ponderación";
			udpEdit.Visible = false;
			btnVolver.Visible = false;
			btnGuardar.Visible = false;
			//udpFiltrosBusqueda.Visible = true;
			//btnBuscar.Visible = true;
			btnNuevo.Visible = true;
            gvwEscalasPonderacion.Visible = true;
			udpFiltros.Update();
			udpGrilla.Update();
		}

		/// <summary>
		/// Cargars the lista.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		private void CargarLista(EscalaMedicion entidad)
		{
			objBLEscalaPonderacion = new BLEscala(entidad);
			listaEscalas = objBLEscalaPonderacion.GetEscalasMedicion(entidad);
		}

		/// <summary>
		/// Obteners the valores pantalla.
		/// </summary>
		/// <returns></returns>
		private EscalaMedicion ObtenerValoresDePantalla()
		{
            EscalaMedicion entidad = new EscalaMedicion();
			entidad = propEscala;

			if (!esNuevo)
			{
				entidad.idEscala = propEscala.idEscala;
			}

			entidad.nombre = txtNombreEdit.Text.Trim();
			entidad.descripcion = txtDescripcionEdit.Text.Trim();

			return entidad;
		}

		/// <summary>
		/// Guardars the encuesta.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		private void GuardarEscala(EscalaMedicion entidad)
		{
			objBLEscalaPonderacion = new BLEscala(entidad);
			objBLEscalaPonderacion.Save();
		}

		/// <summary>
		/// Cargars the entidad.
		/// </summary>
		private void CargarValoresEnPantalla(int idEntidad)
		{
            EscalaMedicion escala = listaEscalas.Find(c => c.idEscala == idEntidad);

			txtNombreEdit.Text = escala.nombre;
			txtDescripcionEdit.Text = escala.descripcion;
		}

		/// <summary>
		/// Validars the pagina.
		/// </summary>
		/// <returns></returns>
		private string ValidarPagina()
		{
			string mensaje = string.Empty;

			if (string.IsNullOrEmpty(txtNombreEdit.Text))
				mensaje += "- Nombre Escala<br />";

            if (string.IsNullOrEmpty(txtDescripcionEdit.Text))
                mensaje += "- Descripción Escala<br />";

			return mensaje;
		}

        /// <summary>
        /// Buscars the entidads.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        private void BuscarEscalaPonderacion(EscalaMedicion entidad)
        {
            CargarLista(entidad);
            CargarGrilla();
        }

		/// <summary>
		/// Cargas the agenda.
		/// </summary>
		private void CargaEscala()
		{
			AccionPagina = enumAcciones.Modificar;
			esNuevo = false;

			CargarValoresEnPantalla(propEscala.idEscala);

			litEditar.Visible = true;
			litNuevo.Visible = false;
			//btnBuscar.Visible = false;
			btnNuevo.Visible = false;
			btnVolver.Visible = true;
			btnGuardar.Visible = true;
            gvwEscalasPonderacion.Visible = false;
			//udpFiltrosBusqueda.Visible = false;
			udpEdit.Visible = true;
			udpFiltros.Update();
			udpEdit.Update();
		}

		/// <summary>
		/// Limpiar the campos.
		/// </summary>
		private void LimpiarCampos()
		{
			txtNombreEdit.Text = string.Empty;
			txtDescripcionEdit.Text = string.Empty;
		}

		/// <summary>
		/// Eliminars the encuesta.
		/// </summary>
		private void EliminarEscala()
		{
			EscalaMedicion escala = new EscalaMedicion() { idEscala = propEscala.idEscala};
			objBLEscalaPonderacion = new BLEscala(escala);
			objBLEscalaPonderacion.Delete();
		}
		#endregion
	}
}