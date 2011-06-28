<%@ Page Title="Modificar Usuario" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="ChangeUser.aspx.cs" Inherits="EDUAR_UI.ChangeUser" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Modificar Usuario</h2>
    <br />
    <table class="tablaInterna" cellpadding="0" cellspacing="0">
        <tr>
            <td align="right">
                <asp:ImageButton ID="btnBuscar" ImageUrl="~/Images/botonBuscar.png" OnClick="btnBuscar_Click"
                    runat="server" ToolTip="Buscar" />
            </td>
        </tr>
    </table>
    <table class="tablaInterna" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 50%; vertical-align: text-top">
                <table class="tablaInterna" cellpadding="1" cellspacing="5">
                    <tr>
                        <td valign="top">
                            <asp:Label ID="lblUserNameBusqeuda" runat="server" Text="Usuario:"></asp:Label>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="txtUsernameBusqueda" runat="server" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <asp:Label ID="lblHabilitadoBusqueda" runat="server" Text="Habilitado:"></asp:Label>
                        </td>
                        <td valign="top">
                            <asp:CheckBox ID="chkHabilitadoBusqueda" runat="server" Checked="true" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 50%">
                <table class="tablaInterna" cellpadding="1" cellspacing="5">
                    <tr>
                        <td valign="top">
                            <asp:Label ID="lblRolesBusqueda" runat="server" Text="Roles:"></asp:Label>
                        </td>
                        <td rowspan="3">
                            <asp:CheckBoxList ID="chkListRolesBusqueda" TabIndex="2" runat="server">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <asp:UpdatePanel ID="udpGrilla" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView ID="gvwUsuarios" runat="server" PagerSettings-Visible="true" BorderStyle="Solid"
                OnRowCommand="gvwUsuarios_RowCommand" Width="100%" AutoGenerateColumns="False"
                AllowPaging="True" DataKeyNames="Nombre">
                <EmptyDataRowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Acciones">
                        <HeaderStyle HorizontalAlign="center" Width="5%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="modificarUsuario" runat="server" CommandName="Editar" CommandArgument='<%# Bind("Nombre") %>'
                                ToolTip="Editar" ImageUrl="~/Images/Grillas/action_edit.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Usuario">
                        <HeaderStyle HorizontalAlign="left" Width="40%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="userName" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:UpdatePanel ID="udpRoles" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <br />
                    <table class="tablaInterna" cellpadding="0" cellspacing="0" style="border-top: 1px solid silver;
                        padding-top: 5px">
                        <tr>
                            <td align="right">
                                <asp:ImageButton ID="btnGuardar" ImageUrl="~/Images/botonGuardar.png" OnClick="btnGuardar_Click"
                                    runat="server" ToolTip="Guardar" />
                            </td>
                        </tr>
                    </table>
                    <table class="tablaInterna" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 50%; vertical-align: text-top">
                                <table class="tablaInterna" cellpadding="1" cellspacing="5">
                                    <tr>
                                        <td valign="top">
                                            <asp:Label ID="lblUsuario" runat="server" Text="Usuario:"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:Label ID="lblUserName" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblBloquear" runat="server" Text="Habilitado:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkHabilitado" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%">
                                <table class="tablaInterna" cellpadding="1" cellspacing="5">
                                    <tr>
                                        <td valign="top">
                                            <asp:Label ID="lblRoles" runat="server" Text="Roles:"></asp:Label>
                                        </td>
                                        <td rowspan="3">
                                            <asp:CheckBoxList ID="chkListRoles" TabIndex="7" runat="server">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="gvwUsuarios" EventName="RowCommand" />
                </Triggers>
            </asp:UpdatePanel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvwUsuarios" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
