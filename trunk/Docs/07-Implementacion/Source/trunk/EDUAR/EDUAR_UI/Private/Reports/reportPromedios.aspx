<%@ Page Title="Informe consolidado" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="reportPromedios.aspx.cs" Inherits="EDUAR_UI.reportPromedios" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Src="~/UserControls/Reporte.ascx" TagName="Reporte" TagPrefix="rep" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Reporte consolidado por período</h2>
    <br />
    <div id="divAccion" runat="server">
        <table class="tablaInterna" cellpadding="1" cellspacing="5" border="0">
            <tr>
                <td valign="top" class="TDCriterios25">
                    <asp:Label ID="lblAccion" runat="server" Text="Tipo reporte consolidado:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td rowspan="3" class="TDCriterios75">
                    <asp:RadioButtonList ID="rdlAccion" runat="server" OnSelectedIndexChanged="rdlAccion_OnSelectedIndexChanged"
                        AutoPostBack="true">
                        <asp:ListItem Text="Promedios" Value="0" Enabled="true" Selected="True" />
                        <asp:ListItem Text="Inasistencias" Value="1" />
                        <asp:ListItem Text="Sanciones" Value="2" />
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
    </div>
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
                <td valign="top" class="TDCriterios75" colspan="3">
                    <asp:DropDownList ID="ddlAsignatura" runat="server">
                    </asp:DropDownList>
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
                    <asp:DropDownList ID="ddlCurso" runat="server" OnSelectedIndexChanged="ddlCurso_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td valign="top" class="TDCriterios25">
                    <asp:Label ID="lblPeriodo" runat="server" Text="Periodo:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top" class="TDCriterios25">
                    <asp:DropDownList ID="ddlPeriodo" runat="server">
                    </asp:DropDownList>
                </td>
                <td valign="top" class="TDCriterios25">
                    <asp:Label ID="lblAlumno" runat="server" Text="Alumno:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top" class="TDCriterios25">
                    <asp:DropDownList ID="ddlAlumno" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <div id="divFiltrosIncidencia" runat="server">
        <table class="tablaInterna" cellpadding="0" cellspacing="0">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="btnBuscarIncidencia" OnClick="btnBuscar_Click" runat="server"
                        ToolTip="Buscar" ImageUrl="~/Images/botonBuscar.png" />
                </td>
            </tr>
        </table>
        <table class="tablaInterna" cellpadding="1" cellspacing="5">
            <tr>
                <td valign="top" class="TDCriterios25">
                    <asp:Label ID="lblCicloLectivo2" runat="server" Text="Ciclo Lectivo:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top" class="TDCriterios25">
                    <asp:DropDownList ID="ddlCicloLectivo2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCicloLectivo_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td valign="top" class="TDCriterios25">
                    <asp:Label ID="lblCurso2" runat="server" Text="Curso:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top" class="TDCriterios25">
                    <asp:DropDownList ID="ddlCurso2" runat="server" OnSelectedIndexChanged="ddlCurso_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td valign="top" class="TDCriterios25">
                    <asp:Label ID="lblPeriodo2" runat="server" Text="Periodo:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top" class="TDCriterios25">
                    <asp:DropDownList ID="ddlPeriodo2" runat="server">
                    </asp:DropDownList>
                </td>
                <td valign="top" class="TDCriterios25">
                    <asp:Label ID="lblAlumno2" runat="server" Text="Alumno:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top" class="TDCriterios25">
                    <asp:DropDownList ID="ddlAlumno2" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <div id="divPromedioPeriodo" runat="server">
        <rep:Reporte ID="rptPromedios" runat="server"></rep:Reporte>
    </div>
    <div id="divInasistenciasPeriodo" runat="server">
        <rep:Reporte ID="rptInasistencias" runat="server"></rep:Reporte>
    </div>
    <div id="divSancionesPeriodo" runat="server">
        <rep:Reporte ID="rptSanciones" runat="server"></rep:Reporte>
    </div>
</asp:Content>
