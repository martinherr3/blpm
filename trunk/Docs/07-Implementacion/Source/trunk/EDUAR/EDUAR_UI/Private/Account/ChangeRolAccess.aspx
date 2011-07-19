<%@ Page Title="Cambiar Nivel de Acceso" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="ChangeRolAccess.aspx.cs" Inherits="EDUAR_UI.ChangeRolAccess" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="udpGrilla" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView ID="gvwSecciones" runat="server" PagerSettings-Visible="true" BorderStyle="Solid"
                OnRowCommand="gvwSecciones_RowCommand" Width="100%" AutoGenerateColumns="False"
                AllowPaging="True" DataKeyNames="">
                <EmptyDataRowStyle HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Acciones">
                        <HeaderStyle HorizontalAlign="center" Width="5%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <%--<asp:ImageButton ID="modificarUsuario" runat="server" CommandName="Editar" CommandArgument='<%# Bind("Title") %>'
                                ToolTip="Editar" ImageUrl="~/Images/Grillas/action_edit.png" />--%>
                            <asp:ImageButton ID="modificarSeccion" runat="server" CommandName="Editar" ToolTip="Editar"
                                ImageUrl="~/Images/Grillas/action_edit.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sección">
                        <HeaderStyle HorizontalAlign="left" Width="40%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="seccion" runat="server" Text='<%# Bind("title") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:ListBox ID="lstRoles" runat="server"></asp:ListBox>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
