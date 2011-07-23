<%@ Page Title="Reporte de Accesos" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="reportAccesos.aspx.cs" Inherits="EDUAR_UI.reportAccesos" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Consultar Accesos</h2>
    <br />
    <table class="tablaInterna" cellpadding="0" cellspacing="0">
        <tr>
            <td align="right">
                <asp:ImageButton ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" ToolTip="Buscar"
                    ImageUrl="~/Images/botonBuscar.png" />
            </td>
        </tr>
    </table>
    <table class="tablaInterna" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 100%; vertical-align: text-top">
                <table class="tablaInterna" cellpadding="1" cellspacing="5">
                    <tr>
                        <td valign="top">
                            <asp:Label ID="lblPagina" runat="server" Text="Página:"></asp:Label>
                        </td>
                        <td valign="top">
                            <asp:DropDownList ID="ddlPagina" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <rsweb:ReportViewer ID="rptAccesos" runat="server" Font-Names="Verdana" Font-Size="8pt"
        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%">
    </rsweb:ReportViewer>
</asp:Content>
