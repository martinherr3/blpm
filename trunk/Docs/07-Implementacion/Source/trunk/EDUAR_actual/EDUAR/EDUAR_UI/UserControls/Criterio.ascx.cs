using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_UI.Utilidades;

namespace EDUAR_UI.UserControls
{
	public partial class Criterio : UserControl
	{
		#region --[Propiedades]--

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
				pesoCriterio = value;
				txtCriterio.Text = pesoCriterio.ToString();
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
		/// Gets a value indicating whether [es maximzante].
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
		}
		#endregion

		#region --[Eventos]--
		protected void Page_Load(object sender, EventArgs e)
		{

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
				switch (pseudoCriterio.SelectedValue)
				{
					case "1":
						break;
					case "2":
						verLimiteIndiferencia = true;
						break;
					case "3":
						verLimitePreferencia = true;
						break;
					case "4":
						verLimiteIndiferencia = true;
						verLimitePreferencia = true;
						break;
					case "5":
						verLimiteIndiferencia = true;
						verLimitePreferencia = true;
						break;
					case "6":
						verLimiteSigma = true;
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
				udpLimites.Update();
			}
			catch (Exception ex)
			{
				throw ex; //Master.ManageExceptions(ex);
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
					break;
			}
			return mensaje;
		}
		#endregion
	}
}