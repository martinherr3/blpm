<%@ Page Title="Reporte de Accesos" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="reportAccesos.aspx.cs" Inherits="EDUAR_UI.reportAccesos" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Src="~/UserControls/Calendario.ascx" TagName="Calendario" TagPrefix="cal" %>
<%@ Register Src="~/UserControls/Reporte.ascx" TagName="Reporte" TagPrefix="rep" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Consultar Accesos</h2>
    <br />
    <div id="divFiltros" runat="server">
        <table class="tablaInterna" cellpadding="0" cellspacing="0">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" ToolTip="Buscar"
                        ImageUrl="~/Images/botonBuscar.png" />
                </td>
            </tr>
        </table>
        <table class="tablaInterna" cellpadding="1" cellspacing="5">
            <tr>
                <td valign="top" class="TDCriterios25">
                    <asp:Label ID="lblPagina" runat="server" Text="Página:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top" class="TDCriterios30">
                    <asp:DropDownList ID="ddlPagina" runat="server">
                    </asp:DropDownList>
                </td>
                <td valign="top" class="TDCriterios25">
                </td>
                <td valign="top" class="TDCriterios25">
                </td>
            </tr>
            <tr>
                <td valign="top" colspan="4" class="TDCriterios100">
                    <cal:Calendario ID="fechas" TipoCalendario="DesdeHasta" runat="server" EtiquetaDesde="Fecha Desde:"
                        EtiquetaHasta="Fecha Hasta:" TipoAlineacion="Izquierda" />
                </td>
            </tr>
            <tr>
                <td valign="top" class="TD25">
                    <asp:Label ID="lblRolesBusqueda" runat="server" Text="Roles:"></asp:Label>
                </td>
                <td class="TD25">
                    <asp:CheckBoxList ID="chkListRolesBusqueda" TabIndex="2" runat="server">
                    </asp:CheckBoxList>
                </td>
                <td valign="top" class="TDCriterios25">
                </td>
                <td valign="top" class="TDCriterios25">
                </td>
            </tr>
        </table>
    </div>
    <div id="divReporte" runat="server">
        <rep:Reporte ID="rptAccesos" runat="server"></rep:Reporte>
    </div>
</asp:Content>
