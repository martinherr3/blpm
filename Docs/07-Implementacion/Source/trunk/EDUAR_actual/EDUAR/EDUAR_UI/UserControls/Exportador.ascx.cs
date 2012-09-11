using System;
using System.Data;
using System.Web;
using System.Web.UI;
using EDUAR_UI.Utilidades;

namespace EDUAR_UI.UserControls
{
    public partial class Exportador : UserControl
    {
        #region --[Atributos]--
        private string _Titulo;
        private DataTable _Datos;
        #endregion

        #region --[Propiedades]--
        public DataTable Datos
        {
            get
            {
                if (_Datos == null)
                    if (ViewState["_Datos"] != null)
                        _Datos = (DataTable)ViewState["_Datos"];
                return _Datos;
            }
            set { ViewState["_Datos"] = value; _Datos = value; }
        }

        public string Titulo
        {
            get
            {
                if (String.IsNullOrEmpty(_Titulo))
                    if (ViewState["_Titulo"] != null)
                        _Titulo = ViewState["_Titulo"].ToString();
                return _Titulo;
            }
            set
            {
                ViewState["_Titulo"] = value;
                _Titulo = value;
            }
        }
        #endregion

        #region --[Eventos]--
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            if (Datos != null && !String.IsNullOrEmpty(Titulo))
            {
                ExportPDF.ExportarPDF(Titulo, Datos, HttpContext.Current.User.Identity.Name, string.Empty);
            }
        }
        #endregion

        #region --[Métodos Públicos]--
        public void Clean()
        {
            Titulo = "";
            Datos = null;
        }
        #endregion
    }
}