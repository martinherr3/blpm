<%@ Page Title="Resultados Encuesta" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="ResultadosEncuestas.aspx.cs" Inherits="EDUAR_UI.ResultadosEncuestas"
    Theme="Tema" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/Grafico.ascx" TagName="Grafico" TagPrefix="gra" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript">
        function AbrirPopup() {
            var popup;
            //Abrir Ventana
            popup = window.open('/EDUAR_UI/Private/Encuestas/VerRespuestas.aspx', 'Encuestas - Respuestas', 'width=800,height=600,left=50,top=100,­menubar=0,toolbar=0,status=0,scrollbars=1,resizable=0,titlebar=0');

            if (popup == null || typeof (popup) == 'undefined') {
                jAlert('Por favor deshabilita el <i>bloqueador de ventanas emergentes</i><br /> y vuelve a hacer en "Ver Respuestas".', 'Aviso');
            }
            else {
                popup.focus();
                //Armar documento
                popup.document.close();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="tablaInterna" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <h2>
                    Análisis de Resultados:
                    <asp:Label ID="lblTitulo" Text="" runat="server" Font-Bold="true" ForeColor="#4264B1" /></h2>
                <br />
            </td>
            <td align="right">
                <%--<asp:ImageButton ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" ToolTip="Guardar"
                            ImageUrl="~/Images/botonGuardar.png" CausesValidation="true" ValidationGroup="validarEdit" />--%>
                <asp:ImageButton ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" ToolTip="Buscar"
                    ImageUrl="~/Images/botonBuscar.png" />
                <asp:ImageButton ID="btnPDF" OnClick="btnPDF_Click" runat="server" ToolTip="Exportar PDF"
                    ImageUrl="~/Images/ExportarPDF.png" />
                <asp:ImageButton ID="btnVolver" OnClick="btnVolver_Click" runat="server" ToolTip="Volver"
                    ImageUrl="~/Images/botonVolver.png" />
            </td>
        </tr>
    </table>
    <table class="tablaInterna" cellpadding="0" cellspacing="0">
        <tr>
            <td class="TD25">
                <asp:Label Text="Ámbito: " runat="server" />
                <asp:Label ID="lblAmbito" Text="" runat="server" Font-Bold="true" /><br />
                <br />
            </td>
            <td class="TD25">
                <asp:Label ID="lblCurso" Text="Curso: " runat="server" Visible="false" />
                <asp:Label ID="lblCursoNombre" Text="" runat="server" Font-Bold="true" /><br />
                <br />
            </td>
            <td>
                <asp:Label ID="lblAsignatura" Text="Asignatura: " runat="server" Visible="false" />
                <asp:Label ID="lblAsignaturaNombre" Text="" runat="server" Font-Bold="true" /><br />
                <br />
            </td>
        </tr>
        <tr>
            <td class="TD25">
                <asp:Label Text="Fecha Lanzamiento: " runat="server" />
                <asp:Label ID="lblFechaLanzamiento" Text="" runat="server" Font-Bold="true" /><br />
                <br />
            </td>
            <td class="TD25">
                <asp:Label Text="Fecha Expiración: " runat="server" />
                <asp:Label ID="lblFechaExpiracion" Text="" runat="server" Font-Bold="true" /><br />
                <br />
            </td>
            <td class="TD50">
            </td>
        </tr>
    </table>
    <%--<asp:UpdatePanel ID="udpIndicadores" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
    <table class="tablaInternaSinBorde" cellpadding="0" cellspacing="0">
        <tr>
            <td class="TD25">
                <asp:Label ID="Label1" Text="Encuestas Enviadas: " runat="server" />
                <asp:Label ID="lblEnviadas" Text="" runat="server" Font-Bold="true" /><br />
                <br />
            </td>
            <td class="TD25">
                <asp:Label ID="Label2" Text="Encuestas Respondidas: " runat="server" />
                <asp:Label ID="lblRespondidas" Text="" runat="server" Font-Bold="true" /><br />
                <br />
            </td>
            <td class="TD50">
                <asp:Label ID="lblEncuestasPendientes" Text="Encuestas Pendientes: " runat="server" />
                <asp:Label ID="lblPendientes" Text="" runat="server" Font-Bold="true" /><br />
                <br />
            </td>
        </tr>
    </table>
    <%-- </ContentTemplate>
        <Triggers>--%>
    <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
    </Triggers> </asp:UpdatePanel>
    <table class="tablaInternaSinBorde" cellpadding="0" cellspacing="0">
        <tr>
            <td class="TD10" style="vertical-align: top">
                <asp:Label ID="Label3" Text="Roles: " runat="server" />
            </td>
            <td colspan="2">
                <asp:CheckBoxList runat="server" ID="lstRoles">
                </asp:CheckBoxList>
            </td>
        </tr>
    </table>
    <cc1:Accordion ID="CuestionarioAccordion" CssClass="accordion" HeaderCssClass="accordionHeader"
        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
        RequireOpenedPane="false" SelectedIndex="-1" runat="server" Width="100%" FadeTransitions="true">
        <Panes>
            <%--LOS COMPONENTES DEL ACCORDION SON AGREGADOS DINÁMICAMENTE DESDE CÓDIGO--%>
        </Panes>
    </cc1:Accordion>
    <div id="divGrafico" runat="server" class="divGraficoOcultar">
        <gra:Grafico ID="grafico" runat="server" />
    </div>
</asp:Content>
