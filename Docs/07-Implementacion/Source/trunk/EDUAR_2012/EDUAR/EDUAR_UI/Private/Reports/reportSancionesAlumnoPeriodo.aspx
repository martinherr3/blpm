﻿<%@ Page Title="Reporte Sanciones" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="reportSancionesAlumnoPeriodo.aspx.cs" Inherits="EDUAR_UI.ReportSancionesAlumnoPeriodo"
    EnableEventValidation="false" %>

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
                        Consultar Sanciones</h2>
                    <br />
                </td>
                <td align="right">
                    <asp:ImageButton ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" ToolTip="Buscar"
                        ImageUrl="~/Images/botonBuscar.png" CausesValidation="true" ValidationGroup="vlsValidador" />
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
                <td valign="top" class="TD140px">
                    <asp:Label ID="lblCicloLectivo" runat="server" Text="Ciclo Lectivo:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top" class="TD160px">
                    <asp:UpdatePanel ID="udpCicloLectivo" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlCicloLectivo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCicloLectivo_SelectedIndexChanged"
                                CausesValidation="true" ValidationGroup="vlsValidador" CssClass="TD140px">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator1" ErrorMessage="El campo Ciclo Lectivo es requerido"
                                ControlToValidate="ddlCicloLectivo" Operator="GreaterThan" Type="Integer" ValidationGroup="vlsValidador"
                                ValueToCompare="0" runat="server" Display="Dynamic" Font-Bold="true" ForeColor="Red">*</asp:CompareValidator>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlCicloLectivo" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td valign="top" class="TD160px">
                    <asp:Label ID="lblCurso" runat="server" Text="Curso:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top">
                    <asp:UpdatePanel ID="udpCurso" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlCurso" runat="server" OnSelectedIndexChanged="ddlCurso_SelectedIndexChanged"
                                AutoPostBack="True" CssClass="TD140px">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlCicloLectivo" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td valign="top" class="TD140px">
                    <asp:Label ID="lblAlumno" runat="server" Text="Alumno:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top" colspan="3">
                    <asp:UpdatePanel ID="udpAlumno" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlAlumno" runat="server">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlCurso" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td valign="middle" class="TD140px">
                    <asp:Label ID="lblTipoSanción" runat="server" Text="Tipo de Sanción:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top" colspan="3">
                    <select data-placeholder="[Seleccione]" style="width: 500px" multiple="true" class="chzn-select"
                        runat="server" id="ddlTipoSancion" enableviewstate="true">
                    </select>
                </td>
            </tr>
            <tr>
                <td valign="middle" class="TD140px">
                    <asp:Label ID="lblMotivoSanción" runat="server" Text="Motivo de Sanción:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top" colspan="3">
                    <select data-placeholder="[Seleccione]" style="width: 500px" multiple="true" class="chzn-select"
                        runat="server" id="ddlMotivoSancion" enableviewstate="true">
                    </select>
                </td>
            </tr>
        </table>
        <table width="600px" cellpadding="1" cellspacing="5">
            <tr>
                <td valign="top" colspan="4" class="TDCriterios100">
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
        <rep:Reporte ID="rptSanciones" runat="server"></rep:Reporte>
    </div>
</asp:Content>
