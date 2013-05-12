<%@ Page Title="Administrar Escalas de Ponderación" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="ManageEscalasPonderacion.aspx.cs" Inherits="EDUAR_UI.ManageEscalasPonderacion"
    Theme="Tema" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>

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
                        <asp:ImageButton ID="btnNuevo" OnClick="btnNuevo_Click" runat="server" ToolTip="Nuevo"
                            ImageUrl="~/Images/botonNuevo.png" />
                        <asp:ImageButton ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" ToolTip="Guardar"
                            ImageUrl="~/Images/botonGuardar.png" CausesValidation="true" ValidationGroup="validarEdit" />
                        <asp:ImageButton ID="btnVolver" OnClick="btnVolver_Click" runat="server" ToolTip="Volver"
                            ImageUrl="~/Images/botonVolver.png" />
                    </td>
                </tr>
            </table>
           <table class="tablaInterna" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                    <br />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="udpGrilla" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView ID="gvwEscalasPonderacion" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListado"
                AutoGenerateColumns="false" AllowPaging="false" Width="100%" DataKeyNames="idEscala"
                OnRowCommand="gvwEscalasPonderacion_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Acciones">
                        <HeaderStyle HorizontalAlign="center" Width="15%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>

                            <asp:ImageButton ID="btnValoresEscalas" runat="server" CommandName="VerValoresEscala" CommandArgument='<%# Bind("idEscala") %>'
                                ToolTip="Ver Valores de Escala" ImageUrl="~/Images/Grillas/action_quest.png"/>
                            <asp:ImageButton ID="btnEditarEscala" runat="server" CommandName="Editar" CommandArgument='<%# Bind("idEscala") %>'
                                ToolTip="Editar Escala" ImageUrl="~/Images/Grillas/action_edit.png" />
                            <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Bind("idEscala") %>'
                                ToolTip="Eliminar Escala" ImageUrl="~/Images/Grillas/action_delete.png" Visible='<%# Bind("eliminable") %>'/>

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
                            <asp:Label ID="lblDescripcion" runat="server" Text='<%# TruncateString(DataBinder.Eval(Container.DataItem, "descripcion").ToString())%>'
                                ToolTip='<%# Bind("descripcion") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:UpdatePanel ID="udpEdit" runat="server" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
<%--                    <table class="tablaInternaSinBorde" cellpadding="1" cellspacing="5">
                        <tr>
                            <td colspan="2">
                                <h3>
                                    <asp:Literal ID="litEditar" runat="server" Text="Editar "></asp:Literal>
                                    <asp:Literal ID="litNuevo" runat="server" Text="Nueva "></asp:Literal>
                                    Escala de Ponderación
                                </h3>
                            </td>
                        </tr>
                        <tr>
                            <td class="TD100">
                                <asp:ValidationSummary ID="validarEdit" runat="server" />
                            </td>
                        </tr>
                    </table>--%>
                    <table width="100%" cellpadding="1" cellspacing="5">
                        <tr>
                            <td valign="top" class="TDCriterios15">
                                <asp:Label runat="server" ID="lblNombreEdit" Text="Nombre escala:"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox runat="server" ID="txtNombreEdit" Width="500px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TDCriterios15">
                                <asp:Label runat="server" ID="lblDecsripcionEdit" Text="Descripción escala:"></asp:Label>
                            </td>
                            <td colspan="2">
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
            <asp:AsyncPostBackTrigger ControlID="gvwEscalasPonderacion" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
