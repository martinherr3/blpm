<%@ Page Title="Modificar Email" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="ChangeEmail.aspx.cs" Inherits="EDUAR_UI.ChangeEmail" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="tablaInterna" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 80%; vertical-align: text-top">
                <h2>
                    Modificar Email
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
                <asp:Image ID="Image1" ImageUrl="~/Images/user-properties.png" runat="server" AlternateText="Modificar Email"
                    ToolTip="Modificar Email" />
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <p class="ui-widget">
                Utilice el siguiente formulario para modificar su dirección de correo electrónico.
            </p>
            <span class="failureNotification">
                <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
            </span>
            <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification"
                ValidationGroup="RegisterUserValidationGroup" />
            <asp:UpdatePanel ID="udpFiltrosBusqueda" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="accountInfo">
                        <fieldset class="register">
                            <legend>Información de cuenta</legend>
                            <p class="ui-widget">
                                <asp:Label ID="lblEmail" runat="server" Text="Email Actual:"></asp:Label>
                                <asp:Label ID="lblEmailActual" runat="server" Text=""></asp:Label>
                            </p>
                            <p class="ui-widget">
                                <asp:Label ID="lblNuevoEmail" runat="server" Text="Nuevo Email:"></asp:Label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="EstiloTxtLargo250" />
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="txtEmail"
                                    CssClass="failureNotification" ErrorMessage="El correo electrónico es obligatoria."
                                    ToolTip="El correo electrónico es obligatoria." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ErrorMessage="La dirección de correo electrónico no tiene un formato válido."
                                    ControlToValidate="txtEmail" ValidationExpression="^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$"
                                    ValidationGroup="RegisterUserValidationGroup" runat="server" CssClass="failureNotification">*</asp:RegularExpressionValidator>
                            </p>
                        </fieldset>
                        <p class="submitButton">
                            <%--<asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" 
                                   CommandName="Cancel" onclick="CancelPushButton_Click" Text="Cancelar" />
							<asp:Button ID="btnGuardar" runat="server" Text="Cambiar Email" ValidationGroup="RegisterUserValidationGroup"
								OnClick="btnGuardar_Click" />--%>
                            <asp:ImageButton ImageUrl="~/Images/botonSiguiente.png" ID="btnGuardar" runat="server"
                                ToolTip="Cambiar Email" ValidationGroup="RegisterUserValidationGroup" OnClick="btnGuardar_Click" />
                            <asp:ImageButton ImageUrl="~/Images/botonVolver.png" ID="CancelPushButton" CausesValidation="false"
                                runat="server" CommandName="Cancel" ToolTip="Volver" OnClick="CancelPushButton_Click" />
                        </p>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
