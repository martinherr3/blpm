using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess.Entity;
using Promethee.Utility;

namespace Promethee.UserControls
{
    public partial class Criterio : UserControl
    {
        #region --[Atributos]--
        /// <summary>
        /// The nombre
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
        private IndicadorEntity indicador
        {
            get
            {
                return (IndicadorEntity)ViewState[this.UniqueID + this.nombre];
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
                decimal.TryParse(txtLimiteIndiferencia.Text.Replace('.', ','), out limite);
                return limite;
            }
            set
            {
                txtLimiteIndiferencia.Text = value.ToString("####.00");
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
                decimal.TryParse(txtLimitePreferencia.Text.Replace('.', ','), out limite);
                return limite;
            }
            set
            {
                txtLimitePreferencia.Text = value.ToString("####.00");
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
                decimal.TryParse(txtLimiteSigma.Text.Replace('.', ','), out limite);
                return limite;
            }
            set
            {
                txtLimiteSigma.Text = value.ToString("####.00");
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
                decimal.TryParse(txtPeso.Text.Replace('.', ','), out peso);
                return peso;
            }
            set
            {
                //pesoCriterio = value;
                txtPeso.Text = value.ToString("####.00");
            }
        }

        /// <summary>
        /// Gets the tipo funcion preferencia.
        /// </summary>
        public enumFuncionPreferencia TipoFuncionPreferencia
        {
            get
            {
                foreach (enumFuncionPreferencia tipoFuncion in Enum.GetValues(typeof(enumFuncionPreferencia)))
                {
                    if (tipoFuncion.GetHashCode().ToString() == pseudoCriterio.SelectedItem.Value)
                        return tipoFuncion;
                }
                return enumFuncionPreferencia.VerdaderoCriterio;
            }
            set
            {
                pseudoCriterio.SelectedValue = value.GetHashCode().ToString();
                VisibilidadLimites();
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
                    //btnDesHabilitar.ImageUrl = "~/Images/Grillas/action_enable.png";
                    //btnDesHabilitar.ToolTip = "Habilitado";
                    //btnDesHabilitar.AlternateText = "Habilitado";
                }
                else
                {
                    //btnDesHabilitar.ImageUrl = "~/Images/Grillas/action_delete.png";
                    //btnDesHabilitar.ToolTip = "Deshabilitado";
                    //btnDesHabilitar.AlternateText = "Deshabilitado";
                }
                //btnDesHabilitar.ImageAlign = ImageAlign.AbsMiddle;
            }
        }

        /// <summary>
        /// Gets or sets the nombre criterio.
        /// </summary>
        /// <value>
        /// The nombre criterio.
        /// </value>
        public string nombreCriterio
        {
            get
            {
                return txtCriterio.Text.Trim();
            }
            set { txtCriterio.Text = value.Trim(); nombre = txtCriterio.Text.Trim(); }
        }

        public string error
        {
            get { return lblErrorCriterio.Text.Trim(); }
            set { lblErrorCriterio.Text = value.Trim(); }
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
                indicador = new IndicadorEntity();
                indicador.nombre = this.nombre;

                this.esMaximzante = indicador.maximiza;
                this.pesoCriterio = indicador.pesoDefault;
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
                //LimpiarControles();
                VisibilidadLimites();

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
                //SliderExtender1.Enabled = habilitarCriterio;
                //lnkConfig.Enabled = habilitarCriterio;
                //Panel1.Visible = habilitarCriterio;
                txtCriterio.Visible = habilitarCriterio;
                //lnkConfig.Visible = habilitarCriterio;
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
        /// <summary>
        /// Actualizars the criterio.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void ActualizarCriterio(object sender, EventArgs e)
        {
            onCriterioTextChanged(CriterioScroll, e);
        }

        /// <summary>
        /// Visibilidads the limites.
        /// </summary>
        private void VisibilidadLimites()
        {
            bool verLimiteIndiferencia = false;
            bool verLimitePreferencia = false;
            bool verLimiteSigma = false;
            string sigma = string.Empty, limiteIndiferencia = string.Empty, limitePreferencia = string.Empty;

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
        }
        #endregion

        #region --[Métodos Públicos]--
        /// <summary>
        /// Obteners the valores.
        /// </summary>
        /// <returns></returns>
        public Promethee.Utility.Promethee obtenerValores()
        {
            Promethee.Utility.Promethee datos = new Promethee.Utility.Promethee();
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
        public bool ValidarMétodo()
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
                            else
                                if (limitePreferencia <= limiteIndiferencia)
                                    mensaje = "El límite de Preferencia debe ser mayor al límite de Indiferencia.";
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
                            else
                                if (limitePreferencia <= limiteIndiferencia)
                                    mensaje = "El límite de Preferencia debe ser mayor al límite de Indiferencia.";
                    break;
                case "6":
                    if (limiteSigma <= 0)
                        mensaje = "Debe ingresar un valor para sigma.";
                    break;
                default:
                    mensaje = "Debe seleccionar una Función de Preferencia para el Criterio.";
                    break;
            }
            if (string.IsNullOrEmpty(txtCriterio.Text.Trim()))
                mensaje = "Debe ingresar un nombre para el Criterio.";

            decimal peso = 0;
            decimal.TryParse(txtPeso.Text, out peso);
            if (peso == 0)
                mensaje = "Debe ingresar el peso para el Criterio.";

            switch (rdlSentido.SelectedValue)
            { 
                case "0":
                    break;
                case "1":
                    break;
                default:
                    mensaje = "Debe seleccionar el sentido de optimización.";
                    break;
            }

            if (mensaje != string.Empty)
            {
                mensaje = "Criterio " + nombreCriterio + ".<br />" + mensaje;
                lblErrorCriterio.Text = mensaje;
                udpError.Update();
            }
            return mensaje == string.Empty;
        }

        /// <summary>
        /// Limpiars the controles.
        /// </summary>
        public void LimpiarControles()
        {
            txtLimiteIndiferencia.Text = string.Empty;
            txtLimitePreferencia.Text = string.Empty;
            txtLimiteSigma.Text = string.Empty;
            txtPeso.Text = string.Empty;
            txtCriterio.Text = string.Empty;
            lblErrorCriterio.Text = string.Empty;
            rdlSentido.ClearSelection();
            pseudoCriterio.ClearSelection();
            lblLimiteIndiferencia.Visible = false;
            txtLimiteIndiferencia.Visible = false;
            lblLimitePreferencia.Visible = false;
            txtLimitePreferencia.Visible = false;
            lblLimiteSigma.Visible = false;
            txtLimiteSigma.Visible = false;
        }
        #endregion
    }
}