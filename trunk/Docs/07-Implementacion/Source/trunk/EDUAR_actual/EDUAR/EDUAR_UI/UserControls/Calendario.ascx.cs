using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_Utility.Enumeraciones;
using EDUAR_Utility.Excepciones;

namespace EDUAR_UI.UserControls
{
	public partial class Calendario : UserControl
	{
		#region --[Enumeraciones]--
		/// <summary>
		/// Enumeración que contiene el tipo de Calendario que se mostrara.
		/// </summary>
		public enum enumTipoCalendario
		{
			/// <summary>
			/// Muestra un calendario con el label Desde.
			/// </summary>
			Desde,

			/// <summary>
			/// Muestra un calendario con el label Desde y Hasta.
			/// </summary>
			DesdeHasta,

			/// <summary>
			/// Muestra un calendario sin label.
			/// </summary>
			SoloFecha
		}

		/// <summary>
		/// Enumeración que contiene el tipo de Calendario que se mostrara.
		/// </summary>
		public enum enumTipoAlineacion
		{
			/// <summary>
			/// Alinea los labels a la izquierda.
			/// </summary>
			Izquierda,

			/// <summary>
			/// Alinea los labels a la derecha.
			/// </summary>
			Derecha,

			/// <summary>
			/// Alinea los labels al centro.
			/// </summary>
			Centro,

			/// <summary>
			/// Alinea los labels justificado.
			/// </summary>
			Justificado
		}
		#endregion

		#region --[Propiedades]--

		/// <summary>
		/// Indica que tipo de ventana se va a mostrar.
		/// </summary>
		public enumTipoCalendario TipoCalendario { get; set; }

		/// <summary>
		/// Indica que tipo de alineación tendran los labels.
		/// </summary>
		public enumTipoAlineacion TipoAlineacion { get; set; }

		/// <summary>
		/// Etiqueta que tendra el label desde.
		/// </summary>
		public string EtiquetaDesde
		{
			set
			{
				lblFechaDesde.Text = value;
				lblFechaDesde_DA.Text = value;
			}
		}

		/// <summary>
		/// Etiqueta que tendra el label Hasta.
		/// </summary>
		public string EtiquetaHasta
		{
			set { lblFechaHasta_DA.Text = value; }
		}

		/// <summary>
		/// Valor a mostrar asociado al mensaje de error de validación.
		/// </summary>
		public string MensajeErrorValidacion
		{
			set { hndMensajeErrorValidacion.Value = value; }
		}

		/// <summary>
		/// textbox que contiene la fecha desde.
		/// </summary>
		public TextBox FechaDesde
		{
			get
			{
				switch (TipoCalendario)
				{
					case enumTipoCalendario.Desde:
						return txtFechaDesde;
					case enumTipoCalendario.DesdeHasta:
						return txtFechaDesde_DA;
					//case enumTipoCalendario.SoloFecha:
					//    break;
					//default:
					//    break;
				}
				return null;
			}
			set
			{
				switch (TipoCalendario)
				{
					case enumTipoCalendario.Desde:
						txtFechaDesde = value;
						break;
					case enumTipoCalendario.DesdeHasta:
						txtFechaDesde_DA = value;
						break;
					case enumTipoCalendario.SoloFecha:
						break;
					default:
						break;
				}
			}
		}

		/// <summary>
		/// textbox que contiene la fecha hasta.
		/// </summary>
		public TextBox FechaHasta
		{
			get { return txtFechaHasta_DA; }
			set { txtFechaHasta_DA = value; }
		}

		/// <summary>
		/// textbox que contiene una fecha
		/// </summary>
		public TextBox Fecha
		{
			get { return txtFecha; }
			set { txtFecha = value; }
		}

		/// <summary>
		/// textbox que contiene la fecha desde.
		/// </summary>
		public DateTime? ValorFechaDesde
		{
			get
			{
				string strValor = string.Empty;
				if (TipoCalendario == enumTipoCalendario.Desde)
					strValor = txtFechaDesde.Text;
				else
					strValor = txtFechaDesde_DA.Text;

				DateTime? retorno = null;
				if (!strValor.Equals(string.Empty))
				{
					strValor += " 00:00:00";
					DateTime valor = Convert.ToDateTime(strValor);
					//valor.AddHours(0);
					//valor.AddMinutes(0);
					//valor.AddSeconds(0);
					retorno = valor;
				}
				return retorno;
			}
		}

