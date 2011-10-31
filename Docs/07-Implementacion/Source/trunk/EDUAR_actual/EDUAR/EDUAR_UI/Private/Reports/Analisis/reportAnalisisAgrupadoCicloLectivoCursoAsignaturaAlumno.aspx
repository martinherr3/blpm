<%@ Page Title="Análisis de promedios por Ciclo Lectivo, Curso, Asignatura y Alumno" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="reportAnalisisAgrupadoCicloLectivoCursoAsignaturaAlumno.aspx.cs" Inherits="EDUAR_UI.reportAnalisisAgrupadoCicloLectivoCursoAsignaturaAlumno" %>

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
    <h2>Análisis de promedios por Ciclo Lectivo, Curso, Asignatura y Alumno</h2>
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
                    <asp:Label ID="lblCicloLectivo" runat="server" Text="Ciclo Lectivo:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top" class="TDCriterios25">
                    <asp:UpdatePanel ID="udpCicloLectivo" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <select data-placeholder="Seleccione" style="width: 100%" multiple="true" class="chzn-select"
                                runat="server" id="ddlCicloLectivo" enableviewstate="true">
                            </select>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td valign="top" class="TDCriterios25">
                    <asp:Label ID="lblNivel" runat="server" Text="Nivel:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top" class="TDCriterios25">
                    <asp:UpdatePanel ID="udpNivel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <select data-placeholder="Seleccione" style="width: 100%" multiple="true" class="chzn-select"
                                runat="server" id="ddlNivel" enableviewstate="true">
                            </select>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td valign="top" class="TDCriterios25">
                    <asp:Label ID="lblAsignatura" runat="server" Text="Asignatura:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top" class="TDCriterios75" colspan="3">
                    <asp:UpdatePanel ID="udpAsignatura" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <select data-placeholder="Seleccione" style="width: 100%" multiple="true" class="chzn-select"
                                runat="server" id="ddlAsignatura" enableviewstate="true">
                            </select>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td valign="top" class="TDCriterios25">
                    <asp:Label ID="lblAlumno" runat="server" Text="Alumno:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top" class="TDCriterios75" colspan="3">
                    <asp:UpdatePanel ID="udpAlumno" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <select data-placeholder="Seleccione" style="width: 100%" multiple="true" class="chzn-select"
                                runat="server" id="ddlAlumno" enableviewstate="true">
                            </select>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <script type="text/javascript">            $(".chzn-select").chosen();</script>
    </div>
    <div id="divReporte" runat="server">
        <rep:Reporte ID="rptPromediosAnalizados" runat="server"></rep:Reporte>
    </div>
</asp:Content>
