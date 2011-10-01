<%@ Page Title="Reporte de Calificaciones por Curso y Asignatura" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="reportPromedioCalificacionesAsignaturaCurso.aspx.cs"
    Inherits="EDUAR_UI.reportPromedioCalificacionesAsignaturaCurso" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Src="~/UserControls/Reporte.ascx" TagName="Reporte" TagPrefix="rep" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Consultar Promedio de Calificaciones por Asignatura y Curso</h2>
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
                    <asp:Label ID="lblTipoReporte" runat="server" Text="Tipo de Reporte:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top" class="TDCriterios75" colspan="3">
                    <asp:DropDownList ID="ddlTipoReporte" runat="server">
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
                    <%--                    <asp:Label ID="lblAlumno" runat="server" Text="Alumno:" CssClass="lblCriterios"></asp:Label>
                    --%>
                </td>
                <td valign="top" class="TDCriterios25">
                    <%--                    <asp:DropDownList ID="ddlAlumno" runat="server">
                    </asp:DropDownList>
                    --%>
                </td>
            </tr>
        </table>
    </div>
    <div id="divPromedioPeriodo" runat="server">
        <rep:Reporte ID="rptPromedioCalificacionesAsignaturaCurso" runat="server"></rep:Reporte>
    </div>
</asp:Content>
