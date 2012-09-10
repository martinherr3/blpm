<%@ Page Title="Administrar Preguntas" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="ContenidoEncuestas.aspx.cs" Inherits="EDUAR_UI.ContenidoEncuestas"
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
                        <h2>Contenido <asp:Label Text="" runat="server" ID="lblTitulo" /></h2>
                        <br/>
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
                                <h3>Buscar Preguntas</h3>
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
                            <td valign="top" class="TD110px">
                                <asp:Label ID="lblCategoria" runat="server" Text="Categoría:"></asp:Label>
                            </td>
                            <td valign="top" class="TD50px">
                                <asp:DropDownList ID="ddlCategoria" runat="server"/>
                            </td>
                            <td valign="top" class="TD110px">
                                <asp:Label ID="lblEscalaPonderacion" runat="server" Text="Escala:"></asp:Label>
                            </td>
                            <td valign="top" class="TD50px">
                                <asp:DropDownList ID="ddlEscalaPonderacion" runat="server"/>
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
        </Triggers>
    </asp:UpdatePanel>

<asp:UpdatePanel ID="udpGrilla" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView ID="gvwPreguntas" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListado"
                AutoGenerateColumns="false" AllowPaging="false" Width="100%" DataKeyNames="idPregunta"
                OnRowCommand="gvwPreguntas_RowCommand">
                <Columns>
 
                   <asp:TemplateField HeaderText="Acciones">
                        <HeaderStyle HorizontalAlign="center" Width="5%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="editarPregunta" runat="server" CommandName="Editar" CommandArgument='<%# Bind("idPregunta") %>'
                                ToolTip="Editar Pregunta" ImageUrl="~/Images/Grillas/action_edit.png" Visible='true' />
                            <asp:ImageButton ImageUrl="~/Images/Grillas/action_delete.png" runat="server" ID="btnEliminar"
                                AlternateText="Eliminar" ToolTip="Eliminar Pregunta" CommandName="Eliminar" CommandArgument='<%# Bind("idPregunta") %>'
                                OnClientClick="return confirm('¿Desea eliminar el tema seleccionado?')" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Categoría">
                        <HeaderStyle HorizontalAlign="left" Width="15%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblCategoria" runat="server" Text='<%# TruncateString(DataBinder.Eval(Container.DataItem, "categoria.nombre").ToString())%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                
 
                    <asp:TemplateField HeaderText="Texto">
                        <HeaderStyle HorizontalAlign="left" Width="25%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblTextoPregunta" runat="server" Text='<%# TruncateString(DataBinder.Eval(Container.DataItem, "textoPregunta").ToString())%>'
                                ToolTip='<%# Bind("textoPregunta") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Objetivo">
                        <HeaderStyle HorizontalAlign="left" Width="25%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblObjetivo" runat="server" Text='<%# TruncateString(DataBinder.Eval(Container.DataItem, "objetivoPregunta").ToString())%>'
                                ToolTip='<%# Bind("objetivoPregunta") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Ponderación">
                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblPonderacion" runat="server" Text='<%# TruncateString(DataBinder.Eval(Container.DataItem, "ponderacion").ToString())%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Escala">
                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblEscala" runat="server" Text='<%# TruncateString(DataBinder.Eval(Container.DataItem, "escala.nombre").ToString())%>'
                                ToolTip='<%# Bind("escala.descripcion") %>'></asp:Label>
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
                                    Encuesta
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
<%--                        <tr>
                            <td valign="top" class="TDCriterios25">
                                <asp:Label ID="lblActivoBusqueda" runat="server" Text="Activo:"></asp:Label>
                            </td>
                            <td valign="top" class="TDCriterios25">
                                <asp:CheckBox ID="chkActivoEdit" runat="server" Checked="false" />
                            </td>
                            <td valign="top" class="TDCriterios25">
                            </td>
                            <td valign="top" class="TDCriterios25">
                            </td>
                        </tr>
                        <tr>--%>
                            <td valign="top" class="TD25">
                                <asp:Label runat="server" ID="lblCategoriaEdit" Text="Categoría:"></asp:Label>
                            </td>
                            <td class="TD75" colspan="3">
                               <asp:DropDownList ID="ddlCategoriaEdit" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TD25">
                                <asp:Label runat="server" ID="lblEscalaPonderacionEdit" Text="Escala Ponderación:"></asp:Label>
                            </td>
                            <td class="TD75" colspan="3">
                                <asp:DropDownList ID="ddlEscalaPonderacionEdit" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TD25">
                                <asp:Label runat="server" ID="lblTextoPreguntaEdit" Text="Pregunta:"></asp:Label>
                            </td>
                            <td class="TD75" colspan="3">
                                <asp:TextBox runat="server" ID="txtTextoPreguntaEdit" Width="500px" TextMode="MultiLine" Rows="3"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TD25">
                                <asp:Label runat="server" ID="lblObjetivoPreguntaEdit" Text="Objetivo:"></asp:Label>
                            </td>
                            <td class="TD75" colspan="3">
                                <asp:TextBox runat="server" ID="txtObjetivoPreguntaEdit" Width="500px" TextMode="MultiLine" Rows="5"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TD25">
                                <asp:Label runat="server" ID="lblPesoPreguntaEdit" Text="Peso:"></asp:Label>
                            </td>
                            <td class="TD75" colspan="3">
                                <asp:TextBox runat="server" ID="txtPesoPreguntaEdit" Width="500px"></asp:TextBox>
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
            <asp:AsyncPostBackTrigger ControlID="gvwPreguntas" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>