<%@ Page Title="Administrar evento Reunión" Language="C#" MasterPageFile="~/EDUARMaster.Master"
AutoEventWireup="true" CodeBehind="ManageReuniones.aspx.cs" Inherits="EDUAR_UI.ManageReuniones" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Src="~/UserControls/Calendario.ascx" TagName="Calendario" TagPrefix="cal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Evaluaciones</h2>
        <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="tablaInterna" cellpadding="0" cellspacing="0">
                <tr>
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
                                    Buscar Reuniones</h3>
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
                            <td valign="top" class="TDCriterios20">
                                <asp:Label ID="lblFecha" runat="server" Text="Fecha:"></asp:Label>
                            </td>
                            <td valign="top" class="TDCriterios40">
                                <cal:Calendario ID="calfecha" runat="server" TipoCalendario="SoloFecha" TipoAlineacion="Izquierda" />
                            </td>
                            <td valign="top" class="TDCriterios25">
                                <asp:Label ID="Label12" runat="server" Text="Activos:"></asp:Label>
                            </td>
                            <td valign="top" class="TDCriterios25">
                                <asp:CheckBox ID="chkActivo" runat="server" Checked="true" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <%--<asp:PostBackTrigger ControlID="ddlCicloLectivo" />--%>
                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="gvwReporte" EventName="RowCommand" />--%>
            <asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>




    <asp:UpdatePanel ID="udpGrilla" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView ID="gvwReporte" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerReporte"
                AutoGenerateColumns="false" AllowPaging="false" Width="100%" DataKeyNames="idEventoAgenda"
                OnRowCommand="gvwReporte_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Acciones">
                        <HeaderStyle HorizontalAlign="center" Width="5%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="editarEvento" runat="server" CommandName="Editar" CommandArgument='<%# Bind("idEventoAgenda") %>'
                                ToolTip="Editar Evento" ImageUrl="~/Images/Grillas/action_edit.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Fecha">
                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblFecha" runat="server" Text='<%# Bind("fechaEvento","{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Descripcion">
                        <HeaderStyle HorizontalAlign="left" Width="20%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblDescripcion" runat="server" Text='<%# Bind("detalle") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Organizador">
                        <HeaderStyle HorizontalAlign="left" Width="20%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblOrganizador" runat="server" Text='<%# String.Format("{0} {1}", Eval("usuario.nombre"), Eval("usuario.apellido")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Activo">
                        <HeaderStyle HorizontalAlign="left" Width="10%" />
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
                                    Evento Institucional</h3>
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
                                <cal:Calendario ID="calFechaEdit" runat="server" TipoCalendario="SoloFecha" TipoAlineacion="Izquierda" MensajeErrorValidacion="Fecha Inválida" />
                            </td>
                            <td valign="top" class="TDCriterios10">
                                <asp:Label runat="server" ID="Label4" Text="Hora:"></asp:Label>
                            </td>
                            <td valign="top" class="TDCriterios25">
                                <asp:TextBox runat="server" ID="txtHoraEdit" MaxLength="5" CssClass="EstiloTxtCorto80"></asp:TextBox>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptAMPM="false"
                                    MaskType="Time" Mask="99:99" ErrorTooltipEnabled="true" InputDirection="LeftToRight"
                                    CultureName="es-ES" TargetControlID="txtHoraEdit" MessageValidatorTip="true">
                                </cc1:MaskedEditExtender>
                                <cc1:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ToolTip="Hora Inválida"
                                    ErrorMessage="*" ControlExtender="MaskedEditExtender1" ControlToValidate="txtHoraEdit"
                                    InvalidValueMessage="Hora Inválida" TooltipMessage="00:00 - 23:59" ValidationGroup="validarEdit">
                                </cc1:MaskedEditValidator>
                            </td>
                        </tr>
                    </table>

                    <table class="tablaInterna" cellpadding="1" cellspacing="5">
                        <tr>
                            <td valign="top" class="TDCriterios25">
                                <asp:Label ID="lblActivoBusqueda" runat="server" Text="Activo:"></asp:Label>
                            </td>
                            <td valign="top" class="TDCriterios25">
                                <asp:CheckBox ID="chkActivoEdit" runat="server" Checked="false" />
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
