<%@ Page Title="Página principal" Language="C#" MasterPageFile="~/EDUARMaster.master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EDUAR_UI.Default" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table width="100%" cellpadding="1" cellspacing="5" border="0">
        <tr>
            <td style="width: 80%; vertical-align: text-top">
                <h2>
                    EDU@R 2.0<hr />
                </h2>
            </td>
            <td style="width: 20%; text-align: right" rowspan="2">
                <asp:Image ID="Image1" ImageUrl="~/Images/web/default.png" runat="server" AlternateText="Página de Inicio"
                    ToolTip="Página de Inicio" />
            </td>
        </tr>
        <tr>
            <td style="width: 80%">
                <p class="ui-widget">
                    Sistema de Comunicación y Explotación de Información Educativa.
                </p>
            </td>
        </tr>
    </table>
</asp:Content>
