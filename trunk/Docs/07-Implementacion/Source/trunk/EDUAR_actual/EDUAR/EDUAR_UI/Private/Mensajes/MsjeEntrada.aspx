<%@ Page Title="Recibidos" Language="C#" MasterPageFile="~/EDUARMaster.Master" AutoEventWireup="true"
    CodeBehind="MsjeEntrada.aspx.cs" Inherits="EDUAR_UI.MsjeEntrada" Theme="Tema"
    StylesheetTheme="Tema" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/UserControls/Editor.ascx" TagName="Editor" TagPrefix="edi" %>
<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="udpGrilla" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="tablaInterna" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <h2>
                            Recibidos
                        </h2>
                        <br />
                    </td>
                    <td align="right">
                        <asp:ImageButton ID="btnEnviar" OnClick="btnEnviar_Click" runat="server" ToolTip="Enviar"
                            ImageUrl="~/Images/botonEnviarMail.png" Visible="false" />
                        <asp:ImageButton ID="btnNuevo" OnClick="btnNuevo_Click" runat="server" ToolTip="Nuevo Mensaje"
                            ImageUrl="~/Images/botonNuevoMail.png" Visible="true" />
                        <asp:ImageButton ID="btnEnviados" OnClick="btnEnviados_Click" runat="server" ToolTip="Enviados"
                            ImageUrl="~/Images/botonEnviados.png" Visible="true" />
                        <asp:ImageButton ID="btnEliminar" runat="server" ToolTip="Eliminar Seleccionados"
                            AlternateText="Eliminar Seleccionados" ImageUrl="~/Images/mail-delete.png" OnClick="btnEliminar_Click" />
                        <asp:ImageButton ID="btnVolver" OnClick="btnVolver_Click" runat="server" ToolTip="Volver"
                            ImageUrl="~/Images/botonVolver.png" Visible="false" />
                    </td>
                </tr>
            </table>
            <table class="tablaInterna" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="udpReporte" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="gvwReporte" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListado"
                        AutoGenerateColumns="false" AllowPaging="true" Width="100%" DataKeyNames="idMensajeDestinatario"
                        OnRowCommand="gvwReporte_RowCommand" AllowSorting="true" OnSorting="gvwReporte_Sorting"
                        ShowFooter="false">
                        <Columns>
                            <asp:TemplateField HeaderText="Acciones" HeaderStyle-VerticalAlign="Middle">
                                <HeaderStyle HorizontalAlign="left" Width="10%" />
                                <HeaderTemplate>
                                    <asp:CheckBox ID="cboxhead" AutoPostBack="true" Text='Acciones' runat="server" Width="100px"
                                        ClientIDMode="Static" OnCheckedChanged="HeaderCheckedChanged" Style="margin-bottom: 6px" />
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <ItemTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td valign="middle" style="border: none">
                                                <asp:CheckBox ID="checkEliminar" Text='<%# Eval("idMensajeDestinatario")%>' runat="server"
                                                    CssClass="HiddenText" Width="30px" ClientIDMode="Static" />
                                            </td>
                                            <td style="border: none">
                                                <asp:ImageButton ImageUrl="~/Images/Grillas/mail-mark-read-2.png" runat="server"
                                                    ID="btnLeer" AlternateText="Leer" ToolTip="Leer" ImageAlign="TextTop" CommandName="Leer"
                                                    CommandArgument='<%# Bind("idMensajeDestinatario") %>' />
                                                <asp:ImageButton ImageUrl="~/Images/Grillas/mail-reply-2.png" runat="server" ID="btnResponder"
                                                    AlternateText="Responder" ToolTip="Responder" ImageAlign="TextTop" CommandName="Responder"
                                                    CommandArgument='<%# Bind("idMensajeDestinatario") %>' />
                                                <asp:ImageButton ImageUrl="~/Images/Grillas/mail-delete-2.png" runat="server" ID="btnEliminar"
                                                    AlternateText="Eliminar" ToolTip="Eliminar" ImageAlign="TextTop" CommandName="Eliminar"
                                                    CommandArgument='<%# Bind("idMensajeDestinatario") %>' />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remitente" SortExpression="nombreRemitente">
                                <HeaderStyle HorizontalAlign="left" Width="15%" />
                                <ItemStyle HorizontalAlign="left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblRemitente" runat="server" Text='<%# String.Format("{0} {1}", Eval("remitente.nombre"), Eval("remitente.apellido")) %>'
                                        Font-Bold='<%# Boolean.Parse(Eval("leido").ToString()) ? false : true  %>'></asp:Label>
                                    <asp:Label ID="lblRol" runat="server" Text='<%# String.Format("({0})", Eval("remitente.tipoPersona.nombre")) %>'
                                        Font-Bold="true"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Asunto" SortExpression="asuntoMensaje">
                                <HeaderStyle HorizontalAlign="left" Width="45%" />
                                <ItemStyle HorizontalAlign="left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAsunto" runat="server" Text='<%# Eval("asuntoMensaje") %>' Font-Bold='<%# Boolean.Parse(Eval("leido").ToString()) ? false : true  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha" SortExpression="fechaEnvio">
                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblFecha" runat="server" Text='<%# (Eval("fechaEnvio","{0:d}") == DateTime.Now.ToShortDateString()) ?  Eval("horaEnvio","{0:HH:mm}") : Eval("fechaEnvio","{0:d}") %>'
                                        Font-Bold='<%# Boolean.Parse(Eval("leido").ToString()) ? false : true  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="divPaginacion" runat="server">
                <hr />
                <table class="tablaInternaMensajes" cellpadding="5" cellspacing="1">
                    <tr>
                        <td class="TDCriterios40">
                            Mostrar
                            <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                            </asp:DropDownList>
                            por Página
                        </td>
                        <td class="TD30" align="center">
                            <asp:Label ID="lblCantidad" Text="" runat="server" />
                        </td>
                        <td class="TD30" align="right">
                            <table>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="lnkbtnFirst" OnClick="lnkbtnFirst_Click" ImageUrl="~/Images/Paginador/go-first-view.png"
                                            runat="server" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="lnkbtnPrevious" OnClick="lnkbtnPrevious_Click" ImageUrl="~/Images/Paginador/go-previous-view.png"
                                            runat="server" />
                                    </td>
                                    <td>
                                        <asp:DataList ID="dlPaging" runat="server" OnItemCommand="dlPaging_ItemCommand" OnItemDataBound="dlPaging_ItemDataBound"
                                            RepeatDirection="Horizontal">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnPaging" runat="server" CommandArgument='<%# Eval("PageIndex") %>'
                                                    CommandName="lnkbtnPaging" Text='<%# Eval("PageText") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="lnkbtnNext" OnClick="lnkbtnNext_Click" ImageUrl="~/Images/Paginador/go-next-view.png"
                                            runat="server" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="lnkbtnLast" OnClick="lnkbtnLast_Click" ImageUrl="~/Images/Paginador/go-last-view.png"
                                            runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divContenido" runat="server" style="padding: 5px 5px 5px 5px; border-top: 1px solid black;
                border-left: 1px solid black; border-right: 1px solid black; border-bottom: 1px solid black;
                overflow: visible">
                <div style="min-height: 250px; height: auto; overflow: visible">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td class="TDCriterios50">
                                <asp:Label Text="Remitente: " runat="server" Font-Bold="true" /><asp:Literal ID="litRemitente"
                                    Text="" runat="server" />
                            </td>
                            <td class="TDCriterios50">
                                <asp:Label Text="Fecha de Envío: " runat="server" Font-Bold="true" /><asp:Literal
                                    ID="litFecha" Text="" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:Label Text="Asunto: " runat="server" Font-Bold="true" /><asp:Literal ID="litAsunto"
                        Text="" runat="server" /><br />
                    <hr />
                    <asp:Literal ID="litContenido" Text="" runat="server" />
                </div>
            </div>
            <div id="divReply" runat="server">
                <asp:HiddenField ID="hdfDestinatario" runat="server" />
                <table class="tablaInternaMensajes" width="100%">
                    <tr>
                        <td class="TD20">
                            Destinatario
                        </td>
                        <td class="TD80">
                            <asp:Label ID="lblDestinatario" Text="" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="TD20">
                            Asunto
                        </td>
                        <td class="TD80">
                            <asp:TextBox ID="txtAsunto" runat="server" Style="width: 595px; background-color: #FFFFFF;
                                background-image: -moz-linear-gradient(center bottom , white 85%, #EEEEEE 99%);
                                border: 1px solid #AAAAAA; cursor: text; height: 26px !important; margin: 0;
                                overflow: hidden; padding: 0; position: relative; font-family: sans-serif; font-size: 1em" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="width: 100%">
                            <edi:Editor ID="textoMensaje" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
