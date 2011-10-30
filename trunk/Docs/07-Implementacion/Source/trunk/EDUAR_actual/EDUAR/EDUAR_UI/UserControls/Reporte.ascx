<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reporte.ascx.cs" Inherits="EDUAR_UI.UserControls.Reporte" %>
<script src="/EDUAR_UI/Scripts/JQuery.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    function AbrirPopup() {
        var popup;
        //Abrir Ventana
        popup = window.open('/EDUAR_UI/Private/Reports/PrintReport.aspx', 'Impresión de Informes', 'width=800,height=600,left=50,top=100,­menubar=0,toolbar=0,status=0,scrollbars=1,resizable=0,titlebar=0');

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
<%@ Register Src="~/UserControls/Grafico.ascx" TagName="Grafico" TagPrefix="gra" %>
<table border="0" cellpadding="0" cellspacing="0" style="min-width: 800px; width: 100%">
    <tr>
        <td>
            <table class="tablaInterna" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="right">
                        <asp:ImageButton ID="btnGraficar" runat="server" ToolTip="Graficar" ImageUrl="~/Images/Graficar.png"
                            Visible="false" />
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
                        <asp:Label ID="lblFiltros" Text="" runat="server" CssClass="lblCriterios" />
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="udpReporte" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="gvwReporte" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerReporte"
                        AutoGenerateColumns="false" Width="100%" AllowPaging="true"
                        PageSize="30">
                    </asp:GridView>
                    <asp:Label ID="lblSinDatos" runat="server" Text="La consulta no produjo resultados."
                        Visible="false"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
<div id="divGrafico" runat="server" class="divGraficoOcultar">
    <gra:Grafico ID="grafico" runat="server" />
</div>
