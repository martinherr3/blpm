<%@ Page Title="Indicadores Generales" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="IndicadoresGenerales.aspx.cs" Inherits="EDUAR_UI.IndicadoresGenerales" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Src="~/UserControls/Indicador.ascx" TagName="Indicador" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/InformeIndicador.ascx" TagName="InformeIndicador"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="tablaInterna" cellpadding="0" cellspacing="0">
        <tr>
            <td class="TD90">
                <h2>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblTitulo" Text="Indicadores De Desempeño" runat="server" /> - 
                            <asp:Label ID="lblIndicador" Text="" runat="server" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dtlIndicadores" EventName="ItemCommand" />
                        </Triggers>
                    </asp:UpdatePanel>
                </h2>
                <br />
            </td>
            <td align="right" rowspan="2">
                <%--<asp:ImageButton ID="btnVolverAnterior" OnClick="btnVolverAnterior_Click" runat="server"
                    ToolTip="Volver" ImageUrl="~/Images/botonVolver.png" Visible="true" />--%>
            </td>
        </tr>
    </table>
    <table class="tablaInterna" cellpadding="1" cellspacing="5">
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="udpIndicadores" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:DataList RepeatLayout="Table" RepeatColumns="6" RepeatDirection="Horizontal"
                runat="server" ID="dtlIndicadores" Width="100%" ShowHeader="true" ItemStyle-HorizontalAlign="Center"
                OnItemCommand="dtlIndicadores_ItemCommand">
                <ItemTemplate>
                    <asp:LinkButton ID="indicador" CommandArgument='<%# Bind("idIndicador") %>' CommandName="Indicador"
                        Text='<%# Bind("nombre") %>' runat="server" />
                </ItemTemplate>
            </asp:DataList>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="udpCursos" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="divCursos" runat="server">
                <asp:DataList RepeatLayout="Table" RepeatColumns="4" RepeatDirection="Horizontal"
                    runat="server" ID="dtlCursos" Width="100%" ShowHeader="true" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <uc1:Indicador ID="indicador" runat="server" idCursoIndicador='<%# Bind("idCurso") %>'
                            cursoIndicador='<%# Bind("nombre") %>'></uc1:Indicador>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <uc1:Indicador ID="indicador2" runat="server" idCursoIndicador='<%# Bind("idCurso") %>'
                            cursoIndicador='<%# Bind("nombre") %>'></uc1:Indicador>
                    </AlternatingItemTemplate>
                </asp:DataList>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="mpeContenido" runat="server" PopupControlID="pnlContenidos"
        TargetControlID="HiddenField1" RepositionMode="RepositionOnWindowResizeAndScroll"
        BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="pnlContenidos">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlContenidos" runat="server" Width="700px" Height="550px" Style="display: none;
        text-align: left" CssClass="CajaDialogo">
        <uc1:InformeIndicador ID="InformeIndicador1" runat="server"></uc1:InformeIndicador>
    </asp:Panel>
</asp:Content>
