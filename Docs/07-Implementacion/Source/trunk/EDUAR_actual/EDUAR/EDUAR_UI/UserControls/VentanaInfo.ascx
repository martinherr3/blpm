<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VentanaInfo.ascx.cs"
    Inherits="EDUAR_UI.UserControls.VentanaInfo" %>
<div id="divPadre" runat="server">
    <iframe runat="server" id="divSysExceptionFrame" visible="true" class="FondoNegroVentana"
        frameborder="0" scrolling="no" width="100%" height="100%" />
    <div id="div2" runat="server" class="FondoNegroVentana" />
    <table class="TablaExternaVentana" border="0" cellpadding="0" cellspacing="0" width="100%"
        visible="true">
        <tr>
            <td>
                <table class="capTitCriterios" border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr style="height: 20px">
                        <td id="TDEncabezado" runat="server" width="100%">
                            <asp:Label ID="lblTitulo" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table id="TablaInterna" runat="server" width="100%" cellpadding="0" cellspacing="8"
                    border="0">
                    <tr>
                        <td id="TDEtiquetas" runat="server" align="center" width="10%">
                            <asp:Image ID="imgIconoVentana" runat="server" />
                        </td>
                        <td id="TDEtiquetas2" runat="server" style="text-align: left">
                            <asp:Label ID="lblDetalleSys" runat="server" Width="90%" Style="text-align: left" />
                        </td>
                    </tr>
                    <tr>
                        <td id="TDEtiquetas3" runat="server" align="right" colspan="2">
                            <asp:ImageButton ID="btnAceptar" ImageUrl="~/Images/botonOk.png" TabIndex="1" runat="server"
                                ToolTip="Aceptar" AlternateText="Aceptar" />
                            <asp:ImageButton ID="btnCancelar" ImageUrl="~/Images/botonCancel.png" TabIndex="2"
                                runat="server" ToolTip="Cancelar" Visible="false" AlternateText="Cancelar" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
