﻿using System;
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

        public int HastaIntermedio
        {
            get
            {
                if (ViewState["HastaIntermedio_" + this.UniqueID] != null)
                    return Convert.ToInt32(ViewState["HastaIntermedio_" + this.UniqueID].ToString());
                return Int32.MinValue;
            }
            set { ViewState["HastaIntermedio_" + this.UniqueID] = value; }
        }

        public int HastaSecundario
        {
            get
            {
                if (ViewState["HastaSecundario_" + this.UniqueID] != null)
                    return Convert.ToInt32(ViewState["HastaSecundario_" + this.UniqueID].ToString());
                return Int32.MinValue;
            }
            set { ViewState["HastaSecundario_" + this.UniqueID] = value; }
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

        public int VerdeIntermedio
        {
            get
            {
                if (ViewState["VerdeIntermedio_" + this.UniqueID] != null)
                    return Convert.ToInt32(ViewState["VerdeIntermedio_" + this.UniqueID].ToString());
                return 0;
            }
            set { ViewState["VerdeIntermedio_" + this.UniqueID] = value; }
        }

        public int RojoIntermedio
        {
            get
            {
                if (ViewState["RojoIntermedio_" + this.UniqueID] != null)
                    return Convert.ToInt32(ViewState["RojoIntermedio_" + this.UniqueID].ToString());
                return 0;
            }
            set { ViewState["RojoIntermedio_" + this.UniqueID] = value; }
        }

        public int VerdeSecundario
        {
            get
            {
                if (ViewState["VerdeSecundario_" + this.UniqueID] != null)
                    return Convert.ToInt32(ViewState["VerdeSecundario_" + this.UniqueID].ToString());
                return 0;
            }
            set { ViewState["VerdeSecundario_" + this.UniqueID] = value; }
        }

        public int RojoSecundario
        {
            get
            {
                if (ViewState["RojoSecundario_" + this.UniqueID] != null)
                    return Convert.ToInt32(ViewState["RojoSecundario_" + this.UniqueID].ToString());
                return 0;
            }
            set { ViewState["RojoSecundario_" + this.UniqueID] = value; }
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
            btnIntermedio.Click += eventoPadre;
            btnSecundario.Click += eventoPadre;
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

            if (HastaIntermedio != Int32.MinValue)
            {
                if (HastaIntermedio == 0)
                    lblIntermedio.Text = "Hoy";
                else
                    lblIntermedio.Text = "Prox. " + HastaIntermedio.ToString() + " días";
            }

            if (HastaSecundario != Int32.MinValue)
            {
                if (HastaSecundario == 0)
                    lblSecundario.Text = "Hoy";
                else
                    lblSecundario.Text = "Prox. " + HastaSecundario.ToString() + " días";
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

				//dt = i.getDatosIndicadores("Indicador_" + SufijoSP, DateTime.Today, DateTime.Today.AddDays(HastaIntermedio)).Tables[0];
                if (dt.Rows.Count != 0 && dt.Rows[0][0].ToString() != "")
                    btnIntermedio.Text = dt.Rows[0][0].ToString();
                else
                    btnIntermedio.Text = "0";
                btnIntermedio.CommandArgument = this.ID;

				//dt = i.getDatosIndicadores("Indicador_" + SufijoSP, DateTime.Today, DateTime.Today.AddDays(HastaSecundario)).Tables[0];
                if (dt.Rows.Count != 0 && dt.Rows[0][0].ToString() != "")                
                    btnSecundario.Text = dt.Rows[0][0].ToString();
                else
                    btnSecundario.Text = "0";
                btnSecundario.CommandArgument = this.ID;

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

            if (btnIntermedio.Text != "")
            {
                indi = Convert.ToInt32(btnIntermedio.Text);
                if (InvertirEscala)
                {
                    if (indi <= RojoIntermedio)
                        btnIntermedio.CssClass = "BotonIndRojoSecundario";
                    else if (indi < VerdeIntermedio)
                        btnIntermedio.CssClass = "BotonIndAmarilloSecundario";
                    else
                        btnIntermedio.CssClass = "BotonIndVerdeSecundario";
                }
                else
                {
                    if (indi <= VerdeIntermedio)
                        btnIntermedio.CssClass = "BotonIndVerdeSecundario";
                    else if (indi < RojoIntermedio)
                        btnIntermedio.CssClass = "BotonIndAmarilloSecundario";
                    else
                        btnIntermedio.CssClass = "BotonIndRojoSecundario";
                }
            }

            if (btnSecundario.Text != "")
            {
                indi = Convert.ToInt32(btnSecundario.Text);
                if (InvertirEscala)
                {
                    if (indi <= RojoSecundario)
                        btnSecundario.CssClass = "BotonIndRojoSecundario";
                    else if (indi < VerdeSecundario)
                        btnSecundario.CssClass = "BotonIndAmarilloSecundario";
                    else
                        btnSecundario.CssClass = "BotonIndVerdeSecundario";
                }
                else
                {
                    if (indi <= VerdeSecundario)
                        btnSecundario.CssClass = "BotonIndVerdeSecundario";
                    else if (indi < RojoSecundario)
                        btnSecundario.CssClass = "BotonIndAmarilloSecundario";
                    else
                        btnSecundario.CssClass = "BotonIndRojoSecundario";
                }
            }
        }

        #endregion
    }
}