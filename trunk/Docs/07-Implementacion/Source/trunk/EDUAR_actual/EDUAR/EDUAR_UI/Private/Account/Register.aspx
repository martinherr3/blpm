<%@ Page Title="Registrarse" Language="C#" MasterPageFile="~/EDUARMaster.master"
    AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="EDUAR_UI.Register" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:CreateUserWizard ID="RegisterUser" runat="server" EnableViewState="false" OnCreatedUser="RegisterUser_CreatedUser"
        ContinueDestinationPageUrl="~/Private/Account/Welcome.aspx" 
        InvalidPasswordErrorMessage="Longitud mínima de la contraseña: {0}.">
        <LayoutTemplate>
            <asp:PlaceHolder ID="wizardStepPlaceholder" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="navigationPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <WizardSteps>
            <asp:CreateUserWizardStep ID="RegisterUserWizardStep" runat="server">
                <ContentTemplate>
                    <table width="100%" cellpadding="1" cellspacing="5" border="0">
                        <tr>
                            <td style="width: 80%; vertical-align: text-top">
                                <h2>
                                    Crear una Nueva Cuenta<hr />
                                </h2>
                            </td>
                            <td style="width: 20%; text-align: right" rowspan="2">
                                <asp:Image ID="Image1" ImageUrl="~/Images/web/new-user.png" runat="server" AlternateText="Nuevo Usuario"
                                    ToolTip="Nuevo Usuario" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 80%">
                                <p class="ui-widget">
                                    Utiliza el siguiente formulario para crear una cuenta nueva.
                                </p>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" cellpadding="1" cellspacing="5" border="0">
                        <tr>
                            <td style="width: 100%">
                                <p class="ui-widget">
                                    Las contraseñas deben tener una longitud mínima de
                                    <%= Membership.MinRequiredPasswordLength %>
                                    caracteres.
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%">
                                <span class="failureNotification">
                                    <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                                </span>
                                <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification"
                                    ValidationGroup="RegisterUserValidationGroup" />
                                <div class="accountInfo">
                                    <fieldset class="register">
                                        <legend>Información de cuenta</legend>
                                        <p class="ui-widget">
                                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Nombre de usuario:</asp:Label>
                                            <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                CssClass="failureNotification" ErrorMessage="El nombre de usuario es obligatorio."
                                                ToolTip="El nombre de usuario es obligatorio." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                        </p>
                                        <p class="ui-widget">
                                            <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">Correo electrónico:</asp:Label>
                                            <asp:TextBox ID="Email" runat="server" CssClass="textEntry"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                                                CssClass="failureNotification" ErrorMessage="El correo electrónico es obligatorio."
                                                ToolTip="El correo electrónico es obligatorio." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                        </p>
                                        <p class="ui-widget">
                                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Contraseña:</asp:Label>
                                            <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                CssClass="failureNotification" ErrorMessage="La contraseña es obligatoria." ToolTip="La contraseña es obligatoria."
                                                ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ErrorMessage="La contraseña debe contener caracteres alfanuméricos"
                                                ToolTip="La contraseña debe contener caracteres alfanuméricos" ControlToValidate="Password"
                                                Display="Dynamic" runat="server" ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{2,10})$"
                                                ValidationGroup="RegisterUserValidationGroup" CssClass="failureNotification">*</asp:RegularExpressionValidator>
                                        </p>
                                        <p class="ui-widget">
                                            <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirmar contraseña:</asp:Label>
                                            <asp:TextBox ID="ConfirmPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ControlToValidate="ConfirmPassword" CssClass="failureNotification"
                                                Display="Dynamic" ErrorMessage="Confirmar contraseña es obligatorio." ID="ConfirmPasswordRequired"
                                                runat="server" ToolTip="Confirmar contraseña es obligatorio." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                                                ControlToValidate="ConfirmPassword" CssClass="failureNotification" Display="Dynamic"
                                                ErrorMessage="Contraseña y Confirmar contraseña deben coincidir." ValidationGroup="RegisterUserValidationGroup">*</asp:CompareValidator>
                                        </p>
                                        <p class="ui-widget">
                                            <asp:Label runat="server" AssociatedControlID="Question" ID="QuestionLabel">Pregunta Secreta:</asp:Label>
                                            <asp:TextBox runat="server" ID="Question" CssClass="textEntry"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Question" CssClass="failureNotification"
                                                ToolTip="Pregunta Secreta requerida." ID="QuestionRequired" ValidationGroup="RegisterUserValidationGroup"
                                                ErrorMessage="La Pregunta Secreta es Requerida.">*</asp:RequiredFieldValidator>
                                        </p>
                                        <p class="ui-widget">
                                            <asp:Label runat="server" AssociatedControlID="Answer" ID="AnswerLabel">Respuesta Secreta:</asp:Label>
                                            <asp:TextBox runat="server" ID="Answer" CssClass="textEntry" ViewStateMode="Disabled"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Answer" CssClass="failureNotification"
                                                ToolTip="Respuesta Secreta requerida." ID="AnswerRequired" ValidationGroup="RegisterUserValidationGroup"
                                                ErrorMessage="La Respuesta Secreta es Requerida.">*</asp:RequiredFieldValidator>
                                        </p>
                                    </fieldset>
                                    <p class="submitButton">
                                        <asp:ImageButton ImageUrl="~/Images/botonSiguiente.png" ID="CreateUserButton" runat="server"
                                            CommandName="MoveNext" ToolTip="Crear usuario" CausesValidation="true" ValidationGroup="RegisterUserValidationGroup" />
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <CustomNavigationTemplate>
                </CustomNavigationTemplate>
            </asp:CreateUserWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
</asp:Content>
