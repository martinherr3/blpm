using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        
        /// <summary>
        /// Método que realiza el Bind de un ListBox.
        /// </summary>
        public static void BindCombo<T>(ListBox ListBox, IList<T> lista, string fieldId, string fieldDescription, Boolean addDefaultValue)
        {
            DataView dataView = new DataView((DataTable)lista);
            ListBox.Items.Clear();
            ListBox.DataSource = null;

            if (dataView != null)
            {
                foreach (DataRowView drv in dataView)
                    ListBox.Items.Insert(ListBox.Items.Count, new ListItem(drv.Row[fieldDescription].ToString(), drv.Row[fieldId].ToString()));
            }

            if (addDefaultValue)
                ListBox.Items.Insert(0, new ListItem("Seleccione", "-1"));
        }


        /// <summary>
        /// Método que realiza el Bind de un DropDownList.
        /// </summary>
        public static void BindCombo<T>(DropDownList dropDownList, IList<T> lista, string fieldId, string fieldDescription, Boolean addDefaultValue)
        {
            DataView dataView = new DataView(BuildDataTable(lista));
            dropDownList.Items.Clear();
            dropDownList.DataSource = null;

            if (dataView != null)
            {
                foreach (DataRowView drv in dataView)
                    dropDownList.Items.Insert(dropDownList.Items.Count, new ListItem(drv.Row[fieldDescription].ToString(), drv.Row[fieldId].ToString()));
            }

            if (addDefaultValue)
                dropDownList.Items.Insert(0, new ListItem("Seleccione", "-1"));
        }

        // <T> is the type of data in the list.
        // If you have a List<int>, for example, then call this as follows:
        // List<int> ListOfInt;
        // DataTable ListTable = BuildDataTable<int>(ListOfInt);
        public static DataTable BuildDataTable<T>(IList<T> lista)
        {
            //create DataTable Structure
            DataTable tbl = CreateTable<T>();
            Type entType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entType);
            //get the list item and add into the list
            foreach (T item in lista)
            {
                DataRow row = tbl.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item);
                }
                tbl.Rows.Add(row);
            }
            return tbl;
        }

        private static DataTable CreateTable<T>()
        {
            //T –> ClassName
            Type entType = typeof(T);
            //set the datatable name as class name
            DataTable tbl = new DataTable(entType.Name);
            //get the property list
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entType);
            foreach (PropertyDescriptor prop in properties)
            {
                Type propType = prop.PropertyType;
                if (propType.IsGenericType &&
                    propType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    propType = Nullable.GetUnderlyingType(propType);
                }
                else
                {
                    propType = prop.PropertyType;
                }
                //add property as column
                tbl.Columns.Add(prop.Name, propType);
            }
            return tbl;
        }
    }
}