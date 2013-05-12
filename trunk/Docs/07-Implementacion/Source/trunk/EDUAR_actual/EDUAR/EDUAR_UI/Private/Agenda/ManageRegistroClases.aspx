<%@ Page Title="Registro De Clases" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="ManageRegistroClases.aspx.cs" Inherits="EDUAR_UI.ManageRegistroClases"
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
                            Registro De Clases
                            <asp:Label Text="" runat="server" ID="lblTitulo" /></h2>
                        <br />
                    </td>
                    <td align="right">
                        <asp:UpdatePanel ID="udpBotonera" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:ImageButton ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" ToolTip="Buscar"
                                    ImageUrl="~/Images/botonBuscar.png" />
                                <asp:ImageButton ID="btnContenidosPopUp" OnClick="btnContenidosPopUp_Click" runat="server"
                                    ToolTip="Asociar Contenidos" ImageUrl="~/Images/botonContenidos.png" />
                                <asp:ImageButton ID="btnNuevo" OnClick="btnNuevo_Click" runat="server" ToolTip="Nuevo"
                                    ImageUrl="~/Images/botonNuevo.png" />
                                <asp:ImageButton ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" ToolTip="Guardar"
                                    ImageUrl="~/Images/botonGuardar.png" CausesValidation="true" ValidationGroup="validarEdit" />
                                <asp:ImageButton ID="btnVolver" OnClick="btnVolver_Click" runat="server" ToolTip="Volver"
                                    ImageUrl="~/Images/botonVolver.png" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlAsignaturaEdit" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="udpFiltrosBusqueda" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table class="tablaInterna" cellpadding="1" cellspacing="5">
                        <tr>
                            <td>
                                <h3>
                                    Buscar Registro De Clases</h3>
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
                                ToolTip="Editar Registro De Clases" ImageUrl="~/Images/Grillas/action_edit.png"
                                Visible='<%#DataBinder.Eval(Container.DataItem, "usuario.username").ToString().ToString() == ObjSessionDataUI.ObjDTUsuario.Nombre ? true : false %>' />
                            <asp:ImageButton ID="ImageButton2" runat="server" ToolTip="El Registro de Clases sólo es editable para su organizador"
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
                    <asp:TemplateField HeaderText="Tipo De Registro">
                        <HeaderStyle HorizontalAlign="left" Width="30%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblTipoRegistroGrilla" runat="server" Text='<%# Bind("tipoRegistro.nombre") %>'></asp:Label>
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
                                    <asp:Literal ID="litNuevo" runat="server" Text="Nuevo "></asp:Literal>
                                    Registro De Clases</h3>
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
                            <td valign="top" class="TDCriterios20">
                                <asp:Label ID="lblAsignaturaEdit" runat="server" Text="Asignatura:"></asp:Label>
                            </td>
                            <td valign="top" class="TDCriterios60">
                                <asp:DropDownList ID="ddlAsignaturaEdit" runat="server" OnSelectedIndexChanged="ddlAsignaturaEdit_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td colspan="2" rowspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TDCriterios20">
                                <asp:Label ID="Label3" runat="server" Text="Fecha:"></asp:Label>
                            </td>
                            <td valign="top" class="TDCriterios40">
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
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlAsignaturaEdit" EventName="SelectedIndexChanged" />
                                        <asp:PostBackTrigger ControlID="ddlMeses" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td valign="top" class="TDCriterios20">
                            </td>
                            <td valign="top" class="TDCriterios20">
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TDCriterios20">
                                <asp:Label ID="Label1" runat="server" Text="Tipo De Clase:"></asp:Label>
                            </td>
                            <td valign="top" class="TDCriterios20">
                                <asp:DropDownList runat="server" ID="ddlTipoRegistroClase">
                                </asp:DropDownList>
                            </td>
                            <td valign="top" class="TDCriterios20">
                            </td>
                            <td valign="top" class="TDCriterios20">
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TDCriterios10">
                                <asp:Label ID="lblActivoBusqueda" runat="server" Text="Activo:"></asp:Label>
                            </td>
                            <td valign="top" class="TDCriterios50">
                                <asp:CheckBox ID="chkActivoEdit" runat="server" Checked="false" />
                            </td>
                            <td valign="top" class="TDCriterios20">
                            </td>
                            <td valign="top" class="TDCriterios20">
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TDCriterios20">
                                <asp:Label runat="server" ID="Label9" Text="Descripción:"></asp:Label>
                            </td>
                            <td class="TDCriterios80" colspan="3">
                                <asp:TextBox runat="server" ID="txtDescripcionEdit" Width="500px" TextMode="MultiLine"
                                    Rows="5" CssClass="txtMultilinea" ></asp:TextBox>
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
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="mpeContenido" runat="server" PopupControlID="pnlContenidos"
        TargetControlID="HiddenField1" RepositionMode="RepositionOnWindowResizeAndScroll"
        BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="pnlContenidos">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlContenidos" runat="server" Width="600px" Height="510px" Style="display: none;
        text-align: left" CssClass="CajaDialogo">
        <asp:UpdatePanel ID="udpContenidosAsociados" runat="server">
            <ContentTemplate>
                <table class="tablaInterna" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <h2>
                                <asp:Label ID="Label2" Text="Asociar Contenidos" runat="server" /></h2>
                            <br />
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="btnGuardarPopUp" runat="server" ToolTip="Guardar" ImageUrl="~/Images/PopUp/botonGuardar.png"
                                OnClick="btnGuardarPopUp_Click" />
                            <asp:ImageButton ID="btnVolverPopUp" runat="server" ToolTip="Volver - Descartar Cambios"
                                ImageUrl="~/Images/PopUp/botonVolver.png" OnClick="btnVolverPopUp_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:GridView ID="gvwContenidos" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListadoSize10"
                                AutoGenerateColumns="false" Width="500px" DataKeyNames="idTemaContenido" OnPageIndexChanging="gvwContenidos_PageIndexChanging"
                                OnPageIndexChanged="gvwContenidos_PageIndexChanged">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelection" Text='<%# Eval("idTemaContenido")%>' runat="server"
                                                CssClass="HiddenText" Width="30px" ClientIDMode="Static" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tema">
                                        <HeaderStyle HorizontalAlign="left" Width="75%" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTituloContenidos" runat="server" Text='<%#Bind("Contenido") + " - " + Bind("Titulo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Obligatorio">
                                        <HeaderStyle HorizontalAlign="center" Width="20%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblObligatorio" runat="server" Text='<%# Boolean.Parse(Eval("Obligatorio").ToString()) ? "Sí" : "No"  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:HiddenField ID="HiddenField2" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="mpeContenidoAtrasado" runat="server" PopupControlID="pnlContenidosAtrasados"
        TargetControlID="HiddenField2" RepositionMode="RepositionOnWindowResizeAndScroll"
        BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="pnlContenidosAtrasados">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlContenidosAtrasados" runat="server" Width="600px" Height="510px"
        Style="display: none; text-align: left" CssClass="CajaDialogo">
        <asp:UpdatePanel ID="udpContenidosAsociadosAtrasados" runat="server">
            <ContentTemplate>
                <table class="tablaInterna" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <h2>
                                <asp:Label ID="LabelContenidosAtrasados" Text="Recordatorio de Contenidos Planificados Atrasados"
                                    runat="server" /></h2>
                            <br />
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="btnVolverContenidoAtrasadoPopUp" runat="server" ToolTip="Volver - Descartar Cambios"
                                ImageUrl="~/Images/PopUp/botonVolver.png" OnClick="btnVolverContenidosAtrasadosPopUp_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:GridView ID="gvwContenidosAtrasados" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListadoSize10"
                                AutoGenerateColumns="false" Width="500px" DataKeyNames="idTemaContenido" OnPageIndexChanging="gvwContenidosAtrasados_PageIndexChanging"
                                OnPageIndexChanged="gvwContenidosAtrasados_PageIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderText="Fecha Planificada">
                                        <HeaderStyle HorizontalAlign="center" Width="25%" />
                                        <ItemStyle HorizontalAlign="center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblFechaContenido" runat="server" Text='<%# Bind("fechaInicio","{0:d}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tema">
                                        <HeaderStyle HorizontalAlign="left" Width="65%" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTituloContenidos" runat="server" Text='<%# Bind("Contenido") + " - " + Bind("Titulo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Obligatorio">
                                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblObligatorio" runat="server" Text='<%# Boolean.Parse(Eval("Obligatorio").ToString()) ? "Sí" : "No"  %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
