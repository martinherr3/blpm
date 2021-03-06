﻿<%@ Page Title="Consolidado" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="reportPromedios.aspx.cs" Inherits="EDUAR_UI.reportPromedios"
    EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
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
    <div id="divAccion" runat="server">
        <table class="tablaInterna" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <h2>
                        Consolidado</h2>
                    <br />
                </td>
                <td align="right">
                    <asp:ImageButton ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" ToolTip="Buscar"
                        ImageUrl="~/Images/botonBuscar.png" ValidationGroup="vlsValidador" CausesValidation="true" />
                </td>
            </tr>
        </table>
        <table class="tablaInterna" cellpadding="1" cellspacing="5" border="0">
            <tr>
                <td valign="top" class="TD140px">
                    <asp:Label ID="lblAccion" runat="server" Text="Reporte:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td rowspan="3">
                    <asp:RadioButtonList ID="rdlAccion" runat="server" OnSelectedIndexChanged="rdlAccion_OnSelectedIndexChanged"
                        AutoPostBack="true">
                        <asp:ListItem Text="Promedios" Value="0" Selected="true" />
                        <asp:ListItem Text="Inasistencias" Value="1" />
                        <asp:ListItem Text="Sanciones" Value="2" />
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
    </div>
    <div id="divFiltros" runat="server">
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
                    <asp:DropDownList ID="ddlCicloLectivo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCicloLectivo_SelectedIndexChanged"
                        CssClass="TD140px">
                    </asp:DropDownList>
                    <%--<asp:UpdatePanel ID="udpCicloLectivo" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            
                            <asp:CompareValidator ID="CompareValidator1" ErrorMessage="El campo Ciclo Lectivo es requerido"
                                ControlToValidate="ddlCicloLectivo" Operator="GreaterThan" Type="Integer" ValidationGroup="vlsValidador"
                                ValueToCompare="0" runat="server" Display="Dynamic" Font-Bold="true" ForeColor="Red">*</asp:CompareValidator>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlCicloLectivo" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
                </td>
                <td valign="top" class="TD140px">
                    <asp:Label ID="lblCurso" runat="server" Text="Curso:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top">
                    <asp:UpdatePanel ID="udpCurso" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlCurso" runat="server" OnSelectedIndexChanged="ddlCurso_SelectedIndexChanged"
                                CausesValidation="true" ValidationGroup="vlsValidador" AutoPostBack="True" CssClass="TD140px">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator2" ErrorMessage="El campo Curso es requerido"
                                ControlToValidate="ddlCurso" Operator="GreaterThan" Type="Integer" ValidationGroup="vlsValidador"
                                ValueToCompare="0" runat="server" Display="Dynamic" Font-Bold="true" ForeColor="Red">*</asp:CompareValidator>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlCicloLectivo" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td valign="top" class="TD140px">
                    <asp:Label ID="lblPeriodo" runat="server" Text="Periodo:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top" class="TD140px">
                    <asp:UpdatePanel ID="udpPeriodo" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlPeriodo" runat="server">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlCurso" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlCicloLectivo" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td valign="top" class="TD140px">
                    <asp:Label ID="lblAlumno" runat="server" Text="Alumno:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top" colspan="3">
                    <asp:UpdatePanel ID="udpAlumno" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlAlumno" runat="server" CssClass="EstiloTxtLargo250">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlCurso" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlCicloLectivo" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td valign="top" class="TD140px">
                    <asp:Label ID="lblAsignatura" runat="server" Text="Asignatura:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top" colspan="3">
                    <asp:UpdatePanel ID="udpAsignatura" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <select data-placeholder="[Seleccione]" style="width: 100%" multiple="true" class="chzn-select"
                                runat="server" id="ddlAsignatura" enableviewstate="true">
                            </select>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlCurso" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlCicloLectivo" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td valign="top" class="TD140px">
                    <asp:Label ID="lblTipoAsistencia" runat="server" Text="Tipo de Inasistencia:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top" colspan="3">
                    <select data-placeholder="[Seleccione]" style="width: 100%" multiple="true" class="chzn-select"
                        runat="server" id="ddlAsistencia" enableviewstate="true">
                    </select>
                </td>
            </tr>
            <tr>
                <td valign="top" class="TD140px">
                    <asp:Label ID="lblTipoSanción" runat="server" Text="Tipo de Sanción:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top" colspan="3">
                    <select data-placeholder="[Seleccione]" style="width: 100%" multiple="true" class="chzn-select"
                        runat="server" id="ddlTipoSancion" enableviewstate="true">
                    </select>
                </td>
            </tr>
            <tr>
                <td valign="top" class="TD140px">
                    <asp:Label ID="lblMotivoSanción" runat="server" Text="Motivo de Sanción:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top" colspan="3">
                    <select data-placeholder="[Seleccione]" style="width: 100%" multiple="true" class="chzn-select"
                        runat="server" id="ddlMotivoSancion" enableviewstate="true">
                    </select>
                </td>
            </tr>
        </table>
        <script type="text/javascript">            $(".chzn-select").chosen();</script>
    </div>
    <div id="divReporte" runat="server">
        <rep:Reporte ID="rptResultado" runat="server"></rep:Reporte>
    </div>
</asp:Content>
