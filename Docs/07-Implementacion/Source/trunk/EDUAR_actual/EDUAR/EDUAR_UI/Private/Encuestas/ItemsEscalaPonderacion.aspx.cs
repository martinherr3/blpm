using System;
using System.Collections.Generic;
using System.Text;
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
    public partial class ItemsEscalaPonderacion : EDUARBasePage
	{
		#region --[Atributos]--
		private BLValorEscala objBLValorEscala;
		#endregion

		#region --[Propiedades]--

        /// <summary>
        /// Gets or sets the escala de medicion sesion.
        /// </summary>
        /// <value>
        /// The escala de medicion sesion.
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

		/// <summary>
		/// Gets or sets the id escala medicion.
		/// </summary>
		/// <value>
		/// The id escala medicion.
		/// </value>
		public int idEscalaMedicion
		{
			get
			{
				if (ViewState["idEscalaMedicion"] == null)
					ViewState["idEscalaMedicion"] = 0;
				return (int)ViewState["idEscalaMedicion"];
			}
			set { ViewState["idEscalaMedicion"] = value; }
		}

		/// <summary>
        /// Gets or sets the valor escala editar.
		/// </summary>
		/// <value>
		/// The valor escala editar.
		/// </value>
		public ValorEscalaMedicion valorEscalaEditar
		{
			get
			{
                if (Session["valorEscalaEditar"] == null)
                    Session["valorEscalaEditar"] = new ValorEscalaMedicion();
                return (ValorEscalaMedicion)Session["valorEscalaEditar"];
			}
            set { Session["valorEscalaEditar"] = value; }
		}

		/// <summary>
		/// Mantiene la pregunta seleccionada en la grilla.
		/// </summary>
		/// <value>
		/// The prop encuesta.
		/// </value>
		public ValorEscalaMedicion propValorEscala
		{
			get
			{
                if (ViewState["propValorEscala"] == null)
                    propValorEscala = new ValorEscalaMedicion();

                return (ValorEscalaMedicion)ViewState["propValorEscala"];
			}
            set { ViewState["propValorEscala"] = value; }
		}

        public ValorEscalaMedicion propFiltroValorEscala
        {
            get
            {
                if (ViewState["propFiltroValorEscala"] == null)
                    propFiltroValorEscala = new ValorEscalaMedicion();

                return (ValorEscalaMedicion)ViewState["propFiltroValorEscala"];
            }
            set { ViewState["propFiltroValorEscala"] = value; }
        }

		/// <summary>
		/// Gets or sets the lista encuesta.
		/// </summary>
		/// <value>
		/// The lista agenda.
		/// </value>
		public List<ValorEscalaMedicion> listaValoresEscala
		{
			get
			{
                if (ViewState["listaValoresEscala"] == null) listaValoresEscala = new List<ValorEscalaMedicion>();

                return (List<ValorEscalaMedicion>)ViewState["listaValoresEscala"];
			}
            set { ViewState["listaValoresEscala"] = value; }
		}
		#endregion

		#region --[Eventos]--
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
					BuscarValoresEscala(null);
				}
				else
				{
					if (Request.Form["__EVENTTARGET"] == "btnGuardar")
						//llamamos el metodo que queremos ejecutar, en este caso el evento onclick del boton Guardar
						btnGuardar_Click(this, new EventArgs());
				}
			}
			catch (Exception ex)
			{
				AvisoMostrar = true;
				AvisoExcepcion = ex;
			}
		}

        /// <summary>
        /// Buscars the entidads.
        /// </summary>
        /// <param name="entidad">The entidad.</param>
        private void BuscarValoresEscala(ValorEscalaMedicion entidad)
        {
            CargarLista(escalaMedicionSesion);
            CargarGrilla();
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
                        BuscarValoresEscala(null);
						break;
					case enumAcciones.Guardar:
						AccionPagina = enumAcciones.Limpiar;
						GuardarValorEscala(ObtenerValoresDePantalla());
						Master.MostrarMensaje(enumTipoVentanaInformacion.Satisfactorio.ToString(), UIConstantesGenerales.MensajeGuardadoOk, enumTipoVentanaInformacion.Satisfactorio);
						break;
					case enumAcciones.Eliminar:
						AccionPagina = enumAcciones.Limpiar;
						EliminarValorEscala();
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
				btnVolver.Visible = true;
				btnNuevo.Visible = false;
				gvwItemsEscala.Visible = false;
				litEditar.Visible = false;
				litNuevo.Visible = true;
				udpEdit.Visible = true;
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
				{
					Master.MostrarMensaje(enumTipoVentanaInformacion.Advertencia.ToString(), UIConstantesGenerales.MensajeDatosFaltantes + mensaje, enumTipoVentanaInformacion.Advertencia);
				}
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
				Response.Redirect("~/Private/Encuestas/ManageEscalasPonderacion.aspx", true);
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
        protected void gvwItemsEscala_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				switch (e.CommandName)
				{
					case "Editar":
						propValorEscala.idValorEscala = Convert.ToInt32(e.CommandArgument.ToString());
						CargaValorEscala();
						break;
					case "Eliminar":
						AccionPagina = enumAcciones.Eliminar;
						propValorEscala.idValorEscala = Convert.ToInt32(e.CommandArgument.ToString());
						Master.MostrarMensaje("Eliminar Pregunta", "¿Desea <b>eliminar</b> el item seleccionado?", enumTipoVentanaInformacion.Confirmación);
                        BuscarValoresEscala(propFiltroValorEscala);
                        break;
                    case "Subir":
                        SubirUnNivel(Convert.ToInt32(e.CommandArgument.ToString()));
                        CargarLista(escalaMedicionSesion);
                        CargarGrilla();
                        break;
                    case "Bajar":
                        BajarUnNivel(Convert.ToInt32(e.CommandArgument.ToString()));
                        CargarLista(escalaMedicionSesion);
                        CargarGrilla();
                        break;
				}
			}
			catch (Exception ex)
			{
				Master.ManageExceptions(ex);
			}
		}

        private void SubirUnNivel(int idSeleccionado)
        {
            ValorEscalaMedicion valorEscalaUp = listaValoresEscala.Find(c => c.idValorEscala == idSeleccionado);
            ValorEscalaMedicion valorEscalaDown = listaValoresEscala.Find(c => c.orden == valorEscalaUp.orden - 1);

            valorEscalaDown.orden = valorEscalaUp.orden;
            valorEscalaUp.orden = valorEscalaUp.orden - 1;

            BLValorEscala objBLValorEscalaDown = new BLValorEscala(valorEscalaDown);
            BLValorEscala objBLValorEscalaUp = new BLValorEscala(valorEscalaUp);

            objBLValorEscalaDown.Save();
            objBLValorEscalaUp.Save();
        }

        private void BajarUnNivel(int idSeleccionado)
        {
            ValorEscalaMedicion valorEscalaDown = listaValoresEscala.Find(c => c.idValorEscala == idSeleccionado);
            ValorEscalaMedicion valorEscalaUp = listaValoresEscala.Find(c => c.orden == valorEscalaDown.orden + 1);

            valorEscalaDown.orden = valorEscalaUp.orden;
            valorEscalaUp.orden = valorEscalaDown.orden - 1;

            BLValorEscala objBLValorEscalaDown = new BLValorEscala(valorEscalaDown);
            BLValorEscala objBLValorEscalaUp = new BLValorEscala(valorEscalaUp);

            objBLValorEscalaDown.Save();
            objBLValorEscalaUp.Save();
        }

		/// <summary>
		/// Handles the PageIndexChanging event of the gvwEncuesta control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
        protected void gvwItemsEscala_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			try
			{
				gvwItemsEscala.PageIndex = e.NewPageIndex;
				CargarGrilla();
				btnVolver.Visible = true;
				gvwItemsEscala.Visible = false;
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
			gvwItemsEscala.DataSource = listaValoresEscala;
			gvwItemsEscala.DataBind();
			udpEdit.Visible = false;
			udpGrilla.Update();
		}

		/// <summary>
		/// Carga el contenido de la grilla de encuestas.
		/// </summary>
		private void CargarPresentacion()
		{
			LimpiarCampos();
			lblTitulo.Text = "de la escala: " + escalaMedicionSesion.nombre;
			udpEdit.Visible = false;
			btnVolver.Visible = true;
			btnNuevo.Visible = true;
			btnGuardar.Visible = false;
			gvwItemsEscala.Visible = true;
			udpFiltros.Update();
			udpGrilla.Update();
		}

		/// <summary>
		/// Cargars the lista.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		private void CargarLista(EscalaMedicion entidad)
		{
            objBLValorEscala = new BLValorEscala(propValorEscala);
            listaValoresEscala = objBLValorEscala.GetValoresEscalasMedicion(entidad);
		}

		/// <summary>
		/// Obteners the valores pantalla.
		/// </summary>
		/// <returns></returns>
		private ValorEscalaMedicion ObtenerValoresDePantalla()
		{
			ValorEscalaMedicion entidad = new ValorEscalaMedicion();
			entidad = propValorEscala;
            entidad.idEscalaMedicion = escalaMedicionSesion.idEscala;

			if (!esNuevo)
			{
				entidad.idValorEscala = propValorEscala.idValorEscala;
			}

			entidad.nombre = txtNombreEdit.Text.Trim();
			entidad.descripcion = txtDescripcionEdit.Text.Trim();

			return entidad;
		}

		/// <summary>
		/// Eliminar la pregunta.
		/// </summary>
		private void EliminarValorEscala()
		{
			ValorEscalaMedicion objEliminar = new ValorEscalaMedicion();
			objEliminar.idValorEscala = propValorEscala.idValorEscala;
            objEliminar.idEscalaMedicion = escalaMedicionSesion.idEscala;

			//escalaMedicionSesion.valoresEscalas.Clear();
            //escalaMedicionSesion.valoresEscalas.Add(objEliminar);

			objBLValorEscala = new BLValorEscala(objEliminar);
			objBLValorEscala.Delete();

			CargarPresentacion();
			BuscarValoresEscala(null);
		}

		/// <summary>
		/// Guardars the valor escala.
		/// </summary>
		/// <param name="entidad">The entidad.</param>
		private void GuardarValorEscala(ValorEscalaMedicion entidad)
		{
            entidad.idEscalaMedicion = escalaMedicionSesion.idEscala;
			BLValorEscala objBLValorEscala = new BLValorEscala(entidad);

            objBLValorEscala.Save();
 		}

		/// <summary>
		/// Cargars the entidad.
		/// </summary>
		private void CargarValoresEnPantalla(int idEntidad)
		{
			ValorEscalaMedicion valorEscala = listaValoresEscala.Find(c => c.idValorEscala == idEntidad);

			txtNombreEdit.Text = valorEscala.nombre;
            txtDescripcionEdit.Text = valorEscala.descripcion;
		}

		/// <summary>
		/// Validars the pagina.
		/// </summary>
		/// <returns></returns>
		private string ValidarPagina()
		{
			StringBuilder message = new StringBuilder();

			if (txtNombreEdit.Text.Trim().Length == 0)
				message.Append("- Nombre Valor Escala<br/>");
			if (txtDescripcionEdit.Text.Trim().Length == 0)
                message.Append("- Descripción Valor Escala<br/>");

            return message.ToString();
		}

		/// <summary>
		/// Cargas the agenda.
		/// </summary>
		private void CargaValorEscala()
		{
			AccionPagina = enumAcciones.Modificar;
			esNuevo = false;

			CargarValoresEnPantalla(propValorEscala.idValorEscala);

			litEditar.Visible = true;
			litNuevo.Visible = false;
			btnNuevo.Visible = false;
			btnVolver.Visible = true;
			btnGuardar.Visible = true;
			gvwItemsEscala.Visible = false;
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

			//reseteo este valor, el cual se actualizará cuando sea necesario
			propValorEscala = null;
		}
		#endregion
	}
}