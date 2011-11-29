<%@ Page Title="Indicadores" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="reportIndicadores.aspx.cs" Inherits="EDUAR_UI.reportIndicadores"
    EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/Criterio.ascx" TagName="Criterio" TagPrefix="cri" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Indicadores Generales de Desempeño -
        <asp:Label ID="lblCicloLectivo" Text="" runat="server" /></h2>
    <br />
    <div id="divAccion" runat="server">
        <table class="tablaInterna" cellpadding="0" cellspacing="0">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="btnCalcular" OnClick="btnCalcular_Click" runat="server" ToolTip="Calcular"
                        ImageUrl="~/Images/botonCalcular.png" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divFiltros" runat="server">
        <table class="tablaInterna" cellpadding="1" cellspacing="5">
            <tr>
                <td valign="top" class="TDCriterios25">
                    <asp:Label ID="lblCurso" runat="server" Text="Curso:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top" class="TDCriterios75">
                    <asp:DropDownList ID="ddlCurso" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <h3>
                        Criterios y Pesos</h3>
                </td>
            </tr>
            <tr>
                <td valign="top" class="TDCriterios25">
                    <asp:Label ID="lblCalificaciones" Text="Calificaciones" runat="server" CssClass="lblCriterios" />
                </td>
                <td valign="top" class="TDCriterios75">
                    <cri:Criterio ID="criterioCalificacion" runat="server"></cri:Criterio>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblInasistencias" Text="Inasistencias" runat="server" CssClass="lblCriterios" />
                </td>
                <td>
                    <cri:Criterio ID="criterioInasistencia" runat="server"></cri:Criterio>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSanciones" Text="Sanciones" runat="server" CssClass="lblCriterios" />
                </td>
                <td>
                    <cri:Criterio ID="criterioSancion" runat="server"></cri:Criterio>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
