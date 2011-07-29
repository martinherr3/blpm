<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reporte.ascx.cs" Inherits="EDUAR_UI.UserControls.Reporte" %>
<table class="tablaInterna" cellpadding="0" cellspacing="0">
    <tr>
        <td align="right">
            <asp:ImageButton ID="btnVolver" runat="server" ToolTip="Volver" ImageUrl="~/Images/botonVolver.png"
                Visible="false" />
            <asp:ImageButton ID="btnPDF" runat="server" ToolTip="Exportar a PDF" ImageUrl="~/Images/ExportarPDF.png"
                Visible="false" />
        </td>
    </tr>
</table>
<asp:UpdatePanel ID="udpReporte" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:GridView ID="gvwReporte" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerReporte"
            AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="20" ><%--OnPageIndexChanging="gvwReporte_PageIndexChanging"--%>
            <EmptyDataRowStyle CssClass="DatosListaNormal" HorizontalAlign="Center" />
            <Columns>
            </Columns>
        </asp:GridView>
    </ContentTemplate>
</asp:UpdatePanel>
