<%@ Page Title="Validar Usuario" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="Validate.aspx.cs" Inherits="EDUAR_UI.Validate" %>

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
                    <table width="100%" cellpadding="1" cellspacing="5" border="0">
                        <tr>
                            <td style="width: 80%; vertical-align: text-top">
                                <h2>
                                    Registrarme<hr />
                                </h2>
                            </td>
                            <td style="width: 20%; text-align: right" rowspan="2">
                                <asp:Image ID="Image1" ImageUrl="~/Images/web/validate.png" runat="server" AlternateText="Registrarme"
                                    ToolTip="Registrarme" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 80%">
                                <p class="ui-widget">
                                    Necesitamos validar su identidad para poder generarle un usuario, por favor, responda
                                    las preguntas a continuación.
                                </p>
                            </td>
                        </tr>
                    </table>
                    <span class="failureNotification">
                        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
                        ValidationGroup="LoginUserValidationGroup" />
                    <table width="100%" cellpadding="1" cellspacing="5" border="0">
                        <tr>
                            <td style="width: 25%">
                                <asp:Label ID="lblTipoDocumento" Text="Tipo de Documento" runat="server" />
                            </td>
                            <td style="width: 75%">
                                <asp:DropDownList ID="ddlTipoDocumento" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlTipoDocumento"
                                    CssClass="failureNotification" ErrorMessage="Debe seleccionar el tipo de documento"
                                    InitialValue="0" ToolTip="Debe seleccionar el tipo de documento." ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
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
                                <asp:RequiredFieldValidator ID="nroDocumentoRequerido" runat="server" ControlToValidate="txtNroDocumento"
                                    CssClass="failureNotification" ErrorMessage="El número de documento es obligatorio."
                                    ToolTip="El número de documento es obligatorio." ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
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
                                    ValidationGroup="LoginUserValidationGroup" ToolTip="Siguiente" CausesValidation="true"
                                    OnClick="btnValidar_Click" ImageAlign="AbsMiddle" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnValidar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
