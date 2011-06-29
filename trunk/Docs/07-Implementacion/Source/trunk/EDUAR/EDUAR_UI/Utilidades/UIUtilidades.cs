using System;
using System.Web.UI.WebControls;
using EDUAR_Utility.Enumeraciones;

namespace EDUAR_UI.Utilidades
{
    public class UIUtilidades
    {
        public static void BindComboTipoPersona(DropDownList ddlTipoUsuario)
        {
            foreach (enumTipoPersona tipoPersona in Enum.GetValues(typeof(enumTipoPersona)))
            {
                ddlTipoUsuario.Items.Add(new ListItem(tipoPersona.ToString(), ((int)tipoPersona).ToString()));
            }

        }
    }
}