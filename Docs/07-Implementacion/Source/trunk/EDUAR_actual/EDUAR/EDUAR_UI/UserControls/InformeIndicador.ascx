<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InformeIndicador.ascx.cs"
	Inherits="EDUAR_UI.UserControls.InformeIndicador" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/Exportador.ascx" TagName="Exportador" TagPrefix="uc1" %>
<asp:UpdatePanel ID="upInforme" runat="server" UpdateMode="Conditional">
	<ContentTemplate>
		<div runat="server" id="divInforme" visible="false">
			<div id="divFondo" runat="server" class="divFondoN1">
			</div>
			<div id="divABM" runat="server" class="divInformeIndicador">
				<div style="margin: 1px; text-align: left;">
					<div class="EncabezadoPopup" style="text-align: center;">
						<asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label>
					</div>
					<fieldset>
						<asp:Panel ID="Panel2" runat="server" DefaultButton="btnBuscar">
							<table border="0" cellpadding="0" width="100%">
								<tr>
									<td>
										Desde:
									</td>
									<td>
										<asp:TextBox ID="txtDesde" runat="server" Width="75px"></asp:TextBox>
										<asp:Button runat="Server" ID="ImageButton3" SkinID="BotonFecha" />
										<cc1:CalendarExtender runat="server" ID="CalendarExtender3" Format="dd/MM/yyyy" TargetControlID="txtDesde"
											PopupButtonID="ImageButton3" />
										<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDesde"
											ValidationGroup="buscar" ErrorMessage="*"></asp:RequiredFieldValidator>
										<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*"
											ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((1[6-9]|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"
											ControlToValidate="txtDesde" ValidationGroup="buscar"></asp:RegularExpressionValidator>
									</td>
									<td>
										Hasta:
									</td>
									<td>
										<asp:TextBox ID="txtHasta" runat="server" Width="75px"></asp:TextBox>
										<asp:Button runat="Server" ID="ImageButton1" SkinID="BotonFecha" />
										<cc1:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtHasta"
											PopupButtonID="ImageButton1" />
										<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtHasta"
											ValidationGroup="buscar" ErrorMessage="*"></asp:RequiredFieldValidator>
										<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="*"
											ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((1[6-9]|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"
											ControlToValidate="txtHasta" ValidationGroup="buscar"></asp:RegularExpressionValidator>
										<asp:CompareValidator ID="cvComparaFecha" runat="server" ErrorMessage="*" ToolTip="La fecha Desde no puede ser mayor a la fecha Hasta."
											ValidationGroup="buscar" ControlToValidate="txtDesde" ControlToCompare="txtHasta"
											Operator="LessThanEqual" Type="Date"></asp:CompareValidator>
									</td>
									<td align="right">
										<asp:Button ID="btnBuscar" runat="server" Text="Buscar" ValidationGroup="buscar"
											OnClick="btnBuscar_Click" />
									</td>
								</tr>
							</table>
						</asp:Panel>
						<cc1:Accordion ID="MyAccordion" runat="Server" SelectedIndex="-1" ContentCssClass="accordionContentInforme"
							HeaderCssClass="accordionHeaderInforme" AutoSize="None" FadeTransitions="true"
							TransitionDuration="50" FramesPerSecond="60" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
							<Panes>
								<cc1:AccordionPane ID="pnlFiltros" runat="server">
									<Header>
										Filtros</Header>
									<Content>
										<asp:Panel ID="Panel1" runat="server" DefaultButton="btnFiltrar">
											<table border="0" cellpadding="0" width="100%">
												<tr>
													<td>
														<asp:Label ID="lblFiltro1" runat="server" Text=""></asp:Label>
													</td>
													<td>
														<asp:TextBox ID="txtFiltro1" runat="server"></asp:TextBox>
													</td>
													<td>
														<asp:Label ID="lblFiltro2" runat="server" Text=""></asp:Label>
													</td>
													<td>
														<asp:TextBox ID="txtFiltro2" runat="server"></asp:TextBox>
													</td>
													<td>
														<asp:Label ID="lblFiltro3" runat="server" Text=""></asp:Label>
													</td>
													<td>
														<asp:TextBox ID="txtFiltro3" runat="server"></asp:TextBox>
													</td>
												</tr>
												<tr>
													<td>
														<asp:Label ID="lblFiltro4" runat="server" Text=""></asp:Label>
													</td>
													<td>
														<asp:TextBox ID="txtFiltro4" runat="server"></asp:TextBox>
													</td>
													<td>
														<asp:Label ID="lblFiltro5" runat="server" Text=""></asp:Label>
													</td>
													<td>
														<asp:TextBox ID="txtFiltro5" runat="server"></asp:TextBox>
													</td>
													<td>
														<asp:Label ID="lblFiltro6" runat="server" Text=""></asp:Label>
													</td>
													<td>
														<asp:TextBox ID="txtFiltro6" runat="server"></asp:TextBox>
													</td>
												</tr>
												<tr>
													<td colspan="9" align="right">
														<asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" OnClick="btnFiltrar_Click" />
													</td>
												</tr>
											</table>
										</asp:Panel>
									</Content>
								</cc1:AccordionPane>
							</Panes>
						</cc1:Accordion>
					</fieldset>
					<br />
					<div style="width: 100%; text-align: right;">
						<uc1:Exportador ID="exp" runat="server">
						</uc1:Exportador>
					</div>
					<asp:GridView ID="grvInforme" runat="server" Width="100%" AllowPaging="true" AllowSorting="true"
						PageSize="10" OnPageIndexChanging="grvInforme_PageIndexChanging" OnSorting="grvInforme_Sorting">
					</asp:GridView>
					<div style="width: 100%; text-align: right; margin-top: 2px;">
						<asp:Button ID="btnSalir" runat="server" Text="Salir" OnClick="btnSalir_Click" />
						<br />
					</div>
				</div>
			</div>
		</div>
	</ContentTemplate>
</asp:UpdatePanel>
