<%@ Page Title="Localización" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="Localizacion.aspx.cs" Inherits="EDUAR_UI.Localizacion" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%" cellpadding="1" cellspacing="5" border="0">
        <tr>
            <td style="width: 80%; vertical-align: text-top">
                <h2>
                    Ubicación
                    <hr />
                </h2>
            </td>
            <td style="width: 20%; text-align: right" rowspan="2">
                <asp:Image ID="Image1" ImageUrl="~/Images/web/map.png" runat="server" ToolTip="Ubicación"
                    AlternateText="Ubicación" />
            </td>
        </tr>
        <tr>
            <td style="width: 80%">
                <h3>
                    Mapa</h3>
                <p class="ui-widget">
                    [En esta sección se puede encontrar un mapa indicando la ubicación del establecimiento.]
                </p>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="1" cellspacing="5" border="0">
        <tr>
            <td style="width: 100%">
            </td>
        </tr>
    </table>
</asp:Content>