		/// <summary>
		/// textbox que contiene la fecha hasta.
		/// </summary>
		public DateTime? ValorFechaHasta
		{
			get
			{
				string strValor = string.Empty;
				strValor = txtFechaHasta_DA.Text;
				DateTime? retorno = null;
				if (!strValor.Equals(string.Empty))
				{
					strValor += " 23:59:59";
					DateTime valor = Convert.ToDateTime(strValor);
					//valor. .AddHours(23);
					//valor.AddMinutes(59);
					//valor.AddSeconds(59);
					retorno = valor;
				}
				return retorno;
			}
		}

		/// <summary>
		/// textbox que contiene la fecha desde.
		/// </summary>
		public DateTime? ValorFecha
		{
			get
			{
				string strValor = string.Empty;
				if (TipoCalendario == enumTipoCalendario.SoloFecha)
					strValor = txtFecha.Text;

				DateTime? retorno = null;
				if (!strValor.Equals(string.Empty))
				{
					strValor += " 00:00:00";
					DateTime valor = Convert.ToDateTime(strValor);
					//valor.AddHours(0);
					//valor.AddMinutes(0);
					//valor.AddSeconds(0);
					retorno = valor;
				}
				return retorno;
			}
		}

		/// <summary>
		/// textbox que contiene la fecha desde.
		/// </summary>
		public Boolean Habilitado
		{
			get
			{
				switch (TipoCalendario)
				{
					case enumTipoCalendario.Desde:
						if (ViewState["Habilitado"] != null)
							return Convert.ToBoolean(ViewState["Habilitado"]);
						else
							return true;
					case enumTipoCalendario.DesdeHasta:
						break;
					case enumTipoCalendario.SoloFecha:
						if (ViewState["Habilitado"] != null)
							return Convert.ToBoolean(ViewState["Habilitado"]);
						break;
					default:
						return true;
				}
				return true;
			}
			set
			{
				switch (TipoCalendario)
				{
					case enumTipoCalendario.Desde:
						ViewState["Habilitado"] = value;
						if (Habilitado)
						{
							txtFechaDesde.Enabled = true;
							calExtDesde.Enabled = true;
						}
						else
						{
							txtFechaDesde.Enabled = false;
							calExtDesde.Enabled = false;
						}
						break;
					case enumTipoCalendario.DesdeHasta:
						Habilitado = true;
						break;
					case enumTipoCalendario.SoloFecha:
						ViewState["Habilitado"] = value;
						if (Habilitado)
						{
							txtFecha.Enabled = true;
							calExtFecha.Enabled = true;
						}
						else
						{
							txtFecha.Enabled = false;
							calExtFecha.Enabled = false;
						}
						break;
					default:
						break;
				}
			}
		}

		/// <summary>
		/// Obtiene o establece la fecha de inicio para un calendario.
		/// </summary>
		/// <value>
		/// The start date.
		/// </value>
		public DateTime? startDate
		{
			get
			{
				DateTime? fecha = DateTime.Now;
				switch (TipoCalendario)
				{
					case enumTipoCalendario.Desde:
						fecha = calExtDesde.StartDate;
						break;
					case enumTipoCalendario.DesdeHasta:
						fecha = calExtFechaDesde_DA.StartDate;
						//calExtFechaHasta_DA.StartDate = fecha;
						break;
					case enumTipoCalendario.SoloFecha:
						fecha = calExtFecha.StartDate;
						break;
					default:
						fecha = null;
						break;
				}
				return fecha;
			}
			set
			{
				switch (TipoCalendario)
				{
					case enumTipoCalendario.Desde:
						calExtDesde.StartDate = value;
						//calExtDesde.SelectedDate = value;
						break;
					case enumTipoCalendario.DesdeHasta:
						calExtFechaDesde_DA.StartDate = value;
						calExtFechaHasta_DA.StartDate = value;
						calExtFechaDesde_DA.SelectedDate = value;
						break;
					case enumTipoCalendario.SoloFecha:
						calExtFecha.StartDate = value;
						//calExtFecha.SelectedDate = value;
						break;
					default:
						break;
				}
			}
		}

