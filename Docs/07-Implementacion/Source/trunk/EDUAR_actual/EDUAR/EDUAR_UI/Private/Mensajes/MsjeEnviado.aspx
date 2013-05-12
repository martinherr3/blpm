<%@ Page Title="Enviados" Language="C#" MasterPageFile="~/EDUARMaster.Master" AutoEventWireup="true"
    CodeBehind="MsjeEnviado.aspx.cs" Inherits="EDUAR_UI.MsjeEnviado" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/UserControls/Editor.ascx" TagName="Editor" TagPrefix="edi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="tablaInterna" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <h2>
                    Enviados
                </h2>
                <br />
            </td>
            <td align="right">
                <asp:ImageButton ID="btnNuevo" OnClick="btnNuevo_Click" runat="server" ToolTip="Nuevo Mensaje"
                    ImageUrl="~/Images/botonNuevoMail.png" Visible="true" />
                <asp:ImageButton ID="btnRecibidos" OnClick="btnRecibidos_Click" runat="server" ToolTip="Recibidos"
                    ImageUrl="~/Images/verMensajes.png" Visible="true" />
                <asp:ImageButton ID="btnEliminar" runat="server" ToolTip="Eliminar Seleccionados"
                    AlternateText="Eliminar Seleccionados" ImageUrl="~/Images/mail-delete.png" OnClick="btnEliminar_Click" />
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
            <asp:UpdatePanel ID="udpReporte" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="gvwReporte" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListado"
                        AutoGenerateColumns="false" AllowPaging="true" Width="100%" DataKeyNames="idMensajeDestinatario"
                        OnRowCommand="gvwReporte_RowCommand" ShowFooter="false" >
                        <Columns>
                            <asp:TemplateField HeaderText="" HeaderStyle-VerticalAlign="Top">
                                <HeaderStyle HorizontalAlign="left" Width="5%" />
                                <HeaderTemplate>
                                    <asp:CheckBox ID="cboxhead" AutoPostBack="true" Text='Acciones' runat="server" Width="100px"
                                        ClientIDMode="Static" OnCheckedChanged="HeaderCheckedChanged" Style="margin-bottom: 4px" />
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <ItemTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td valign="middle" style="border: none">
                                                <asp:CheckBox ID="checkEliminar" Text='<%# Eval("idMensaje")%>' runat="server" CssClass="HiddenText"
                                                    Width="30px" ClientIDMode="Static" />
                                            </td>
                                            <td style="border: none">
                                                <asp:ImageButton ImageUrl="~/Images/Grillas/mail-mark-read-2.png" runat="server"
                                                    ID="btnLeer" AlternateText="Leer" ToolTip="Leer" ImageAlign="TextTop" CommandName="Leer"
                                                    CommandArgument='<%# Bind("idMensaje") %>' />
                                                <asp:ImageButton ImageUrl="~/Images/Grillas/mail-delete-2.png" runat="server" ID="btnEliminar"
                                                    AlternateText="Eliminar" ToolTip="Eliminar" ImageAlign="TextTop" CommandName="Eliminar"
                                                    CommandArgument='<%# Bind("idMensaje") %>' />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Destinatario" SortExpression="nombreDestinatario">
                                <HeaderStyle HorizontalAlign="left" Width="20%" />
                                <ItemStyle HorizontalAlign="left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblRemitente" runat="server" Text='<%# String.Format("{0} {1}", Eval("destinatario.nombre"), Eval("destinatario.apellido")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Asunto" SortExpression="asuntoMensaje">
                                <HeaderStyle HorizontalAlign="left" Width="20%" />
                                <ItemStyle HorizontalAlign="left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAsunto" runat="server" Text='<%# Eval("asuntoMensaje") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha" SortExpression="fechaEnvio">
                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblFecha" runat="server" Text='<%# (Eval("fechaEnvio","{0:d}") == DateTime.Now.ToShortDateString()) ?  Eval("horaEnvio","{0:HH:mm}") : Eval("fechaEnvio","{0:d}") %>'></asp:Label>
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
                                <asp:Label ID="Label1" Text="Destinatario: " runat="server" Font-Bold="true" /><asp:Literal
                                    ID="litRemitente" Text="" runat="server" />
                            </td>
                            <td class="TDCriterios50">
                                <asp:Label ID="Label2" Text="Fecha de Envío: " runat="server" Font-Bold="true" /><asp:Literal
                                    ID="litFecha" Text="" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:Label ID="Label3" Text="Asunto: " runat="server" Font-Bold="true" /><asp:Literal
                        ID="litAsunto" Text="" runat="server" /><br />
                    <hr />
                    <asp:Literal ID="litContenido" Text="" runat="server" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
