<%@ Page Title="" Language="C#" MasterPageFile="~/EDUARMaster.Master" AutoEventWireup="true"
    CodeBehind="RegisterUser.aspx.cs" Inherits="EDUAR_UI.RegisterUser" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Registrar usuario</h2>
    <table cellpadding="0" cellspacing="0" class="tablaInterna">
        <tr>
            <td align="right">
                <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/Images/botonBuscar.png"
                    OnClick="btnBuscar_Click" ToolTip="Buscar" />
            </td>
        </tr>
    </table>
    <table class="tablaInterna" cellpadding="1" cellspacing="5">
        <tr>
            <td valign="top">
                <asp:Label ID="lblNombreBusqueda" runat="server" Text="Nombre:"></asp:Label>
            </td>
            <td valign="top">
                <asp:TextBox ID="txtNombreBusqueda" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:Label ID="lblApellidoBusqueda" runat="server" Text="Apellido:"></asp:Label>
            </td>
            <td valign="top">
                <asp:TextBox ID="txtApellidoBusqueda" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:Label ID="lblActivoBusqueda" runat="server" Text="Activo:"></asp:Label>
            </td>
            <td valign="top">
                <asp:CheckBox ID="chkActivoBusqueda" runat="server" Checked="false" />
            </td>
        </tr>
    </table>
    <br />
    <asp:UpdatePanel ID="udpGrilla" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView ID="gvwUsuarios" runat="server" PagerSettings-Visible="true" BorderStyle="Solid"
                OnRowCommand="gvwUsuarios_RowCommand" Width="100%" AutoGenerateColumns="False"
                OnPageIndexChanging="gvwUsuarios_PageIndexChanging" AllowPaging="True" DataKeyNames="idPersona">
                <EmptyDataRowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Acciones">
                        <HeaderStyle HorizontalAlign="center" Width="5%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="modificarUsuario" runat="server" CommandName="Editar" CommandArgument='<%# Bind("idPersona") %>'
                                ToolTip="Editar" ImageUrl="~/Images/Grillas/action_edit.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nombre">
                        <HeaderStyle HorizontalAlign="left" Width="20%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblNombre" runat="server" Text='<%# Bind("nombre") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Apellido">
                        <HeaderStyle HorizontalAlign="left" Width="20%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblApellido" runat="server" Text='<%# Bind("apellido") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email">
                        <HeaderStyle HorizontalAlign="left" Width="20%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("email") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Activo">
                        <HeaderStyle HorizontalAlign="left" Width="10%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblActivo" runat="server" Text='<%# Boolean.Parse(Eval("activo").ToString()) ? "Sí" : "No"  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nro DNI">
                        <HeaderStyle HorizontalAlign="left" Width="10%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblNumeroDocumento" runat="server" Text='<%# Bind("numeroDocumento") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:UpdatePanel ID="udpRoles" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <br />
                    <table cellpadding="0" cellspacing="0" class="tablaInterna" style="border-top: 1px solid silver;
                        padding-top: 5px">
                        <tr>
                            <td align="right">
                                <asp:ImageButton ID="btnGuardar" runat="server" ImageUrl="~/Images/botonGuardar.png"
                                    OnClick="btnGuardar_Click" ToolTip="Guardar" />
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
                                            <asp:TextBox ID="txtUserName" runat="server" Font-Bold="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                     <td valign="top">
                                            <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:Label ID="lblEmailUsuario" runat="server" Font-Bold="true"></asp:Label>
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
                    <asp:AsyncPostBackTrigger ControlID="gvwUsuarios" EventName="PageIndexChanging" />
                </Triggers>
            </asp:UpdatePanel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvwUsuarios" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="gvwUsuarios" EventName="PageIndexChanging" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