		/// <summary>
		/// Obtiene o establece la fecha de fin para un calendario.
		/// </summary>
		/// <value>
		/// The end date.
		/// </value>
		public DateTime? endDate
		{
			get
			{
				DateTime? fecha = DateTime.Now;
				switch (TipoCalendario)
				{
					case enumTipoCalendario.Desde:
						fecha = calExtDesde.EndDate;
						break;
					case enumTipoCalendario.DesdeHasta:
						fecha = calExtFechaDesde_DA.EndDate;
						//calExtFechaHasta_DA.EndDate = fecha;
						break;
					case enumTipoCalendario.SoloFecha:
						fecha = calExtFecha.EndDate;
						break;
					default:
						fecha = null;
						break;
				}
				return fecha;
			}
			set
			{
				switch (TipoCalendario)
				{
					case enumTipoCalendario.Desde:
						calExtDesde.EndDate = value;
						//calExtDesde.SelectedDate = value;
						break;
					case enumTipoCalendario.DesdeHasta:
						calExtFechaDesde_DA.EndDate = value;
						calExtFechaHasta_DA.EndDate = value;
						calExtFechaHasta_DA.SelectedDate = value;
						break;
					case enumTipoCalendario.SoloFecha:
						calExtFecha.EndDate = value;
						//calExtFecha.SelectedDate = value;
						break;
					default:
						break;
				}
			}
		}
		#endregion

