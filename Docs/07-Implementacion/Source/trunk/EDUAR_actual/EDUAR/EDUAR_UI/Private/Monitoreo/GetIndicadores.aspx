<%@ Page Title="Obtener Indicadores" Language="C#" MasterPageFile="~/EDUARMaster.Master"
	AutoEventWireup="true" CodeBehind="GetIndicadores.aspx.cs" Inherits="EDUAR_UI.GetIndicadores" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Src="~/UserControls/Indicador.ascx" TagName="Indicador" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/InformeIndicador.ascx" TagName="InformeIndicador"
	TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table class="tablaInterna" cellpadding="0" cellspacing="0">
		<tr>
			<td class="TD50">
				<h2>
					Seleccione un Curso para ver los Indicadores</h2>
				<br />
			</td>
			<td class="TD50px">
				<asp:Label ID="lblCurso" runat="server" Text="Curso:" CssClass="lblCriterios"></asp:Label>
			</td>
			<td class="TD140px">
				<asp:DropDownList ID="ddlCurso" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCurso_SelectedIndexChanged">
				</asp:DropDownList>
			</td>
			<td>
			</td>
		</tr>
	</table>
	<table class="tablaInterna" cellpadding="1" cellspacing="5">
		<tr>
			<td>
			</td>
		</tr>
	</table>
	<asp:UpdatePanel runat="server" ID="udpIndicadores" UpdateMode="Conditional">
		<ContentTemplate>
			<table width="100%" cellpadding="0" cellspacing="0">
				<tr>
					<td align="center" style="width: 70%;">
						<div runat="server" id="divIndicadores" visible="false">
							<table style="width: 100%;" cellpadding="1" cellspacing="5">
								<tr>
									<td style="width: 50%; text-align: center; padding-left: 50px">
										<uc1:Indicador ID="Indicador1" runat="server" Visible="false"></uc1:Indicador>
									</td>
									<td style="width: 50%; text-align: center; padding-left: 50px">
										<uc1:Indicador ID="Indicador2" runat="server" Visible="false"></uc1:Indicador>
									</td>
								</tr>
								<tr>
									<td style="width: 50%; text-align: center; padding-left: 50px">
										<uc1:Indicador ID="Indicador3" runat="server" Visible="false"></uc1:Indicador>
									</td>
									<td style="width: 50%; text-align: center; padding-left: 50px">
										<uc1:Indicador ID="Indicador4" runat="server" Visible="false"></uc1:Indicador>
									</td>
								</tr>
								<tr>
									<td style="width: 50%; text-align: center; padding-left: 50px">
										<uc1:Indicador ID="Indicador5" runat="server" Visible="false"></uc1:Indicador>
									</td>
									<td style="width: 50%; text-align: center; padding-left: 50px">
										<uc1:Indicador ID="Indicador6" runat="server" Visible="false"></uc1:Indicador>
									</td>
								</tr>
							</table>
						</div>
					</td>
					<td valign="top">
						<div runat="server" id="divNovedades" visible="false">
							<table width="100%" style="text-align: center;">
								<tr>
									<td align="center" style="border-bottom-style: solid; border-bottom-width: thin;
										border-bottom-color: #341010;">
										<h2>
											NOVEDADES DEL CURSO</h2>
									</td>
								</tr>
								<tr>
									<td align="justify">
										<asp:Label ID="lblNoHay" runat="server" Text="No se registraron Novedades durante la última semana." Font-Size="Medium"></asp:Label>
										<div id="divNotificaciones" runat="server">
										</div>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
			</table>
		</ContentTemplate>
		<Triggers>
			<asp:AsyncPostBackTrigger ControlID="ddlCurso" EventName="SelectedIndexChanged" />
		</Triggers>
	</asp:UpdatePanel>
	<asp:HiddenField ID="HiddenField1" runat="server" />
	<ajaxToolkit:ModalPopupExtender ID="mpeContenido" runat="server" PopupControlID="pnlContenidos"
		TargetControlID="HiddenField1" RepositionMode="RepositionOnWindowResizeAndScroll"
		BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="pnlContenidos">
	</ajaxToolkit:ModalPopupExtender>
	<asp:Panel ID="pnlContenidos" runat="server" Width="700px" Height="650px" Style="display: none;
		text-align: left" BorderStyle="Groove" CssClass="CajaDialogo">
		<uc1:InformeIndicador ID="InformeIndicador1" runat="server"></uc1:InformeIndicador>
	</asp:Panel>
</asp:Content>
