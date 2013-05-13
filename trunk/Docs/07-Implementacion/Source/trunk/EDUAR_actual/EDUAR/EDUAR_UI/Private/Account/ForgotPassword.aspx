<%@ Page Title="Olvidé Mi Clave" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="EDUAR_UI.ForgotPassword" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="udpForgotPassword" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:UpdatePanel ID="udpEmail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table width="100%" cellpadding="1" cellspacing="5" border="0">
                        <tr>
                            <td style="width: 80%; vertical-align: text-top">
                                <h2>
                                    Olvidé Mi Clave<hr />
                                </h2>
                            </td>
                            <td style="width: 20%; text-align: right" rowspan="2">
                                <asp:Image ID="Image1" ImageUrl="~/Images/web/password.png" runat="server" AlternateText="Olvidé Mi Clave"
                                    ToolTip="Olvidé Mi Clave" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 80%">
                                <p class="ui-widget">
                                    Recupere los datos de acceso a su cuenta. Para ello, ingrese su dirección de e-mail.
                                </p>
                            </td>
                        </tr>
                    </table>
                    <div class="accountInfo">
                        <fieldset class="login">
                            <legend>Información de cuenta</legend>
                            <p class="ui-widget">
                                <table width="100%" cellpadding="1" cellspacing="5" border="0">
                                    <tr>
                                        <td style="width: 15%">
                                            <asp:Label ID="Label1" Text="Email" runat="server" />
                                        </td>
                                        <td style="width: 85%">
                                            <asp:TextBox runat="server" ID="txtEmail" Width="250px" />&nbsp;
                                            <asp:RegularExpressionValidator ID="validarEmail" runat="server" ControlToValidate="txtEmail"
                                                Display="Dynamic" ErrorMessage="El email ingresado no es válido." ForeColor="Red"
                                                ValidationExpression="^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                </table>
                            </p>
                        </fieldset>
                        <p class="submitButton">
                            <asp:ImageButton ID="btnEnviarMail" ImageUrl="~/Images/botonEnviarMail.png" ToolTip="Solicitar Datos de Acceso"
                                OnClick="btnEnviarMail_Click" runat="server" ImageAlign="AbsMiddle" />
                        </p>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnEnviarMail" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="udpRecover" runat="server" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
                    <table width="100%" cellpadding="1" cellspacing="5" border="0">
                        <tr>
                            <td style="width: 80%; vertical-align: text-top">
                                <h2>
                                    Nueva Clave De Acceso<hr />
                                </h2>
                            </td>
                            <td style="width: 20%; text-align: right" rowspan="2">
                                <asp:Image ID="Image2" ImageUrl="~/Images/web/password.png" runat="server" AlternateText="Olvidé Mi Clave"
                                    ToolTip="Olvidé Mi Clave" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 80%">
                                <p class="ui-widget">
                                    Para obtener una nueva clave de acceso responde a la siguiente pregunta.
                                </p>
                            </td>
                        </tr>
                    </table>
                    <div class="accountInfo">
                        <fieldset class="login">
                            <legend>Información de cuenta</legend>
                            <p class="ui-widget">
                                <table width="100%" cellpadding="1" cellspacing="5" border="0">
                                    <tr>
                                        <td class="TD160px">
                                            <asp:Literal Text="Pregunta Secreta" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPregunta" Text="" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TD160px">
                                            <asp:Literal Text="Respuesta" runat="server" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRespuesta" runat="server" ViewStateMode="Disabled" />
                                        </td>
                                    </tr>
                                </table>
                            </p>
                        </fieldset>
                        <p class="submitButton">
                            <asp:ImageButton ID="btnRecoverPassword" ImageUrl="~/Images/botonSiguiente.png" runat="server"
                                ToolTip="Siguiente" OnClick="btnRecoverPassword_Click" ImageAlign="AbsMiddle"
                                Style="margin-left: 150px" />
                        </p>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnRecoverPassword" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="udpNewPassword" runat="server" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
                    <table width="100%" cellpadding="1" cellspacing="5" border="0">
                        <tr>
                            <td style="width: 80%; vertical-align: text-top">
                                <h2>
                                    Nueva Clave De Acceso<hr />
                                </h2>
                            </td>
                            <td style="width: 20%; text-align: right" rowspan="2">
                                <asp:Image ID="Image3" ImageUrl="~/Images/web/password.png" runat="server" AlternateText="Olvidé Mi Clave"
                                    ToolTip="Olvidé Mi Clave" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 80%">
                                <p class="ui-widget">
                                    Ingrese su nueva clave de acceso.
                                </p>
                            </td>
                        </tr>
                    </table>
                    <div class="accountInfo">
                        <fieldset class="login">
                            <legend>Información de cuenta</legend>
                            <asp:ValidationSummary ID="ChangeUserPasswordValidationSummary" runat="server" CssClass="failureNotification"
                                ValidationGroup="ChangeUserPasswordValidationGroup" />
                            <table width="100%" cellpadding="1" cellspacing="5" border="0">
                                <tr>
                                    <td class="TD160px">
                                        <asp:Literal Text="Nueva Contraseña" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" />
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtPassword"
                                            ErrorMessage="La contraseña es obligatoria." ToolTip="La contraseña es obligatoria."
                                            ValidationGroup="ChangeUserPasswordValidationGroup" ForeColor="Red">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ErrorMessage="La Contraseña debe tener al menos 5 caracteres, de los cuales uno debe ser numérico."
                                            ControlToValidate="txtPassword" runat="server" Display="Dynamic" ForeColor="Red"
                                            ValidationExpression="^[a-zA-Z0-9,'+-_¿!¡=;:\.\?]\w{5,20}$" ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TD160px">
                                        <asp:Literal Text="Repita la Contraseña" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtPasswordConfirm" TextMode="Password" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPasswordConfirm"
                                            ErrorMessage="La contraseña es obligatoria." ToolTip="La contraseña es obligatoria."
                                            ForeColor="Red" ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Las contraseñas no son iguales"
                                            ControlToCompare="txtPasswordConfirm" ControlToValidate="txtPassword" ForeColor="Red"
                                            ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:CompareValidator>
                                    </td>
                                </tr>
                            </table>
                            </p>
                        </fieldset>
                        <p class="submitButton">
                            <asp:ImageButton ID="btnConfirmarPassword" ImageUrl="~/Images/botonSiguiente.png"
                                runat="server" ToolTip="Siguiente" OnClick="btnConfirmarPassword_Click" ImageAlign="AbsMiddle"
                                ValidationGroup="ChangeUserPasswordValidationGroup" Style="margin-left: 150px" />
                        </p>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnConfirmarPassword" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
