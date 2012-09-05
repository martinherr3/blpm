using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EDUAR_UI.UserControls
{
    public partial class Indicador : System.Web.UI.UserControl
    {
        #region [Propiedades]

        public int HastaPrincipal
        {
            get
            {
                if (ViewState["HastaPrincipal_" + this.UniqueID] != null)
                    return Convert.ToInt32(ViewState["HastaPrincipal_" + this.UniqueID].ToString());
                return Int32.MinValue;
            }
            set { ViewState["HastaPrincipal_" + this.UniqueID] = value; }
        }

        public int HastaSecundario1
        {
            get
            {
                if (ViewState["HastaSecundario1_" + this.UniqueID] != null)
                    return Convert.ToInt32(ViewState["HastaSecundario1_" + this.UniqueID].ToString());
                return Int32.MinValue;
            }
            set { ViewState["HastaSecundario1_" + this.UniqueID] = value; }
        }

        public int HastaSecundario2
        {
            get
            {
                if (ViewState["HastaSecundario2_" + this.UniqueID] != null)
                    return Convert.ToInt32(ViewState["HastaSecundario2_" + this.UniqueID].ToString());
                return Int32.MinValue;
            }
            set { ViewState["HastaSecundario2_" + this.UniqueID] = value; }
        }

        public int VerdePrincipal
        {
            get
            {
                if (ViewState["VerdePrincipal_" + this.UniqueID] != null)
                    return Convert.ToInt32(ViewState["VerdePrincipal_" + this.UniqueID].ToString());
                return 0;
            }
            set { ViewState["VerdePrincipal_" + this.UniqueID] = value; }
        }

        public int RojoPrincipal
        {
            get
            {
                if (ViewState["RojoPrincipal_" + this.UniqueID] != null)
                    return Convert.ToInt32(ViewState["RojoPrincipal_" + this.UniqueID].ToString());
                return 0;
            }
            set { ViewState["RojoPrincipal_" + this.UniqueID] = value; }
        }

        public int VerdeSecundario1
        {
            get
            {
                if (ViewState["VerdeSecundario1_" + this.UniqueID] != null)
                    return Convert.ToInt32(ViewState["VerdeSecundario1_" + this.UniqueID].ToString());
                return 0;
            }
            set { ViewState["VerdeSecundario1_" + this.UniqueID] = value; }
        }

        public int RojoSecundario1
        {
            get
            {
                if (ViewState["RojoSecundario1_" + this.UniqueID] != null)
                    return Convert.ToInt32(ViewState["RojoSecundario1_" + this.UniqueID].ToString());
                return 0;
            }
            set { ViewState["RojoSecundario1_" + this.UniqueID] = value; }
        }

        public int VerdeSecundario2
        {
            get
            {
                if (ViewState["VerdeSecundario2_" + this.UniqueID] != null)
                    return Convert.ToInt32(ViewState["VerdeSecundario2_" + this.UniqueID].ToString());
                return 0;
            }
            set { ViewState["VerdeSecundario2_" + this.UniqueID] = value; }
        }

        public int RojoSecundario2
        {
            get
            {
                if (ViewState["RojoSecundario2_" + this.UniqueID] != null)
                    return Convert.ToInt32(ViewState["RojoSecundario2_" + this.UniqueID].ToString());
                return 0;
            }
            set { ViewState["RojoSecundario2_" + this.UniqueID] = value; }
        }

        public string SufijoSP
        {
            get
            {
                if (ViewState["SP_" + this.UniqueID] != null)
                    return ViewState["SP_" + this.UniqueID].ToString();
                return "";
            }
            set { ViewState["SP_" + this.UniqueID] = value; }
        }

        public string[] Filtros
        {
            get
            {
                if (ViewState["Filtros_" + this.UniqueID] != null)
                    return (string[])ViewState["Filtros_" + this.UniqueID];
                return new string[0];
            }
            set { ViewState["Filtros_" + this.UniqueID] = value; }
        }

        public string Título
        {
            get
            {
                if (ViewState["Título_" + this.UniqueID] != null)
                    return ViewState["Título_" + this.UniqueID].ToString();
                return "";
            }
            set { ViewState["Título_" + this.UniqueID] = value; }
        }

        /// <summary>
        /// Define para que lado van los simbolos al armar la jerarquia de rojo y verde.
        /// </summary>
        public bool InvertirEscala
        {
            get
            {
                if (ViewState["InvertirRangos_" + this.UniqueID] != null)
                    return (bool)ViewState["InvertirRangos_" + this.UniqueID];
                return false;
            }
            set { ViewState["InvertirRangos_" + this.UniqueID] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetLabels();
                SetIndicadores();
                SetStyleIndicadores();
                //divIndicador.Attributes.Add("onmouseover", "$( '#" + divIndicador.ClientID + "' ).effect('Shake','', 500);");
                UpdatePanel1.Update();
            }
        }

        /// <summary>
        /// Click del boton del indicador principal
        /// </summary>
        /// <param name="eventoPadre"></param>
        public void SetEventoClick(EventHandler eventoPadre)
        {
            btnIndicador.Click += eventoPadre;
            btnSecundario1.Click += eventoPadre;
            btnSecundario2.Click += eventoPadre;
        }

        #region [Carga de datos]

        /// <summary>
        /// Carga los labels de los dias
        /// </summary>
        private void SetLabels()
        {
            lblTitulo.Text = Título;

            if (HastaPrincipal != Int32.MinValue)
            {
                if (HastaPrincipal == 0)
                    lblTiempo.Text = "Hoy";
                else
                    lblTiempo.Text = "Prox. " + HastaPrincipal.ToString() + " días";
            }

            if (HastaSecundario1 != Int32.MinValue)
            {
                if (HastaSecundario1 == 0)
                    lblSecundario1.Text = "Hoy";
                else
                    lblSecundario1.Text = "Prox. " + HastaSecundario1.ToString() + " días";
            }

            if (HastaSecundario2 != Int32.MinValue)
            {
                if (HastaSecundario2 == 0)
                    lblSecundario2.Text = "Hoy";
                else
                    lblSecundario2.Text = "Prox. " + HastaSecundario2.ToString() + " días";
            }
        }

        /// <summary>
        /// Carga los valores de los indicadores
        /// </summary>
        private void SetIndicadores()
        {
            if (!String.IsNullOrEmpty(SufijoSP))
            {
				//Informes i = new Informes();
                //DataTable dt = i.getDatosIndicadores("Indicador_" + SufijoSP, DateTime.Today, DateTime.Today.AddDays(HastaPrincipal)).Tables[0];
				DataTable dt = new DataTable();
                if (dt.Rows.Count != 0 && dt.Rows[0][0].ToString()!="")
                    btnIndicador.Text = dt.Rows[0][0].ToString();
                else
                    btnIndicador.Text = "0";
                btnIndicador.CommandArgument = this.ID;

				//dt = i.getDatosIndicadores("Indicador_" + SufijoSP, DateTime.Today, DateTime.Today.AddDays(HastaSecundario1)).Tables[0];
                if (dt.Rows.Count != 0 && dt.Rows[0][0].ToString() != "")
                    btnSecundario1.Text = dt.Rows[0][0].ToString();
                else
                    btnSecundario1.Text = "0";
                btnSecundario1.CommandArgument = this.ID;

				//dt = i.getDatosIndicadores("Indicador_" + SufijoSP, DateTime.Today, DateTime.Today.AddDays(HastaSecundario2)).Tables[0];
                if (dt.Rows.Count != 0 && dt.Rows[0][0].ToString() != "")                
                    btnSecundario2.Text = dt.Rows[0][0].ToString();
                else
                    btnSecundario2.Text = "0";
                btnSecundario2.CommandArgument = this.ID;

            }
        }

        /// <summary>
        /// Carga los estilos de los indicadores
        /// </summary>
        private void SetStyleIndicadores()
        {
            int indi = 0;
            if (btnIndicador.Text != "")
            {
                indi = Convert.ToInt32(btnIndicador.Text);

                if (InvertirEscala)
                {
                    if (indi <= RojoPrincipal)
                        btnIndicador.CssClass = "BotonIndRojo";
                    else if (indi < VerdePrincipal)
                        btnIndicador.CssClass = "BotonIndAmarillo";
                    else
                        btnIndicador.CssClass = "BotonIndVerde";
                }
                else
                {

                    if (indi <= VerdePrincipal)
                        btnIndicador.CssClass = "BotonIndVerde";
                    else if (indi < RojoPrincipal)
                        btnIndicador.CssClass = "BotonIndAmarillo";
                    else
                        btnIndicador.CssClass = "BotonIndRojo";
                }
            }

            if (btnSecundario1.Text != "")
            {
                indi = Convert.ToInt32(btnSecundario1.Text);
                if (InvertirEscala)
                {
                    if (indi <= RojoSecundario1)
                        btnSecundario1.CssClass = "BotonIndRojoSecundario";
                    else if (indi < VerdeSecundario1)
                        btnSecundario1.CssClass = "BotonIndAmarilloSecundario";
                    else
                        btnSecundario1.CssClass = "BotonIndVerdeSecundario";
                }
                else
                {
                    if (indi <= VerdeSecundario1)
                        btnSecundario1.CssClass = "BotonIndVerdeSecundario";
                    else if (indi < RojoSecundario1)
                        btnSecundario1.CssClass = "BotonIndAmarilloSecundario";
                    else
                        btnSecundario1.CssClass = "BotonIndRojoSecundario";
                }
            }

            if (btnSecundario2.Text != "")
            {
                indi = Convert.ToInt32(btnSecundario2.Text);
                if (InvertirEscala)
                {
                    if (indi <= RojoSecundario2)
                        btnSecundario2.CssClass = "BotonIndRojoSecundario";
                    else if (indi < VerdeSecundario2)
                        btnSecundario2.CssClass = "BotonIndAmarilloSecundario";
                    else
                        btnSecundario2.CssClass = "BotonIndVerdeSecundario";
                }
                else
                {
                    if (indi <= VerdeSecundario2)
                        btnSecundario2.CssClass = "BotonIndVerdeSecundario";
                    else if (indi < RojoSecundario2)
                        btnSecundario2.CssClass = "BotonIndAmarilloSecundario";
                    else
                        btnSecundario2.CssClass = "BotonIndRojoSecundario";
                }
            }
        }

        #endregion
    }
}