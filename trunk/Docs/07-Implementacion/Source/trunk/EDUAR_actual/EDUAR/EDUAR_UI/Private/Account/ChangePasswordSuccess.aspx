<%@ Page Title="Contraseña Modificada" Language="C#" MasterPageFile="~/EDUARMaster.master"
    AutoEventWireup="true" CodeBehind="ChangePasswordSuccess.aspx.cs" Inherits="EDUAR_UI.ChangePasswordSuccess" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table width="100%" cellpadding="1" cellspacing="5" border="0">
        <tr>
            <td style="width: 80%; vertical-align: text-top">
                <h2>
                    Modificar contraseña<hr />
                </h2>
            </td>
            <td style="width: 20%; text-align: right" rowspan="2">
                <asp:Image ID="Image1" ImageUrl="~/Images/user-properties.png" runat="server" AlternateText="Modificar contraseña"
                    ToolTip="Modificar contraseña" />
            </td>
        </tr>
    </table>
    <p class="ui-widget">
        La contraseña se ha cambiado correctamente.
    </p>
</asp:Content>
