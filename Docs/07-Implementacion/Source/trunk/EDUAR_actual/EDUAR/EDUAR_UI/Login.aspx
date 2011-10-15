<%@ Page Title="Iniciar sesión" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs"
    Inherits="EDUAR_UI.Login" %>

<%@ Register Src="~/UserControls/VentanaInfo.ascx" TagName="VentanaInfo" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title title="Iniciar Sesión"></title>
    <link href="~/App_Themes/Tema/Estilo.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-color: White">
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="scmManager" runat="server" AsyncPostBackTimeout="9000" EnablePartialRendering="true"
        ScriptMode="Release" LoadScriptsBeforeUI="false" EnableScriptLocalization="true"
        EnableScriptGlobalization="true" />
    <div class="page">
        <div class="header">
            <table border="0" cellpadding="1" cellspacing="5" style="width: 100%;">
                <tr>
                    <td style="width: 50%">
                        <div class="title">
                            <img src="/EDUAR_UI/Images/Logo_chico.png" alt="EDU@R 2.0" style="vertical-align: middle" />
                        </div>
                    </td>
                    <td style="width: 50%">
                        <div class="loginDisplay">
                            <asp:LoginView ID="HeadLoginView" runat="server" EnableViewState="false">
                                <AnonymousTemplate>
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; text-align: right">
                                        <tr>
                                            <td style="width: 80%">
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <a href="~/Login.aspx" id="A1" runat="server" style="text-decoration: none">
                                                    <asp:Image ID="Image1" ImageUrl="~/Images/web/user-offline.png" runat="server" AlternateText="Iniciar Sesión"
                                                        ToolTip="Iniciar Sesión" /></a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 80%">
                                            </td>
                                            <td align="center" style="width: 20%">
                                                [ <a href="~/Login.aspx" id="HeadLoginStatus" runat="server">Iniciar Sesión</a>
                                                ]
                                            </td>
                                        </tr>
                                    </table>
                                </AnonymousTemplate>
                            </asp:LoginView>
                        </div>
                    </td>
                </tr>
            </table>
            <div class="clear hideSkiplink">
                <asp:Menu ID="NavigationMenu" runat="server" CssClass="menu" EnableViewState="false"
                    IncludeStyleBlock="true" Orientation="Horizontal">
                </asp:Menu>
                <asp:SiteMapDataSource ID="SiteMapAnonymusEDUAR" runat="server" ShowStartingNode="false"
                    SiteMapProvider="AnonymusXmlSiteMapProvider" />
            </div>
        </div>
        <table>
            <tr>
                <td class="contenedormenu">
                    <!-- MENU -->
                    <%--<asp:TreeView ID="trvMenu" runat="server" CollapseImageToolTip="Contraer" ExpandImageToolTip="Expander"
                        Visible="false">
                        <HoverNodeStyle />
                        <RootNodeStyle />
                        <NodeStyle />
                        <LeafNodeStyle />
                    </asp:TreeView>--%>
                    <!-- FIN MENU -->
                </td>
                <td style="width: 85%; min-height: 800px; height: auto; vertical-align: top">
                    <div class="main">
                        <h2>
                            Iniciar sesión
                        </h2>
                        <p class="ui-widget">
                            Especifique su nombre de usuario y contraseña.
                            <asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false">Registrarse</asp:HyperLink>
                            si no tiene una cuenta.
                        </p>
                        <p class="ui-widget">
                            Si ha olvidado su clave de ingreso haga click
                            <asp:HyperLink ID="ForgotPasswordHyperLink" runat="server" EnableViewState="false">Aquí</asp:HyperLink>
                        </p>
                        <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false"
                            OnAuthenticate="LoginUsuario_Authenticate">
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
                                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Iniciar sesión"
                                            ValidationGroup="LoginUserValidationGroup" />
                                    </p>
                                </div>
                            </LayoutTemplate>
                        </asp:Login>
                </td>
            </tr>
        </table>
        <div class="clear">
            <table style="width: 100%">
                <tr>
                    <td style="width: 100%; text-align: center">
                        Copyright (c)
                        <%=DateTime.Now.Year.ToString() %>
                        - BLPM - <a href="http://www.gnu.org/licenses/gpl-3.0.html" title="Licencia GNU - GPLv3"
                            target="_blank">GPLv3</a>
                        <br />
                        Sitio web compatible con Mozilla Firefox<br />
                        <a href="http://www.mozilla.org/es-ES/firefox/" target="_blank">
                            <img src="/EDUAR_UI/Images/firefox.png" alt="Compatible con Mozilla Firefox" title="Compatible con Mozilla Firefox"
                                style="border: 0" /></a>
                    </td>
                </tr>
            </table>
        </div>
        <!--******** INICIO FUNCIONALIDAD VENTANA MENSAJES  ********-->
        <asp:UpdatePanel ID="updVentaneMensajes" runat="server" UpdateMode="Conditional"
            RenderMode="Inline">
            <ContentTemplate>
                <uc1:VentanaInfo ID="ventanaInfoLogin" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <!--******** FIN FUNCIONALIDAD VENTANA MENSAJES  ********-->
    </div>
    </form>
</body>
</html>
