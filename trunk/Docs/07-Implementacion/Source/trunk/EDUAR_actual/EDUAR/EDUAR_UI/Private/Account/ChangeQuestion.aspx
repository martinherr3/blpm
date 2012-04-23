<%@ Page Title="Cambiar Pregunta Secreta" Language="C#" MasterPageFile="~/EDUARMaster.Master"
	AutoEventWireup="true" CodeBehind="ChangeQuestion.aspx.cs" Inherits="EDUAR_UI.ChangeQuestion" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<table class="tablaInterna" cellpadding="0" cellspacing="0">
		<tr>
			<td style="width: 80%; vertical-align: text-top">
				<h2>
					Modificar Pregunta Secreta
				</h2>
				<br />
				<table class="tablaInterna" cellpadding="0" cellspacing="0">
					<tr>
						<td>
						</td>
					</tr>
				</table>
			</td>
			<td style="width: 20%; text-align: right" rowspan="2">
				<asp:Image ID="Image1" ImageUrl="~/Images/user-properties.png" runat="server" AlternateText="Modificar Pregunta Secreta"
					ToolTip="Modificar Pregunta Secreta" />
			</td>
		</tr>
	</table>
	<p class="ui-widget">
		Utilice el siguiente formulario para modificar su pregunta y respuesta secreta.
	</p>
	<span class="failureNotification">
		<asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
	</span>
	<asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification"
		ValidationGroup="RegisterUserValidationGroup" />
	<div class="accountInfo">
		<fieldset class="register">
			<legend>Información de cuenta</legend>
			<p class="ui-widget">
				<asp:Label runat="server" AssociatedControlID="Question" ID="QuestionLabel">Pregunta Secreta:</asp:Label>
				<asp:TextBox runat="server" ID="Question" CssClass="textEntry"></asp:TextBox>
				<asp:RequiredFieldValidator runat="server" ControlToValidate="Question" CssClass="failureNotification"
					Display="Dynamic" ToolTip="Pregunta Secreta requerida." ID="QuestionRequired"
					ValidationGroup="RegisterUserValidationGroup" ErrorMessage="La Pregunta Secreta es Requerida.">*</asp:RequiredFieldValidator>
			</p>
			<p class="ui-widget">
				<asp:Label runat="server" AssociatedControlID="Answer" ID="AnswerLabel">Respuesta Secreta:</asp:Label>
				<asp:TextBox runat="server" ID="Answer" CssClass="textEntry"></asp:TextBox>
				<asp:RequiredFieldValidator runat="server" ControlToValidate="Answer" CssClass="failureNotification"
					Display="Dynamic" ToolTip="Respuesta Secreta requerida." ID="AnswerRequired"
					ValidationGroup="RegisterUserValidationGroup" ErrorMessage="La Respuesta Secreta es Requerida.">*</asp:RequiredFieldValidator>
			</p>
		</fieldset>
		<p class="submitButton">
			<asp:Button ID="btnChangeQuestion" runat="server" Text="Cambiar Pregunta" ValidationGroup="RegisterUserValidationGroup"
				OnClick="btnChangeQuestion_Click" />
		</p>
	</div>
</asp:Content>
