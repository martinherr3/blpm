<%@ Page Title="Administrar Citaciones" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="ManageCitaciones.aspx.cs" Inherits="EDUAR_UI.ManageCitaciones"
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
                            Citaciones
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
                                    Buscar Citaciones</h3>
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
                            <td valign="top" class="TD140px">
                                <asp:Label ID="Label2" runat="server" Text="Motivo de Citación:"></asp:Label>
                            </td>
                            <td valign="top" class="TD250px">
                                <asp:DropDownList ID="ddlMotivoCitacion" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td valign="top" class="TD100px">
                                <asp:Label ID="Label12" runat="server" Text="Activos:"></asp:Label>
                            </td>
                            <td valign="top">
                                <asp:CheckBox ID="chkActivo" runat="server" Checked="true" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TD140px">
                                <asp:Label ID="lblCicloLectivo" runat="server" Text="Ciclo Lectivo:" CssClass="lblCriterios"></asp:Label>
                            </td>
                            <td valign="top" class="TD250px">
                                <asp:DropDownList ID="ddlCicloLectivo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCicloLectivo_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td valign="top" class="TD100px">
                                <asp:Label ID="lblCurso" runat="server" Text="Curso:" CssClass="lblCriterios"></asp:Label>
                            </td>
                            <td valign="top">
                                <asp:DropDownList ID="ddlCurso" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCurso_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TD140px">
                                <asp:Label ID="Label1" runat="server" Text="Tutores:" CssClass="lblCriterios"></asp:Label>
                            </td>
                            <td valign="top">
                                <asp:DropDownList ID="ddlTutores" runat="server" CssClass="EstiloTxtLargo250" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlTutores_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td valign="top">
                            </td>
                        </tr>
                    </table>
                    <table width="600px" cellpadding="1" cellspacing="5">
                        <tr>
                            <td valign="top" colspan="4" class="TDCriterios100">
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
                AutoGenerateColumns="false" AllowPaging="false" Width="100%" DataKeyNames="idCitacion"
                OnRowCommand="gvwReporte_RowCommand" OnPageIndexChanging="gvwReporte_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Acciones">
                        <HeaderStyle HorizontalAlign="center" Width="5%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="editarEvento" runat="server" CommandName="Editar" CommandArgument='<%# Bind("idCitacion") %>'
                                ToolTip="Editar Citación" ImageUrl="~/Images/Grillas/action_edit.png" Visible='<%#DataBinder.Eval(Container.DataItem, "organizador.username").ToString().ToString() ==  ObjSessionDataUI.ObjDTUsuario.Nombre.ToLower() ? true : false %>' />
                            <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="Consultar Citación" ImageUrl="~/Images/Grillas/lock.png" Enabled="false"
                                Visible='<%#DataBinder.Eval(Container.DataItem, "organizador.username").ToString().ToString() == ObjSessionDataUI.ObjDTUsuario.Nombre.ToLower() ? false : true %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fecha">
                        <HeaderStyle HorizontalAlign="center" Width="15%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label ID="lblFechaGrilla" runat="server" Text='<%# String.Format("{0} {1} hs.", Eval("fecha","{0:d}"), Eval("hora","{0:HH:mm}")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Organizador">
                        <HeaderStyle HorizontalAlign="left" Width="20%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblOrganizador" runat="server" Text='<%# String.Format("{0} {1}", Eval("organizador.nombre"), Eval("organizador.apellido")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tutor">
                        <HeaderStyle HorizontalAlign="left" Width="20%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblTutorGrilla" runat="server" Text='<%# String.Format("{0} {1}", Eval("tutor.nombre"), Eval("tutor.apellido")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Motivo">
                        <HeaderStyle HorizontalAlign="left" Width="20%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblMotivoGrilla" runat="server" Text='<%# Bind("motivoCitacion.nombre") %>'></asp:Label>
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
                                    Citación</h3>
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
                            <td valign="top" style="width: 17%; text-align: left">
                                <asp:Label ID="Label3" runat="server" Text="Fecha:"></asp:Label>
                            </td>
                            <td valign="top" class="TDCriterios40">
                                <cal:Calendario ID="calFechaEvento" runat="server" TipoCalendario="SoloFecha" TipoAlineacion="Izquierda" />
                            </td>
                            <td valign="top" class="TDCriterios10" colspan="2">
                                <asp:Label runat="server" ID="lblHora" Text="Hora:"></asp:Label>
                            </td>
                            <td valign="top" class="TDCriterios40">
                                <asp:TextBox runat="server" ID="txtHoraEdit" MaxLength="5" CssClass="EstiloTxtCorto80"></asp:TextBox>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptAMPM="false"
                                    MaskType="Time" Mask="99:99" ErrorTooltipEnabled="true" InputDirection="LeftToRight"
                                    CultureName="es-ES" TargetControlID="txtHoraEdit" MessageValidatorTip="true">
                                </cc1:MaskedEditExtender>
                                <cc1:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ToolTip="Hora Inválida"
                                    ErrorMessage="*" ControlExtender="MaskedEditExtender2" ControlToValidate="txtHoraEdit"
                                    InvalidValueMessage="Hora Inválida" TooltipMessage="00:00 - 23:59" ValidationGroup="validarEdit">
                                </cc1:MaskedEditValidator>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TDCriterios25">
                                <asp:Label ID="Label5" runat="server" Text="Curso:" CssClass="lblCriterios"></asp:Label>
                            </td>
                            <td valign="top" class="TDCriterios25">
                                <asp:DropDownList ID="ddlCursoEdit" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCursoEdit_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td valign="top" class="TDCriterios25" colspan="2">
                            </td>
                            <td valign="top" class="TDCriterios25">
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TDCriterios25">
                                <asp:Label ID="Label7" runat="server" Text="Motivo de Citación:"></asp:Label>
                            </td>
                            <td valign="top" class="TDCriterios25">
                                <asp:DropDownList ID="ddlMotivoEdit" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td valign="top" class="TDCriterios25" colspan="2">
                                <asp:Label ID="lblActivoEdit" runat="server" Text="Activo:" ToolTip="Setear en activo cuando la citacion este en curso, porque aparecera como Citacion al tutor"></asp:Label>
                            </td>
                            <td valign="top" class="TDCriterios25">
                                <asp:CheckBox ID="chkActivoEdit" runat="server" Checked="true" ToolTip="Setear en activo cuando la citacion este en curso, porque aparecera como Citacion al tutor" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TDCriterios25">
                                <asp:Label ID="Label6" runat="server" Text="Tutor:" CssClass="lblCriterios"></asp:Label>
                            </td>
                            <td valign="top" style="width: 40%; text-align: left;" colspan="2">
                                <asp:DropDownList ID="ddlTutorEdit" runat="server" Enabled="false" CssClass="EstiloTxtLargo250"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlTutorEdit_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td valign="top" class="TDCriterios75">
                                <asp:Label ID="Label4" runat="server" CssClass="lblTutor" Text="Alumno:"></asp:Label>
                                <asp:TextBox ID="txtAlumno" runat="server" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TD25">
                                <asp:Label runat="server" ID="Label9" Text="Descripción:"></asp:Label>
                            </td>
                            <td colspan="4" class="TDCriterios75">
                                <asp:TextBox runat="server" ID="txtDescripcionEdit" Width="600px" TextMode="MultiLine"
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
