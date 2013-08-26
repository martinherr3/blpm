<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Novedad.ascx.cs" Inherits="EDUAR_UI.UserControls.Novedad" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ImageButton ID="btnNuevaNovedad" ToolTip="Nueva Novedad Aulica" ImageUrl="~/Images/botonNovedades.png"
    runat="server" OnClick="btnNuevaNovedad_Click" />
<asp:HiddenField ID="HiddenField1" runat="server" />
<ajaxToolkit:ModalPopupExtender ID="mpeNueva" runat="server" PopupControlID="pnlNueva"
    OkControlID="btnOcultoNovedad" CancelControlID="btnVolver" TargetControlID="HiddenField1"
    RepositionMode="RepositionOnWindowResizeAndScroll" BackgroundCssClass="modalBackground"
    DropShadow="false" PopupDragHandleControlID="pnlNueva">
</ajaxToolkit:ModalPopupExtender>
<script type="text/javascript">
    function DoPostBack() {
        var observaciones = document.getElementById('<%= txtObservaciones.ClientID %>').value;
        var mensaje = '';
        if (observaciones.toString().trim() == '')
            mensaje = '<br />* Observaciones';

        var estado = document.getElementById('<%= ddlEstado.ClientID %>').value;
        if (estado.toString() == '-1')
            mensaje = mensaje + '<br />* Estado';

        var tipoNovedad = document.getElementById('<%= ddlNovedad.ClientID %>').value;
        if (tipoNovedad.toString() == '-1')
            mensaje = mensaje + '<br />* Tipo de Novedad';

        if (mensaje == '') {
            __doPostBack('btnOcultoNovedad', 'Click');
            //            var boton = document.getElementById('<%= btnOcultoNovedad.ClientID %>');
            //            boton.click();
            mostrarMensaje();
        }
        else
            jAlert('Los siguientes campos son <b>Obligatorios</b>:' + mensaje, 'Mensaje');
    }

    function mostrarMensaje() {
        jAlert('La Novedad ha sido <b>registrada</b>.', 'Mensaje');
    }
</script>
<asp:Panel ID="pnlNueva" runat="server" DefaultButton="btnOcultoNovedad" Width="600px"
    Height="400px" Style="display: none; text-align: left; z-index: 5000" CssClass="CajaDialogo">
    <asp:UpdatePanel ID="udpNueva" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="tablaInterna" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <h2>
                            Registrar Novedad Aulica</h2>
                        <br />
                        <asp:LinkButton ID="btnOcultoNovedad" Text="" OnClientClick="mostrarMensaje();" runat="server" />
                        <!--OnClick="btnOcultoNovedad_Click"-->
                    </td>
                    <td align="right">
                        <asp:ImageButton ID="btnGuardar" runat="server" ToolTip="Guardar" ImageUrl="~/Images/PopUp/botonGuardar.png"
                            Visible="true" ValidationGroup="Novedad" CausesValidation="true" OnClientClick="DoPostBack();" />
                        <!-- OnClick="btnOcultoNovedad_Click"-->
                        <asp:ImageButton ID="btnVolver" OnClick="btnVolver_Click" runat="server" ToolTip="Cerrar"
                            ImageUrl="~/Images/PopUp/botonCerrar.png" />
                    </td>
                </tr>
            </table>
            <table class="tablaInterna" cellpadding="1" cellspacing="5">
                <tr>
                    <td class="TD140px">
                        <asp:Label ID="lblEstado" runat="server" Text="Estado:" CssClass="lblCriterios"></asp:Label>
                    </td>
                    <td class="TD140px">
                        <asp:DropDownList ID="ddlEstado" runat="server">
                        </asp:DropDownList>
                        <asp:CompareValidator ID="cmvddlEstado" ErrorMessage="*" ControlToValidate="ddlEstado"
                            runat="server" Operator="GreaterThan" ValueToCompare="0" Type="Integer" ForeColor="Red"
                            Display="Dynamic" ToolTip="Campo Requerido" ValidationGroup="Novedad" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="TD140px">
                        <asp:Label ID="lblTipo" runat="server" Text="Tipo Novedad:" CssClass="lblCriterios"></asp:Label>
                    </td>
                    <td class="TD140px" colspan="2">
                        <asp:DropDownList ID="ddlNovedad" runat="server">
                        </asp:DropDownList>
                        <asp:CompareValidator ID="cmvddlNovedad" ErrorMessage="*" ControlToValidate="ddlNovedad"
                            runat="server" Operator="GreaterThan" ValueToCompare="0" Type="Integer" ForeColor="Red"
                            Display="Dynamic" ToolTip="Campo Requerido" ValidationGroup="Novedad" />
                    </td>
                </tr>
                <tr>
                    <td class="TD140px" style="vertical-align: text-top">
                        <asp:Label ID="lblObservaciones" runat="server" Text="Comentario:"></asp:Label><br />
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" Columns="40"
                            Rows="10" CssClass="txtMultilinea" />
                        <asp:RequiredFieldValidator ID="rqftxtObservaciones" ErrorMessage="*" ControlToValidate="txtObservaciones"
                            Display="Dynamic" runat="server" ToolTip="Campo Requerido" ValidationGroup="Novedad"
                            ForeColor="Red" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnOcultoNovedad" />
            <asp:PostBackTrigger ControlID="btnGuardar" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Panel>
