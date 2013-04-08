<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Usuarios.aspx.cs" Inherits="Promethee.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellspacing="1" cellpadding="5">
        <tr>
            <td align="center" class="titulo">
                Creacion de usuario
            </td>
        </tr>
        <tr>
            <td>
                <table class="tablaInterna" width="400px" border="0" cellspacing="1" cellpadding="5">
                    <tr>
                        <td width="80">
                            Nombre:
                        </td>
                        <td width="10">
                        </td>
                        <td>
                            <asp:TextBox ID="txtNombre" Width="150" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Apellido:
                        </td>
                        <td width="10">
                        </td>
                        <td>
                            <asp:TextBox ID="txtApellido" Width="150" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Login:
                        </td>
                        <td width="10">
                        </td>
                        <td>
                            <asp:TextBox ID="txtLogin" Width="150" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Password:
                        </td>
                        <td width="10">
                        </td>
                        <td>
                            <asp:TextBox ID="txtPassword" Width="150" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center">
                            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <div id="lblMessage" runat="server" />
</asp:Content>
