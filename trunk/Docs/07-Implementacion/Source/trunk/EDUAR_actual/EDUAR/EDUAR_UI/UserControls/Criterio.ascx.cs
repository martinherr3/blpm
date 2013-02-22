using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_UI.Utilidades;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;

namespace EDUAR_UI.UserControls
{
	public partial class Criterio : UserControl
	{
		#region --[Atributos]--
		/// <summary>
		/// 
		/// </summary>
		private string nombre;
		#endregion

		#region --[Propiedades]--
		/// <summary>
		/// Gets or sets the indicador.
		/// </summary>
		/// <value>
		/// The indicador.
		/// </value>
		private EDUAR_Entities.DEC.Indicador indicador
		{
			get
			{
				return (EDUAR_Entities.DEC.Indicador)ViewState[this.UniqueID + this.nombre];
			}
			set
			{
				ViewState[this.UniqueID + this.nombre] = value;
			}
		}

		/// <summary>
		/// Gets or sets the limite indiferencia.
		/// </summary>
		/// <value>
		/// The limite indiferencia.
		/// </value>
		public decimal limiteIndiferencia
		{
			get
			{
				decimal limite = 0;
				decimal.TryParse(txtLimiteIndiferencia.Text, out limite);
				return limite;
			}
			set
			{
				limiteIndiferencia = value;
			}
		}

		/// <summary>
		/// Gets or sets the limite preferencia.
		/// </summary>
		/// <value>
		/// The limite preferencia.
		/// </value>
		public decimal limitePreferencia
		{
			get
			{
				decimal limite = 0;
				decimal.TryParse(txtLimitePreferencia.Text, out limite);
				return limite;
			}
			set
			{
				limitePreferencia = value;
			}
		}

		/// <summary>
		/// Gets or sets the limite sigma.
		/// </summary>
		/// <value>
		/// The limite sigma.
		/// </value>
		public decimal limiteSigma
		{
			get
			{
				decimal limite = 0;
				decimal.TryParse(txtLimiteSigma.Text, out limite);
				return limite;
			}
			set
			{
				limiteSigma = value;
			}
		}

		/// <summary>
		/// Gets or sets the peso criterio (valor del txtCriterio).
		/// </summary>
		/// <value>
		/// The peso criterio.
		/// </value>
		public decimal pesoCriterio
		{
			get
			{
				decimal peso = 0;
				decimal.TryParse(txtCriterio.Text.Replace('.', ','), out peso);
				return peso;
			}
			set
			{
				//pesoCriterio = value;
				txtCriterio.Text = value.ToString();
			}
		}

		/// <summary>
		/// Gets the tipo funcion preferencia.
		/// </summary>
		public Promethee.enumFuncionPreferencia TipoFuncionPreferencia
		{
			get
			{
				foreach (Promethee.enumFuncionPreferencia tipoFuncion in Enum.GetValues(typeof(Promethee.enumFuncionPreferencia)))
				{
					if (tipoFuncion.GetHashCode().ToString() == pseudoCriterio.SelectedItem.Value)
						return tipoFuncion;
				}
				return Promethee.enumFuncionPreferencia.VerdaderoCriterio;
			}
		}


