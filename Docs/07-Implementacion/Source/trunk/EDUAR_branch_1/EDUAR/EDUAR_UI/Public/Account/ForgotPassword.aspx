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
                    <h2>
                        Olvidé Mi Clave</h2>
                    <p>
                        Recupere los datos de acceso a su cuenta. Para ello, ingrese su dirección de e-mail.</p>
                    <table class="tablaInterna" border="0" cellpadding="1" cellspacing="5">
                        <tr>
                            <td style="width: 15%">
                                <asp:Label ID="Label1" Text="Email" runat="server" />
                            </td>
                            <td style="width: 85%">
                                <asp:TextBox runat="server" ID="txtEmail" Width="250px" />&nbsp;<asp:ImageButton
                                    ID="btnEnviarMail" ImageUrl="~/Images/botonEnviarMail.png" ToolTip="Solicitar Datos de Acceso"
                                    OnClick="btnEnviarMail_Click" runat="server" ImageAlign="AbsMiddle" />
                                <asp:RegularExpressionValidator ID="validarEmail" runat="server" ControlToValidate="txtEmail"
                                    Display="Dynamic" ErrorMessage="El email ingresado no es válido." ForeColor="Red"
                                    ValidationExpression="^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnEnviarMail" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="udpRecover" runat="server" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
                    <h2>
                        Nueva clave de acceso</h2>
                    <p>
                        Para obtener una nueva clave de acceso responde a la siguiente pregunta.</p>
                    <table class="tablaInterna" border="0" cellpadding="1" cellspacing="5">
                        <tr>
                            <td>
                                <asp:Literal Text="Pregunta Secreta" runat="server" />
                            </td>
                            <td>
                                <asp:Label ID="lblPregunta" Text="" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal Text="Respuesta" runat="server" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtRespuesta" runat="server" /><asp:ImageButton ID="btnRecoverPassword"
                                    ImageUrl="~/Images/botonSiguiente.png" runat="server" ToolTip="Siguiente" OnClick="btnRecoverPassword_Click"
                                    ImageAlign="AbsMiddle" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnRecoverPassword" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="udpNewPassword" runat="server" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
                    <h2>
                        Nueva clave de acceso</h2>
                    <p>
                        Ingrese su nueva clave de acceso.</p>
                    <table class="tablaInterna" border="0" cellpadding="1" cellspacing="5">
                        <tr>
                            <td>
                                <asp:Literal Text="Nueva Contraseña" runat="server" />
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" /><asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Las contraseñas no son iguales"
                                    ControlToCompare="txtPassword" ControlToValidate="txtPasswordConfirm"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal Text="Repita la Contraseña" runat="server" />
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtPasswordConfirm" TextMode="Password" />&nbsp;<asp:ImageButton
                                    ID="btnConfirmarPassword" ImageUrl="~/Images/botonSiguiente.png" runat="server"
                                    ToolTip="Siguiente" OnClick="btnConfirmarPassword_Click" ImageAlign="AbsMiddle" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnConfirmarPassword" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
