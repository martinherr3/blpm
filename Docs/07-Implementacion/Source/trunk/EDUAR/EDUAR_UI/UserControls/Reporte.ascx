<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reporte.ascx.cs" Inherits="EDUAR_UI.UserControls.Reporte" %>
<script language="javascript" type="text/javascript">
    function abrir() {
        window.open('PrintReport.aspx', 'Impresión de Informes', 'scrollbars=No,status=yes,width=800,height=600');
        return false;
    }

    function AbrirPopup() {
        var popup;
        //Abrir Ventana
        popup = window.open('PrintReport.aspx', 'Impresión de Informes', 'width=800,height=600,left=50,top=100,­menubar=0,toolbar=0,status=0,scrollbars=1,resizable=0,titlebar=0');

        //Armar documento
        popup.document.close();
    }
</script>
<table class="tablaInterna" cellpadding="0" cellspacing="0">
    <tr>
        <td align="right">
            <asp:ImageButton ID="btnVolver" runat="server" ToolTip="Volver" ImageUrl="~/Images/botonVolver.png"
                Visible="false" />
            <asp:ImageButton ID="btnPDF" runat="server" ToolTip="Exportar a PDF" ImageUrl="~/Images/ExportarPDF.png"
                Visible="false" />
            <asp:ImageButton ID="btnImprimir" runat="server" ToolTip="Imprimir" ImageUrl="~/Images/botonImprimir.png"
                Visible="false" OnClick="btnImprimir_Click" />
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
            AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="20">
            <EmptyDataRowStyle CssClass="DatosListaNormal" HorizontalAlign="Center" />
        </asp:GridView>
    </ContentTemplate>
</asp:UpdatePanel>
