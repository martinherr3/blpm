<%@ Page Title="Validar Usuario" Language="C#" MasterPageFile="~/EDUARMaster.Master" AutoEventWireup="true"
    CodeBehind="Validate.aspx.cs" Inherits="EDUAR_UI.Validate" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Calendario.ascx" TagName="Calendario" TagPrefix="cal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="udpValidate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:UpdatePanel ID="udpDatosValidar" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <h2>
                        Registrarme</h2>
                    <p class="ui-widget">
                        Necesitamos validar su identidad para poder generarle un usuario, por favor, responda
                        las preguntas a continuación.</p>
                    <table class="tablaInterna" border="0" cellpadding="1" cellspacing="5">
                        <tr>
                            <td style="width: 25%">
                                <asp:Label ID="lblTipoDocumento" Text="Tipo de Documento" runat="server" />
                            </td>
                            <td style="width: 75%">
                                <asp:DropDownList ID="ddlTipoDocumento" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblNroDocumento" Text="Número de Documento" runat="server" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtNroDocumento" runat="server" MaxLength="10" /><asp:FilteredTextBoxExtender
                                    ID="filterTxtNroDocumento" runat="server" TargetControlID="txtNroDocumento" FilterType="Numbers">
                                </asp:FilteredTextBoxExtender>
                                <asp:MaskedEditExtender ID="maskEditNroDocumento" runat="server" Mask="99999999"
                                    TargetControlID="txtNroDocumento" MaskType="Number" InputDirection="RightToLeft"
                                    AcceptNegative="None" DisplayMoney="None" ErrorTooltipEnabled="false" AutoComplete="false"
                                    ClearMaskOnLostFocus="false" />
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblFechaNacimiento" Text="Fecha de Nacimiento" runat="server" />
                            </td>
                            <td>
                                <cal:Calendario ID="calFechaNacimiento" runat="server" TipoCalendario="SoloFecha"
                                    EtiquetaDesde="Fecha de Nacimiento" Habilitado="true" TipoAlineacion="Izquierda" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="center">
                                <asp:ImageButton ID="btnValidar" ImageUrl="~/Images/botonSiguiente.png" runat="server"
                                    ToolTip="Siguiente" OnClick="btnValidar_Click" ImageAlign="AbsMiddle" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnValidar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <%--            <asp:UpdatePanel ID="udpRecover" runat="server" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
                    <h2>
                        Nueva clave de acceso</h2>
                    <p class="ui-widget">
                        Para obtener una nueva clave de acceso responde a la siguiente pregunta.</p>
                    <table class="tablaInterna" border="0" cellpadding="1" cellspacing="5">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal1" Text="Pregunta Secreta" runat="server" />
                            </td>
                            <td>
                                <asp:Label ID="lblPregunta" Text="" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Literal2" Text="Respuesta" runat="server" />
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
                    <p class="ui-widget">
                        Ingrese su nueva clave de acceso.</p>
                    <table class="tablaInterna" border="0" cellpadding="1" cellspacing="5">
                        <tr>
                            <td>
                                <asp:Literal ID="Literal3" Text="Nueva Contraseña" runat="server" />
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" /><asp:CompareValidator
                                    ID="CompareValidator1" runat="server" ErrorMessage="Las contraseñas no son iguales"
                                    ControlToCompare="txtPassword" ControlToValidate="txtPasswordConfirm"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="Literal4" Text="Repita la Contraseña" runat="server" />
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
            </asp:UpdatePanel>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
