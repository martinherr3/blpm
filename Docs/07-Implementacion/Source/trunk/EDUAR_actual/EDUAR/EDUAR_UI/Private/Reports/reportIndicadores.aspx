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
        <br />
        <asp:UpdatePanel runat="server" ID="udpResultado" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Label ID="lblResultadoGrilla" Text="Se muestra la grilla de resultados de Preorden Parcial"
                    runat="server" Visible="false" Font-Bold="true" /><br />
                <asp:Label ID="lblResultado" Text="" runat="server" CssClass="lblCriterios" Visible="false" />
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvwResultado" runat="server" CssClass="DatosLista" AllowPaging="True"
                            AllowSorting="True" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="true"
                            OnPageIndexChanging="gvwResultado_PageIndexChanging" PageSize="15" OnSorting="gvwResultado_Sorting"
                            OnRowCreated="gvwResultado_RowCreated" OnRowDataBound="gvwResultado_RowDataBound">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Height="24px" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center"/>
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnCalcular" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="gvwResultado" EventName="PageIndexChanging" />
                <asp:AsyncPostBackTrigger ControlID="gvwResultado" EventName="Sorting" />
                <asp:AsyncPostBackTrigger ControlID="gvwResultado" EventName="RowCreated" />
                <asp:AsyncPostBackTrigger ControlID="gvwResultado" EventName="RowDeleting" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
