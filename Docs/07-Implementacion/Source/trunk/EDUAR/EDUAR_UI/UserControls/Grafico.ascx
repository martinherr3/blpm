<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Grafico.ascx.cs" Inherits="EDUAR_UI.UserControls.Grafico" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script type="text/javascript">
    $(function () {

        if ($("#btnDatos").text() == "Ver Datos") {
            $('#divDatos').hide();
        }

        $("#btnDatos").click(function () {
            if ($('#divDatos').is(':visible')) {
                $('#divDatos').hide('Blind', '', 1000);
                $("#btnDatos").text("Ver Datos");
            }
            else {
                $('#divDatos').show('Blind', '', 1000);
                $("#btnDatos").text("Ocultar Datos");
            }
            return false;
        });
    });
</script>
<div id="divGrafico" runat="server" class="MarcoGrafico">
    <iframe runat="server" id="divSysExceptionFrame" visible="true" class="FondoNegroVentana"
        frameborder="0" scrolling="no" width="100%" height="100%" />
    <div id="div2" runat="server" class="FondoNegroVentana" />
    <table class="TablaExternaVentana" border="0" cellpadding="0" cellspacing="0" width="100%"
        visible="true">
        <tr>
            <td id="TDGrafico" runat="server" style="min-height: 450px; height: auto !important;
                height: 450px;">
                <asp:Chart ID="Chart1" runat="server" Height="400px" Width="645px" ImageType="Png"
                    AntiAliasing="All" BorderDashStyle="Solid" BackSecondaryColor="White" Palette="BrightPastel"
                    RenderType="ImageTag" BackGradientStyle="TopBottom" BackColor="#D3D3D3" BorderWidth="2"
                    BorderColor="26, 59, 105">
                    <Legends>
                        <asp:Legend IsTextAutoFit="False" BorderWidth="1" BorderDashStyle="Solid" ShadowOffset="3"
                            Name="Default" BackColor="Transparent" BorderColor="26, 59, 105" Docking="Bottom">
                        </asp:Legend>
                    </Legends>
                    <BorderSkin SkinStyle="Emboss"></BorderSkin>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                            BackSecondaryColor="LightGray" BackColor="#D3D3D3" ShadowColor="Transparent"
                            BackGradientStyle="TopBottom">
                            <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False"
                                WallWidth="0" IsClustered="False" Enable3D="true"></Area3DStyle>
                            <AxisY LineColor="64, 64, 64, 64">
                                <LabelStyle Font="Verdana, 8.25pt, style=Bold" />
                                <MajorGrid LineColor="64, 64, 64, 64" />
                            </AxisY>
                            <AxisX LineColor="64, 64, 64, 64">
                                <LabelStyle Font="Verdana, 8.25pt, style=Bold" />
                                <MajorGrid LineColor="64, 64, 64, 64" />
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
                <cc1:AnimationExtender ID="AnimationExtender2" runat="server" TargetControlID="Chart1">
                    <Animations>
                        <OnLoad>  
                            <Sequence>       
                                <Scale Duration="0" Fps="60" ScaleFactor="0.03" Center="true" /> 
                                <Condition ConditionScript="1==1">                 
                                    <Parallel>
                                        <FadeIn Duration="1" Fps="60" />
                                        <Scale Duration="1" Fps="60" ScaleFactor="34.0" Center="true" />                                         
                                    </Parallel>
                                </Condition>
                            </Sequence>
                        </OnLoad>
                    </Animations>
                </cc1:AnimationExtender>
            </td>
        </tr>
        <tr>
            <td id="TDBotonera" runat="server" align="right">
                <table cellpadding="0" cellspacing="0" border="0" width="646px">
                    <tr>
                        <td style="width: 50%">
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td style="" align="left">
                                        <asp:ImageButton ID="btnLineal" runat="server" OnClick="btnLineal_Click" ImageUrl="/EDUAR_UI/Images/chart-line.png"
                                            ToolTip="Líneas" AlternateText="Líneas" />
                                    </td>
                                    <td style="" align="left">
                                        <asp:ImageButton ID="btnBar" runat="server" OnClick="btnBar_Click" ImageUrl="/EDUAR_UI/Images/chart-bar.png"
                                            ToolTip="Barras" AlternateText="Barras" />
                                    </td>
                                    <td style="" align="left">
                                        <asp:ImageButton ID="btnTorta" runat="server" OnClick="btnTorta_Click" ImageUrl="/EDUAR_UI/Images/chart-pie.png"
                                            ToolTip="Torta" AlternateText="Torta" />
                                    </td>
                                    <td style="" align="left">
                                        <asp:ImageButton ID="btnRadar" runat="server" OnClick="btnRadar_Click" ImageUrl="/EDUAR_UI/Images/chart-polar.png"
                                            ToolTip="Radar" AlternateText="Radar" />
                                    </td>
                                    <td style="width: 60%" align="right">
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 50%; text-align: right">
                            <asp:ImageButton ID="btnCerrar" runat="server" OnClick="btnCerrar_Click" ImageUrl="/EDUAR_UI/Images/CerrarPopup.png"
                                ToolTip="Cerrar" AlternateText="Cerrar" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
