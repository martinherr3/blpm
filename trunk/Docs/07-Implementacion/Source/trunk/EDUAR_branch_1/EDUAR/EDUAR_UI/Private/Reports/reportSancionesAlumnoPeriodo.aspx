<%@ Page Title="Reporte Sanciones" Language="C#" MasterPageFile="~/EDUARMaster.Master" 
    AutoEventWireup="true" CodeBehind="reportSancionesAlumnoPeriodo.aspx.cs" 
    Inherits="EDUAR_UI.ReportSancionesAlumnoPeriodo" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Src="~/UserControls/Calendario.ascx" TagName="Calendario" TagPrefix="cal" %>
<%@ Register Src="~/UserControls/Reporte.ascx" TagName="Reporte" TagPrefix="rep" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Consultar Sanciones</h2>
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
                <td valign="top" colspan="4" class="TDCriterios100">
                    <cal:Calendario ID="fechas" TipoCalendario="DesdeHasta" runat="server" EtiquetaDesde="Fecha Desde:"
                        EtiquetaHasta="Fecha Hasta:" TipoAlineacion="Izquierda" />
                </td>
            </tr>
            <tr>
                <td valign="top" class="TDCriterios25">
                    <asp:Label ID="lblCicloLectivo" runat="server" Text="Ciclo Lectivo:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top" class="TDCriterios25">
                    <asp:DropDownList ID="ddlCicloLectivo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCicloLectivo_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td valign="top" class="TDCriterios25">
                    <asp:Label ID="lblCurso" runat="server" Text="Curso:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top" class="TDCriterios25">
                    <asp:DropDownList ID="ddlCurso" runat="server" onselectedindexchanged="ddlCurso_SelectedIndexChanged1" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td valign="top" class="TDCriterios25">
                    <asp:Label ID="lblAlumno" runat="server" Text="Alumno:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top" class="TDCriterios25">
                    <asp:DropDownList ID="ddlAlumno" runat="server"></asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <div id="divReporte" runat="server">
        <rep:Reporte ID="rptSanciones" runat="server"></rep:Reporte>
    </div>
</asp:Content>
