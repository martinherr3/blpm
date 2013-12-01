<%@ Page Title="Reporte Calificaciones" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="reportCalificacionesAlumnoPeriodo.aspx.cs"
    Inherits="EDUAR_UI.ReportCalificacionesAlumnoPeriodo" EnableEventValidation="false" %>

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
                        Calificaciones</h2>
                    <br />
                </td>
                <td align="right">
                    <asp:ImageButton ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" ToolTip="Buscar"
                        ImageUrl="~/Images/botonBuscar.png" ValidationGroup="vlsValidador" CausesValidation="true" />
                </td>
            </tr>
        </table>
        <table class="tablaInterna" cellpadding="1" cellspacing="5">
            <tr>
                <td colspan="4">
                    <asp:ValidationSummary ID="vlsValidador" runat="server" CssClass="failureNotification"
                        DisplayMode="BulletList" ValidationGroup="vlsValidador" ShowSummary="true" />
                </td>
            </tr>
            <tr>
                <td valign="middle" class="TD140px">
                    <asp:Label ID="lblCicloLectivo" runat="server" Text="Ciclo Lectivo:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="middle" class="TD160px">
                    <asp:DropDownList ID="ddlCicloLectivo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCicloLectivo_SelectedIndexChanged"
                        CssClass="TD140px" CausesValidation="false">
                    </asp:DropDownList>
                    <%-- <asp:UpdatePanel ID="udpCicloLectivo" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlCurso" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
                </td>
                <td valign="middle" class="TD160px">
                    <asp:Label ID="lblCurso" runat="server" Text="Curso:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="middle">
                    <asp:UpdatePanel ID="udpCurso" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlCurso" runat="server" OnSelectedIndexChanged="ddlCurso_SelectedIndexChanged"
                                CausesValidation="true" ValidationGroup="vlsValidador" AutoPostBack="True" CssClass="TD140px">
                            </asp:DropDownList>
                            <asp:CompareValidator ErrorMessage="El campo Curso es requerido" ControlToValidate="ddlCurso"
                                Operator="GreaterThan" Type="Integer" ValidationGroup="vlsValidador" ValueToCompare="0"
                                runat="server" Display="Dynamic" Font-Bold="true" ForeColor="Red">*</asp:CompareValidator>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlCicloLectivo" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td valign="middle" class="TD140px">
                    <asp:Label ID="lblAlumno" runat="server" Text="Alumno:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="middle" colspan="3">
                    <asp:UpdatePanel ID="udpAlumno" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlAlumno" runat="server">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlCicloLectivo" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlCurso" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td valign="middle" class="TD140px">
                    <asp:Label ID="lblAsignatura" runat="server" Text="Asignatura:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="middle" colspan="3" style="width: 400px">
                    <asp:UpdatePanel ID="udpAsignatura" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <select data-placeholder="[Seleccione]" style="width: 700px" multiple="true" class="chzn-select"
                                runat="server" id="ddlAsignatura" enableviewstate="true">
                            </select>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlCicloLectivo" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlCurso" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <table width="600px" cellpadding="1" cellspacing="5">
            <tr>
                <td valign="middle" colspan="4" class="TDCriterios100">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <cal:Calendario ID="fechas" TipoCalendario="DesdeHasta" runat="server" EtiquetaDesde="Fecha Desde:"
                                EtiquetaHasta="Fecha Hasta:" TipoAlineacion="Izquierda" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlCicloLectivo" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <script type="text/javascript">            $(".chzn-select").chosen();</script>
    </div>
    <div id="divReporte" runat="server">
        <rep:Reporte ID="rptCalificaciones" runat="server" EnableViewState="true"></rep:Reporte>
    </div>
</asp:Content>
