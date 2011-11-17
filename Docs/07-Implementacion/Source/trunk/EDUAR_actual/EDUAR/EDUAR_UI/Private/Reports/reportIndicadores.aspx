<%@ Page Title="Indicadores" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="reportIndicadores.aspx.cs" Inherits="EDUAR_UI.reportIndicadores"
    EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Indicadores de Desempeño - <asp:Label ID="lblCicloLectivo" Text="" runat="server" /></h2>
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
                <td valign="top" class="TDCriterios75" colspan="3">
                    <asp:DropDownList ID="ddlCurso" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
