<%@ Page Title="Reporte Calificaciones" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="ReportCalificacionesAlumnoPeriodo.aspx.cs"
    Inherits="EDUAR_UI.ReportCalificacionesAlumnoPeriodo" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/UserControls/Calendario.ascx" TagName="Calendario" TagPrefix="cal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Consultar Calificaciones</h2>
    <br />
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
            <%--<td valign="top" class="TDCriterios25">
            </td>
            <td valign="top" class="TDCriterios25">
            </td>--%>
        </tr>
        <tr>
            <td valign="top" colspan="4" class="TDCriterios100">
                <cal:Calendario ID="fechas" TipoCalendario="DesdeHasta" runat="server" EtiquetaDesde="Fecha Desde:"
                    EtiquetaHasta="Fecha Hasta:" TipoAlineacion="Izquierda" />
            </td>
        </tr>
    </table>
    <%--<asp:UpdatePanel ID="udpReporte" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
    <rsweb:reportviewer id="rptReporte" runat="server" width="100%" height="100%" style="overflow: visible;"
        waitmessagefont-names="Verdana" waitmessagefont-size="14pt" interactivitypostbackmode="SynchronousOnDrillthrough">
            </rsweb:reportviewer>
    <%--</ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="rptAccesos" EventName="Drillthrough" />
        </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>
