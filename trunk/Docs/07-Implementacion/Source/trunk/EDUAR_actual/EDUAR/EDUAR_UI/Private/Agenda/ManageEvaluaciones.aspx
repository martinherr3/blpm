<%@ Page Title="Administrar Evaluaciones" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="ManageEvaluaciones.aspx.cs" Inherits="EDUAR_UI.ManageEvaluaciones"
    Theme="Tema" StylesheetTheme="Tema" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Src="~/UserControls/Calendario.ascx" TagName="Calendario" TagPrefix="cal" %>
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
                            Evaluaciones
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
                                    Buscar Evaluaciones</h3>
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
                            <td valign="top" class="TD100px">
                                <asp:Label ID="lblAsignatura" runat="server" Text="Asignatura:"></asp:Label>
                            </td>
                            <td valign="top" style="width: 500px">
                                <asp:DropDownList ID="ddlAsignatura" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td valign="top" class="TD50px">
                                <asp:Label ID="Label12" runat="server" Text="Activos:"></asp:Label>
                            </td>
                            <td valign="top" class="TD50px">
                                <asp:CheckBox ID="chkActivo" runat="server" Checked="true" />
                            </td>
                        </tr>
                    </table>
                    <table width="600px" cellpadding="1" cellspacing="5">
                        <tr>
                            <td valign="top" class="TDCriterios100">
                                <cal:Calendario ID="calfechas" TipoCalendario="DesdeHasta" runat="server" EtiquetaDesde="Fecha Desde:"
                                    EtiquetaHasta="Fecha Hasta:" TipoAlineacion="Izquierda" />
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
            <asp:GridView ID="gvwReporte" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListado"
                AutoGenerateColumns="false" AllowPaging="false" Width="100%" DataKeyNames="idEventoAgenda"
                OnRowCommand="gvwReporte_RowCommand" OnPageIndexChanging="gvwReporte_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Acciones">
                        <HeaderStyle HorizontalAlign="center" Width="5%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="editarEvento" runat="server" CommandName="Editar" CommandArgument='<%# Bind("idEventoAgenda") %>'
                                ToolTip="Editar Evaluación" ImageUrl="~/Images/Grillas/action_edit.png" Visible='<%#DataBinder.Eval(Container.DataItem, "usuario.username").ToString().ToString() == ObjSessionDataUI.ObjDTUsuario.Nombre ? true : false %>' />
                            <asp:ImageButton ID="ImageButton2" runat="server" ToolTip="La Evaluación sólo es editable para su organizador"
                                ImageUrl="~/Images/Grillas/lock.png" Visible='<%#DataBinder.Eval(Container.DataItem, "usuario.username").ToString().ToString() == ObjSessionDataUI.ObjDTUsuario.Nombre ? false : true %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fecha">
                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label ID="lblFechaGrilla" runat="server" Text='<%# Bind("fechaEvento","{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Asignatura">
                        <HeaderStyle HorizontalAlign="left" Width="30%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblEventoGrilla" runat="server" Text='<%# Bind("asignatura.nombre") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Organizador">
                        <HeaderStyle HorizontalAlign="left" Width="15%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblOrganizador" runat="server" Text='<%# String.Format("{0} {1}", Eval("usuario.nombre"), Eval("usuario.apellido")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Activo">
                        <HeaderStyle HorizontalAlign="center" Width="10%" />
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
                                    Evaluación</h3>
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
                                <asp:Label ID="lblAsignaturaEdit" runat="server" Text="Asignatura:"></asp:Label>
                            </td>
                            <td valign="top" class="TDCriterios75" colspan="3">
                                <asp:DropDownList ID="ddlAsignaturaEdit" runat="server" OnSelectedIndexChanged="ddlAsignaturaEdit_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="width: 17%; text-align: left">
                                <asp:Label ID="Label3" runat="server" Text="Fecha:"></asp:Label>
                            </td>
                            <td valign="top" class="TDCriterios40">
                                <%--<cal:Calendario ID="calFechaEvento" runat="server" TipoCalendario="SoloFecha" TipoAlineacion="Izquierda" />--%>
                                <asp:UpdatePanel ID="udpMeses" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                            <tr>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddlMeses" OnSelectedIndexChanged="ddlMeses_SelectedIndexChanged"
                                                        AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList runat="server" ID="ddlDia" Enabled="false">
                                                    </asp:DropDownList>
                                                </td>
                                                <%--<td>
                                                </td>--%>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlAsignaturaEdit" EventName="SelectedIndexChanged" />
                                        <asp:PostBackTrigger ControlID="ddlMeses" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td valign="top" class="TDCriterios10">
                            </td>
                            <td valign="top" class="TDCriterios25">
                            </td>
                        </tr>
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
                                <asp:Label runat="server" ID="Label9" Text="Descripción:"></asp:Label>
                            </td>
                            <td class="TD75" colspan="3">
                                <asp:TextBox runat="server" ID="txtDescripcionEdit" Width="500px" TextMode="MultiLine"
                                    Rows="5"></asp:TextBox>
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
            <asp:AsyncPostBackTrigger ControlID="gvwReporte" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
