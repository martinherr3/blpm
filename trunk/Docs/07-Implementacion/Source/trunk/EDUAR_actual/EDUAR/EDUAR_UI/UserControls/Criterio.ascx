<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Criterio.ascx.cs" Inherits="EDUAR_UI.UserControls.Criterio" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:TextBox ID="txtCriterio" runat="server" CssClass="EstiloTxtCorto80" AutoPostBack="true" />
<ajaxToolkit:SliderExtender ID="SliderExtender1" BoundControlID="slider1_display"
    Decimals="2" runat="server" TargetControlID="txtCriterio" EnableHandleAnimation="true"
    TooltipText="{0}">
</ajaxToolkit:SliderExtender>
<asp:Label ID="slider1_display" runat="server"></asp:Label>
<asp:ImageButton ImageUrl="~/Images/Grillas/action_enable.png" AlternateText="Habilitado"
    ToolTip="Habilitado" ID="btnDesHabilitar" runat="server" OnClick="btnDesHabilitar_Click"
    ImageAlign="AbsMiddle" />
<asp:LinkButton Text="Configurar" ID="lnkConfig" runat="server" OnClick="lnkConfig_Click" />
<ajaxToolkit:CollapsiblePanelExtender ID="cpe" runat="Server" TargetControlID="Panel1"
    CollapsedSize="0" ExpandedSize="220" Collapsed="True" ExpandControlID="lnkConfig"
    CollapseControlID="lnkConfig" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
    ExpandDirection="Vertical" />
<asp:Panel runat="server" ID="Panel1" BackColor="#FFFFFF" Width="500px">
    <table width="100%">
        <tr>
            <td style="width: 60%">
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
                                    <asp:TextBox ID="txtLimiteIndiferencia" runat="server" Visible="false" CssClass="EstiloTxtCorto80" />
                                    <cc1:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="9999,99" MaskType="Number"
                                        InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None" TargetControlID="txtLimiteIndiferencia">
                                    </cc1:MaskedEditExtender>
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
                                    <asp:TextBox ID="txtLimitePreferencia" runat="server" Visible="false" CssClass="EstiloTxtCorto80" />
                                    <cc1:MaskedEditExtender runat="server" ID="MaskedEditExtender2" Mask="9999,99" MaskType="Number"
                                        InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None" TargetControlID="txtLimitePreferencia">
                                    </cc1:MaskedEditExtender>
                                </td>
                            </tr>
                            <!--Criterio Gaussiano (6) -->
                            <tr>
                                <td>
                                    <asp:Label ID="lblLimiteSigma" Text="Sigma (&sigma;):" runat="server" Visible="false" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLimiteSigma" runat="server" Visible="false" CssClass="EstiloTxtCorto80" />
                                    <cc1:MaskedEditExtender runat="server" ID="MaskedEditExtender3" Mask="9999,99" MaskType="Number"
                                        InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None" TargetControlID="txtLimiteSigma">
                                    </cc1:MaskedEditExtender>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="pseudoCriterio" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td style="width: 40%; vertical-align: text-top">
                <asp:Label ID="Label2" Text="Sentido de Optimización:" runat="server" Font-Bold="true"
                    Font-Italic="true" />
                <asp:RadioButtonList ID="rdlSentido" runat="server">
                    <asp:ListItem Text="Maximizar" Value="1" Selected="True" />
                    <asp:ListItem Text="Minimizar" Value="0" />
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
</asp:Panel>