		#region --[Eventos]--
		/// <summary>
		/// Método que se ejecuta al Cargar la página. 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				switch (TipoCalendario)
				{
					case enumTipoCalendario.Desde:
						DivDesde.Attributes.Add("style", "display:block");
						DivDesdeHasta.Attributes.Add("style", "display:none");
						DivSoloFecha.Attributes.Add("style", "display:none");
						break;
					case enumTipoCalendario.DesdeHasta:
						DivDesdeHasta.Attributes.Add("style", "display:block");
						DivDesde.Attributes.Add("style", "display:none");
						DivSoloFecha.Attributes.Add("style", "display:none");
						break;
					case enumTipoCalendario.SoloFecha:
						DivDesdeHasta.Attributes.Add("style", "display:none");
						DivDesde.Attributes.Add("style", "display:none");
						DivSoloFecha.Attributes.Add("style", "display:block");
						break;
					default:
						break;
				}
				AplicarAlineacion();
				//LimpiarControles();
			}
		}

		#endregion

		#region --[Métodos Privados]--
		private void AplicarAlineacion()
		{
			switch (TipoAlineacion)
			{
				case enumTipoAlineacion.Izquierda:
					divEtiquetaDesdeDA.Attributes.Add("style", "vertical-align: middle;text-align: left");
					divEtiquetaHastaDA.Attributes.Add("style", "vertical-align: middle;text-align: left");
					divEtiquetaDesdeD.Attributes.Add("style", "vertical-align: middle;text-align: left");
					break;
				case enumTipoAlineacion.Derecha:
					divEtiquetaDesdeDA.Attributes.Add("style", "vertical-align: middle;text-align: right");
					divEtiquetaHastaDA.Attributes.Add("style", "vertical-align: middle;text-align: right");
					divEtiquetaDesdeD.Attributes.Add("style", "vertical-align: middle;text-align: right");
					break;
				case enumTipoAlineacion.Centro:
					divEtiquetaDesdeDA.Attributes.Add("style", "vertical-align: middle;text-align: center");
					divEtiquetaHastaDA.Attributes.Add("style", "vertical-align: middle;text-align: center");
					divEtiquetaDesdeD.Attributes.Add("style", "vertical-align: middle;text-align: center");
					break;
				case enumTipoAlineacion.Justificado:
					divEtiquetaDesdeDA.Attributes.Add("style", "vertical-align: middle;text-align: justify");
					divEtiquetaHastaDA.Attributes.Add("style", "vertical-align: middle;text-align: justify");
					divEtiquetaDesdeD.Attributes.Add("style", "vertical-align: middle;text-align: justify");
					break;
				default:
					divEtiquetaDesdeDA.Attributes.Add("style", "vertical-align: middle;text-align: left");
					divEtiquetaHastaDA.Attributes.Add("style", "vertical-align: middle;text-align: left");
					divEtiquetaDesdeD.Attributes.Add("style", "vertical-align: middle;text-align: left");
					break;
			}

		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Limpia los controles.
		/// </summary>
		public void LimpiarControles()
		{
			txtFechaDesde.Text = string.Empty;
			txtFechaDesde_DA.Text = string.Empty;
			txtFechaHasta_DA.Text = string.Empty;
			txtFecha.Text = string.Empty;
		}

		/// <summary>
		/// Valida que el formato de la Fecha Desde sea correcto.
		/// </summary>
		/// <returns>Retonar una excepcion de tipo validacion en caso de tener un formato incorrecto.
		/// La excepcion que retorna es "El campo "+ lblFechaDesde.Text+" tiene un formato incorrecto".
		/// </returns>
		private void ValidarFormatoDesdeEx()
		{
			if (!ValidarFormatoDesde())
			{
				string campo = lblFechaDesde.Text;
				if (lblFechaDesde.Text.Trim().Length == 0)
					campo = "Fecha";
				throw new CustomizedException(string.Format("El campo {0} tiene un formato incorrecto", campo), null, enuExceptionType.ValidationException);
			}

			DateTime fechaDesde;
			bool boolFechaDesde = false;
			if (TipoCalendario == enumTipoCalendario.Desde)
				boolFechaDesde = DateTime.TryParse(txtFechaDesde.Text.Trim(), out fechaDesde);
			else
				boolFechaDesde = DateTime.TryParse(txtFechaDesde_DA.Text.Trim(), out fechaDesde);
			if (TipoCalendario == enumTipoCalendario.SoloFecha)
				boolFechaDesde = DateTime.TryParse(txtFecha.Text.Trim(), out fechaDesde);

			if (boolFechaDesde)
			{
				int anio = fechaDesde.Year;
				if (anio < 1753 || anio > 9999)
					throw new CustomizedException(string.Format("El campo {0} tiene un formato incorrecto. El año debe estar entre 1753 y 9999", lblFechaDesde_DA.Text), null, enuExceptionType.ValidationException);
			}
		}

		/// <summary>
		/// Valida que el formato de la Fecha Desde sea correcto.
		/// </summary>
		/// <returns>Retonar 
		/// TRUE en caso que el formato sea correro y 
		/// FALSE en caso de ser incorrecto</returns>
		private Boolean ValidarFormatoDesde()
		{
			DateTime fechaDesde;
			if (TipoCalendario == enumTipoCalendario.Desde)
			{
				return txtFechaDesde.Text.Trim() == string.Empty ||
					   (DateTime.TryParse(txtFechaDesde.Text.Trim(), out fechaDesde));
			}
			if (TipoCalendario == enumTipoCalendario.SoloFecha)
			{
				return txtFecha.Text.Trim() == string.Empty ||
					   (DateTime.TryParse(txtFecha.Text.Trim(), out fechaDesde));
			}
			return txtFechaDesde_DA.Text.Trim() == string.Empty ||
				   (DateTime.TryParse(txtFechaDesde_DA.Text.Trim(), out fechaDesde));
		}

		/// <summary>
		/// Valida que el formato de la Fecha Hasta sea correcto.
		/// </summary>
		/// <returns>Retonar una excepcion de tipo validacion en caso de tener un formato incorrecto.
		/// La excepcion que retorna es "El campo "+ lblFechaDesde.Text+" tiene un formato incorrecto".
		/// </returns>
		private void ValidarFormatoHastaEx()
		{
			if (!ValidarFormatoHasta())
				throw new CustomizedException(string.Format("El campo {0} tiene un formato incorrecto", lblFechaHasta_DA.Text), null, enuExceptionType.ValidationException);

			DateTime fechaHasta;
			if (DateTime.TryParse(txtFechaHasta_DA.Text.Trim(), out fechaHasta))
			{
				int anio = fechaHasta.Year;
				if (anio < 1753 || anio > 9999)
					throw new CustomizedException(string.Format("El campo {0} tiene un formato incorrecto. El año debe estar entre 1753 y 9999", lblFechaHasta_DA.Text), null, enuExceptionType.ValidationException);
			}
		}

		/// <summary>
		/// Valida que el formato de la Fecha Hasta sea correcto.
		/// </summary>
		/// <returns>Retonar 
		/// TRUE en caso que el formato sea correro y 
		/// FALSE en caso de ser incorrecto</returns>
		private Boolean ValidarFormatoHasta()
		{
			if (txtFechaHasta_DA.Text.Trim() == string.Empty)
				return true;

			DateTime fechaDesde;
			return (DateTime.TryParse(txtFechaHasta_DA.Text.Trim(), out fechaDesde));
		}

		/// <summary>
		/// Valida que el formato de la Fecha Desde y Hasta sea correcto.
		/// </summary>
		/// <param name="fechaHastaHoy">Determina si la fecha Hasta debe ser considerada, de no existir, como la fecha actual</param>
		/// <returns>Retonar 
		/// TRUE en caso que el formato sea correro y 
		/// FALSE en caso de ser incorrecto</returns>
		private Boolean ValidarFormatoDesdeHasta(bool fechaHastaHoy)
		{
			bool fechasValidas = false;
			bool retorno = false;
			DateTime fechaDesde;
			DateTime fechaHasta;

			bool boolFechaDesdeCorrecta = false;
			bool boolFechaHastaCorrecta = false;

			boolFechaDesdeCorrecta = (DateTime.TryParse(txtFechaDesde_DA.Text.Trim(), out fechaDesde));
			boolFechaHastaCorrecta = (DateTime.TryParse(txtFechaHasta_DA.Text.Trim(), out fechaHasta));

			if (fechaHastaHoy && txtFechaHasta_DA.Text.Trim().Equals(string.Empty))
			{
				boolFechaHastaCorrecta = true;
				fechaHasta = DateTime.Now;
			}

			if ((txtFechaDesde_DA.Text.Trim() == string.Empty || boolFechaDesdeCorrecta)
				&& (txtFechaHasta_DA.Text.Trim() == string.Empty || boolFechaHastaCorrecta))
				fechasValidas = true;

			if (fechasValidas)
			{
				if (!txtFechaDesde_DA.Text.Trim().Equals(string.Empty)
					&& !txtFechaHasta_DA.Text.Trim().Equals(string.Empty))
				{
					if (fechaDesde.Date <= fechaHasta.Date)
						retorno = true;
				}
				else
					retorno = true;
			}
			return retorno;
		}


		/// <summary>
		/// Valida que el formato de la Fecha Desde y Hasta sea correcto.
		/// </summary>
		/// <param name="fechaHastaHoy">Determina si la fecha Hasta debe ser considerada, de no existir, como la fecha actual</param>
		/// <returns>Retonar una excepcion de tipo validacion en caso de tener un formato incorrecto.
		/// La excepcion que retorna es "El campo "+ lblFechaDesde.Text+" tiene un formato incorrecto".
		/// </returns>
		private void ValidarFormatoDesdeHastaEx(bool fechaHastaHoy)
		{
			ValidarFormatoHastaEx();
			ValidarFormatoDesdeEx();

			if (!ValidarFormatoDesdeHasta(fechaHastaHoy))
			{
				string campo = lblFechaDesde_DA.Text;
				if (lblFechaDesde_DA.Text.Trim().Length == 0)
					campo = "Fecha";
				throw new CustomizedException(string.Format("Período incorrecto en {0}. La fecha inicial debe ser menor o igual a la fecha final.", this.lblFechaDesde_DA.Text + " " + this.lblFechaHasta_DA.Text), null, enuExceptionType.ValidationException);
			}
		}


		/// <summary>
		/// Método utilizado para validar las fechas en calendario Desde - Hasta
		/// </summary>
		public void ValidarRangoDesdeHasta()
		{
			ValidarFormatoDesdeEx();
			ValidarFormatoHastaEx();
			ComprobarRangosDesdeHasta();
		}

		/// <summary>
		/// Método utilizado para validar las fechas en calendario Desde - Hasta
		/// </summary>
		/// <param name="comprobarFechaHoy">if set to <c>true</c> [comprobar fecha hoy].</param>
		public void ValidarRangoDesdeHasta(bool comprobarFechaHoy)
		{
			ValidarFormatoDesdeEx();
			ValidarFormatoHastaEx();
			ComprobarRangosDesdeHasta(comprobarFechaHoy);
		}

		/// <summary>
		/// Método utilizado para validar las fechas en calendario Desde
		/// </summary>
		public void ValidarRangoDesde()
		{
			ValidarFormatoDesdeEx();
			ComprobarRangosDesde();
		}

		/// <summary>
		/// Método utilizado para validar las fechas en calendario Desde
		/// </summary>
		/// <param name="comprobarFechaHoy">Indica si el campo se debe validar que no sea superior a la fecha de hoy (TRUE) o si no se valida fecha superior a la de hoy (FALSE)</param>
		public void ValidarRangoDesde(bool comprobarFechaHoy)
		{
			ValidarFormatoDesdeEx();
			ComprobarRangosDesde(comprobarFechaHoy);
		}

		/// <summary>
		/// Método utilizado para validar las fechas en calendario Desde
		/// </summary>
		/// <param name="comprobarFechaHoy">Indica si el campo se debe validar que no sea superior a la fecha de hoy (TRUE) o si no se valida fecha superior a la de hoy (FALSE)</param>
		public void ValidarRangoDesde(bool comprobarFechaPosteriorHoy, bool comprobarFechaAnteriorHoy)
		{
			ValidarFormatoDesdeEx();
			ComprobarRangosDesde(comprobarFechaPosteriorHoy, comprobarFechaAnteriorHoy);
			ComprobarDiaHabil();
		}

		private void ComprobarRangosDesdeHasta()
		{ ComprobarRangosDesdeHasta(true); }

		private void ComprobarRangosDesdeHasta(bool comprobarFechaHoy)
		{
			bool boolFechaDesdeCorrecta = false;
			bool boolFechaHastaCorrecta = false;

			DateTime fechaDesde;
			DateTime fechaHasta;

			//if (txtFechaDesde_DA.Text.Trim().Equals(string.Empty))
			//    //txtFechaDesde_DA.Text = DateTime.Now.AddDays(-7).ToShortDateString();
			//    txtFechaDesde_DA.Text = new DateTime(1753, 1, 1).ToString();

			//if (txtFechaHasta_DA.Text.Trim().Equals(string.Empty))
			//    //txtFechaHasta_DA.Text = DateTime.Now.ToShortDateString();
			//    txtFechaHasta_DA.Text = new DateTime(9999, 12, 31).ToString(); 

			boolFechaDesdeCorrecta = (DateTime.TryParse(txtFechaDesde_DA.Text.Trim(), out fechaDesde));
			boolFechaHastaCorrecta = (DateTime.TryParse(txtFechaHasta_DA.Text.Trim(), out fechaHasta));

			if (comprobarFechaHoy)
				//Valida que la fecha final no sea superior a la fecha del día
				if (boolFechaHastaCorrecta)
					if (fechaHasta.Date > DateTime.Now.Date)
						throw new CustomizedException(string.Format("Período incorrecto en {0}. La fecha final no puede ser superior a la fecha actual.", this.lblFechaDesde_DA.Text.Replace(":", "") + " " + this.lblFechaHasta_DA.Text.Replace(":", "")), null, enuExceptionType.ValidationException);

			//Valida que la fecha inicial no sea superior a la fecha final
			if (boolFechaDesdeCorrecta && boolFechaHastaCorrecta)
				if (fechaDesde.Date > fechaHasta.Date)
					throw new CustomizedException(string.Format("Período incorrecto en {0}. La fecha inicial debe ser menor o igual a la fecha final.", this.lblFechaDesde_DA.Text.Replace(":", "") + " " + this.lblFechaHasta_DA.Text.Replace(":", "")), null, enuExceptionType.ValidationException);
		}

		private void ComprobarRangosDesde(bool comprobarFechaHoy)
		{
			bool boolFechaDesdeCorrecta = false;
			DateTime fechaDesde;

			boolFechaDesdeCorrecta = (DateTime.TryParse(txtFechaDesde.Text.Trim(), out fechaDesde));

			if (comprobarFechaHoy)
			{
				//Valida que la fecha final no sea superior a la fecha del día
				if (boolFechaDesdeCorrecta)
					if (fechaDesde.Date > DateTime.Now.Date)
					{
						string campoDesde = lblFechaDesde_DA.Text;
						if (lblFechaDesde_DA.Text.Trim().Length == 0)
							campoDesde = "Fecha";
						string campoHasta = lblFechaHasta_DA.Text;
						if (lblFechaHasta_DA.Text.Trim().Length == 0)
							campoHasta = "";
						throw new CustomizedException(string.Format("Período incorrecto en {0}. La fecha no puede ser superior a la fecha actual.", (campoDesde + " " + campoHasta).Trim()), null, enuExceptionType.ValidationException);
					}
			}
		}

		private void ComprobarRangosDesde(bool comprobarFechaPosteriorHoy, bool comprobarFechaAnteriorHoy)
		{
			bool boolFechaDesdeCorrecta = false;
			DateTime fechaDesde;

			if (TipoCalendario == enumTipoCalendario.SoloFecha) txtFechaDesde = txtFecha;
			boolFechaDesdeCorrecta = (DateTime.TryParse(txtFechaDesde.Text.Trim(), out fechaDesde));

			if (comprobarFechaPosteriorHoy)
			{
				//Valida que la fecha final no sea superior a la fecha del día
				if (boolFechaDesdeCorrecta)
					if (fechaDesde.Date > DateTime.Now.Date)
					{
						string campoDesde = lblFechaDesde_DA.Text;
						if (lblFechaDesde_DA.Text.Trim().Length == 0)
							campoDesde = "Fecha";
						string campoHasta = lblFechaHasta_DA.Text;
						if (lblFechaHasta_DA.Text.Trim().Length == 0)
							campoHasta = "";
						throw new CustomizedException(string.Format("Período incorrecto en {0}. La fecha no puede ser superior a la fecha actual.", (campoDesde + " " + campoHasta).Trim()), null, enuExceptionType.ValidationException);
					}
			}
			if (comprobarFechaAnteriorHoy)
			{
				//Valida que la fecha final no sea anterior a la fecha del día
				if (boolFechaDesdeCorrecta)
					if (fechaDesde.Date <= DateTime.Now.Date)
					{
						string campoDesde = lblFechaDesde_DA.Text;
						if (lblFechaDesde_DA.Text.Trim().Length == 0)
							campoDesde = "Fecha";
						string campoHasta = lblFechaHasta_DA.Text;
						if (lblFechaHasta_DA.Text.Trim().Length == 0)
							campoHasta = "";
						throw new CustomizedException(string.Format("Período incorrecto en {0}. La fecha no puede ser anterior a la fecha actual.", (campoDesde + " " + campoHasta).Trim()), null, enuExceptionType.ValidationException);
					}
			}
		}


		private void ComprobarRangosDesde()
		{
			this.ComprobarRangosDesde(true);
		}

		private void ComprobarDiaHabil()
		{
			bool boolFechaDesdeCorrecta = false;
			DateTime fechaDesde;

			if (TipoCalendario == enumTipoCalendario.SoloFecha) txtFechaDesde = txtFecha;
			boolFechaDesdeCorrecta = (DateTime.TryParse(txtFechaDesde.Text.Trim(), out fechaDesde));

			//Valida que la fecha final no sea superior a la fecha del día
			if (boolFechaDesdeCorrecta)
				if (fechaDesde.Date.DayOfWeek == DayOfWeek.Saturday || fechaDesde.Date.DayOfWeek == DayOfWeek.Sunday)
				{
					throw new CustomizedException("La fecha seleccionada corresponde a un día laborable.", null, enuExceptionType.ValidationException);
				}
		}
		#endregion
	}
}