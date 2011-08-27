﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reporte.ascx.cs" Inherits="EDUAR_UI.UserControls.Reporte" %>
<script language="javascript" type="text/javascript">
    function AbrirPopup() {
        var popup;
        //Abrir Ventana
        popup = window.open('PrintReport.aspx', 'Impresión de Informes', 'width=800,height=600,left=50,top=100,­menubar=0,toolbar=0,status=0,scrollbars=1,resizable=0,titlebar=0');

        if (popup == null || typeof (popup) == 'undefined') {
            alert('Por favor deshabilita el bloqueador de ventanas emergentes y vuelve a hacer clic en "Imprimir".');
        }
        else {
            popup.focus();
            //Armar documento
            popup.document.close();
        }
    }
</script>
<table border="0" cellpadding="0" cellspacing="0" width="800px">
    <tr>
        <td>
            <table class="tablaInterna" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="right">
                        <asp:ImageButton ID="btnPDF" runat="server" ToolTip="Exportar a PDF" ImageUrl="~/Images/ExportarPDF.png"
                            Visible="false" />
                        <asp:ImageButton ID="btnImprimir" runat="server" ToolTip="Imprimir" ImageUrl="~/Images/botonImprimir.png"
                            Visible="false" OnClick="btnImprimir_Click" />
                        <asp:ImageButton ID="btnVolver" runat="server" ToolTip="Volver" ImageUrl="~/Images/botonVolver.png"
                            Visible="false" />
                    </td>
                </tr>
            </table>
            <table class="tablaInterna" cellpadding="1" cellspacing="5">
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="udpReporte" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="gvwReporte" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerReporte"
                        AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="30">
                        <%--<EmptyDataRowStyle CssClass="DatosListaNormal" HorizontalAlign="Center" />--%>
                    </asp:GridView>
                    <asp:Label ID="lblSinDatos" runat="server" Text="La consulta no produjo resultados."
                        Visible="false"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
