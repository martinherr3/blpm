<%@ Page Title="Reporte Calificaciones" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="reportCalificacionesAlumnoPeriodo.aspx.cs"
    Inherits="EDUAR_UI.ReportCalificacionesAlumnoPeriodo" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Src="~/UserControls/Calendario.ascx" TagName="Calendario" TagPrefix="cal" %>
<%@ Register Src="~/UserControls/Reporte.ascx" TagName="Reporte" TagPrefix="rep" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Consultar Calificaciones</h2>
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
                    <asp:Label ID="lblAsignatura" runat="server" Text="Asignatura:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top" colspan="3" class="TDCriterios75">
                    <asp:DropDownList ID="ddlAsignatura" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td valign="top" colspan="4" class="TDCriterios100">
                    <cal:Calendario ID="fechas" TipoCalendario="DesdeHasta" runat="server" EtiquetaDesde="Fecha Desde:"
                        EtiquetaHasta="Fecha Hasta:" TipoAlineacion="Izquierda" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divReporte" runat="server">
        <rep:reporte id="rptCalificaciones" runat="server"></rep:reporte>
    </div>
</asp:Content>
