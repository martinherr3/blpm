<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Calendario.ascx.cs"
    Inherits="EDUAR_UI.UserControls.Calendario" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<div id="DivDesdeHasta" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: middle; width: 25%">
                <asp:HiddenField runat="server" ID="hndMensajeErrorValidacion" Value="" />
                <div id="divEtiquetaDesdeDA" runat="server">
                    <asp:Label ID="lblFechaDesde_DA" runat="server" CssClass="lblCriterios"></asp:Label>
                </div>
            </td>
            <td style="width: 25%">
                <div style="vertical-align: middle; overflow: visible">
                    <asp:TextBox ID="txtFechaDesde_DA" runat="server" CssClass="EstiloTxtMedio120" />
                    <cc1:MaskedEditExtender ID="maskEditFechaDesde_DA" runat="server" Mask="99/99/9999"
                        MaskType="Date" TargetControlID="txtFechaDesde_DA" UserDateFormat="DayMonthYear"
                        Enabled="True" ClearTextOnInvalid="True">
                    </cc1:MaskedEditExtender>
                    <asp:Image ID="imgFechaDesde_DA" runat="server" ImageUrl="~/Images/calendario.png"
                        ImageAlign="AbsMiddle" />
                    <cc1:CalendarExtender ID="calExtFechaDesde_DA" runat="server" TargetControlID="txtFechaDesde_DA"
                        Format="dd/MM/yyyy" PopupButtonID="imgFechaDesde_DA">
                    </cc1:CalendarExtender>
                </div>
            </td>
            <td style="vertical-align: middle; width: 25%">
                <div id="divEtiquetaHastaDA" runat="server">
                    <asp:Label ID="lblFechaHasta_DA" runat="server" CssClass="lblCriterios"></asp:Label>
                </div>
            </td>
            <td style="width: 25%">
                <div style="vertical-align: middle; overflow: visible">
                    <asp:TextBox ID="txtFechaHasta_DA" runat="server" CssClass="EstiloTxtMedio120" />
                    <cc1:MaskedEditExtender ID="maskEditFechaHasta_DA" runat="server" Mask="99/99/9999"
                        MaskType="Date" TargetControlID="txtFechaHasta_DA" UserDateFormat="DayMonthYear"
                        Enabled="True" ClearTextOnInvalid="True">
                    </cc1:MaskedEditExtender>
                    <asp:Image ID="imgFechaHasta_DA" runat="server" ImageUrl="~/Images/calendario.png"
                        ImageAlign="AbsMiddle" />
                    <cc1:CalendarExtender ID="calExtFechaHasta_DA" runat="server" TargetControlID="txtFechaHasta_DA"
                        Format="dd/MM/yyyy" PopupButtonID="imgFechaHasta_DA">
                    </cc1:CalendarExtender>
                </div>
            </td>
        </tr>
    </table>
</div>
<div id="DivDesde" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: middle; width: 40%">
                <div id="divEtiquetaDesdeD" runat="server">
                    <asp:Label ID="lblFechaDesde" runat="server" CssClass="lblCriterios"></asp:Label>
                </div>
            </td>
            <td style="vertical-align: middle; width: 60%">
                <div style="vertical-align: middle; overflow: visible">
                    <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="EstiloTxtMedio120"></asp:TextBox>
                    <cc1:MaskedEditExtender ID="txtFechaDesde_MaskedEditExtender" runat="server" Mask="99/99/9999"
                        MaskType="Date" TargetControlID="txtFechaDesde" UserDateFormat="DayMonthYear"
                        Enabled="True" ClearTextOnInvalid="True">
                    </cc1:MaskedEditExtender>
                    <asp:Image ID="imgFechaDesde" runat="server" ImageUrl="~/Images/calendario.png" ImageAlign="AbsMiddle" />
                    <cc1:CalendarExtender ID="calExtDesde" runat="server" TargetControlID="txtFechaDesde"
                        Format="dd/MM/yyyy" PopupButtonID="imgFechaDesde">
                    </cc1:CalendarExtender>
                </div>
            </td>
        </tr>
    </table>
</div>
<div id="DivSoloFecha" runat="server">
    <asp:TextBox ID="txtFecha" runat="server"></asp:TextBox>
    <cc1:MaskedEditExtender ID="txtFecha_MaskedEditExtender" runat="server" Mask="99/99/9999"
        MaskType="Date" TargetControlID="txtFecha" UserDateFormat="DayMonthYear" Enabled="True"
        ClearTextOnInvalid="True">
    </cc1:MaskedEditExtender>
    <asp:Image ID="imgFecha" runat="server" ImageUrl="~/Images/calendario.png" ImageAlign="AbsMiddle" />
    <cc1:CalendarExtender ID="calExtFecha" runat="server" TargetControlID="txtFecha"
        Format="dd/MM/yyyy" PopupButtonID="imgFecha">
    </cc1:CalendarExtender>
</div>
