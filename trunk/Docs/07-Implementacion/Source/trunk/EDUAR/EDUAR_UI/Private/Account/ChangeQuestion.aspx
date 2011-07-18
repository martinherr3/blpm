<%@ Page Language="C#" MasterPageFile="~/EDUARMaster.Master" AutoEventWireup="true"
    CodeBehind="ChangeQuestion.aspx.cs" Inherits="EDUAR_UI.ChangeQuestion" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Modificación de Pregunta Secreta
    </h2>
    <p>
        Use el formulario siguiente para modificar su pregunta y respuesta secreta.
    </p>
    <span class="failureNotification">
        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
    </span>
    <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification"
        ValidationGroup="RegisterUserValidationGroup" />
    <div class="accountInfo">
        <fieldset class="register">
            <legend>Información de cuenta</legend>
            <p>
                <asp:Label runat="server" AssociatedControlID="Question" ID="QuestionLabel">Pregunta Secreta:</asp:Label>
                <asp:TextBox runat="server" ID="Question" CssClass="textEntry"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Question" CssClass="failureNotification"
                    ToolTip="Pregunta Secreta requerida." ID="QuestionRequired" ValidationGroup="Createuserwizard1"
                    ErrorMessage="La Pregunta Secreta es Requerida.">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:Label runat="server" AssociatedControlID="Answer" ID="AnswerLabel">Respuesta Secreta:</asp:Label>
                <asp:TextBox runat="server" ID="Answer" CssClass="textEntry"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Answer" CssClass="failureNotification"
                    ToolTip="Respuesta Secreta requerida." ID="AnswerRequired" ValidationGroup="Createuserwizard1"
                    ErrorMessage="La Respuesta Secreta es Requerida.">*</asp:RequiredFieldValidator>
            </p>
        </fieldset>
        <p class="submitButton">
            <asp:Button ID="btnChangeQuestion" runat="server" CommandName="MoveNext" Text="Cambiar Pregunta"
                ValidationGroup="RegisterUserValidationGroup" OnClick="btnChangeQuestion_Click" />
        </p>
    </div>
</asp:Content>
