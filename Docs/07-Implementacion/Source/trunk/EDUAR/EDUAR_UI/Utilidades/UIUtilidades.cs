using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web.UI.WebControls;
using EDUAR_Utility.Enumeraciones;
using System.Web.UI;

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

    // Create a template class to represent a dynamic template column.
    public class GridViewTemplate : ITemplate
    {
        private DataControlRowType templateType;
        private string columnName;

        public GridViewTemplate(DataControlRowType type, string colname)
        {
            templateType = type;
            columnName = colname;
        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            // Create the content for the different row types.
            switch (templateType)
            {
                case DataControlRowType.Header:
                    // Create the controls to put in the header
                    // section and set their properties.
                    Literal lc = new Literal();
                    lc.Text = "<b>" + columnName + "</b>";

                    // Add the controls to the Controls collection
                    // of the container.
                    container.Controls.Add(lc);
                    break;
                case DataControlRowType.DataRow:
                    // Create the controls to put in a data row
                    // section and set their properties.
                    Label lblFila = new Label();

                    Literal spacer = new Literal();
                    spacer.Text = " ";

                    // To support data binding, register the event-handling methods
                    // to perform the data binding. Each control needs its own event
                    // handler.
                    //firstName.DataBinding += new EventHandler(this.FirstName_DataBinding);
                    lblFila.DataBinding += new EventHandler(this.row_DataBinding);

                    // Add the controls to the Controls collection
                    // of the container.
                    container.Controls.Add(lblFila);
                    //container.Controls.Add(spacer);
                    //container.Controls.Add(lastName);
                    break;

                // Insert cases to create the content for the other 
                // row types, if desired.

                default:
                    // Insert code to handle unexpected values.
                    break;
            }
        }

        private void row_DataBinding(Object sender, EventArgs e)
        {
            // Get the Label control to bind the value. The Label control
            // is contained in the object that raised the DataBinding 
            // event (the sender parameter).
            Label l = (Label)sender;

            // Get the GridViewRow object that contains the Label control. 
            GridViewRow row = (GridViewRow)l.NamingContainer;

            // Get the field value from the GridViewRow object and 
            // assign it to the Text property of the Label control.
            l.Text = DataBinder.Eval(row.DataItem, columnName).ToString();
        }

    }
}