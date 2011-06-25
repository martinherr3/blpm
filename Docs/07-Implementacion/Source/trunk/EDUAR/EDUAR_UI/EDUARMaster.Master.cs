using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDUAR_UI.Shared;
using EDUAR_Entities.Shared;
using EDUAR_Entities.Security;

namespace EDUAR_UI
{
    public partial class EDUARMaster : MasterPage
    {
        #region --[Propiedades]--
        /// <summary>
        /// Mantiene los datos del usuario logueado.
        /// </summary>
        public DTSessionDataUI ObjDTSessionDataUI
        {
            get
            {
                if (Session["SessionObjDTODataPage"] == null)
                    Session["SessionObjDTODataPage"] = new DTSessionDataUI();

                return (DTSessionDataUI)Session["SessionObjDTODataPage"];
            }
            set { Session["SessionObjDTODataPage"] = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            CargarMenu();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void siteMapPathEDUAR_PreRender(object sender, EventArgs e)
        {
            SiteMapNodeItem sepItem = new SiteMapNodeItem(-1, SiteMapNodeItemType.PathSeparator);
            ITemplate sepTemplate = siteMapPathEDUAR.PathSeparatorTemplate;
            if (sepTemplate == null)
            {
                Literal separator = new Literal { Text = siteMapPathEDUAR.PathSeparator };
                sepItem.Controls.Add(separator);
            }
            else
                sepTemplate.InstantiateIn(sepItem);

            sepItem.ApplyStyle(siteMapPathEDUAR.PathSeparatorStyle);
        }

        /// <summary>
        /// 
        /// </summary>
        private void CargarMenu()
        {
            foreach (SiteMapNode node in SiteMapEDUAR.Provider.RootNode.ChildNodes)
            {
                if (!ValidarNodo(node))
                    continue;
                trvMenu.Visible = true;
                TreeNode objTreeNode = new TreeNode(node.Title);
                if (node.Url != String.Empty)
                    objTreeNode.NavigateUrl = node.Url;
                
                objTreeNode.SelectAction = TreeNodeSelectAction.Expand;
                //Recorre los nodos hijos
                foreach (SiteMapNode nodeChild in node.ChildNodes)
                {
                    if (!ValidarNodo(nodeChild))
                        continue;

                    TreeNode objTreeNodeChild = new TreeNode(nodeChild.Title) { NavigateUrl = nodeChild.Url };
                    objTreeNode.ChildNodes.Add(objTreeNodeChild);
                }
                trvMenu.Nodes.Add(objTreeNode);
            }
            trvMenu.ExpandAll();
        }

        /// <summary>
        /// Valida si el nodo se debe mostrar. 
        /// Puede tener el atributo visible=false o puede que el perfil del usuario lo permita.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected Boolean ValidarNodo(SiteMapNode node)
        {
            //Si el nodo está marcado como visible False es porque solo se utiliza para que sea visible 
            //en el menu superior y no se debe mostrar en el menu lateral
            Boolean isVisible;
            if (bool.TryParse(node["visible"], out isVisible) && !isVisible)
                return false;

            foreach (DTRol rolUsuario in ObjDTSessionDataUI.ObjDTUsuario.ListaRoles)
            {
                if (node.Roles.Contains(rolUsuario.Nombre))
                    return true;
            }
            return false;
        }
    }
}
