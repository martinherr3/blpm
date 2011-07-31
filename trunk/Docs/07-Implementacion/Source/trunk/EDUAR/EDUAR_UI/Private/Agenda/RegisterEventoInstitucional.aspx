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
                    <asp:ImageButton ID="btnBuscar" runat="server" ToolTip="Buscar" ImageUrl="~/Images/botonBuscar.png" />
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
        <asp:GridView ID="gvwReporte" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerReporte"
            AutoGenerateColumns="false" AllowPaging="false" Width="100%">
            <HeaderStyle CssClass="gridheader" />
            <RowStyle CssClass="gridrow" />
        </asp:GridView>
    </div>
</asp:Content>
