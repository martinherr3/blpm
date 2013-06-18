<%@ Page Title="Administrar Encuestas" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="ManageContenidoEncuestas.aspx.cs" Inherits="EDUAR_UI.ManageContenidoEncuestas"
    Theme="Tema" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/Calendario.ascx" TagName="Calendario" TagPrefix="cal" %>
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
                            <td valign="top" class="TD50px">
                                <asp:Label ID="lblAmbito" runat="server" Text="Ambito:"></asp:Label>
                            </td>
                            <td valign="top" class="TD50px">
                                <asp:DropDownList ID="ddlAmbito" runat="server" />
                            </td>
                            <td valign="top" class="TD110px" align="right">
                                <asp:Label ID="lblActivo" runat="server" Text="Activos:"></asp:Label>
                            </td>
                            <td valign="top" class="TD50px">
                                <asp:CheckBox ID="chkActivo" runat="server" Checked="false" />
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
            <asp:GridView ID="gvwEncuestas" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListado"
                AutoGenerateColumns="false" AllowPaging="false" Width="100%" DataKeyNames="idEncuesta"
                OnRowCommand="gvwEncuestas_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Acciones">
                        <HeaderStyle HorizontalAlign="center" Width="15%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="btnPreguntas" runat="server" CommandName="Preguntas" CommandArgument='<%# Bind("idEncuesta") %>'
                                ToolTip="Ver Preguntas" ImageUrl="~/Images/Grillas/action_quest.png" Visible='<%# CheckLanzada(Eval("usuario.username"), Eval("fechaLanzamiento"), true) %>' />
                            <asp:ImageButton ID="editarEncuesta" runat="server" CommandName="Editar" CommandArgument='<%# Bind("idEncuesta") %>'
                                ToolTip="Editar Encuesta" ImageUrl="~/Images/Grillas/action_edit.png" Visible='<%# CheckLanzada(Eval("usuario.username"), Eval("fechaLanzamiento"), true) %>' />
                            <asp:ImageButton ID="btnBloquear" runat="server" ToolTip="La encuesta sólo es editable para su propietario"
                                Enabled="false" ImageUrl="~/Images/Grillas/lock.png" Visible='<%#DataBinder.Eval(Container.DataItem, "usuario.username").ToString().ToString() == ObjSessionDataUI.ObjDTUsuario.Nombre ? false : true %>' />
                            <asp:ImageButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Bind("idEncuesta") %>'
                                ToolTip="Eliminar Encuesta" ImageUrl="~/Images/Grillas/action_delete.png" Visible='<%# CheckLanzada(Eval("usuario.username"), Eval("fechaLanzamiento"), true) %>' />
                            <asp:ImageButton ID="btnLanzar" runat="server" CommandName="Lanzar" CommandArgument='<%# Bind("idEncuesta") %>'
                                ToolTip="Enviar Encuesta a Usuarios" ImageUrl="~/Images/Grillas/action_Lanzar.png"
                                Visible='<%# CheckLanzada(Eval("usuario.username"), Eval("fechaLanzamiento"), true) %>' />
                            <asp:ImageButton ID="btnLanzada" runat="server" Enabled="false" ToolTip="Encuesta Enviada a Usuarios"
                                ImageUrl="~/Images/Grillas/action_run.png" Visible='<%# CheckLanzada(Eval("usuario.username"), Eval("fechaLanzamiento"), false) %>' />
                            <asp:ImageButton ID="btnResultados" runat="server" ToolTip="Resultados" CommandName="Resultados" CommandArgument='<%# Bind("idEncuesta") %>'
                                ImageUrl="~/Images/Grillas/action_results.png" Visible='<%# CheckLanzada(Eval("usuario.username"), Eval("fechaLanzamiento"), false) %>' />
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
                            <asp:Label ID="lblNombre" runat="server" Text='<%# TruncateString(DataBinder.Eval(Container.DataItem, "nombreEncuesta").ToString())%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Creación">
                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label ID="lblCreacion" runat="server" Text='<%# Bind("fechaCreacion","{0:d}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Lanzamiento">
                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label ID="lblLanzamiento" runat="server" Text='<%# Bind("fechaLanzamiento","{0:d}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Expiración">
                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label ID="lblExpiracion" runat="server" Text='<%# Bind("fechaVencimiento","{0:d}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Activo">
                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblActivo" runat="server" Text='<%# Boolean.Parse(Eval("activo").ToString()) ? "Sí" : "No"  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Respuestas">
                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblRespuestas" runat="server" Text='<%# Bind("nroRespuestas")  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:UpdatePanel ID="udpEdit" runat="server" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
                    <table class="tablaInterna" width="100%" cellpadding="1" cellspacing="5">
                        <tr>
                            <td valign="top" class="TDCriterios15">
                                <asp:Label runat="server" ID="lblNombreEdit" Text="Nombre encuesta:"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox runat="server" ID="txtNombreEdit" Width="500px"></asp:TextBox>
                            </td>
                            <td valign="top" class="TDCriterios15">
                                <asp:Label ID="lblActivoBusqueda" runat="server" Text="Activo:"></asp:Label>
                                <asp:CheckBox ID="chkActivoEdit" runat="server" Checked="false" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TDCriterios15">
                                <asp:Label runat="server" ID="lblAmbitoEdit" Text="Ambito:"></asp:Label>
                            </td>
                            <td class="TDCriterios10" colspan="3">
                                <asp:DropDownList ID="ddlAmbitoEdit" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAmbitoEdit_SelectedIndexChanged" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Label runat="server" ID="lblCurso" Text="Curso:"></asp:Label>
                                <asp:DropDownList ID="ddlCurso" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCurso_SelectedIndexChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TDCriterios15">
                                <asp:Label runat="server" ID="lblRoles" Text="Roles a Asociar:"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:UpdatePanel runat="server" ID="udpAmbitoRol" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:CheckBoxList ID="lstRoles" TabIndex="2" runat="server">
                                        </asp:CheckBoxList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlAmbitoEdit" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                    <asp:UpdatePanel runat="server" ID="udpAsignatura" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table width="100%" cellpadding="1" cellspacing="5">
                                <tr>
                                    <td class="TDCriterios15">
                                        <asp:Label runat="server" ID="lblAsignatura" Text="Asignatura:" Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlAsignatura" runat="server" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="ddlCurso" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlAmbitoEdit" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <table width="100%" cellpadding="1" cellspacing="5">
                        <tr>
                            <td valign="top" class="TDCriterios15">
                                <asp:Label runat="server" ID="lblFecha" Text="Fecha de Cierre:"></asp:Label>
                            </td>
                            <td colspan="3">
                                <cal:Calendario ID="calFechaCierre" runat="server" TipoCalendario="SoloFecha" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TDCriterios15">
                                <asp:Label runat="server" ID="lblObjetivoEdit" Text="Objetivo:"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox runat="server" ID="txtObjetivoEdit" Width="500px" TextMode="MultiLine"
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
            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvwEncuestas" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
