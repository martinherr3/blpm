<%@ Page Title="Boletín Alumno" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="Boletin.aspx.cs" Inherits="EDUAR_UI.Boletin" %>

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
                        Boletín Académico</h2>
                    <br />
                </td>
                <td align="right" rowspan="2">
                    <asp:ImageButton ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" ToolTip="Buscar"
                        ImageUrl="~/Images/botonBuscar.png" />
                </td>
            </tr>
            <%--<tr>
                <td align="left">
                </td>
            </tr>--%>
        </table>
        <table class="tablaInterna" cellpadding="1" cellspacing="5" border="0">
            <tr>
                <td valign="top" class="TD100px">
                    <asp:Label ID="lblAccion" runat="server" Text="Consultar:" Font-Bold="true" CssClass="lblCriterios"></asp:Label>
                </td>
                <td rowspan="3">
                    <asp:RadioButtonList ID="rdlAccion" runat="server" OnSelectedIndexChanged="rdlAccion_OnSelectedIndexChanged"
                        AutoPostBack="true">
                        <asp:ListItem Text="Calificaciones" Value="0" Selected="true" />
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
                <td class="TD100px">
                    <asp:Label ID="lblAlumnos" runat="server" Text="Alumno:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAlumnosTutor" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAlumnosTutor_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td valign="top" class="TD100px">
                    <asp:Label ID="lblPeriodo" runat="server" Text="Periodo:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top">
                    <asp:UpdatePanel ID="udpPeriodo" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlPeriodo" runat="server">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td valign="top" class="TD100px">
                    <asp:Label ID="lblAsignatura" runat="server" Text="Asignatura:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top">
                    <asp:UpdatePanel ID="udpAsignatura" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <select data-placeholder="[Seleccione]" style="width: 100%" multiple="true" class="chzn-select"
                                runat="server" id="ddlAsignatura" enableviewstate="true">
                            </select>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlAlumnosTutor" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <script type="text/javascript">            $(".chzn-select").chosen();</script>
    </div>
    <div id="divReporte" runat="server">
        <rep:Reporte ID="rptResultado" runat="server"></rep:Reporte>
    </div>
</asp:Content>
