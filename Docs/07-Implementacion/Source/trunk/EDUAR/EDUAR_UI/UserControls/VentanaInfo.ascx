<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VentanaInfo.ascx.cs"
    Inherits="EDUAR_UI.UserControls.VentanaInfo" %>

<%--<style type="text/css">
    html, body, div, iframe
    {
        margin: 0;
        padding: 0;
        height: 100%;
    }
    .pol
    {
        display: block;
        width: 100%;
        border: none;
        background: #000000;
        background-color: Gray;
        position: fixed; /**position:absolute;*/
        border: 1px solid #cecece;
        z-index: 990;
        -moz-opacity: 0.5;
        -khtml-opacity: 0.5; /*opacity: 0.5;
	        -ms-filter: progid:DXImageTransform.Microsoft.Alpha(Opacity=50); 
	        filter: alpha(opacity=50);*/
        visibility: inherit;
    }
</style>--%>
<div id="divPadre" runat="server">

<%--<div id="divPadre" runat="server" class="DivVentana">--%>
 <iframe runat="server" id="divSysExceptionFrame" visible="true" class="FondoNegroVentana" frameborder="0" scrolling="no" width="100%" height="100%"  />
    <div id="div2" runat="server" class="FondoNegroVentana" />

    <table class="TablaExternaVentana" border="0" cellpadding="0" cellspacing="0"
        width="100%" visible="true">
        <tr>
            <td>
                <table class="capTitCriterios" border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr style="height: 20px">
                        <td id="TDEncabezado" runat="server" width="100%" >
                            <asp:Label ID="lblTitulo" runat="server" ></asp:Label>
                        </td>
                    </tr>
                </table>
                <table id="TablaInterna" runat="server" width="100%" cellpadding="0" cellspacing="8" border="0">
                    <tr>
                        <td id="TDEtiquetas" runat="server" align="center" width="10%">
                            <asp:Image ID="imgIconoVentana" runat="server" />
                        </td>
                        <td id="TDEtiquetas2" runat="server"  style="text-align: left">
                            <asp:Label ID="lblDetalleSys" runat="server" Width="80%" Style="text-align: left" />
                        </td>
                    </tr>
                    <tr>
                        <td id="TDEtiquetas3" runat="server" align="right" colspan="2">
                            <asp:Button ID="btnAceptar" Text="Aceptar" runat="server" TabIndex="1"/>
                            <asp:Button ID="btnCancelar" Text="Cancelar" runat="server" TabIndex="2" Visible="false"/>
                            <%--<uc1:BotonPersonalizado ID="btnAceptar" runat="server" TipoBoton="Aceptar" TabIndex="1"/>
                            <uc1:BotonPersonalizado ID="btnCancelar" runat="server" TipoBoton="Cancelar" TabIndex="2" Visible="false"/>--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
