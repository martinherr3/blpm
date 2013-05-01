<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Usuarios.aspx.cs" Inherits="Promethee.Usuarios" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 30%" valign="top">
                <table border="0" cellspacing="1" cellpadding="5">
                    <tr>
                        <td align="center" class="titulo">
                            Creacion de usuario
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="tablaInterna" width="400px" border="0" cellspacing="1" cellpadding="5">
                                <tr>
                                    <td width="80">
                                        Nombre:
                                    </td>
                                    <td width="10">
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNombre" Width="150" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Apellido:
                                    </td>
                                    <td width="10">
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtApellido" Width="150" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Login:
                                    </td>
                                    <td width="10">
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLogin" Width="150" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Password:
                                    </td>
                                    <td width="10">
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPassword" Width="150" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="center">
                                        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" />
                                        &nbsp;&nbsp;
                                        <%--<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />--%>
                                    </td>
                                </tr>
                            </table>
                            <div id="lblMessage" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table border="0" cellspacing="1" cellpadding="5" width="100%">
                    <tr>
                        <td align="center" class="titulo">
                            Usuarios Existentes
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="tablaInterna" width="100%" border="0" cellspacing="1" cellpadding="5">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="udpGrilla" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:GridView ID="gvwUsuarios" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListado"
                                                    AllowPaging="true" AutoGenerateColumns="false" AllowSorting="false" Width="100%"
                                                    CellPadding="5" CellSpacing="1" OnRowCommand="gvwUsuarios_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Acciones">
                                                            <HeaderStyle HorizontalAlign="center" Width="5%" />
                                                            <ItemStyle HorizontalAlign="center" />
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnEliminar" runat="server" CommandName="eliminar" CommandArgument='<%# Bind("idUsuario") %>'
                                                                    ToolTip="Eliminar" ImageUrl="~/Images/Grillas/trash.png" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Apellido">
                                                            <HeaderStyle HorizontalAlign="center" Width="30%" />
                                                            <ItemStyle HorizontalAlign="center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApellido" runat="server" Text='<%# Bind("apellido") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Nombre">
                                                            <HeaderStyle HorizontalAlign="center" Width="30%" />
                                                            <ItemStyle HorizontalAlign="center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNombre" runat="server" Text='<%# Bind("nombre") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Username">
                                                            <HeaderStyle HorizontalAlign="center" Width="30%" />
                                                            <ItemStyle HorizontalAlign="center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUsername" runat="server" Text='<%# Bind("username") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HiddenField5" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="mpeEliminar" runat="server" PopupControlID="pnlEliminar"
        TargetControlID="HiddenField5" RepositionMode="RepositionOnWindowResizeAndScroll"
        BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="pnlEliminar">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlEliminar" runat="server" Width="300px" Height="180px" Style="display: none;
        text-align: left" BorderStyle="Outset" CssClass="CajaDialogo">
        <table class="tablaInterna" border="0" cellpadding="1" cellspacing="5">
            <tr>
                <td>
                    <h2>
                        Eliminar Usuario</h2>
                </td>
            </tr>
        </table>
        <table class="tablaInterna" border="0" cellpadding="1" cellspacing="5">
            <tr>
                <td class="TD140px" style="vertical-align: text-bottom">
                    <asp:Label ID="lblEliminar" Text="¿Desea Eliminar el Usuario y todos sus modelos?"
                        runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <br />
                    <asp:ImageButton ID="btnEliminar" runat="server" ToolTip="Aceptar" ImageUrl="~/Images/button_ok.png"
                        OnClick="btnEliminar_OnClick" />
                    <asp:ImageButton ID="btnCancelEliminar" runat="server" ToolTip="Cancelar" ImageUrl="~/Images/button_cancel.png"
                        OnClick="btnCerrarPopUp_Click" CausesValidation="false" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
