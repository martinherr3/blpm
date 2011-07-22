using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_UI.Shared;
using System.Xml;
using EDUAR_Entities;
using EDUAR_Entities.Security;

namespace EDUAR_UI
{
    public partial class ChangeRolAccess : EDUARBasePage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    SiteMapNode currentNode = SiteMap.RootNode;
                    List<Seccion> listaSecciones = new List<Seccion>();
                    Seccion objSeccion = new Seccion();
                    foreach (SiteMapNode nodo in currentNode.ChildNodes)
                    {
                        listaSecciones.Add(recorrerColeccion(nodo));
                    }
                    gvwSecciones.DataSource = listaSecciones;
                    gvwSecciones.DataBind();
                    udpGrilla.Update();
                }
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }

        /// <summary>
        /// Recorrers the coleccion.
        /// </summary>
        /// <param name="nodo">The nodo.</param>
        /// <returns></returns>
        private Seccion recorrerColeccion(SiteMapNode nodo)
        {
            List<DTRol> listaRoles;
            List<Seccion> listaSecciones = new List<Seccion>();
            Seccion objSeccion;
            DTRol rol;
            objSeccion = new Seccion();
            objSeccion.title = nodo.Title;
            objSeccion.url = nodo.Url;
            listaRoles = new List<DTRol>();
            foreach (string strRol in nodo.Roles)
            {
                rol = new DTRol();
                rol.Nombre = strRol;
                listaRoles.Add(rol);
            }
            objSeccion.listaRoles = listaRoles;
            foreach (SiteMapNode nodoHijo in nodo.ChildNodes)
            {
                listaSecciones.Add(recorrerColeccion(nodoHijo));
            }
            objSeccion.listaSecciones = listaSecciones;
            return objSeccion;
        }

        protected void gvwSecciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Editar")
                {
                    SiteMapNode currentNode = SiteMap.Provider.FindSiteMapNodeFromKey(e.CommandArgument.ToString());
                    //SiteMapNode currentNode = SiteMap.RootNode;
                    List<Seccion> listaSecciones = new List<Seccion>();
                    Seccion objSeccion = new Seccion();
                    foreach (SiteMapNode nodo in currentNode.ChildNodes)
                    {
                        listaSecciones.Add(recorrerColeccion(nodo));
                    }
                    gvwSecciones.DataSource = listaSecciones;
                    gvwSecciones.DataBind();
                    udpGrilla.Update();
                }
            }
            catch (Exception ex)
            {
                Master.ManageExceptions(ex);
            }
        }
    }
}