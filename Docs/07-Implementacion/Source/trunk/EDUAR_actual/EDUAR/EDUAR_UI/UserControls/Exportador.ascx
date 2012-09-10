<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Exportador.ascx.cs"
    Inherits="EDUAR_UI.UserControls.Exportador" %>
<asp:UpdatePanel ID="upInforme" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Button ID="btnExportar" runat="server" OnClick="btnExportar_Click" SkinID="BotonWord" />
        <div id="divHTML" runat="server" visible="false">
            <%--<div id="div1" runat="server" style='border: solid 1px #341010; padding: 2px;'>--%>
                <table width="1000px" border="0" cellpadding="0" cellspacing="0">
                    <tr style="color: #FFFFFF; text-align: center; background-color: #6495ED;">
                        <td align="left" style="width: 30%; padding-left: 10px;">
                            <h3>
                                EDU@R</h3>
                        </td>
                        <td style="width: 50%;" />
                        <td align="right" style="width: 20%; padding-right: 10px;">
                            <h4>
                                <asp:Label ID="lblFecha" runat="server" Text=""></asp:Label>
                            </h4>
                        </td>
                    </tr>
                    <tr style="color: #FFFFFF; text-align: center; background-color: #341010; padding-bottom: 10px;">
                        <td>
                        </td>
                        <td align="center">
                            <h3>
                                <asp:Label ID="lblTitulo" Font-Underline="true" runat="server" Text=""></asp:Label>
                            </h3>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <br />
                            <asp:Label ID="lblObservaciones" runat="server" Text=""></asp:Label>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <td colspan="3">
                        §
                    </td>
                </table>
           <%-- </div>--%>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnExportar" />
    </Triggers>
</asp:UpdatePanel>
