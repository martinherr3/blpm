<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Indicadores.ascx.cs"
    Inherits="EDUAR_UI.UserControls.Indicadores" %>
<script type="text/javascript" language="javascript">
    function panelGraficoAuxiliar(valor) {
        __doPostBack("btnPanel", valor);
    }
</script>
<table class="tablaInterna" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <h2>
                Planeado vs Dictado</h2>
            <br />
        </td>
        <td>
            <asp:Chart ID="Chart1" runat="server" BackColor="#D3DFF0" Palette="BrightPastel"
                ImageType="Png" ImageUrl="ChartPic_#SEQ(300,3)" Width="412px" Height="296px"
                borderlinestyle="Solid" backgradientendcolor="White" backgradienttype="TopBottom"
                BorderlineWidth="2" BorderlineColor="26, 59, 105">
                <Titles>
                    <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3"
                        Text="Regional Sales" Alignment="TopLeft" ForeColor="26, 59, 105">
                    </asp:Title>
                </Titles>
                <Legends>
                    <asp:Legend Enabled="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                        <Position Y="21" Height="22" Width="18" X="73"></Position>
                    </asp:Legend>
                </Legends>
                <BorderSkin SkinStyle="Emboss"></BorderSkin>
                <Series>
                    <asp:Series Name="Sales" BorderColor="180, 26, 59, 105">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="Default" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                        BackSecondaryColor="White" BackColor="64, 165, 191, 228" ShadowColor="Transparent"
                        BackGradientStyle="TopBottom">
                        <AxisY LineColor="64, 64, 64, 64" IsLabelAutoFit="False">
                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                            <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                        </AxisY>
                        <AxisX LineColor="64, 64, 64, 64" IsLabelAutoFit="False">
                            <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                            <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                        </AxisX>
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </td>
    </tr>
</table>
<div style="visibility: hidden">
    <asp:Button Text="oculto" ID="btnPanel" runat="server" OnClick="btnPanel_Click" Width="0px"
        Height="0px" />
</div>
<asp:HiddenField ID="HiddenField1" runat="server" />
<ajaxToolkit:ModalPopupExtender ID="mpeContenido" runat="server" PopupControlID="pnlContenidos"
    TargetControlID="HiddenField1" RepositionMode="RepositionOnWindowResizeAndScroll"
    BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="pnlContenidos">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnlContenidos" runat="server" Width="600px" Height="400px" Style="display: none;
    text-align: left" BorderStyle="Groove" CssClass="CajaDialogo">
    <asp:UpdatePanel ID="udpContenidosAsociados" runat="server">
        <ContentTemplate>
            <table class="tablaInterna" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <h2>
                            <asp:Label ID="Label2" Text="Gráfico Auxiliar" runat="server" /></h2>
                        <br />
                    </td>
                    <td align="right">
                        <%--asp:ImageButton ID="btnGuardarPopUp" runat="server" ToolTip="Guardar" ImageUrl="~/Images/PopUp/botonGuardar.png"
                                OnClick="btnGuardarPopUp_Click" />--%>
                        <asp:ImageButton ID="btnVolverPopUp" runat="server" ToolTip="Volver" ImageUrl="~/Images/PopUp/botonVolver.png"
                            OnClick="btnVolverPopUp_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Chart ID="Chart2" runat="server" RenderType="ImageTag" ImageType="Png" Height="152px"
                            Width="160px" Palette="BrightPastel" BackColor="#F3DFC1" borderlinestyle="Solid"
                            backgradienttype="TopBottom" BorderlineWidth="2" BorderlineColor="181, 64, 1">
                            <Titles>
                                <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 8pt, style=Bold" ShadowOffset="2"
                                    Text="Statistics" Alignment="TopCenter" ForeColor="26, 59, 105">
                                </asp:Title>
                            </Titles>
                            <Legends>
                                <asp:Legend Enabled="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold">
                                </asp:Legend>
                            </Legends>
                            <Series>
                                <asp:Series Name="Sales" BorderColor="180, 26, 59, 105">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="Default" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                                    BackSecondaryColor="White" BackColor="OldLace" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                    <AxisY LineColor="64, 64, 64, 64">
                                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" Format="$#0,k"></LabelStyle>
                                        <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                                    </AxisY>
                                    <AxisX LineColor="64, 64, 64, 64" IsLabelAutoFit="False">
                                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" Enabled="False"></LabelStyle>
                                        <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                                    </AxisX>
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Panel>
