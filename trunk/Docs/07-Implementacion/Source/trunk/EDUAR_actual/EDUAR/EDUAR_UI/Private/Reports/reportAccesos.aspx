<%@ Page Title="Reporte de Accesos" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="reportAccesos.aspx.cs" Inherits="EDUAR_UI.reportAccesos"
    Theme="Tema" StylesheetTheme="Tema" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Src="~/UserControls/Calendario.ascx" TagName="Calendario" TagPrefix="cal" %>
<%@ Register Src="~/UserControls/Reporte.ascx" TagName="Reporte" TagPrefix="rep" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() == undefined) {
                alertTest();
            }
        }

        function alertTest() {
            $(document).ready(function () {
                $(".chzn-select").chosen();
            });
        }

        alertTest();    
    </script>
    <div id="divFiltros" runat="server">
        <table class="tablaInterna" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <h2>
                        Consultar Accesos</h2>
                    <br />
                </td>
                <td align="right">
                    <asp:ImageButton ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" ToolTip="Buscar"
                        ImageUrl="~/Images/botonBuscar.png" />
                </td>
            </tr>
        </table>
        <table class="tablaInterna" cellpadding="1" cellspacing="5">
            <tr>
                <td valign="middle" class="TD110px">
                    <asp:Label ID="lblPagina" runat="server" Text="Página:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td>
                    <select data-placeholder="Seleccione" style="width: 100%" multiple="true" class="chzn-select"
                        runat="server" id="ddlDestino" enableviewstate="true">
                    </select>
                </td>
            </tr>
            <tr>
                <td valign="middle" class="TD110px">
                    <asp:Label ID="lblRolesBusqueda" runat="server" Text="Roles:"></asp:Label>
                </td>
                <td valign="middle">
                    <select data-placeholder="Seleccione" style="width: 100%" multiple="true" class="chzn-select"
                        runat="server" id="ddlRoles" enableviewstate="true">
                    </select>
                </td>
            </tr>
        </table>
        <table width="490px" cellpadding="1" cellspacing="5">
            <tr>
                <td valign="top" class="TDCriterios100">
                    <cal:Calendario ID="fechas" TipoCalendario="DesdeHasta" runat="server" EtiquetaDesde="Fecha Desde:"
                        EtiquetaHasta="Fecha Hasta:" TipoAlineacion="Izquierda" />
                </td>
            </tr>
        </table>
        <script type="text/javascript">            $(".chzn-select").chosen();</script>
    </div>
    <div id="divReporte" runat="server">
        <rep:Reporte ID="rptAccesos" runat="server"></rep:Reporte>
    </div>
</asp:Content>
