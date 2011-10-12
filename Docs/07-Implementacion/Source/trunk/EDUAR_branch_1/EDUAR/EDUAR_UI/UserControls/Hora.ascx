<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Hora.ascx.cs" Inherits="EDUAR_UI.UserControls.Hora" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:TextBox runat="server" ID="txtHora" MaxLength="5" CssClass="EstiloTxtCorto50"></asp:TextBox>
<cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptAMPM="false"
    MaskType="Time" Mask="99:99" ErrorTooltipEnabled="true" InputDirection="LeftToRight"
    CultureName="es-ES" TargetControlID="txtHora" MessageValidatorTip="true">
</cc1:MaskedEditExtender>
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtHora"
    runat="server" ErrorMessage="Hora Inválida" ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$"
    ValidationGroup="ValidarBusqueda"></asp:RegularExpressionValidator>
