<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Novedad.ascx.cs" Inherits="EDUAR_UI.UserControls.Novedad" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script type="text/javascript">

	function alerta() {
		alert('La Novedad ha sido Registrada.');
	}

</script>
<asp:ImageButton ID="btnNuevaNovedad" ToolTip="Nueva Novedad Aulica" ImageUrl="~/Images/botonNovedades.png"
	runat="server" OnClick="btnNuevaNovedad_Click" />
<asp:HiddenField ID="HiddenField1" runat="server" />
<ajaxToolkit:ModalPopupExtender ID="mpeNueva" runat="server" PopupControlID="pnlNueva"
	OkControlID="btnGuardar" TargetControlID="HiddenField1" RepositionMode="RepositionOnWindowResizeAndScroll"
	BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="pnlNueva"
	OnOkScript="javascript:alerta();">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnlNueva" runat="server" DefaultButton="btnGuardar" Width="600px"
	Height="400px" Style="display: none; text-align: left" BorderStyle="Groove" CssClass="CajaDialogo">
	<asp:UpdatePanel ID="udpNueva" runat="server" UpdateMode="Conditional">
		<ContentTemplate>
			<table class="tablaInterna" cellpadding="0" cellspacing="0">
				<tr>
					<td>
						<h2>
							Registrar Novedad Aulica</h2>
						<br />
					</td>
					<td align="right">
						<asp:ImageButton ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" ToolTip="Guardar"
							ImageUrl="~/Images/PopUp/botonGuardar.png" Visible="true" />
						<asp:ImageButton ID="btnVolver" OnClick="btnVolver_Click" runat="server" ToolTip="Volver"
							ImageUrl="~/Images/PopUp/botonVolver.png" />
					</td>
				</tr>
			</table>
			<table class="tablaInterna" cellpadding="1" cellspacing="5">
				<tr>
					<td class="TD140px">
						<asp:Label ID="lblEstado" runat="server" Text="Estado:" CssClass="lblCriterios"></asp:Label>
					</td>
					<td class="TD140px">
						<asp:DropDownList ID="ddlEstado" runat="server">
						</asp:DropDownList>
					</td>
					<td>
					</td>
				</tr>
				<tr>
					<td class="TD140px">
						<asp:Label ID="lblTipo" runat="server" Text="Tipo Novedad:" CssClass="lblCriterios"></asp:Label>
					</td>
					<td class="TD140px">
						<asp:DropDownList ID="ddlNovedad" runat="server">
						</asp:DropDownList>
					</td>
					<td>
					</td>
				</tr>
				<tr>
					<td class="TD140px" style="vertical-align: text-top">
						<asp:Label ID="lblObservaciones" runat="server" Text="Comentario:"></asp:Label><br />
					</td>
					<td colspan="2">
						<asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" Columns="40"
							Rows="10" />
					</td>
				</tr>
			</table>
		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Panel>
