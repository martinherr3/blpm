<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Indicador.ascx.cs" Inherits="EDUAR_UI.UserControls.Indicador" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div id="divIndicador" runat="server" class="DivIndicador" style="width: 204px;">
            <table border="0" cellpadding="2" cellspacing="0" width="204px">
                <tr>
                    <td rowspan="2" valign="top" style="width: 106px; height: 203px;">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="center" style="height:40px; vertical-align:middle;">
                                    <asp:Label ID="lblCurso" runat="server" Font-Underline="true" Font-Bold="true" Text=""></asp:Label>
                                    <asp:Label ID="lblTitulo" runat="server" Font-Underline="true" Font-Bold="true" Text="-"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="height:129px;">
                                    <asp:Button ID="btnIndicador" runat="server" Text="-1" CssClass="BotonIndRojo" CommandName="Principal"
                                        ToolTip="Click para ver detalle del indicador." />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblTiempo" runat="server" Text="-" Font-Size="X-Small"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 72px; height: 93px; border-left: solid 1px #341010;vertical-align:top;">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnIntermedio" runat="server" Text="-1" CssClass="BotonIndRojoSecundario"
                                        CommandName="Intermedio" ToolTip="Click para ver detalle del indicador." />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblIntermedio" runat="server" Text="-" Font-Size="XX-Small"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 72px; height: 93px; border-left: solid 1px #341010; border-top: solid 1px #341010;vertical-align:top;">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnSecundario" runat="server" Text="-1" CssClass="BotonIndRojoSecundario"
                                        CommandName="Secundario" ToolTip="Click para ver detalle del indicador." />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblSecundario" runat="server" Text="-" Font-Size="XX-Small"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
