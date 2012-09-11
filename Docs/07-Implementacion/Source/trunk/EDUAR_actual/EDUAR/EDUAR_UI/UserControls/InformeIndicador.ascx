<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InformeIndicador.ascx.cs"
	Inherits="EDUAR_UI.UserControls.InformeIndicador" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/Exportador.ascx" TagName="Exportador" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/Calendario.ascx" TagName="Calendario" TagPrefix="uc1" %>
<asp:UpdatePanel ID="upInforme" runat="server" UpdateMode="Conditional">
	<ContentTemplate>
		<div runat="server" id="divInforme" visible="false">
			<div id="divABM" runat="server" class="divInformeIndicador">
				<div style="margin: 1px; text-align: left;">
					<div class="EncabezadoPopup" style="text-align: center;">
						<h2>
							<asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label></h2>
					</div>
					<asp:Panel ID="Panel2" runat="server" DefaultButton="btnBuscar" Style="display: inline;
						text-align: left" BorderStyle="None">
						<table class="tablaInterna" cellpadding="0" width="100%">
							<tr>
								<td>
									Desde:
								</td>
								<td>
									<uc1:Calendario runat="server" ID="calFechaDesde" TipoCalendario="SoloFecha" />
								</td>
								<td>
									Hasta:
								</td>
								<td>
									<uc1:Calendario runat="server" ID="calFechaHasta" TipoCalendario="SoloFecha" />
								</td>
								<td align="right">
									<table width="100%">
										<td>
										</td>
										<td style="width: 10%" align="right">
											<asp:ImageButton ID="btnBuscar" ToolTip="Buscar" ImageUrl="~/Images/PopUp/botonBuscar.png"
												runat="server" OnClick="btnBuscar_Click" />
										</td>
										<td style="width: 10%" align="right">
											<uc1:Exportador ID="exp" runat="server"></uc1:Exportador>
										</td>
										<td style="width: 10%" align="right">
											<asp:ImageButton ID="btnSalir" OnClick="btnSalir_Click" runat="server" ToolTip="Volver"
												ImageUrl="~/Images/PopUp/botonVolver.png" />
										</td>
									</table>
								</td>
							</tr>
						</table>
					</asp:Panel>
					<br />
					<asp:GridView ID="grvInforme" runat="server" Width="100%" AllowPaging="true" AllowSorting="true"
						PageSize="10" OnPageIndexChanging="grvInforme_PageIndexChanging" OnSorting="grvInforme_Sorting">
					</asp:GridView>
				</div>
			</div>
		</div>
	</ContentTemplate>
</asp:UpdatePanel>
