<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Exportador.ascx.cs"
    Inherits="EDUAR_UI.UserControls.Exportador" %>
<asp:UpdatePanel ID="upInforme" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:ImageButton ID="btnExportar" ToolTip="Exportar PDF" ImageUrl="~/Images/PopUp/ExportarPDF.png"
            runat="server" OnClick="btnExportar_Click" CommandName="ExportPDF" />
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnExportar" />
        <asp:PostBackTrigger ControlID="btnExcel" />
    </Triggers>
</asp:UpdatePanel>
