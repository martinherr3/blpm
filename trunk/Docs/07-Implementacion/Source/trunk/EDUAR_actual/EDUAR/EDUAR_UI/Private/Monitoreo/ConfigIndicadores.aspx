<%@ Page Title="Monitoreo General" Language="C#" MasterPageFile="~/EDUARMaster.Master"
	AutoEventWireup="true" CodeBehind="ConfigIndicadores.aspx.cs" Inherits="EDUAR_UI.ConfigIndicadores" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%--<%@ Register Src="~/UserControls/Indicadores.ascx" TagName="Indicadores" TagPrefix="ind" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<%--<ind:Indicadores ID="indicador1" runat="server" />
    <ind:Indicadores ID="Indicador2" runat="server" />--%>
	<asp:UpdatePanel ID="udpGrilla" runat="server" UpdateMode="Conditional">
		<ContentTemplate>
			<asp:GridView ID="gvwIndicadores" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListado"
				AutoGenerateColumns="false" AllowPaging="true" Width="500px" DataKeyNames="idIndicador"
				OnRowCommand="gvwIndicadores_RowCommand" OnPageIndexChanging="gvwIndicadores_PageIndexChanging">
				<Columns>
					<asp:TemplateField HeaderText="Acciones">
						<HeaderStyle HorizontalAlign="center" Width="5%" />
						<ItemStyle HorizontalAlign="center" />
						<ItemTemplate>
							<%--<asp:ImageButton ID="btnTemas" runat="server" CommandName="Temas" CommandArgument='<%# Bind("idContenido") %>'
                                    ToolTip="Ver Temas" ImageUrl="~/Images/Grillas/action_new.png" />--%>
							<asp:ImageButton ID="editarIndicador" runat="server" CommandName="Editar" CommandArgument='<%# Bind("idIndicador") %>'
								ToolTip="Editar" ImageUrl="~/Images/Grillas/action_edit.png" />
							<%--<asp:ImageButton ImageUrl="~/Images/Grillas/action_delete.png" runat="server" ID="btnEliminar"
                                    AlternateText="Eliminar" ToolTip="Eliminar" CommandName="Eliminar" CommandArgument='<%# Bind("idContenido") %>'
                                    OnClientClick="return confirm('¿Desea eliminar el contenido seleccionado?')" />--%>
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
			<%--<asp:AsyncPostBackTrigger ControlID="ddlAsignatura" EventName="SelectedIndexChanged" />--%>
		</Triggers>
	</asp:UpdatePanel>
	<%--<asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <fieldset>
                <table width="100%">
                    <tr>
                        <td align="center">
                            <table width="90%" border="0" cellpadding="0" cellspacing="10px">
                                <tr>
                                    <td class="LinkIndicador" align="center">
                                        <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click"></asp:LinkButton>
                                    </td>
                                    <td class="LinkIndicador" align="center">
                                        <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton1_Click"></asp:LinkButton>
                                    </td>
                                    <td class="LinkIndicador" align="center">
                                        <asp:LinkButton ID="LinkButton3" runat="server" onclick="LinkButton1_Click"></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="LinkIndicador" align="center">
                                        <asp:LinkButton ID="LinkButton4" runat="server" onclick="LinkButton1_Click"></asp:LinkButton>
                                    </td>
                                    <td class="LinkIndicador" align="center">
                                        <asp:LinkButton ID="LinkButton5" runat="server" onclick="LinkButton1_Click"></asp:LinkButton>
                                    </td>
                                    <td class="LinkIndicador" align="center">
                                        <asp:LinkButton ID="LinkButton6" runat="server" onclick="LinkButton1_Click"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>--%>
	<asp:UpdatePanel ID="up2" runat="server">
		<ContentTemplate>
			<div runat="server" id="divConfig" visible="false">
				<div id="div2" runat="server" class="divFondoN1">
				</div>
				<div id="div3" runat="server" class="divPopupN1">
					<div style="margin: 1px">
						<div class="EncabezadoPopup">
							<%--<asp:Label ID="lblTitulo" runat="server" Text="Indicador: "></asp:Label>--%>
						</div>
						<fieldset>
							<table border="0" cellpadding="0" width="100%">
								<tr>
									<td>
										<fieldset>
											<legend>Configuraci&oacute;n General</legend>
											<table border="0" cellpadding="0" width="100%">
												<tr>
													<td>
														<asp:Label ID="lblTituloDesc" runat="server" Text="Indicador:" ToolTip="Nombre del Indicador:"></asp:Label>
													</td>
													<td style="width: 400px">
														<asp:TextBox ID="lblTitulo" runat="server" MaxLength="3" Style="text-align: left;
															width: 350px"></asp:TextBox>
													</td>
													<td style="width: 150px">
														<asp:Label ID="lblParametroCantidad" runat="server" Text="Parámetro de Cantidad:"
															ToolTip=""></asp:Label>
													</td>
													<td>
														<asp:TextBox ID="txtParametroCantdiad" runat="server" MaxLength="3" Style="text-align: right;
															width: 50px"></asp:TextBox>
													</td>
												</tr>
												<tr>
													<td>
														<asp:Label ID="lblInvertir" runat="server" Text="Invertir Escala:" ToolTip="Activar para que el indicador sea rojo mientras más pequeño es su valor."></asp:Label>
													</td>
													<td>
														<asp:CheckBox ID="chkInvertirEscala" runat="server" />
													</td>
													<td colspan="2">
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
													</td>
													<td>
														<asp:Label ID="lblVerdePrincipal" runat="server" Text="Verde:" ToolTip="Valor hasta el cual el indicador será verde."></asp:Label>
													</td>
													<td>
														<asp:TextBox ID="txtVerdePrincipal" runat="server" MaxLength="3" Style="text-align: right;"
															Width="50px"></asp:TextBox>
													</td>
													<td>
														<asp:Label ID="Label3" runat="server" Text="Rojo:" ToolTip="Valor hasta el cual el indicador será rojo."></asp:Label>
													</td>
													<td>
														<asp:TextBox ID="txtRojoPrincipal" runat="server" MaxLength="3" Style="text-align: right;"
															Width="50px"></asp:TextBox>
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
													</td>
													<td>
														<asp:Label ID="Label4" runat="server" Text="Verde:" ToolTip="Valor hasta el cual el indicador será verde."></asp:Label>
													</td>
													<td>
														<asp:TextBox ID="txtVerdeIntermedio" runat="server" MaxLength="3" Style="text-align: right;"
															Width="50px"></asp:TextBox>
													</td>
													<td>
														<asp:Label ID="Label5" runat="server" Text="Rojo:" ToolTip="Valor hasta el cual el indicador será rojo."></asp:Label>
													</td>
													<td>
														<asp:TextBox ID="txtRojoIntermedio" runat="server" MaxLength="3" Style="text-align: right;"
															Width="50px"></asp:TextBox>
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
													</td>
													<td>
														<asp:Label ID="Label7" runat="server" Text="Verde:" ToolTip="Valor hasta el cual el indicador será verde."></asp:Label>
													</td>
													<td>
														<asp:TextBox ID="txtVerdeSecundario" runat="server" MaxLength="3" Style="text-align: right;"
															Width="50px"></asp:TextBox>
													</td>
													<td>
														<asp:Label ID="Label8" runat="server" Text="Rojo:" ToolTip="Valor hasta el cual el indicador será rojo."></asp:Label>
													</td>
													<td>
														<asp:TextBox ID="txtRojoSecundario" runat="server" MaxLength="3" Style="text-align: right;"
															Width="50px"></asp:TextBox>
													</td>
												</tr>
											</table>
										</fieldset>
									</td>
								</tr>
							</table>
						</fieldset>
						<table border="0" cellpadding="0" width="100%">
							<tr>
								<td align="right">
									<asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
									<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
								</td>
							</tr>
						</table>
					</div>
				</div>
			</div>
		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>
