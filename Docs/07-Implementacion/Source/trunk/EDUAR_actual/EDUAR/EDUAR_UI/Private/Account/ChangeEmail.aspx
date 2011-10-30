<%@ Page Title="" Language="C#" MasterPageFile="~/EDUARMaster.Master" AutoEventWireup="true"
    CodeBehind="ChangeEmail.aspx.cs" Inherits="EDUAR_UI.ChangeEmail" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Modificar Email</h2>
    <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="tablaInterna" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="right">
                        <asp:ImageButton ID="btnGuardar" ImageUrl="~/Images/botonGuardar.png" OnClick="btnGuardar_Click"
                            runat="server" ToolTip="Guardar" CausesValidation="true" ValidationGroup="RegisterUserValidationGroup" />
                    </td>
                </tr>
            </table>
            <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification"
                ValidationGroup="RegisterUserValidationGroup" />
            <asp:UpdatePanel ID="udpFiltrosBusqueda" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table class="tablaInterna" cellpadding="1" cellspacing="5">
                        <%--<tr>
                            <td colspan="2">
                                <h3>
                                    Buscar</h3>
                            </td>
                        </tr>--%>
                        <tr>
                            <td style="width: 20%;">
                                <asp:Label ID="lblEmail" runat="server" Text="Email Actual:"></asp:Label>
                            </td>
                            <td valign="top">
                                <asp:Label ID="lblEmailActual" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="lblNuevoEmail" runat="server" Text="Nuevo Email:"></asp:Label>
                            </td>
                            <td valign="top">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="EstiloTxtLargo250" />
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="txtEmail"
                                    CssClass="failureNotification" ErrorMessage="El correo electrónico es obligatorio."
                                    ToolTip="El correo electrónico es obligatorio." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ErrorMessage="La dirección de correo electrónico no tiene un formato válido"
                                    ControlToValidate="txtEmail" ValidationExpression="^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$"
                                    ValidationGroup="RegisterUserValidationGroup" runat="server" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
