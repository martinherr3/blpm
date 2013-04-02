<%@ Page Title="" Language="C#" MasterPageFile="~/EDUARMaster.Master" AutoEventWireup="true" CodeBehind="ManageRegistroPlanificaciones.aspx.cs" Inherits="EDUAR_UI.ManageRegistroPlanificaciones" %>

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
                            Administrar Planificaciones de Clases
                        <br />
                    </td>
                    <td align="right">
                        <asp:UpdatePanel ID="udpBotonera" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:ImageButton ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" ToolTip="Buscar"
                                    ImageUrl="~/Images/botonBuscar.png" />
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
                                    Buscar Planificaciones Clases</h3>
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
            </asp:UpdatePanel>
        </ContentTemplate>
   </asp:UpdatePanel>
    <asp:UpdatePanel ID="udpGrilla" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView ID="gvwReporte" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListado"
                AutoGenerateColumns="false" AllowPaging="false" Width="100%" DataKeyNames="idPlanificacionAnual"
                OnRowCommand="gvwReporte_RowCommand" 
                OnPageIndexChanging="gvwReporte_PageIndexChanging"> 
                <Columns>
                    <asp:TemplateField HeaderText="Acciones">
                        <HeaderStyle HorizontalAlign="center" Width="5%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="editarPlanificacion" runat="server" CommandName="Editar" CommandArgument='<%# Bind("idPlanificacionAnual") %>'
                                ToolTip="Editar Planificacion de Clases" ImageUrl="~/Images/Grillas/action_edit.png"
                                Visible='true' />
                            <asp:ImageButton ID="aprobarPlanificacion" runat="server" CommandName="Aprobar" CommandArgument='<%# Bind("idPlanificacionAnual") %>'
                                ToolTip="Aprobar la Planificacion de clases"
                                ImageUrl="~/Images/Grillas/action_enable.png"
                                Visible='true' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fechade Creacion">
                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label ID="lblFechaGrilla" runat="server" Text='<%# Bind("fechaCreacion","{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Asignatura">
                        <HeaderStyle HorizontalAlign="left" Width="15%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblEventoGrilla" runat="server" Text='<%# Bind("curricula.asignatura.nombre") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Nivel">
                        <HeaderStyle HorizontalAlign="left" Width="15%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblEventoGrilla" runat="server" Text='<%# Bind("curricula.nivel.nombre") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Orientacion">
                        <HeaderStyle HorizontalAlign="left" Width="30%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblEventoGrilla" runat="server" Text='<%# Bind("curricula.orientacion.nombre") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Creador">
                        <HeaderStyle HorizontalAlign="left" Width="20%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblCreadorGrilla" runat="server" Text='<%#  String.Format("{0} {1}", Eval("creador.nombre"), Eval("creador.apellido")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Porcentaje adecuación">
                        <HeaderStyle HorizontalAlign="left" Width="5%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblPorcentaje" runat="server" Text='<%# Bind("porcentajeCobertura") %>'></asp:Label>
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
            </asp:UpdatePanel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvwReporte" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="mpeContenido" runat="server" PopupControlID="pnlContenidos"
        TargetControlID="HiddenField1" RepositionMode="RepositionOnWindowResizeAndScroll"
        BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="pnlContenidos">
    </ajaxToolkit:ModalPopupExtender>


</asp:Content>
