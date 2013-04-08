<%@ Page Title="Login" Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs"
    Inherits="Promethee.frmLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="App_Themes/Tema/Estilo.css" rel="stylesheet" type="text/css" />
    <link rel="icon" type="image/png" href="~/favicon.ico" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="position: absolute; left: 25%; top: 10%;">
        <fieldset style="width: 400px; text-align: left">
            <table class="tablaInternaSinBorde" cellpadding="1" cellspacing="5">
                <tr align="center">
                    <td>
                        Autenticaci&oacute;n
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="400" border="0" cellspacing="1" cellpadding="5">
                            <tr>
                                <td width="80">
                                    Usuario:
                                </td>
                                <td width="10">
                                </td>
                                <td>
                                    <asp:TextBox ID="txtUser" Width="150" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Password:
                                </td>
                                <td width="10">
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPassword" Width="150" TextMode="Password" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td width="10">
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkPersistLogin" runat="server" />Recordar Mis Credenciales<br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center">
                                    <asp:Button ID="cmdLogin" OnClick="ProcessLogin" Text="Login" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <div id="ErrorMessage" runat="server" />
        </fieldset>
    </div>
    </form>
</body>
</html>
