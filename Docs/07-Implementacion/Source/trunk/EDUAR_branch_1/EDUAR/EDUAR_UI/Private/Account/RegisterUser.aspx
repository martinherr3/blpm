<%@ Page Title="Registrar Usuario" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="RegisterUser.aspx.cs" Inherits="EDUAR_UI.RegisterUser" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Registrar usuario</h2>
    <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" class="tablaInterna">
                <tr>
                    <td align="right">
                        <asp:ImageButton ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" ToolTip="Buscar"
                            ImageUrl="~/Images/botonBuscar.png" />
                        <asp:ImageButton ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" ToolTip="Guardar"
                            ImageUrl="~/Images/botonGuardar.png" />
                        <asp:ImageButton ID="btnVolver" OnClick="btnVolver_Click" runat="server" ToolTip="Volver"
                            ImageUrl="~/Images/botonVolver.png" />
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="udpFiltrosBusqueda" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table class="tablaInterna" cellpadding="1" cellspacing="5">
                        <tr>
                            <td colspan="2">
                                <h3>
                                    Buscar</h3>
                            </td>
                        </tr>
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
                                <asp:CheckBox ID="chkActivoBusqueda" runat="server" Checked="true" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTipoUsuario" runat="server" Text="Tipo de Usuario:"></asp:Label>
                            </td>
                            <td valign="top">
                                <asp:DropDownList ID="ddlTipoUsuario" runat="server">
                                </asp:DropDownList>
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
            <asp:AsyncPostBackTrigger ControlID="gvwUsuarios" EventName="RowCommand" />
            <%--<asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click" />--%>
        </Triggers>
    </asp:UpdatePanel>
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
                            <asp:ImageButton ID="nuevoUsuario" runat="server" CommandName="Nuevo" CommandArgument='<%# Bind("idPersona") %>'
                                ToolTip="Nuevo Usuario" ImageUrl="~/Images/Grillas/action_new.png" />
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
                    <table class="tablaInterna" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="2">
                                <h3>
                                    Registrar Usuario</h3>
                                <br />
                                <asp:Label ID="lblNombreApellido" Text="" runat="server" Font-Bold="true" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary ID="RegisterUserValidationGroup" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%;">
                                <table class="tablaInterna" cellpadding="1" cellspacing="5">
                                    <tr>
                                        <td valign="top">
                                            <asp:Label ID="lblUsuario" runat="server" Text="Usuario:"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:TextBox ID="txtUserName" runat="server" Font-Bold="true"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtUserName"
                                                CssClass="failureNotification" ErrorMessage="El nombre de usuario es obligatorio."
                                                ToolTip="El nombre de usuario es obligatorio." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <%--<asp:Label ID="lblEmailUsuario" runat="server" Font-Bold="true"></asp:Label>--%>
                                            <asp:TextBox ID="txtEmailUsuario" runat="server" Font-Bold="true" />
                                            <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="txtEmailUsuario"
                                                CssClass="failureNotification" ErrorMessage="El correo electrónico es obligatorio."
                                                ToolTip="El correo electrónico es obligatorio." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
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
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPregunta" runat="server" Text="Pregunta Secreta:"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:Label ID="lblPreguntaUsuario" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblRespuesta" runat="server" Text="Respuesta:"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:Label ID="lblRespuestaUsuario" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%; vertical-align: text-top">
                                <table class="tablaInterna" cellpadding="1" cellspacing="5">
                                    <tr>
                                        <td valign="top">
                                            <asp:Label ID="lblRoles" runat="server" Text="Rol:"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:DropDownList ID="ddlListRoles" runat="server" TabIndex="7">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
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
            <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvwUsuarios" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
