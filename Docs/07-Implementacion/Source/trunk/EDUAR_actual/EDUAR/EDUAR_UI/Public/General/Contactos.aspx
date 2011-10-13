<%@ Page Title="Contacto" Language="C#" MasterPageFile="~/EDUARMaster.Master" AutoEventWireup="true"
    CodeBehind="Contactos.aspx.cs" Inherits="EDUAR_UI.Contactos" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%" cellpadding="1" cellspacing="5" border="0">
        <tr>
            <td style="width: 80%; vertical-align: text-top">
                <h2>
                    Información General<hr />
                </h2>
            </td>
            <td style="width: 20%; text-align: right" rowspan="2">
                <asp:Image ID="Image1" ImageUrl="~/Images/web/address-book.png" runat="server" AlternateText="Información de Contacto"
                    ToolTip="Información de Contacto" />
            </td>
        </tr>
        <tr>
            <td style="width: 80%">
                <p class="ui-widget">
                    [Se detallan distintas vías de comunicación con la institución.]
                </p>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="1" cellspacing="5" border="0">
        <tr>
            <td style="width: 100%">
                <h3>
                    Direccion</h3>
                <p class="ui-widget">
                    Calle Pública S/N
                </p>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <h3>
                    Teléfonos</h3>
                <p class="ui-widget">
                    0351 - 4123456
                </p>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <h3>
                    Email</h3>
                <p class="ui-widget">
                    <a href="mailto:proyecto.eduar2.0@gmail.com" style="text-decoration: none">
                        <asp:Image ID="Image2" ImageUrl="~/Images/web/mail-new.png" runat="server" AlternateText="Solicitar Información"
                            ToolTip="Solicitar Información" style="vertical-align: middle" />Más Información</a>
                </p>
            </td>
        </tr>
    </table>
</asp:Content>
