<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reporte.ascx.cs" Inherits="EDUAR_UI.UserControls.Reporte" %>
<script language="javascript" type="text/javascript">
    function abrir() {
        window.open('PrintReport.aspx', 'Impresión de Informes', 'scrollbars=No,status=yes,width=100%,height=100%'); 
        return false;
    }
    </script>
<table class="tablaInterna" cellpadding="0" cellspacing="0">
    <tr>
        <td align="right">
            <asp:ImageButton ID="btnVolver" runat="server" ToolTip="Volver" ImageUrl="~/Images/botonVolver.png"
                Visible="false" />
            <asp:ImageButton ID="btnPDF" runat="server" ToolTip="Exportar a PDF" ImageUrl="~/Images/ExportarPDF.png"
                Visible="false" />
            <asp:ImageButton ID="btnImprimir" runat="server" ImageUrl="~/Images/ExportarPDF.png"
                 />
        </td>
    </tr>
</table>
<asp:UpdatePanel ID="udpReporte" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:GridView ID="gvwReporte" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerReporte"
            AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="20">
            <EmptyDataRowStyle CssClass="DatosListaNormal" HorizontalAlign="Center" />
            <Columns>
            </Columns>
        </asp:GridView>
    </ContentTemplate>
</asp:UpdatePanel>