		/// <summary>
		/// Gets or sets a value indicating whether [es maximzante].
		/// </summary>
		/// <value>
		///   <c>true</c> if [es maximzante]; otherwise, <c>false</c>.
		/// </value>
		public bool esMaximzante
		{
			get
			{
				//Si es 0 (cero) está minimizando el criterio
				if (rdlSentido.SelectedValue == "0")
					return false;
				return true;
			}
			set
			{
				rdlSentido.SelectedValue = value.GetHashCode().ToString();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether [habilitar criterio].
		/// </summary>
		/// <value>
		///   <c>true</c> if [habilitar criterio]; otherwise, <c>false</c>.
		/// </value>
		public bool habilitarCriterio
		{
			get
			{
				return txtCriterio.Enabled;
			}
			set
			{
				txtCriterio.Enabled = value;
				if (value)
				{
					btnDesHabilitar.ImageUrl = "~/Images/Grillas/action_enable.png";
					btnDesHabilitar.ToolTip = "Habilitado";
					btnDesHabilitar.AlternateText = "Habilitado";
				}
				else
				{
					btnDesHabilitar.ImageUrl = "~/Images/Grillas/action_delete.png";
					btnDesHabilitar.ToolTip = "Deshabilitado";
					btnDesHabilitar.AlternateText = "Deshabilitado";
				}
				btnDesHabilitar.ImageAlign = ImageAlign.AbsMiddle;
			}
		}

		public string nombreCriterio
		{
			get { return this.nombre; }
			set { nombre = value; }
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
			txtCriterio.TextChanged += (ActualizarCriterio);
			if (!Page.IsPostBack)
			{
				indicador = new EDUAR_Entities.DEC.Indicador();
				indicador.nombre = this.nombre;

				BLIndicador objBLIndicador = new BLIndicador();
				indicador = objBLIndicador.GetIndicador(indicador);

				this.esMaximzante = indicador.maximiza;
				this.pesoCriterio = indicador.pesoDefault;

				SliderExtender1.Minimum = (double)indicador.pesoMinimo;
				SliderExtender1.Maximum = (double)indicador.pesoMaximo;
				//valCriterioMax.ValueToCompare = indicador.pesoMaximo.ToString();
				//valCriterioMax.ErrorMessage = "El valor MÁXIMO admitido es " + indicador.pesoMaximo.ToString();

				//valCriterioMin.ValueToCompare = indicador.pesoMinimo.ToString();
				//valCriterioMin.ErrorMessage = "El valor MÍNIMO admitido es " + indicador.pesoMinimo.ToString();
			}
		}

		/// <summary>
		/// Handles the SelectedIndexChanged event of the pseudoCriterio control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void pseudoCriterio_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				LimpiarControles();
				bool verLimiteIndiferencia = false;
				bool verLimitePreferencia = false;
				bool verLimiteSigma = false;
				string sigma = string.Empty, limiteIndiferencia = string.Empty, limitePreferencia = string.Empty;

				List<ConfigFuncionPreferencia> config = indicador.listaConfig.FindAll(p => p.idFuncionPreferencia == Convert.ToInt16(pseudoCriterio.SelectedValue));

				switch (pseudoCriterio.SelectedValue)
				{
					case "1":
						break;
					case "2":
						verLimiteIndiferencia = true;
						txtLimiteIndiferencia.Text = config.Find(p => p.idValorFuncionPreferencia == 1).valorDefault.ToString();
						break;
					case "3":
						verLimitePreferencia = true;
						txtLimitePreferencia.Text = config.Find(p => p.idValorFuncionPreferencia == 3).valorDefault.ToString();
						break;
					case "4":
						verLimiteIndiferencia = true;
						verLimitePreferencia = true;
						txtLimiteIndiferencia.Text = config.Find(p => p.idValorFuncionPreferencia == 1).valorDefault.ToString();
						txtLimitePreferencia.Text = config.Find(p => p.idValorFuncionPreferencia == 3).valorDefault.ToString();
						break;
					case "5":
						verLimiteIndiferencia = true;
						verLimitePreferencia = true;
						txtLimiteIndiferencia.Text = config.Find(p => p.idValorFuncionPreferencia == 1).valorDefault.ToString();
						txtLimitePreferencia.Text = config.Find(p => p.idValorFuncionPreferencia == 3).valorDefault.ToString();
						break;
					case "6":
						verLimiteSigma = true;
						txtLimiteSigma.Text = config.Find(p => p.idValorFuncionPreferencia == 2).valorDefault.ToString();
						break;
					default:
						break;
				}
				lblLimiteIndiferencia.Visible = verLimiteIndiferencia;
				txtLimiteIndiferencia.Visible = verLimiteIndiferencia;
				lblLimitePreferencia.Visible = verLimitePreferencia;
				txtLimitePreferencia.Visible = verLimitePreferencia;
				lblLimiteSigma.Visible = verLimiteSigma;
				txtLimiteSigma.Visible = verLimiteSigma;
				ActualizarCriterio(sender, e);

				udpLimites.Update();
			}
			catch (Exception ex)
			{
				throw ex; //Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the Click event of the lnkConfig control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		protected void lnkConfig_Click(object sender, EventArgs e)
		{
			try
			{
				ActualizarCriterio(sender, e);
			}
			catch (Exception ex)
			{
				throw ex; //Master.ManageExceptions(ex);
			}
		}

		/// <summary>
		/// Handles the Click event of the btnDesHabilitar control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
		protected void btnDesHabilitar_Click(object sender, ImageClickEventArgs e)
		{
			try
			{
				habilitarCriterio = !habilitarCriterio;
				SliderExtender1.Enabled = habilitarCriterio;
				//lnkConfig.Enabled = habilitarCriterio;
				Panel1.Visible = habilitarCriterio;
				txtCriterio.Visible = habilitarCriterio;
				lnkConfig.Visible = habilitarCriterio;
				ActualizarCriterio(sender, e);
			}
			catch (Exception ex)
			{
				throw ex; //Master.ManageExceptions(ex);
			}
		}
		#endregion

		#region --[Delegados]--

		public delegate void ValorCriterioScrollHandler(object sender, EventArgs e);

		public event ValorCriterioScrollHandler CriterioScroll;

		/// <summary>
		/// Called when [exportar PDF click].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		public virtual void onCriterioTextChanged(ValorCriterioScrollHandler sender, EventArgs e)
		{
			if (sender != null)
			{
				//Invoca el delegados
				sender(this, e);
			}
		}
		#endregion

		#region --[Métodos Privados]--
		private void LimpiarControles()
		{
			txtLimiteIndiferencia.Text = string.Empty;
			txtLimitePreferencia.Text = string.Empty;
			txtLimiteSigma.Text = string.Empty;
		}

		/// <summary>
		/// Actualizars the criterio.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		void ActualizarCriterio(object sender, EventArgs e)
		{
			onCriterioTextChanged(CriterioScroll, e);
		}
		#endregion

		#region --[Métodos Públicos]--
		/// <summary>
		/// Obteners the valores.
		/// </summary>
		/// <returns></returns>
		public Promethee obtenerValores()
		{
			Promethee datos = new Promethee();
			datos.limiteIndiferencia = limiteIndiferencia;
			datos.limitePreferencia = limitePreferencia;
			datos.limiteSigma = limiteSigma;
			datos.tipoFuncion = TipoFuncionPreferencia;
			datos.pesoCriterio = pesoCriterio;
			return datos;
		}

		/// <summary>
		/// Validars the método.
		/// </summary>
		/// <returns></returns>
		public string ValidarMétodo()
		{
			string mensaje = string.Empty;
			switch (pseudoCriterio.SelectedValue)
			{
				case "1":
					break;
				case "2":
					if (limiteIndiferencia <= 0)
						mensaje = "Debe ingresar un valor para el límite de Indiferencia.";
					break;
				case "3":
					if (limitePreferencia <= 0)
						mensaje = "Debe ingresar un valor para el límite de Preferencia.";
					break;
				case "4":
					if (limitePreferencia <= 0 && limiteIndiferencia <= 0)
						mensaje = "Debe ingresar un valor para el límte de Indiferencia y el límite de Preferencia.";
					else
						if (limiteIndiferencia <= 0)
							mensaje = "Debe ingresar un valor para el límite de Indiferencia.";
						else
							if (limitePreferencia <= 0)
								mensaje = "Debe ingresar un valor para el límite de Preferencia.";
					break;
				case "5":
					if (limitePreferencia <= 0 && limiteIndiferencia <= 0)
						mensaje = "Debe ingresar un valor para el límte de Indiferencia y el límite de Preferencia.";
					else
						if (limiteIndiferencia <= 0)
							mensaje = "Debe ingresar un valor para el límite de Indiferencia.";
						else
							if (limitePreferencia <= 0)
								mensaje = "Debe ingresar un valor para el límite de Preferencia.";
					break;
				case "6":
					if (limiteSigma <= 0)
						mensaje = "Debe ingresar un valor para sigma.";
					break;
				default:
					mensaje = "Debe seleccionar una Función de Preferencia para el Criterio.";
					break;
			}
			if (mensaje == string.Empty)
				if (string.IsNullOrEmpty(txtCriterio.Text))
					mensaje = "Debe ingresar un peso para el Criterio.";
			if (mensaje != string.Empty)
				mensaje = "Criterio " + nombreCriterio + ".<br />" + mensaje;

			return mensaje;
		}
		#endregion

	}
}