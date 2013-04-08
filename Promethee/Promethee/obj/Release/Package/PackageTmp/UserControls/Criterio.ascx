<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Criterio.ascx.cs" Inherits="Promethee.UserControls.Criterio" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<table class="tablaInternaSinBorde" border="0" cellpadding="1" cellspacing="5">
    <tr>
        <td class="TD50px">
            Nombre
        </td>
        <td>
            <asp:TextBox ID="txtCriterio" runat="server" CssClass="EstiloTxtLargo250" MaxLength="50"
                AutoCompleteType="None" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="TD50px">
            Peso
        </td>
        <td>
            <asp:TextBox ID="txtPeso" runat="server" CssClass="EstiloTxtCorto80" AutoCompleteType="None" />
            <ajaxToolkit:MaskedEditExtender ID="mskPesoCriterio" TargetControlID="txtPeso" Mask="9999.99"
                MessageValidatorTip="true" MaskType="Number" InputDirection="RightToLeft" AcceptNegative="None"
                DisplayMoney="None" ErrorTooltipEnabled="True" runat="server" />
            &nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:UpdatePanel ID="udpError" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                <ContentTemplate>
                    <asp:Label Text="" ID="lblErrorCriterio" runat="server" ForeColor="Red" Font-Bold="true" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
<table class="tablaInternaSinBorde" border="0" cellpadding="1" cellspacing="5">
    <tr>
        <td style="width: 70%">
            &nbsp;<asp:Label ID="Label1" Text="Función de Preferencia:" runat="server" Font-Bold="true"
                Font-Italic="true" />
            <asp:RadioButtonList ID="pseudoCriterio" runat="server" OnSelectedIndexChanged="pseudoCriterio_SelectedIndexChanged"
                AutoPostBack="true">
                <asp:ListItem Text="Cuasi Criterio" Value="2" />
                <asp:ListItem Text="Gaussiano" Value="6" />
                <asp:ListItem Text="Level Criterio" Value="4" />
                <asp:ListItem Text="Preferencia Lineal y Área de Indiferencia" Value="5" />
                <asp:ListItem Text="Pseudo Criterio con Preferencia Lineal" Value="3" />
                <asp:ListItem Text="Verdadero Criterio" Value="1" />
            </asp:RadioButtonList>
            <asp:UpdatePanel ID="udpLimites" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                <ContentTemplate>
                    <table>
                        <!--Cuasi Criterio (2) -->
                        <!--Level Criterio (4) -->
                        <!--Criterio con Preferencia Lineal y Área de Indiferencia (5) -->
                        <tr>
                            <td>
                                <asp:Label ID="lblLimiteIndiferencia" Text="Límite de Indiferencia (q):" runat="server"
                                    Visible="false" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtLimiteIndiferencia" runat="server" Visible="false" CssClass="EstiloTxtCorto80"
                                    AutoCompleteType="None" />
                                <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="9999.99"
                                    MaskType="Number" InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None"
                                    TargetControlID="txtLimiteIndiferencia">
                                </ajaxToolkit:MaskedEditExtender>
                            </td>
                        </tr>
                        <tr>
                            <!--Pseudo Criterio con Preferencia Lineal (3) -->
                            <!--Level Criterio (4) -->
                            <!--Criterio con Preferencia Lineal y Área de Indiferencia (5) -->
                            <td>
                                <asp:Label ID="lblLimitePreferencia" Text="Límite de Preferencia (p):" runat="server"
                                    Visible="false" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtLimitePreferencia" runat="server" Visible="false" CssClass="EstiloTxtCorto80"
                                    AutoCompleteType="None" />
                                <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender2" Mask="9999.99"
                                    MaskType="Number" InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None"
                                    TargetControlID="txtLimitePreferencia">
                                </ajaxToolkit:MaskedEditExtender>
                            </td>
                        </tr>
                        <!--Criterio Gaussiano (6) -->
                        <tr>
                            <td>
                                <asp:Label ID="lblLimiteSigma" Text="Sigma (&sigma;):" runat="server" Visible="false" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtLimiteSigma" runat="server" Visible="false" CssClass="EstiloTxtCorto80"
                                    AutoCompleteType="None" />
                                <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender3" Mask="9999.99"
                                    MaskType="Number" InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None"
                                    TargetControlID="txtLimiteSigma">
                                </ajaxToolkit:MaskedEditExtender>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="pseudoCriterio" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 30%; vertical-align: text-top">
            <asp:Label ID="Label2" Text="Sentido de Optimización:" runat="server" Font-Bold="true"
                Font-Italic="true" />
            <asp:RadioButtonList ID="rdlSentido" runat="server">
                <asp:ListItem Text="Maximizar" Value="1" Selected="True" />
                <asp:ListItem Text="Minimizar" Value="0" />
            </asp:RadioButtonList>
        </td>
    </tr>
</table>
<%--</asp:Panel>--%>
