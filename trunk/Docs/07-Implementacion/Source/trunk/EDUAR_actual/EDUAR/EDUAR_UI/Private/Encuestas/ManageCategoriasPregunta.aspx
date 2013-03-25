<%@ Page Title="Administrar Categorías de Pregunta" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="ManageCategoriasPregunta.aspx.cs" Inherits="EDUAR_UI.ManageCategoriasPregunta"
    Theme="Tema" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="tablaInterna" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <h2>
                            Administrar
                            <asp:Label Text="" runat="server" ID="lblTitulo" /></h2>
                        <br />
                    </td>
                    <td align="right">
                        <asp:ImageButton ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" ToolTip="Buscar"
                            ImageUrl="~/Images/botonBuscar.png" />
                        <asp:ImageButton ID="btnNuevo" OnClick="btnNuevo_Click" runat="server" ToolTip="Nuevo"
                            ImageUrl="~/Images/botonNuevo.png" />
                        <asp:ImageButton ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" ToolTip="Guardar"
                            ImageUrl="~/Images/botonGuardar.png" CausesValidation="true" ValidationGroup="validarEdit" />
                        <asp:ImageButton ID="btnVolver" OnClick="btnVolver_Click" runat="server" ToolTip="Volver"
                            ImageUrl="~/Images/botonVolver.png" />
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="udpFiltrosBusqueda" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table class="tablaInterna" cellpadding="1" cellspacing="5">
                        <tr>
                            <td>
                                <h3>
                                    Buscar Categorías Disponibles</h3>
                            </td>
                        </tr>
                        <tr>
                            <td class="TD100">
                                <asp:ValidationSummary ID="ValidarBusqueda" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <table class="tablaInterna" cellpadding="1" cellspacing="5">
                        <tr>
                            <td valign="top" class="TD50px">
                                <asp:Label ID="lblAmbito" runat="server" Text="Ambito:"></asp:Label>
                            </td>
                            <td valign="top" class="TD50px">
                                <asp:DropDownList ID="ddlAmbito" runat="server" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="udpGrilla" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView ID="gvwCategorias" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListado"
                AutoGenerateColumns="false" AllowPaging="false" Width="100%" DataKeyNames="idCategoriaPregunta"
                OnRowCommand="gvwCategorias_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Acciones">
                        <HeaderStyle HorizontalAlign="center" Width="15%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="editarCategoria" runat="server" CommandName="Editar" CommandArgument='<%# Bind("idCategoriaPregunta") %>'
                                ToolTip="Editar Categoría" ImageUrl="~/Images/Grillas/action_edit.png" />
                            <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Bind("idCategoriaPregunta") %>'
                                ToolTip="Eliminar Categoría" ImageUrl="~/Images/Grillas/action_delete.png" Visible='<%# Bind("disponible") %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ambito">
                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label ID="lblAmbito" runat="server" Text='<%# TruncateString(DataBinder.Eval(Container.DataItem, "ambito.nombre").ToString())%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nombre">
                        <HeaderStyle HorizontalAlign="left" Width="25%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblNombre" runat="server" Text='<%# TruncateString(DataBinder.Eval(Container.DataItem, "nombre").ToString())%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descripción">
                        <HeaderStyle HorizontalAlign="left" Width="25%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblDescripcion" runat="server" Text='<%# TruncateString(DataBinder.Eval(Container.DataItem, "descripcion").ToString())%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:UpdatePanel ID="udpEdit" runat="server" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
                    <table class="tablaInterna" cellpadding="1" cellspacing="5">
                        <tr>
                            <td colspan="2">
                                <h3>
                                    <asp:Literal ID="litEditar" runat="server" Text="Editar "></asp:Literal>
                                    <asp:Literal ID="litNuevo" runat="server" Text="Nueva "></asp:Literal>
                                    Categoría de Preguntas
                                </h3>
                            </td>
                        </tr>
                        <tr>
                            <td class="TD100">
                                <asp:ValidationSummary ID="validarEdit" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <table width="100%" cellpadding="1" cellspacing="5">
                        <tr>
                            <td valign="top" class="TDCriterios15">
                                <asp:Label runat="server" ID="lblNombreEdit" Text="Nombre categoría:"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox runat="server" ID="txtNombreEdit" Width="500px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TDCriterios15">
                                <asp:Label runat="server" ID="lblDescripcionEdit" Text="Descripción categoría:"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox runat="server" ID="txtDescripcionEdit" Width="500px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TDCriterios15">
                                <asp:Label runat="server" ID="lblAmbitoEdit" Text="Ambito:"></asp:Label>
                            </td>
                            <td class="TDCriterios10" colspan="3">
                                <asp:DropDownList ID="ddlAmbitoEdit" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAmbitoEdit_SelectedIndexChanged" />
                                &nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                        </table>
                        <p>
                            <asp:Label runat="server" ID="lblUtilizada"></asp:Label>
                        </p>
                    
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
            <asp:AsyncPostBackTrigger ControlID="gvwCategorias" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>