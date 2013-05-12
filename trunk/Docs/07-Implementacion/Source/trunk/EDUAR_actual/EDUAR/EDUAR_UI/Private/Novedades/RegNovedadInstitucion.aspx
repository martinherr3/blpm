<%@ Page Title="Novedades Institucionales" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="RegNovedadInstitucion.aspx.cs" Inherits="EDUAR_UI.RegNovedadInstitucion" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Src="~/UserControls/Calendario.ascx" TagName="Calendario" TagPrefix="cal" %>
<%@ Register Src="~/UserControls/Hora.ascx" TagName="Hora" TagPrefix="hor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="ContentHeader" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="tablaInterna" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <h2>
                            Novedades Institucionales</h2>
                        <br />
                    </td>
                    <td align="right">
                        <asp:ImageButton ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" ToolTip="Buscar"
                            ImageUrl="~/Images/botonBuscar.png" ValidationGroup="ValidarBusqueda" />
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
<%--                    <table class="tablaInterna" cellpadding="1" cellspacing="5">
                        <tr>
                            <td>
                                <h3>
                                    Buscar Evento</h3>
                            </td>
                        </tr>
                        <tr>
                            <td class="TD100">
                                <asp:ValidationSummary ID="ValidarBusqueda" runat="server" />
                            </td>
                        </tr>
                    </table>--%>
                    <table class="tablaInterna" cellpadding="1" cellspacing="5">
                        <tr>
                            <td valign="top" class="TD100px">
                                <asp:Label ID="lblLugar" runat="server" Text="Lugar:"></asp:Label>
                            </td>
                            <td valign="top" colspan="3">
                                <asp:TextBox runat="server" ID="txtLugar" CssClass="EstiloTxtLargo250"></asp:TextBox>
                            </td>
                            <td valign="top" class="TD100px">
                                <asp:Label runat="server" ID="Label15" Text="Tipo de Evento:"></asp:Label>
                            </td>
                            <td valign="top">
                                <asp:DropDownList runat="server" ID="ddlTipoEvento">
                                </asp:DropDownList>
                            </td>
                            <td valign="top" class="TD100px">
                                <asp:Label ID="Label12" runat="server" Text="Solo Activos:"></asp:Label>
                            </td>
                            <td valign="top">
                                <asp:CheckBox ID="chkActivo" runat="server" Checked="false" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TD100px">
                                <asp:Label runat="server" ID="lblTitulo" Text="Título:"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox runat="server" ID="txtTitulo" CssClass="EstiloTxtLargo250"></asp:TextBox>
                            </td>
                            <td class="TD100px">
                                <asp:Label ID="lblFecha" runat="server" Text="Fecha:"></asp:Label>
                            </td>
                            <td valign="top" class="TD100px" style="padding-left: 3px">
                                <cal:Calendario ID="calfecha" runat="server" TipoCalendario="SoloFecha" TipoAlineacion="Izquierda" />
                            </td>
                            <td valign="middle" class="TD100px">
                                <asp:Label runat="server" ID="lblHora" Text="Hora:"></asp:Label>
                            </td>
                            <td valign="middle">
                                <hor:Hora ID="txtHora" runat="server" />
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
            <asp:AsyncPostBackTrigger ControlID="gvwReporte" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="udpGrilla" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView ID="gvwReporte" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListado"
                AutoGenerateColumns="false" AllowPaging="false" Width="100%" DataKeyNames="idEventoInstitucional"
                OnRowCommand="gvwReporte_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Acciones">
                        <HeaderStyle HorizontalAlign="center" Width="5%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="editarEvento" runat="server" CommandName="Editar" CommandArgument='<%# Bind("idEventoInstitucional") %>'
                                ToolTip="Editar Evento" ImageUrl="~/Images/Grillas/action_edit.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fecha">
                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblFecha" runat="server" Text='<%# Bind("fecha","{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Evento">
                        <HeaderStyle HorizontalAlign="left" Width="40%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblTipoEvento" runat="server" Text='<%# TruncateString(String.Format("{0} - {1}", Eval("tipoEventoInstitucional.descripcion"), Eval("titulo")))%>'
                                ToolTip='<%# String.Format("{0} - {1}", Eval("tipoEventoInstitucional.descripcion"), Eval("titulo"))%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Lugar">
                        <HeaderStyle HorizontalAlign="left" Width="20%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblLugar" runat="server" Text='<%# Bind("lugar") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Organizador">
                        <HeaderStyle HorizontalAlign="left" Width="20%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblOrganizador" runat="server" Text='<%# String.Format("{0} {1}", Eval("organizador.nombre"), Eval("organizador.apellido")) %>'></asp:Label>
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
<%--                    <table class="tablaInterna" cellpadding="1" cellspacing="5">
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
                    </table>--%>
                    <table class="tablaInterna" cellpadding="1" cellspacing="5">
                        <tr>
                            <td valign="top" class="TD110px">
                                <asp:Label ID="Label1" runat="server" Text="Lugar:"></asp:Label>
                            </td>
                            <td valign="top" colspan="3">
                                <asp:TextBox runat="server" ID="txtLugarEdit" CssClass="EstiloTxtLargo250"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TD110px">
                                <asp:Label runat="server" ID="Label2" Text="Título:"></asp:Label>
                            </td>
                            <td valign="top" colspan="3">
                                <asp:TextBox runat="server" ID="txtTituloEdit" CssClass="EstiloTxtLargo250"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TD110px">
                                <asp:Label runat="server" ID="Label13" Text="Tipo de Evento:"></asp:Label>
                            </td>
                            <td class="TD250px">
                                <asp:DropDownList runat="server" ID="ddlTipoEventoEdit">
                                </asp:DropDownList>
                            </td>
                            <td valign="top" class="TD110px">
                                <asp:Label ID="lblActivoBusqueda" runat="server" Text="Activo:"></asp:Label>
                            </td>
                            <td valign="top">
                                <asp:CheckBox ID="chkActivoEdit" runat="server" Checked="false" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TD110px">
                                <asp:Label ID="Label3" runat="server" Text="Fecha:"></asp:Label>
                            </td>
                            <td class="TD100px" style="padding-left: 3px">
                                <cal:Calendario ID="calFechaEdit" runat="server" TipoCalendario="SoloFecha" TipoAlineacion="Izquierda"
                                    MensajeErrorValidacion="Fecha Inválida" />
                            </td>
                            <td valign="top" class="TD110px">
                                <asp:Label runat="server" ID="Label4" Text="Hora:"></asp:Label>
                            </td>
                            <td valign="top">
                                <hor:Hora ID="txtHoraEdit" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TD110px">
                                <asp:Label runat="server" ID="Label9" Text="Descripción:"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox runat="server" ID="txtDescripcionEdit" Width="500px" TextMode="MultiLine"
                                    CssClass="txtMultilinea" Rows="5"></asp:TextBox>
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
