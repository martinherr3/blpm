<%@ Page Title="Login" Language="C#" MasterPageFile="~/EDUARMaster.Master" AutoEventWireup="true"
	CodeBehind="Login.aspx.cs" Inherits="EDUAR_UI.Login" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table width="100%" cellpadding="1" cellspacing="5" border="0">
		<tr>
			<td style="width: 80%; vertical-align: text-top">
				<h2>
					Iniciar Sesión
				</h2>
				<hr />
			</td>
			<td style="width: 20%; text-align: right" rowspan="2">
				<asp:Image ID="imgLogin" ImageUrl="~/Images/web/login.png" runat="server" AlternateText="Iniciar Sesión"
					ToolTip="Iniciar Sesión" />
			</td>
		</tr>
		<tr>
			<td style="width: 80%">
				<p class="ui-widget">
					Especifique su nombre de usuario y contraseña y a continuación presione el botón
					<asp:Image ID="imgNext" ImageUrl="~/Images/botonSiguiente_small.png" runat="server"
						ToolTip="Iniciar Sesión" AlternateText="Iniciar Sesión" Style="vertical-align: middle" />
					para iniciar su sesión.
					<br />
					<br />
					Click
					<asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false">aquí</asp:HyperLink>
					para registrarse si no tiene una cuenta.
				</p>
			</td>
		</tr>
	</table>
	<table width="800px" cellpadding="1" cellspacing="5" border="0">
		<tr>
			<td style="width: 100%">
				<p class="ui-widget">
					Si ha olvidado su clave de ingreso haga click
					<asp:HyperLink ID="ForgotPasswordHyperLink" runat="server" EnableViewState="false">aquí</asp:HyperLink>
				</p>
				<asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false"
					OnAuthenticate="LoginUsuario_Authenticate" DestinationPageUrl="~/Private/Account/Welcome.aspx">
					<LayoutTemplate>
						<span class="failureNotification">
							<asp:Literal ID="FailureText" runat="server"></asp:Literal>
						</span>
						<asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
							ValidationGroup="LoginUserValidationGroup" />
						<div class="accountInfo">
							<fieldset class="login">
								<legend>Información de cuenta</legend>
								<p class="ui-widget">
									<asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Nombre de usuario:</asp:Label>
									<asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
									<asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
										CssClass="failureNotification" ErrorMessage="El nombre de usuario es obligatorio."
										ToolTip="El nombre de usuario es obligatorio." ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
								</p>
								<p class="ui-widget">
									<asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Contraseña:</asp:Label>
									<asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
									<asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
										CssClass="failureNotification" ErrorMessage="La contraseña es obligatoria." ToolTip="La contraseña es obligatoria."
										ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
								</p>
							</fieldset>
							<p class="submitButton">
								<%--<asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Iniciar sesión"
											ValidationGroup="LoginUserValidationGroup" />--%>
								<asp:ImageButton ImageUrl="~/Images/botonSiguiente.png" ID="LoginButton" runat="server"
									CommandName="Login" ToolTip="Iniciar sesión" ValidationGroup="LoginUserValidationGroup" />
							</p>
						</div>
					</LayoutTemplate>
				</asp:Login>
			</td>
		</tr>
	</table>
</asp:Content>
