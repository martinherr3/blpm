<%@ Page Title="Organización Interna" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="Organizacion.aspx.cs" Inherits="EDUAR_UI.Organizacion" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%" cellpadding="1" cellspacing="5" border="0">
        <tr>
            <td style="width: 80%; vertical-align: text-top">
                <h2>
                    Organización
                    <hr />
                </h2>
            </td>
            <td style="width: 20%; text-align: right" rowspan="2">
                <asp:Image ID="Image1" ImageUrl="~/Images/web/organigrama.png" runat="server" ToolTip="Organización"
                    AlternateText="Organización" />
            </td>
        </tr>
        <tr>
            <td style="width: 80%">
                <p class="ui-widget">
                    [En esta sección se encuentra toda la información a lo que respecta a la distribución
                    del personal que coordina y gestiona la Institución Educativa.]
                </p>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="1" cellspacing="5" border="0">
        <tr>
            <td style="width: 100%">
                <h3>
                    Director</h3>
                <p class="ui-widget">
                    [Nombre y apellido]</p>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <h3>
                    Vicedirector</h3>
                <p class="ui-widget">
                    <p class="ui-widget">
                        [Nombre y apellido]</p>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <h3>
                    Secretaria</h3>
                <p class="ui-widget">
                    [Nombre y apellido]</p>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <h3>
                    Bibliotecario</h3>
                <p class="ui-widget">
                    [Nombre y apellido]</p>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <h3>
                    Psicopedagogo</h3>
                <p class="ui-widget">
                    [Nombre y apellido]</p>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <h3>
                    Preceptores</h3>
                <p class="ui-widget">
                    [Nombre y apellido]</p>
            </td>
        </tr>
    </table>
</asp:Content>
