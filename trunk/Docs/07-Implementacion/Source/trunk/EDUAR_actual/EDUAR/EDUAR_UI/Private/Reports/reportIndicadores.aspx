<%@ Page Title="Indicadores" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="reportIndicadores.aspx.cs" Inherits="EDUAR_UI.reportIndicadores"
    EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/Criterio.ascx" TagName="Criterio" TagPrefix="cri" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divAccion" runat="server">
        <table class="tablaInterna" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <h2>
                        Indicadores Generales de Desempeño -
                        <asp:Label ID="lblCicloLectivo" Text="" runat="server" /></h2>
                    <br />
                </td>
                <td align="right">
                    <asp:ImageButton ID="btnExportarPDF" OnClick="btnExportarPDF_Click" runat="server"
                        ToolTip="Exportar a PDF" ImageUrl="~/Images/ExportarPDF.png" AlternateText="Exportar a PDF"
                        Enabled="true" />
                    <asp:ImageButton ID="btnExcel" OnClick="btnExcel_Click" runat="server" ToolTip="Exportar a Excel"
                        ImageUrl="~/Images/ExportarExcel.png" AlternateText="Exportar a Excel" Enabled="true" />
                    <asp:ImageButton ID="btnCalcular" OnClick="btnCalcular_Click" runat="server" ToolTip="Calcular"
                        AlternateText="Calcular" ImageUrl="~/Images/botonCalcular.png" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divFiltros" runat="server">
        <table class="tablaInterna" cellpadding="1" cellspacing="5">
            <tr>
                <td valign="top" class="TDCriterios25">
                    <asp:Label ID="lblCurso" runat="server" Text="Curso:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top" class="TDCriterios25">
                    <asp:DropDownList ID="ddlCurso" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCurso_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td valign="top" class="TDCriterios25">
                    <asp:Label ID="lblAlumnos" runat="server" Text="Obtener los primeros:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td valign="top" class="TDCriterios25">
                    <asp:DropDownList ID="ddlTop" runat="server">
                        <asp:ListItem Text="3" Value="3" />
                        <asp:ListItem Text="5" Value="5" />
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="Todos" Value="0" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <h3>
                        Criterios y Pesos</h3>
                </td>
            </tr>
        </table>
        <table class="tablaInterna" cellpadding="1" cellspacing="5">
            <tr>
                <td valign="top" colspan="2" align="center" style="width: 100%">
                    <table class="tablaInternaSinBorde" cellpadding="1" cellspacing="5" style="text-align: justify;
                        width: 100%">
                        <tr>
                            <td valign="top" class="TDCriterios10">
                                <asp:Label ID="lblCalificaciones" Text="Calificaciones" runat="server" CssClass="lblCriterios" />
                            </td>
                            <td valign="top">
                                <cri:Criterio ID="criterioCalificacion" runat="server" nombreCriterio="Calificaciones"
                                    esMaximzante="true"></cri:Criterio>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TDCriterios10">
                                <asp:Label ID="lblInasistencias" Text="Inasistencias" runat="server" CssClass="lblCriterios" />
                            </td>
                            <td valign="top">
                                <cri:Criterio ID="criterioInasistencia" runat="server" nombreCriterio="Inasistencias"
                                    esMaximzante="false"></cri:Criterio>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TDCriterios10">
                                <asp:Label ID="lblSanciones" Text="Sanciones" runat="server" CssClass="lblCriterios" />
                            </td>
                            <td valign="top">
                                <cri:Criterio ID="criterioSancion" runat="server" nombreCriterio="Sanciones" esMaximzante="false">
                                </cri:Criterio>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TDCriterios50" valign="middle">
                    <asp:UpdatePanel ID="updDistribucion" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Chart ID="chartCriterios" runat="server" Height="300px" Width="350px" ImageType="Png"
                                AntiAliasing="All" Palette="SeaGreen" RenderType="ImageTag">
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64">
                                        <Area3DStyle Rotation="35" Perspective="20" Inclination="30" IsRightAngleAxes="False"
                                            WallWidth="0" IsClustered="False" Enable3D="true"></Area3DStyle>
                                    </asp:ChartArea>
                                </ChartAreas>
                            </asp:Chart>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="criterioCalificacion" EventName="CriterioScroll" />
                            <asp:AsyncPostBackTrigger ControlID="criterioInasistencia" EventName="CriterioScroll" />
                            <asp:AsyncPostBackTrigger ControlID="criterioSancion" EventName="CriterioScroll" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td class="TDCriterios50">
                    <div style="width: 100%; text-align: center">
                        <asp:UpdatePanel ID="udpImgPodio" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <%--<asp:Image ID="imgPodio" AlternateText="Resultado" ToolTip="Resultado" runat="server"
                                    Visible="false" Style="text-align: center" />--%>
                                <asp:Chart ID="chartPodio" runat="server" Height="300px" Width="350px" ImageType="Png"
                                    AntiAliasing="All" Palette="SeaGreen" RenderType="ImageTag" Visible="false">
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64">
                                            <Area3DStyle Rotation="35" Perspective="20" Inclination="30" IsRightAngleAxes="true"
                                                WallWidth="0" IsClustered="False" Enable3D="true"></Area3DStyle>
                                        </asp:ChartArea>
                                    </ChartAreas>
                                </asp:Chart>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnCalcular" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <asp:UpdatePanel runat="server" ID="udpResultado" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:LinkButton Text="Más Información" ID="lnkConfig" runat="server" OnClick="lnkConfig_Click"
                    Visible="false" />
                <ajaxToolkit:CollapsiblePanelExtender ID="cpe" runat="Server" TargetControlID="pnlResultado"
                    CollapsedSize="0" ExpandedSize="500" Collapsed="True" ExpandControlID="lnkConfig"
                    CollapseControlID="lnkConfig" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                    ExpandDirection="Vertical" />
                <asp:Panel runat="server" ID="pnlResultado">
                    <asp:Label ID="lblResultadoGrilla" Text="Resultados obtenidos en orden Descendente"
                        runat="server" Visible="false" Font-Bold="true" /><br />
                    <asp:Label ID="lblResultado" Text="" runat="server" CssClass="lblCriterios" Visible="false" />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvwResultado" runat="server" CssClass="DatosLista" AllowPaging="True"
                                AllowSorting="True" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None"
                                ShowFooter="false" OnPageIndexChanging="gvwResultado_PageIndexChanging" PageSize="15"
                                OnSorting="gvwResultado_Sorting" OnRowCreated="gvwResultado_RowCreated" OnRowDataBound="gvwResultado_RowDataBound">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Height="24px" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                            <br />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnCalcular" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="gvwResultado" EventName="PageIndexChanging" />
                <asp:AsyncPostBackTrigger ControlID="gvwResultado" EventName="Sorting" />
                <asp:AsyncPostBackTrigger ControlID="gvwResultado" EventName="RowCreated" />
                <asp:AsyncPostBackTrigger ControlID="gvwResultado" EventName="RowDeleting" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
