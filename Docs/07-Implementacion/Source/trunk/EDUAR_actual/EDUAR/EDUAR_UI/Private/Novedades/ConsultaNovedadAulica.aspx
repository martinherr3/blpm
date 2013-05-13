<%@ Page Title="Novedades Aulicas" Language="C#" MasterPageFile="~/EDUARMaster.Master" AutoEventWireup="true"
    CodeBehind="ConsultaNovedadAulica.aspx.cs" Inherits="EDUAR_UI.ConsultaNovedadAulica" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Src="~/UserControls/Novedad.ascx" TagName="Novedad" TagPrefix="nov" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="tablaInterna" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <h2>
                    <asp:Label ID="lblTitulo" Text="Novedades Aulicas" runat="server" /></h2>
                <br />
            </td>
            <td align="right" rowspan="2">
                <asp:UpdatePanel ID="udpBotonera" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <nov:Novedad ID="novControl" runat="server" visible="false" ToolTip="Responder" />
                        <asp:ImageButton ID="btnVolver" OnClick="btnVolver_Click" runat="server" ToolTip="Volver"
                            ImageUrl="~/Images/botonVolver.png" Visible="false" />
                        <asp:ImageButton ID="btnVolverAnterior" OnClick="btnVolverAnterior_Click" runat="server"
                            ToolTip="Volver" ImageUrl="~/Images/botonVolver.png" Visible="true" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvwNovedades" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="novControl" EventName="GuardarClick" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <table class="tablaInterna" cellpadding="0" cellspacing="0">
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="udpGrilla" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="divGrilla" runat="server">
                <asp:GridView ID="gvwNovedades" runat="server" SkinID="gridviewSkinPagerListado"
                    OnPageIndexChanging="gvwNovedades_PageIndexChanging" DataKeyNames="idNovedad"
                    OnRowCommand="gvwNovedades_RowCommand">
                    <EmptyDataRowStyle CssClass="DatosListaNormal" HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="Acciones">
                            <HeaderStyle HorizontalAlign="center" Width="5%" />
                            <ItemStyle HorizontalAlign="center" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnVerConversacion" runat="server" CommandName="verConversacion"
                                    CommandArgument='<%# Bind("idNovedad") %>' ToolTip="Ver Conversación" ImageUrl="~/Images/Grillas/ver_conversacion.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha">
                            <HeaderStyle HorizontalAlign="center" Width="10%" />
                            <ItemStyle HorizontalAlign="center" />
                            <ItemTemplate>
                                <asp:Label ID="lblFechaGrilla" runat="server" Text='<%# Bind("fecha","{0:d}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Usuario">
                            <HeaderStyle HorizontalAlign="left" Width="10%" />
                            <ItemStyle HorizontalAlign="left" />
                            <ItemTemplate>
                                <asp:Label ID="lblUsuario" runat="server" Text='<%# String.Format("{0} {1}", Eval("usuario.nombre"), Eval("usuario.apellido")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipo de Novedad">
                            <HeaderStyle HorizontalAlign="left" Width="10%" />
                            <ItemStyle HorizontalAlign="left" />
                            <ItemTemplate>
                                <asp:Label ID="lblTipoGrilla" runat="server" Text='<%# Bind("tipo.nombre") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado">
                            <HeaderStyle HorizontalAlign="left" Width="10%" />
                            <ItemStyle HorizontalAlign="left" />
                            <ItemTemplate>
                                <asp:Label ID="lblEstadoGrilla" runat="server" Text='<%# Bind("estado.nombre") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Novedad">
                            <HeaderStyle HorizontalAlign="left" Width="40%" />
                            <ItemStyle HorizontalAlign="left" />
                            <ItemTemplate>
                                <asp:Label ID="lblObservacionesGrilla" runat="server" Text='<%# TruncateString(DataBinder.Eval(Container.DataItem, "observaciones").ToString())%>'
                                    ToolTip='<%# Bind("observaciones") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnVolver" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="novControl" EventName="GuardarClick" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="udpConversacion" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="divConversacion" runat="server">
                <asp:Repeater ID="rptConversacion" runat="server">
                    <ItemTemplate>
                        <div id="divIzquierda" runat="server" class="bubble" style="background: #B0C4DE;
                            border-color: #B0C4DE;">
                            <div style="text-align: right; float: right">
                                <asp:Label ID="lblFecha" Text='<%# Bind("fecha","{0:dd/MM/yyyy HH:mm}") %>' runat="server"
                                    Font-Italic="true" /></div>
                            <div style="text-align: left">
                                <asp:Label ID="lblUsuario" Text='<%# String.Format("{0} {1}", Eval("usuario.nombre"), Eval("usuario.apellido")) %>'
                                    runat="server" Font-Bold="true" /><br />
                            </div>
                            <div style="text-align: left">
                                <asp:Label ID="lblConversacion" Text='<%# Bind("observaciones") %>' runat="server" />
                            </div>
                        </div>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <div id="divDerecha" runat="server" class="bubbleRight" style="background: #87CEFF;
                            border-color: #87CEFF;">
                            <div style="text-align: right; float: right">
                                <asp:Label ID="lblFecha" Text='<%# Bind("fecha","{0:dd/MM/yyyy HH:mm}") %>' runat="server"
                                    Font-Italic="true" /></div>
                            <div style="text-align: left">
                                <asp:Label ID="lblUsuario" Text='<%# String.Format("{0} {1}", Eval("usuario.nombre"), Eval("usuario.apellido")) %>'
                                    runat="server" Font-Bold="true" /><br />
                            </div>
                            <div style="text-align: left">
                                <asp:Label ID="lblConversacion" Text='<%# Bind("observaciones") %>' runat="server" />
                            </div>
                        </div>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gvwNovedades" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="btnVolver" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="novControl" EventName="GuardarClick" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
