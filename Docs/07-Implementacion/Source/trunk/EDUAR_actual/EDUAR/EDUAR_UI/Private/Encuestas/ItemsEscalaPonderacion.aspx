<%@ Page Title="Administrar Items de Escala de Ponderación" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="ItemsEscalaPonderacion.aspx.cs" Inherits="EDUAR_UI.ItemsEscalaPonderacion"
    Theme="Tema" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server" />
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="tablaInterna" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <h2>
                            Contenido
                            <asp:Label Text="" runat="server" ID="lblTitulo" /></h2>
                        <br />
                    </td>
                    <td align="right">
                        <asp:ImageButton ID="btnNuevo" OnClick="btnNuevo_Click" runat="server" ToolTip="Nuevo"
                            ImageUrl="~/Images/botonNuevo.png" />
                        <asp:ImageButton ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" ToolTip="Guardar"
                            ImageUrl="~/Images/botonGuardar.png" CausesValidation="true" ValidationGroup="validarEdit" />
                        <asp:ImageButton ID="btnVolver" OnClick="btnVolver_Click" runat="server" ToolTip="Volver"
                            ImageUrl="~/Images/botonVolver.png" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <table class="tablaInterna" cellpadding="0" cellspacing="0">
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="udpGrilla" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView ID="gvwItemsEscala" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListado"
                AutoGenerateColumns="false" AllowPaging="false" Width="100%" DataKeyNames="idValorEscala"
                OnRowCommand="gvwItemsEscala_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Acciones">
                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="editarItemEscala" runat="server" CommandName="Editar" CommandArgument='<%# Bind("idValorEscala") %>'
                                ToolTip="Editar valor escala" ImageUrl="~/Images/Grillas/action_edit.png" Visible='true' />
                            <asp:ImageButton ImageUrl="~/Images/Grillas/action_delete.png" runat="server" ID="btnEliminar"
                                AlternateText="Eliminar" ToolTip="Eliminar Item" CommandName="Eliminar" CommandArgument='<%# Bind("idValorEscala") %>' />
                            <!--OnClientClick="javascript:return jConfirm('¿Desea <b>eliminar</b> la pregunta seleccionada?', 'Confirmación')"-->
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Posición">
                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <table class="tablaInternaSinBorde" border="0" cellpadding="0" cellspacing="0" style="border: none;
                                border-collapse: collapse">
                                <tr>
                                    <td style="width: 75%; text-align: center; border: none">
                                        <asp:ImageButton ID="subirOrden" runat="server" CommandName="Subir" CommandArgument='<%# Bind("idValorEscala") %>'
                                            ToolTip="Subir un nivel" ImageUrl="~/Images/Grillas/view-sort-descending-2.png"
                                            Visible='<%# Convert.ToInt32(Eval("orden")) > 1 ? true : false %>' />
                                        <asp:ImageButton ID="bajarOrden" runat="server" CommandName="Bajar" CommandArgument='<%# Bind("idValorEscala") %>'
                                            ToolTip="Bajar un nivel" ImageUrl="~/Images/Grillas/view-sort-ascending-2.png"
                                            Visible='<%# Convert.ToInt32(Eval("orden")) < Convert.ToInt32(Eval("cantidadValores")) ? true : false %>' />
                                    </td>
                                    <td style="width: 25%; text-align: center; border: none">
                                        <asp:Label ID="lblOrden" runat="server" Text='<%# Bind("orden")%>' Font-Bold="true"
                                            ToolTip="Posicion"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nombre">
                        <HeaderStyle HorizontalAlign="left" Width="25%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblNombre" runat="server" Text='<%# TruncateString(DataBinder.Eval(Container.DataItem, "nombre").ToString())%>'
                                ToolTip='<%# Bind("nombre") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descripción">
                        <HeaderStyle HorizontalAlign="left" Width="50%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblDescripcion" runat="server" Text='<%# TruncateString(DataBinder.Eval(Container.DataItem, "descripcion").ToString())%>'
                                ToolTip='<%# Bind("descripcion") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:UpdatePanel ID="udpEdit" runat="server" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
                    <table class="tablaInternaSinBorde" cellpadding="1" cellspacing="5">
                        <tr>
                            <td colspan="2">
                                <h3>
                                    <asp:Literal ID="litEditar" runat="server" Text="Editar "></asp:Literal>
                                    <asp:Literal ID="litNuevo" runat="server" Text="Nueva "></asp:Literal>
                                    Valores de Escala de Ponderación
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
                            <td valign="top" class="TD25">
                                <asp:Label runat="server" ID="lblTNombreEdit" Text="Nombre:"></asp:Label>
                            </td>
                            <td class="TD75" colspan="3">
                                <asp:TextBox runat="server" ID="txtNombreEdit" Width="500px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TD25">
                                <asp:Label runat="server" ID="lblDescripcionEdit" Text="Descripción:"></asp:Label>
                            </td>
                            <td class="TD75" colspan="3">
                                <asp:TextBox runat="server" ID="txtDescripcionEdit" Width="500px" TextMode="MultiLine"
                                    Rows="5" CssClass="txtMultilinea"></asp:TextBox>
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
            <asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvwItemsEscala" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
