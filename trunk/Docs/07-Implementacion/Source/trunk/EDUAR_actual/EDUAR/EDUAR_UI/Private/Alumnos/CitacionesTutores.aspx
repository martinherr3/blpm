<%@ Page Title="Citaciones Tutor" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="CitacionesTutores.aspx.cs" Inherits="EDUAR_UI.CitacionesTutores"
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
                        </h2>
                    </td>
                    <td align="right">
                        <asp:ImageButton ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" ToolTip="Buscar"
                            ImageUrl="~/Images/botonBuscar.png" />
                        <asp:ImageButton ID="btnVolver" OnClick="btnVolver_Click" runat="server" ToolTip="Volver"
                            ImageUrl="~/Images/botonVolver.png" />
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="udpFiltrosBusqueda" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table class="tablaInterna" cellpadding="1" cellspacing="5">
                        <tr>
                            <td valign="top" class="TD140px">
                                <asp:Label ID="Label2" runat="server" Text="Motivo de Citación:"></asp:Label>
                            </td>
                            <td valign="top" class="TD140px">
                                <asp:DropDownList ID="ddlMotivoCitacion" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td valign="top" class="TD140px">
                                <asp:Label ID="Label12" runat="server" Text="Activos:" Visible="false"></asp:Label>
                            </td>
                            <td valign="top">
                                <asp:CheckBox ID="chkActivo" runat="server" Checked="true" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TD140px">
                                <asp:Label ID="Label1" runat="server" Text="Alumno:" CssClass="lblCriterios" Visible="false"></asp:Label>
                            </td>
                            <td valign="top" colspan="3">
                                <asp:DropDownList ID="ddlAlumnos" runat="server" CssClass="EstiloTxtLargo250" Visible="false">
                                </asp:DropDownList>
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
            </asp:UpdatePanel>
        </ContentTemplate>
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
                                ToolTip="Ver Detalle" ImageUrl="~/Images/Grillas/action_lookup.png" />
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
                </Columns>
            </asp:GridView>
            <asp:UpdatePanel ID="udpEdit" runat="server" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
                    <table class="tablaInterna" cellpadding="1" cellspacing="5">
                        <tr>
                            <td colspan="2">
                                <h3>
                                    <asp:Literal ID="litconsultar" runat="server" Text="Detalle De Citación"></asp:Literal><hr />
                                </h3>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" cellpadding="1" cellspacing="5">
                        <tr>
                            <td valign="top" class="TDCriterios25">
                                <asp:Label ID="Label3" runat="server" Text="Fecha:"></asp:Label>
                            </td>
                            <td valign="top" class="TDCriterios75" colspan="3">
                                <cal:Calendario ID="calFechaEvento" runat="server" TipoCalendario="SoloFecha" TipoAlineacion="Izquierda"
                                    Habilitado="false" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TDCriterios25">
                                <asp:Label runat="server" ID="lblHora" Text="Hora:"></asp:Label>
                            </td>
                            <td valign="top" class="TDCriterios75" colspan="3">
                                <asp:TextBox runat="server" ID="txtHoraEdit" MaxLength="5" CssClass="EstiloTxtCorto80"
                                    Enabled="false"></asp:TextBox>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptAMPM="false"
                                    MaskType="Time" Mask="99:99" ErrorTooltipEnabled="true" InputDirection="LeftToRight"
                                    CultureName="es-ES" TargetControlID="txtHoraEdit" MessageValidatorTip="true">
                                </cc1:MaskedEditExtender>
                                <cc1:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ToolTip="Hora Inválida"
                                    ErrorMessage="*" ControlExtender="MaskedEditExtender2" ControlToValidate="txtHoraEdit"
                                    InvalidValueMessage="Hora Inválida" TooltipMessage="00:00 - 23:59" ValidationGroup="validarEdit"
                                    ForeColor="Red">
                                </cc1:MaskedEditValidator>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TDCriterios25">
                                <asp:Label ID="Label5" runat="server" Text="Curso:" CssClass="lblCriterios" Visible="false"></asp:Label>
                            </td>
                            <td valign="top" class="TDCriterios25">
                                <asp:DropDownList ID="ddlCursoEdit" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCursoEdit_SelectedIndexChanged"
                                    Enabled="false" Visible="false">
                                </asp:DropDownList>
                            </td>
                            <td valign="top" class="TDCriterios25">
                            </td>
                            <td valign="top" class="TDCriterios25">
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TDCriterios25">
                                <asp:Label ID="Label7" runat="server" Text="Motivo de Citación:"></asp:Label>
                            </td>
                            <td valign="top" class="TDCriterios25">
                                <asp:DropDownList ID="ddlMotivoEdit" runat="server" Enabled="false">
                                </asp:DropDownList>
                            </td>
                            <td valign="top" class="TDCriterios25">
                                <asp:Label ID="lblActivoEdit" runat="server" Text="Activo:" Visible="false"></asp:Label>
                            </td>
                            <td valign="top" class="TDCriterios25">
                                <asp:CheckBox ID="chkActivoEdit" runat="server" Checked="true" Enabled="false" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TDCriterios25">
                                <asp:Label ID="Label6" runat="server" Text="Tutor:" CssClass="lblCriterios" Visible="false"></asp:Label>
                            </td>
                            <td valign="top" class="TDCriterios75" colspan="3">
                                <asp:DropDownList ID="ddlTutorEdit" runat="server" Enabled="false" CssClass="EstiloTxtLargo250"
                                    Visible="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TD25">
                                <asp:Label runat="server" ID="Label9" Text="Descripción:"></asp:Label>
                            </td>
                            <td colspan="3" class="TDCriterios75">
                                <asp:Label Text="" ID="lblDescripcion" runat="server" />
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
</asp:Content>
