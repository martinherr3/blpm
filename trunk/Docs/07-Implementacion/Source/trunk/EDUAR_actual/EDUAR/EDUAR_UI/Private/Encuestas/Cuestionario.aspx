<%@ Page Title="Cuestionario" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="Cuestionario.aspx.cs" Inherits="EDUAR_UI.Cuestionario"
    Theme="Tema" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="udpSeleccionEncuesta" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="tablaInterna" cellpadding="1" cellspacing="5">
                <tr>
                    <td colspan="3">
                        <h3>
                            Seleccionar Encuesta(s) disponible(s)</h3>
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="TD50px">
                        <asp:Label ID="lblEncuesta" runat="server" Text="Encuesta:"></asp:Label>
                    </td>
                    <td valign="top" class="TD50px">
                        <asp:DropDownList ID="ddlEncuesta" runat="server" />
                    </td>
                    <td valign="top" class="TD50px">
                        <asp:ImageButton ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" ToolTip="Buscar"
                            ImageUrl="~/Images/botonBuscar.png" />
                    </td>
                </tr>
            </table>

            <asp:UpdatePanel ID="udpFormulario" runat="server">
            <ContentTemplate>
                <h2>
                    <asp:Label ID="lblNombreEncuesta" runat="server" />
                </h2>
                <p />
                <cc1:Accordion ID="CuestionarioAccordion" CssClass="accordion" HeaderCssClass="accordionHeader"
                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                    RequireOpenedPane="false" SelectedIndex="-1" runat="server">
                    <Panes>
                        <%--LOS COMPONENTES DEL ACCORDION SON AGREGADOS DINÁMICAMENTE DESDE CÓDIGO--%>
                    </Panes>
                </cc1:Accordion>
                <table class="tablaInterna" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="right">
                            <asp:ImageButton ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" ToolTip="Guardar"
                                ImageUrl="~/Images/botonGuardar.png" CausesValidation="true" ValidationGroup="validarCompleto" />
                            <asp:ImageButton ID="btnCancelar" OnClick="btnCancelar_Click" runat="server" ToolTip="Cancelar"
                                ImageUrl="~/Images/botonGuardar.png" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
                    <%--<asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />--%>
                </Triggers>
            </asp:UpdatePanel>
        </ContentTemplate>
        <%--<Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
        </Triggers>--%>
        </asp:UpdatePanel>
</asp:Content>
