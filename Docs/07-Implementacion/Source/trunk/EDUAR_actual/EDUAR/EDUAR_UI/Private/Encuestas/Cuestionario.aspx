<%@ Page Title="Cuestionario" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="Cuestionario.aspx.cs" Inherits="EDUAR_UI.Cuestionario"
    Theme="Tema" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">

        function GuardarRespuesta(textareaControl) {
            __doPostBack("GuardarRespuesta");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="udpSeleccionEncuesta" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="tablaInterna" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <h2>
                            <asp:Label ID="lblNombreEncuesta" runat="server" CssClass="lblTitulo" /></h2>
                            <br />
                    </td>
                </tr>
            </table>
            <table class="tablaInterna" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="TD50">
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="udpFormulario" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table class="tablaInternaSinBorde" cellpadding="1" cellspacing="5">
                        <tr>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <cc1:Accordion ID="CuestionarioAccordion" CssClass="accordion" HeaderCssClass="accordionHeader"
                        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                        RequireOpenedPane="false" SelectedIndex="-1" runat="server" Width="99%">
                        <Panes>
                            <%--LOS COMPONENTES DEL ACCORDION SON AGREGADOS DINÁMICAMENTE DESDE CÓDIGO--%>
                        </Panes>
                    </cc1:Accordion>
                    <br />
                    <table class="tablaInterna" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="center">
                                <asp:ImageButton ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" ToolTip="Guardar Respuesta"
                                    ImageUrl="~/Images/ventanaSatisfactorio.png" CausesValidation="true" ValidationGroup="validarCompleto" />
                                <asp:ImageButton ID="btnCancelar" OnClick="btnCancelar_Click" runat="server" ToolTip="Responder en otro momento"
                                    ImageUrl="~/Images/botonCancelar48.png" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
