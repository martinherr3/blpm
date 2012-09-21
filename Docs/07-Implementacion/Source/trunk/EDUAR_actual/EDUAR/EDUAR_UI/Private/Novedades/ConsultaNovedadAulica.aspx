<%@ Page Title="" Language="C#" MasterPageFile="~/EDUARMaster.Master" AutoEventWireup="true"
	CodeBehind="ConsultaNovedadAulica.aspx.cs" Inherits="EDUAR_UI.ConsultaNovedadAulica" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Src="~/UserControls/Novedad.ascx" TagName="Novedad" TagPrefix="nov" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table class="tablaInterna" cellpadding="0" cellspacing="0">
		<tr>
			<td>
				<h2>
					Novedades Aulicas</h2>
				<br />
			</td>
			<td align="right" rowspan="2">
				<nov:Novedad ID="conNovedad" runat="server" visible="false" />
			</td>
		</tr>
	</table>
	<table class="tablaInterna" cellpadding="1" cellspacing="5">
		<tr>
			<td class="TD110px">
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
	<asp:UpdatePanel ID="udpGrilla" runat="server" UpdateMode="Conditional">
		<ContentTemplate>
			<asp:GridView ID="gvwNovedades" runat="server" SkinID="gridviewSkinPagerListado"
				OnPageIndexChanging="gvwNovedades_PageIndexChanging">
				<EmptyDataRowStyle CssClass="DatosListaNormal" HorizontalAlign="Center" />
				<Columns>
					<asp:TemplateField HeaderText="Fecha">
						<HeaderStyle HorizontalAlign="center" Width="10%" />
						<ItemStyle HorizontalAlign="center" />
						<ItemTemplate>
							<asp:Label ID="lblFechaGrilla" runat="server" Text='<%# Bind("fecha","{0:d}") %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Usuario">
						<HeaderStyle HorizontalAlign="left" Width="10%" />
						<ItemStyle HorizontalAlign="left" />
						<ItemTemplate>
							<asp:Label ID="lblUsuario" runat="server" Text='<%# String.Format("{0} {1}", Eval("usuario.nombre"), Eval("usuario.apellido")) %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Tipo de Novedad">
						<HeaderStyle HorizontalAlign="left" Width="10%" />
						<ItemStyle HorizontalAlign="left" />
						<ItemTemplate>
							<asp:Label ID="lblTipoGrilla" runat="server" Text='<%# Bind("tipo.nombre") %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Estado">
						<HeaderStyle HorizontalAlign="left" Width="10%" />
						<ItemStyle HorizontalAlign="left" />
						<ItemTemplate>
							<asp:Label ID="lblEstadoGrilla" runat="server" Text='<%# Bind("estado.nombre") %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Novedad">
						<HeaderStyle HorizontalAlign="left" Width="40%" />
						<ItemStyle HorizontalAlign="left" />
						<ItemTemplate>
							<asp:Label ID="lblObservacionesGrilla" runat="server" Text='<%# TruncateString(DataBinder.Eval(Container.DataItem, "observaciones").ToString())%>'
								ToolTip='<%# Bind("observaciones") %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
		</ContentTemplate>
		<Triggers>
			<asp:AsyncPostBackTrigger ControlID="ddlCurso" EventName="SelectedIndexChanged" />
		</Triggers>
	</asp:UpdatePanel>
</asp:Content>
