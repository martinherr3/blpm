<%@ Page Title="Modificar contraseña" Language="C#" MasterPageFile="~/EDUARMaster.master"
    AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="EDUAR_UI.ChangePassword" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table class="tablaInterna" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 80%; vertical-align: text-top">
                <h2>
                    Modificar Contraseña
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
                <asp:Image ID="Image1" ImageUrl="~/Images/user-properties.png" runat="server" AlternateText="Modificar contraseña"
                    ToolTip="Modificar contraseña" />
            </td>
        </tr>
    </table>
    <p class="ui-widget">
        Utilice el siguiente formulario para modificar su contraseña.
    </p>
    <p class="ui-widget">
        La contraseña debe ser alfanumérica y tener una longitud mínima de
        <%= Membership.MinRequiredPasswordLength %>
        caracteres.
    </p>
    <asp:ChangePassword ID="ChangeUserPassword" runat="server" CancelDestinationPageUrl="~/Private/Account/Welcome.aspx"
        EnableViewState="false" RenderOuterTable="false" SuccessPageUrl="ChangePasswordSuccess.aspx">
        <ChangePasswordTemplate>
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
            </span>
            <asp:ValidationSummary ID="ChangeUserPasswordValidationSummary" runat="server" CssClass="failureNotification"
                ValidationGroup="ChangeUserPasswordValidationGroup" />
            <div class="accountInfo">
                <fieldset class="changePassword">
                    <legend>Información de cuenta</legend>
                    <p class="ui-widget">
                        <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword">Contraseña anterior:</asp:Label>
                        <asp:TextBox ID="CurrentPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword"
                            CssClass="failureNotification" ErrorMessage="La contraseña es obligatoria." ToolTip="La contraseña actual es obligatoria."
                            ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p class="ui-widget">
                        <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">Nueva contraseña:</asp:Label>
                        <asp:TextBox ID="NewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword"
                            CssClass="failureNotification" ErrorMessage="La nueva contraseña es obligatoria."
                            ToolTip="La nueva contraseña es obligatoria." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ErrorMessage="La Contraseña debe tener al menos 5 caracteres, <br />de los cuales uno debe ser numérico."
                            ControlToValidate="NewPassword" runat="server" Display="Dynamic" ForeColor="Red"
                            ValidationExpression="^[a-zA-Z0-9,'+-_¿!¡=;:\.\?]\w{5,20}$" ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RegularExpressionValidator>
                    </p>
                    <p class="ui-widget">
                        <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword">Confirmar la nueva contraseña:</asp:Label>
                        <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword"
                            CssClass="failureNotification" Display="Dynamic" ErrorMessage="Confirmar la nueva contraseña es obligatorio."
                            ToolTip="Confirmar la nueva contraseña es obligatoria." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword"
                            ControlToValidate="ConfirmNewPassword" CssClass="failureNotification" Display="Dynamic"
                            ErrorMessage="Confirmar la nueva contraseña debe coincidir con la entrada Nueva contraseña."
                            ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:CompareValidator>
                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" ErrorMessage="La Contraseña debe tener al menos 5 caracteres, de los cuales uno debe ser numérico."
                            ControlToValidate="ConfirmNewPassword" runat="server" Display="Dynamic" ForeColor="Red"
                            ValidationExpression="^[a-zA-Z0-9,'+-_¿!¡=;:\.\?]\w{5,20}$" />--%>
                    </p>
                </fieldset>
                <p class="submitButton">
                    <table width="100%" style="text-align: right">
                        <tr>
                            <td style="width: 100%">
                                <%--<asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                    OnClick="CancelPushButton_Click" Text="Cancelar" />--%>
                                <asp:ImageButton ImageUrl="~/Images/botonSiguiente.png" ID="ChangePasswordPushButton"
                                    runat="server" CommandName="ChangePassword" ToolTip="Cambiar contraseña" ValidationGroup="ChangeUserPasswordValidationGroup"
                                    OnClick="ChangePasswordPushButton_Click" />
                                <%--<asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword"
                                    Text="Cambiar contraseña" ValidationGroup="ChangeUserPasswordValidationGroup" />--%>
                                <asp:ImageButton ImageUrl="~/Images/botonVolver.png" ID="CancelPushButton" CausesValidation="false"
                                    runat="server" CommandName="Cancel" ToolTip="Volver" OnClick="CancelPushButton_Click" />
                            </td>
                        </tr>
                    </table>
                </p>
            </div>
        </ChangePasswordTemplate>
    </asp:ChangePassword>
</asp:Content>
