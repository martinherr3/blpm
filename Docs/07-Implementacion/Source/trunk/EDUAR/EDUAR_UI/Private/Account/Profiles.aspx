<%@ Page Title="" Language="C#" MasterPageFile="~/EDUARMaster.Master" AutoEventWireup="true"
    CodeBehind="Profiles.aspx.cs" Inherits="EDUAR_UI.Profiles" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Perfiles</h2>
    <br />
    <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="tablaInterna" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="right">
                        <asp:ImageButton ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" ToolTip="Buscar"
                            ImageUrl="~/Images/botonBuscar.png" />
                        <asp:ImageButton ID="btnNuevo" OnClick="btnNuevo_Click" runat="server" ToolTip="Nuevo"
                            ImageUrl="~/Images/botonNuevo.png" />
                        <asp:ImageButton ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" ToolTip="Guardar"
                            ImageUrl="~/Images/botonGuardar.png" />
                        <asp:ImageButton ID="btnVolver" OnClick="btnVolver_Click" runat="server" ToolTip="Volver"
                            ImageUrl="~/Images/botonVolver.png" />
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="udpControlesBusqueda" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table class="tablaInterna" cellpadding="1" cellspacing="5">
                        <tr>
                            <td colspan="2">
                                <h3>
                                    Buscar</h3>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="width: 10%">
                                <asp:Label ID="lblRolesBusqueda" runat="server" Text="Perfiles:"></asp:Label>
                            </td>
                            <td style="width: 90%">
                                <asp:CheckBoxList ID="chkListRolesBusqueda" TabIndex="2" runat="server">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                    </table>
                    <br />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gvwPerfiles" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="udpGrilla" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView ID="gvwPerfiles" runat="server" PagerSettings-Visible="true" BorderStyle="Solid"
                OnRowCommand="gvwPerfiles_RowCommand" Width="100%" AutoGenerateColumns="False"
                AllowPaging="True" DataKeyNames="RoleId">
                <EmptyDataRowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Acciones">
                        <HeaderStyle HorizontalAlign="center" Width="5%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="modificarPerfil" runat="server" CommandName="Editar" CommandArgument='<%# Bind("RoleId") %>'
                                ToolTip="Editar" ImageUrl="~/Images/Grillas/action_edit.png" />
                            <asp:ImageButton ID="eliminarPerfil" runat="server" CommandName="Eliminar" CommandArgument='<%# Bind("Nombre") %>'
                                ToolTip="Eliminar" ImageUrl="~/Images/Grillas/action_delete.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Perfil">
                        <HeaderStyle HorizontalAlign="left" Width="20%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="nombre" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descripcion">
                        <HeaderStyle HorizontalAlign="left" Width="75%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="descripcion" runat="server" Text='<%# Bind("Descripcion") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:UpdatePanel ID="udpEditRoles" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table class="tablaInterna" cellpadding="1" cellspacing="5">
                        <tr>
                            <td colspan="2">
                                <h3>
                                    Editar Perfil</h3>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="lblNombre" runat="server" Text="Nombre:"></asp:Label>
                            </td>
                            <td valign="top">
                                <asp:Label ID="lblNombreRol" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="lblDescripcion" runat="server" Text="Descripcion:"></asp:Label>
                            </td>
                            <td valign="top">
                                <asp:TextBox runat="server" ID="txtDescripcion" CssClass="txtMultilinea" TextMode="MultiLine"
                                    MaxLength="256" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="udpNewRol" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table class="tablaInterna" cellpadding="1" cellspacing="5">
                        <tr>
                            <td colspan="2">
                                <h3>
                                    Nuevo Perfil</h3>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="lblNuevoRol" runat="server" Text="Nombre:"></asp:Label>
                            </td>
                            <td valign="top">
                                <asp:TextBox ID="txtNuevoRol" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="lblNuevaDescripcion" runat="server" Text="Descripcion:"></asp:Label>
                            </td>
                            <td valign="top">
                                <asp:TextBox runat="server" ID="txtNuevaDescripcion" CssClass="txtMultilinea" TextMode="MultiLine"
                                    MaxLength="256" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvwPerfiles" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
