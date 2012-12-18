<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Exportador.ascx.cs"
	Inherits="EDUAR_UI.UserControls.Exportador" %>
<asp:UpdatePanel ID="upInforme" runat="server" UpdateMode="Conditional">
	<ContentTemplate>
		<asp:ImageButton ID="btnExportar" ToolTip="Exportar" ImageUrl="~/Images/PopUp/ExportarPDF.png"
			runat="server" OnClick="btnExportar_Click" />
	</ContentTemplate>
	<Triggers>
		<asp:PostBackTrigger ControlID="btnExportar" />
	</Triggers>
</asp:UpdatePanel>
