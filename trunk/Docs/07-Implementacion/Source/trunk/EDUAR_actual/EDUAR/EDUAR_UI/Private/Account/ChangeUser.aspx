<%@ Page Title="Modificar Usuarios" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="ChangeUser.aspx.cs" Inherits="EDUAR_UI.ChangeUser" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        //<!-- WPJD37: Copiar esto-->
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() == undefined) {
                alertTest();
            }
        }

        function alertTest() {
            $(document).ready(function () {
                $(".chzn-select").chosen();
            });
        }

        alertTest();    
    </script>
    <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="tablaInterna" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <h2>
                            Modificar Usuario
                        </h2>
                        <br />
                    </td>
                    <td align="right">
                        <asp:ImageButton ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" ToolTip="Buscar"
                            ImageUrl="~/Images/botonBuscar.png" />
                        <asp:ImageButton ID="btnGuardar" ImageUrl="~/Images/botonGuardar.png" OnClick="btnGuardar_Click"
                            runat="server" ToolTip="Guardar" />
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
                            <td class="TD100px">
                                <asp:Label ID="lblUserNameBusqueda" runat="server" Text="Usuario:"></asp:Label>
                            </td>
                            <td valign="top">
                                <select data-placeholder="Seleccione" style="width: 100%" multiple="false" class="chzn-select"
                                    runat="server" id="ddlUser" enableviewstate="true">
                                </select>
                           </td>
                        </tr>
                        <tr>
                            <td class="TD100px">
                                <asp:Label ID="lblHabilitadoBusqueda" runat="server" Text="Habilitado:"></asp:Label>
                            </td>
                            <td valign="top">
                                <asp:CheckBox ID="chkHabilitadoBusqueda" runat="server" Checked="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TD100px">
                                <asp:Label ID="lblRolesBusqueda" runat="server" Text="Rol:"></asp:Label>
                            </td>
                            <td valign="top">
                                <asp:DropDownList ID="ddlListRolesBusqueda" runat="server" TabIndex="2">
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
        </Triggers>
    </asp:UpdatePanel>
   
    <asp:UpdatePanel ID="udpGrilla" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView ID="gvwUsuarios" runat="server" PagerSettings-Visible="true" BorderStyle="Solid"
                OnRowCommand="gvwUsuarios_RowCommand" Width="500px" AutoGenerateColumns="False"
                AllowPaging="True" DataKeyNames="Nombre" SkinID="gridviewSkinPagerListado" OnPageIndexChanging="gvwUsuarios_PageIndexChanging">
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
                    <table class="tablaInterna" cellpadding="1" cellspacing="5">
                        <tr>
                            <td valign="top" class="TD100px">
                                <asp:Label ID="lblUsuario" runat="server" Text="Usuario:"></asp:Label>
                            </td>
                            <td valign="top">
                                <asp:Label ID="lblUserName" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="TD100px">
                                <asp:Label ID="lblBloquear" runat="server" Text="Habilitado:"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkHabilitado" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TD100px">
                                <asp:Label ID="lblRoles" runat="server" Text="Rol:"></asp:Label>
                            </td>
                            <td valign="top">
                                <asp:DropDownList ID="ddlListRoles" runat="server" TabIndex="7">
                                </asp:DropDownList>
                                <%--<asp:CheckBoxList ID="chkListRoles" TabIndex="7" runat="server">
                                            </asp:CheckBoxList>--%>
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
     <script type="text/javascript">         $(".chzn-select").chosen();</script>

</asp:Content>
