<%@ Page Title="Configuración de Indicadores" Language="C#" MasterPageFile="~/EDUARMaster.Master"
	AutoEventWireup="true" CodeBehind="ConfigIndicadores.aspx.cs" Inherits="EDUAR_UI.ConfigIndicadores" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<asp:UpdatePanel ID="udpGrilla" runat="server" UpdateMode="Conditional">
		<ContentTemplate>
			<table class="tablaInterna" cellpadding="0" cellspacing="0">
				<tr>
					<td>
						<h2>
							Configuración De Indicadores</h2>
						<br />
					</td>
				</tr>
			</table>
			<table class="tablaInterna" cellpadding="0" cellspacing="0">
				<tr>
					<td style="width: 100%">
						<br />
					</td>
				</tr>
			</table>
			<asp:GridView ID="gvwIndicadores" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListado"
				AutoGenerateColumns="false" AllowPaging="true" Width="500px" DataKeyNames="idIndicador"
				OnRowCommand="gvwIndicadores_RowCommand" OnPageIndexChanging="gvwIndicadores_PageIndexChanging">
				<Columns>
					<asp:TemplateField HeaderText="Acciones">
						<HeaderStyle HorizontalAlign="center" Width="5%" />
						<ItemStyle HorizontalAlign="center" />
						<ItemTemplate>
							<asp:ImageButton ID="editarIndicador" runat="server" CommandName="Editar" CommandArgument='<%# Bind("idIndicador") %>'
								ToolTip="Editar" ImageUrl="~/Images/Grillas/action_edit.png" />
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Indicador">
						<HeaderStyle HorizontalAlign="left" Width="50%" />
						<ItemStyle HorizontalAlign="left" />
						<ItemTemplate>
							<asp:Label ID="lblDescripcionGrilla" runat="server" Text='<%# Bind("nombre") %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
		</ContentTemplate>
		<Triggers>
		</Triggers>
	</asp:UpdatePanel>
	<asp:HiddenField ID="HiddenField1" runat="server" />
	<ajaxtoolkit:ModalPopupExtender ID="mpeContenido" runat="server" PopupControlID="pnlContenidos"
		TargetControlID="HiddenField1" RepositionMode="RepositionOnWindowResizeAndScroll"
		BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="pnlContenidos">
	</ajaxtoolkit:ModalPopupExtender>
	<asp:Panel ID="pnlContenidos" runat="server" Width="600px" Height="550px" Style="display: none;
		text-align: left" CssClass="CajaDialogo">
		<asp:UpdatePanel ID="up2" runat="server">
			<ContentTemplate>
				<div runat="server" id="divConfig" visible="false">
					<table class="tablaInterna" cellpadding="0" cellspacing="0">
						<tr>
							<td>
								<h2>
									<asp:Label ID="Label9" Text="Edición de Indicadores" runat="server" /></h2>
							</td>
							<td align="right">
								<asp:ImageButton ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" ToolTip="Guardar"
									ImageUrl="~/Images/PopUp/botonGuardar.png" CausesValidation="true" />
								<asp:ImageButton ID="btnCancelar" OnClick="btnCancelar_Click" runat="server" ToolTip="Volver"
									ImageUrl="~/Images/PopUp/botonVolver.png" CausesValidation="false" />
							</td>
						</tr>
					</table>
					<fieldset>
						<table border="0" cellpadding="0" width="100%">
							<tr>
								<td>
									<fieldset>
										<legend>Indicador:
											<asp:Label ID="lblTitulo" Text="" runat="server" ToolTip="Nombre del Indicador" />
										</legend>
										<table border="0" cellpadding="0" width="100%">
											<tr>
												<td class="TD140px">
													<asp:Label ID="lblInvertir" runat="server" Text="Invertir Escala:" ToolTip="Activar para que el indicador sea rojo mientras más pequeño es su valor."></asp:Label>
												</td>
												<td class="TD50px">
													<asp:CheckBox ID="chkInvertirEscala" runat="server" />
												</td>
												<td class="TD140px">
													<asp:Label ID="lblParametroCantidad" runat="server" Text="Parámetro de Cantidad:"
														ToolTip=""></asp:Label>
												</td>
												<td>
													<asp:TextBox ID="txtParametroCantidad" runat="server" MaxLength="2" Style="text-align: right;
														width: 50px"></asp:TextBox>
													<ajaxtoolkit:FilteredTextBoxExtender ID="filParametroCantidad" runat="server" FilterMode="ValidChars"
														FilterType="Numbers" TargetControlID="txtParametroCantidad">
													</ajaxtoolkit:FilteredTextBoxExtender>
													<asp:RequiredFieldValidator ID="rfvTxtParametroCantidad" ErrorMessage="*" ControlToValidate="txtParametroCantidad"
														runat="server" Enabled="false" ForeColor="Red" ToolTip="Campo Requerido" Display="Dynamic" />
													<asp:CompareValidator ID="cmvtxtParametroCantidad" ErrorMessage="*" ControlToValidate="txtParametroCantidad"
														runat="server" Operator="GreaterThan" ValueToCompare="0" Type="Integer" ForeColor="Red"
														Display="Dynamic" ToolTip="Campo Requerido" />
												</td>
											</tr>
										</table>
									</fieldset>
								</td>
							</tr>
							<tr>
								<td>
									<fieldset>
										<legend>Principal</legend>
										<table border="0" cellpadding="0" width="100%">
											<tr>
												<td>
													<asp:Label ID="Label1" runat="server" Text="Cantidad de días:" ToolTip="Cantidad de días a partir del del dia de la fecha para la obtención del indicador."></asp:Label>
												</td>
												<td>
													<asp:TextBox ID="txtHastaPrincipal" runat="server" MaxLength="3" Style="text-align: right;"
														Width="50px"></asp:TextBox>
													<ajaxtoolkit:FilteredTextBoxExtender ID="filtxtHastaPrincipal" runat="server" FilterMode="ValidChars"
														FilterType="Numbers" TargetControlID="txtHastaPrincipal">
													</ajaxtoolkit:FilteredTextBoxExtender>
												</td>
												<td>
													<asp:Label ID="lblVerdePrincipal" runat="server" Text="Verde:" ToolTip="Valor hasta el cual el indicador será verde."></asp:Label>
												</td>
												<td>
													<asp:TextBox ID="txtVerdePrincipal" runat="server" MaxLength="3" Style="text-align: right;"
														Width="50px"></asp:TextBox>
													<ajaxtoolkit:FilteredTextBoxExtender ID="filtxtVerdePrincipal" runat="server" FilterMode="ValidChars"
														FilterType="Numbers" TargetControlID="txtVerdePrincipal">
													</ajaxtoolkit:FilteredTextBoxExtender>
												</td>
												<td>
													<asp:Label ID="Label3" runat="server" Text="Rojo:" ToolTip="Valor hasta el cual el indicador será rojo."></asp:Label>
												</td>
												<td>
													<asp:TextBox ID="txtRojoPrincipal" runat="server" MaxLength="3" Style="text-align: right;"
														Width="50px"></asp:TextBox>
													<ajaxtoolkit:FilteredTextBoxExtender ID="filtxtRojoPrincipal" runat="server" FilterMode="ValidChars"
														FilterType="Numbers" TargetControlID="txtRojoPrincipal">
													</ajaxtoolkit:FilteredTextBoxExtender>
												</td>
											</tr>
										</table>
									</fieldset>
								</td>
							</tr>
							<tr>
								<td>
									<fieldset>
										<legend>Intermedio</legend>
										<table border="0" cellpadding="0" width="100%">
											<tr>
												<td>
													<asp:Label ID="Label2" runat="server" Text="Cantidad de días:" ToolTip="Cantidad de días a partir del del dia de la fecha para la obtención del indicador."></asp:Label>
												</td>
												<td>
													<asp:TextBox ID="txtHastaIntermedio" runat="server" MaxLength="3" Style="text-align: right;"
														Width="50px"></asp:TextBox>
													<ajaxtoolkit:FilteredTextBoxExtender ID="filtxtHastaIntermedio" runat="server" FilterMode="ValidChars"
														FilterType="Numbers" TargetControlID="txtHastaIntermedio">
													</ajaxtoolkit:FilteredTextBoxExtender>
												</td>
												<td>
													<asp:Label ID="Label4" runat="server" Text="Verde:" ToolTip="Valor hasta el cual el indicador será verde."></asp:Label>
												</td>
												<td>
													<asp:TextBox ID="txtVerdeIntermedio" runat="server" MaxLength="3" Style="text-align: right;"
														Width="50px"></asp:TextBox>
													<ajaxtoolkit:FilteredTextBoxExtender ID="filtxtVerdeIntermedio" runat="server" FilterMode="ValidChars"
														FilterType="Numbers" TargetControlID="txtVerdeIntermedio">
													</ajaxtoolkit:FilteredTextBoxExtender>
												</td>
												<td>
													<asp:Label ID="Label5" runat="server" Text="Rojo:" ToolTip="Valor hasta el cual el indicador será rojo."></asp:Label>
												</td>
												<td>
													<asp:TextBox ID="txtRojoIntermedio" runat="server" MaxLength="3" Style="text-align: right;"
														Width="50px"></asp:TextBox>
													<ajaxtoolkit:FilteredTextBoxExtender ID="filtxtRojoIntermedio" runat="server" FilterMode="ValidChars"
														FilterType="Numbers" TargetControlID="txtRojoIntermedio">
													</ajaxtoolkit:FilteredTextBoxExtender>
												</td>
											</tr>
										</table>
									</fieldset>
								</td>
							</tr>
							<tr>
								<td>
									<fieldset>
										<legend>Secundario</legend>
										<table border="0" cellpadding="0" width="100%">
											<tr>
												<td>
													<asp:Label ID="Label6" runat="server" Text="Cantidad de días:" ToolTip="Cantidad de días a partir del del dia de la fecha para la obtención del indicador."></asp:Label>
												</td>
												<td>
													<asp:TextBox ID="txtHastaSecundario" runat="server" MaxLength="3" Style="text-align: right;"
														Width="50px"></asp:TextBox>
													<ajaxtoolkit:FilteredTextBoxExtender ID="filtxtHastaSecundario" runat="server" FilterMode="ValidChars"
														FilterType="Numbers" TargetControlID="txtHastaSecundario">
													</ajaxtoolkit:FilteredTextBoxExtender>
												</td>
												<td>
													<asp:Label ID="Label7" runat="server" Text="Verde:" ToolTip="Valor hasta el cual el indicador será verde."></asp:Label>
												</td>
												<td>
													<asp:TextBox ID="txtVerdeSecundario" runat="server" MaxLength="3" Style="text-align: right;"
														Width="50px"></asp:TextBox>
													<ajaxtoolkit:FilteredTextBoxExtender ID="filtxtVerdeSecundario" runat="server" FilterMode="ValidChars"
														FilterType="Numbers" TargetControlID="txtVerdeSecundario">
													</ajaxtoolkit:FilteredTextBoxExtender>
												</td>
												<td>
													<asp:Label ID="Label8" runat="server" Text="Rojo:" ToolTip="Valor hasta el cual el indicador será rojo."></asp:Label>
												</td>
												<td>
													<asp:TextBox ID="txtRojoSecundario" runat="server" MaxLength="3" Style="text-align: right;"
														Width="50px"></asp:TextBox>
													<ajaxtoolkit:FilteredTextBoxExtender ID="filtxtRojoSecundario" runat="server" FilterMode="ValidChars"
														FilterType="Numbers" TargetControlID="txtRojoSecundario">
													</ajaxtoolkit:FilteredTextBoxExtender>
												</td>
											</tr>
										</table>
									</fieldset>
								</td>
							</tr>
						</table>
					</fieldset>
				</div>
			</ContentTemplate>
		</asp:UpdatePanel>
	</asp:Panel>
</asp:Content>
