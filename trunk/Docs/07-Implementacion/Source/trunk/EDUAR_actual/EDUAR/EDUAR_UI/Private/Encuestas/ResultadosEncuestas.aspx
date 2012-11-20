<%@ Page Title="Resultados Encuesta" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="ResultadosEncuestas.aspx.cs" Inherits="EDUAR_UI.ResultadosEncuestas"
    Theme="Tema" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/Grafico.ascx" TagName="Grafico" TagPrefix="gra" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="tablaInterna" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <h2>
                    Análisis de Resultados:
                    <asp:Label ID="lblTitulo" Text="" runat="server" Font-Bold="true" ForeColor="#4264B1" /></h2>
                <br />
            </td>
            <td align="right">
                <%-- <asp:ImageButton ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" ToolTip="Buscar"
                            ImageUrl="~/Images/botonBuscar.png" />
                        <asp:ImageButton ID="btnNuevo" OnClick="btnNuevo_Click" runat="server" ToolTip="Nuevo"
                            ImageUrl="~/Images/botonNuevo.png" />
                        <asp:ImageButton ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" ToolTip="Guardar"
                            ImageUrl="~/Images/botonGuardar.png" CausesValidation="true" ValidationGroup="validarEdit" />--%>
                <asp:ImageButton ID="btnVolver" OnClick="btnVolver_Click" runat="server" ToolTip="Volver"
                    ImageUrl="~/Images/botonVolver.png" />
            </td>
        </tr>
    </table>
    <table class="tablaInterna" cellpadding="0" cellspacing="0">
        <tr>
            <td class="TD25">
                <asp:Label Text="Fecha Lanzamiento: " runat="server" />
                <asp:Label ID="lblFechaLanzamiento" Text="" runat="server" Font-Bold="true" /><br />
                <br />
            </td>
            <td class="TD25">
                <asp:Label Text="Fecha Expiración: " runat="server" />
                <asp:Label ID="lblFechaExpiracion" Text="" runat="server" Font-Bold="true" /><br />
                <br />
            </td>
            <td class="TD50">
            </td>
        </tr>
        <tr>
            <td class="TD25">
                <asp:Label Text="Encuestas Enviadas: " runat="server" />
                <asp:Label ID="lblEnviadas" Text="" runat="server" Font-Bold="true" />
            </td>
            <td class="TD25">
                <asp:Label Text="Encuestas Respondidas: " runat="server" />
                <asp:Label ID="lblRespondidas" Text="" runat="server" Font-Bold="true" />
            </td>
            <td class="TD50">
                <asp:Label ID="lblEncuestasPendientes" Text="Encuestas Pendientes: " runat="server" />
                <asp:Label ID="lblPendientes" Text="" runat="server" Font-Bold="true" />
            </td>
        </tr>
    </table>
    <%--<asp:UpdatePanel ID="udpPreguntas" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView ID="gvwPreguntas" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListado"
                AutoGenerateColumns="true" AllowPaging="false" Width="100%" DataKeyNames="idPregunta">
                <Columns>
                    <asp:TemplateField HeaderText="Pregunta">
                        <HeaderStyle HorizontalAlign="Center" Width="25%" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblPregunta" runat="server" Text='<%# Bind("textoPregunta")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Valor 1">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblValor1" runat="server" Text='<%# Bind("cant1")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Valor 2">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblValor2" runat="server" Text='<%# Bind("cant2")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Valor 3">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblValor3" runat="server" Text='<%# Bind("cant3")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Valor 4">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblValor4" runat="server" Text='<%# Bind("cant4")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Valor 5">
                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblValor5" runat="server" Text='<%# Bind("cant5")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>--%>
    <%--   <asp:UpdatePanel ID="udpPreguntas" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
    <cc1:Accordion ID="CuestionarioAccordion" CssClass="accordion" HeaderCssClass="accordionHeader"
        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
        RequireOpenedPane="false" SelectedIndex="-1" runat="server" Width="100%">
        <Panes>
            <%--LOS COMPONENTES DEL ACCORDION SON AGREGADOS DINÁMICAMENTE DESDE CÓDIGO--%>
        </Panes>
    </cc1:Accordion>
    <div id="divGrafico" runat="server" class="divGraficoOcultar">
        <gra:Grafico ID="grafico" runat="server" />
    </div>
</asp:Content>
