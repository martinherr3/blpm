<%@ Page Title="Administrar Contenido Encuestas" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="ManageContenidoEncuestas.aspx.cs" Inherits="EDUAR_UI.ManageContenidoEncuestas"
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
                                <h3>Buscar Encuestas</h3>
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
                                <asp:Label ID="lblAmbito" runat="server" Text="Ambito:"></asp:Label>
                            </td>
                            <td valign="top" class="TD50px">
                                <asp:DropDownList ID="ddlAmbito" runat="server"/>
                            </td>
                            <td valign="top" class="TD110px">
                                <asp:Label ID="lblActivo" runat="server" Text="Activos:"></asp:Label>
                            </td>
                            <td valign="top" class="TD50px">
                                <asp:CheckBox ID="chkActivo" runat="server" Checked="false" />
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
            <asp:GridView ID="gvwEncuestas" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListado"
                AutoGenerateColumns="false" AllowPaging="false" Width="100%" DataKeyNames="idEncuesta"
                OnRowCommand="gvwEncuestas_RowCommand">
                <Columns>
 <%--
                   <asp:TemplateField HeaderText="Acciones">
                        <HeaderStyle HorizontalAlign="center" Width="5%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="editarEncuesta" runat="server" CommandName="Editar" CommandArgument='<%# Bind("idEncuesta") %>'
                                ToolTip="Editar Encuesta" ImageUrl="~/Images/Grillas/action_edit.png" Visible='<%#DataBinder.Eval(Container.DataItem, "usuario.username").ToString() == ObjSessionDataUI.ObjDTUsuario.Nombre ? true : false %>' />
                            <asp:ImageButton ID="ImageButton2" runat="server" ToolTip="La encuesta sólo es editable para su propietario"
                                ImageUrl="~/Images/Grillas/lock.png" Visible='<%#DataBinder.Eval(Container.DataItem, "usuario.username").ToString().ToString() == ObjSessionDataUI.ObjDTUsuario.Nombre ? false : true %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
--%>
                    <asp:TemplateField HeaderText="Ambito">
                        <HeaderStyle HorizontalAlign="left" Width="15%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblAmbito" runat="server" Text='<%# TruncateString(DataBinder.Eval(Container.DataItem, "ambito.nombre").ToString())%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                
 
                    <asp:TemplateField HeaderText="Nombre">
                        <HeaderStyle HorizontalAlign="left" Width="25%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblNombre" runat="server" Text='<%# TruncateString(DataBinder.Eval(Container.DataItem, "nombreEncuesta").ToString())%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Objetivo">
                        <HeaderStyle HorizontalAlign="left" Width="25%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblObjetivo" runat="server" Text='<%# TruncateString(DataBinder.Eval(Container.DataItem, "objetivo").ToString())%>'
                                ToolTip='<%# Bind("objetivo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Activo">
                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblActivo" runat="server" Text='<%# Boolean.Parse(Eval("activo").ToString()) ? "Sí" : "No"  %>'></asp:Label>
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
                        <tr>
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
                        <tr>
                            <td valign="top" class="TD25">
                                <asp:Label runat="server" ID="lblAmbitoEdit" Text="Ambito:"></asp:Label>
                            </td>
                            <td class="TD75" colspan="3">
                               <asp:DropDownList ID="ddlAmbitoEdit" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TD25">
                                <asp:Label runat="server" ID="lblNombreEdit" Text="Nombre encuesta:"></asp:Label>
                            </td>
                            <td class="TD75" colspan="3">
                                <asp:TextBox runat="server" ID="txtNombreEdit" Width="500px"></asp:TextBox>
                            </td>
                        </tr>
<%--                        <tr>
                            <td valign="top" class="TD25">
                                <asp:Label runat="server" ID="lblPreguntaBusqueda" Text="Pregunta:"></asp:Label>
                            </td>
                            <td class="TD75" colspan="3">
                                <asp:TextBox runat="server" ID="txtPreguntaEdit" Width="500px"></asp:TextBox>
                            </td>
                        </tr>--%>
                        <tr>
                            <td valign="top" class="TD25">
                                <asp:Label runat="server" ID="lblObjetivoEdit" Text="Objetivo:"></asp:Label>
                            </td>
                            <td class="TD75" colspan="3">
                                <asp:TextBox runat="server" ID="txtObjetivoEdit" Width="500px" TextMode="MultiLine" Rows="5"></asp:TextBox>
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
            <asp:AsyncPostBackTrigger ControlID="gvwEncuestas" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
