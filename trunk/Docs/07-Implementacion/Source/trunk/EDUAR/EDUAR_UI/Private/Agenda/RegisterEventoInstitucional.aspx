<%@ Page Title="Registrar eventos institucionales" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="RegisterEventoInstitucional.aspx.cs" Inherits="EDUAR_UI.RegisterEventoInstitucional" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Src="~/UserControls/Calendario.ascx" TagName="Calendario" TagPrefix="cal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="ContentHeader" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Evento Institucional</h2>
    <div id="divFiltros" runat="server">
        <table class="tablaInterna" cellpadding="0" cellspacing="0">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="btnBuscar" runat="server" ToolTip="Buscar" ImageUrl="~/Images/botonBuscar.png" OnClick="btnBuscar_Click" />
                    <asp:ImageButton ID="btnGuardar" runat="server" ToolTip="Guardar" ImageUrl="~/Images/botonGuardar.png"
                        Visible="false" OnClick="btnGuardar_Click" />
                </td>
            </tr>
        </table>
        <table class="tablaInterna" cellpadding="1" cellspacing="5">
            <tr>
                <td>
                    <h3>
                        Buscar Evento</h3>
                </td>
            </tr>
            <tr>
                <td class="TD100">
                    <asp:ValidationSummary ID="ValidarPagina" runat="server" />
                </td>
            </tr>
        </table>
        <table class="tablaInterna" cellpadding="1" cellspacing="5">
            <tr>
                <td valign="top" class="TDCriterios25">
                    <asp:Label ID="lblLugar" runat="server" Text="Lugar:"></asp:Label>
                </td>
                <td valign="top" class="TDCriterios25">
                    <asp:TextBox runat="server" ID="txtLugar" CssClass="EstiloTxtLargo250"></asp:TextBox>
                </td>
                <td valign="top" class="TDCriterios25">
                </td>
                <td valign="top" class="TDCriterios25">
                </td>
            </tr>
            <tr>
                <td valign="top" class="TD25">
                    <asp:Label runat="server" ID="lblTitulo" Text="Título:"></asp:Label>
                </td>
                <td class="TD75" colspan="3">
                    <asp:TextBox runat="server" ID="txtTitulo" CssClass="EstiloTxtLargo250"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table width="100%" cellpadding="1" cellspacing="5">
            <tr>
                <td valign="top" class="TDCriterios20">
                    <asp:Label ID="lblFecha" runat="server" Text="Fecha:"></asp:Label>
                </td>
                <td valign="top" class="TDCriterios40">
                    <cal:Calendario ID="calfecha" runat="server" TipoCalendario="SoloFecha" TipoAlineacion="Izquierda" />
                </td>
                <td valign="top" class="TDCriterios10">
                    <asp:Label runat="server" ID="lblHora" Text="Hora:"></asp:Label>
                </td>
                <td valign="top" class="TDCriterios25">
                    <asp:TextBox runat="server" ID="txtHora" MaxLength="5" CssClass="EstiloTxtCorto80"></asp:TextBox>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptAMPM="false"
                        MaskType="Time" Mask="99:99" ErrorTooltipEnabled="true" InputDirection="RightToLeft"
                        CultureName="es-ES" TargetControlID="txtHora" MessageValidatorTip="true">
                    </cc1:MaskedEditExtender>
                    <cc1:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ToolTip="Hora Inválida"
                        ErrorMessage="*" ControlExtender="MaskedEditExtender1" ControlToValidate="txtHora"
                        InvalidValueMessage="Hora Inválida" TooltipMessage="00:00 - 23:59" ValidationGroup="ValidarPagina">
                    </cc1:MaskedEditValidator>
                </td>
            </tr>
        </table>
    </div>
    <div id="divReporte" runat="server">
        <asp:UpdatePanel ID="udpReporte" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gvwReporte" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerReporte"
                    AutoGenerateColumns="false" AllowPaging="false" Width="100%">
                    <Columns>
                    <asp:TemplateField HeaderText="Acciones">
                        <HeaderStyle HorizontalAlign="center" Width="5%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="editarEvento" runat="server" CommandName="Nuevo" CommandArgument='<%# Bind("idEventoInstitucional") %>'
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
                    <asp:TemplateField HeaderText="Titulo">
                        <HeaderStyle HorizontalAlign="left" Width="20%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblTitulo" runat="server" Text='<%# Bind("descripcionBreve") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Lugar">
                        <HeaderStyle HorizontalAlign="left" Width="20%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblLugar" runat="server" Text='<%# Bind("lugar") %>'></asp:Label>
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
                            <asp:Label ID="lblOrganizador" runat="server" Text='<%# Bind("organizador") %>'></asp:Label>
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
