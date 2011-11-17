<%@ Page Title="Docentes" Language="C#" MasterPageFile="~/EDUARMaster.Master" AutoEventWireup="true"
    CodeBehind="Docentes.aspx.cs" Inherits="EDUAR_UI.Docentes" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%" cellpadding="1" cellspacing="5" border="0">
        <tr>
            <td style="width: 80%; vertical-align: text-top">
                <h2>
                    Nuestros Docentes
                    <hr />
                </h2>
            </td>
            <td style="width: 20%; text-align: right" rowspan="2">
                <asp:Image ID="Image1" ImageUrl="~/Images/web/docentes.png" runat="server" ToolTip="Nuestros Docentes"
                    AlternateText="Nuestros Docentes" />
            </td>
        </tr>
        <tr>
            <td style="width: 80%">
                <p class="ui-widget">
                    [En esta sección se encuentra una descripción general de los docentes para detallarlos
                    en la siguiente sección.]
                </p>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="1" cellspacing="5" border="0">
        <tr>
            <td style="width: 100%">
                <h3>
                    Matemáticas</h3>
                <p class="ui-widget">
                    Docente
                </p>
                <h3>
                    Lengua y Literatura</h3>
                <p class="ui-widget">
                    Docente
                </p>
                <h3>
                    Ciencias Sociales</h3>
                <p class="ui-widget">
                    Docente
                </p>
                <h3>
                    Ciencias Naturales</h3>
                <p class="ui-widget">
                    Docente
                </p>
                <h3>
                    Especialización</h3>
                <p class="ui-widget">
                    Docente
                </p>
            </td>
        </tr>
    </table>
</asp:Content>
