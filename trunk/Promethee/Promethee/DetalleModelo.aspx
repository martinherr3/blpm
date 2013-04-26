<%@ Page Title="Modelo en Detalle" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DetalleModelo.aspx.cs" Inherits="Promethee.DetalleModelo" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- <asp:UpdatePanel runat="server">
        <ContentTemplate>--%>
    Modelo:
    <asp:UpdatePanel runat="server" ID="udpNombre" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Label ID="lblModelo" Text="" runat="server" Font-Bold="true" Font-Size="Large" />
            [<asp:LinkButton ID="lnkModelo" Text="Editar" runat="server" OnClick="lnkModelo_Click" />]
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    -Editar Criterio (nombre, peso, función)
    <br />
    <asp:GridView ID="gvwModelo" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListado"
        AllowPaging="false" AutoGenerateColumns="false" AllowSorting="false" Width="100%"
        CellPadding="5" CellSpacing="1" AutoGenerateEditButton="True" OnRowCancelingEdit="gvwModelo_RowCancelingEdit"
        OnRowEditing="gvwModelo_RowEditing" OnRowUpdating="gvwModelo_RowUpdating">
        <SelectedRowStyle BackColor="#2461BF" Font-Bold="True" ForeColor="SlateBlue" />
        <AlternatingRowStyle ForeColor="Black" />
        <RowStyle ForeColor="Black" />
    </asp:GridView>
    <asp:UpdatePanel ID="udpGrilla" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="mpeModelo" runat="server" PopupControlID="pnlModelos"
        TargetControlID="HiddenField1" RepositionMode="RepositionOnWindowResizeAndScroll"
        BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="pnlModelos">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlModelos" runat="server" Width="600px" Height="200px" Style="display: none;
        text-align: left" BorderStyle="Outset" CssClass="CajaDialogo">
        <h2>
            <asp:Label ID="Label2" Text="Modelo" runat="server" /></h2>
        <table class="tablaInterna" cellpadding="1" cellspacing="5">
            <tr>
                <td class="TDCriterios30">
                    Nombre
                </td>
                <td>
                    <asp:UpdatePanel ID="udpModelosAsociados" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="txtNombre" runat="server" MaxLength="50" CssClass="EstiloTxtLargo350"
                                AutoCompleteType="Disabled" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnGuardarModelo" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:ImageButton ID="btnGuardarModelo" runat="server" ToolTip="Aceptar" ImageUrl="~/Images/button_ok.png"
                        OnClick="btnGuardarModelo_Click" />
                    <asp:ImageButton ID="btnVolverModelo" runat="server" ToolTip="Cancelar" ImageUrl="~/Images/button_cancel.png"
                        OnClick="btnCerrarPopUp_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:HiddenField ID="HiddenField5" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="mpeError" runat="server" PopupControlID="pnlError"
        TargetControlID="HiddenField5" RepositionMode="RepositionOnWindowResizeAndScroll"
        BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="pnlError">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlError" runat="server" Width="300px" Height="170px" Style="display: none;
        text-align: left" BorderStyle="Outset" CssClass="CajaDialogo">
        <table class="tablaInterna" border="0" cellpadding="1" cellspacing="5">
            <tr>
                <td>
                    <h2>
                        Mensaje</h2>
                </td>
            </tr>
        </table>
        <table class="tablaInterna" border="0" cellpadding="1" cellspacing="5">
            <tr>
                <td class="TD140px" style="vertical-align: text-bottom">
                    <asp:UpdatePanel UpdateMode="Conditional" ID="udpError" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblError" Text="" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <br />
                    <asp:ImageButton ID="btnError" runat="server" ToolTip="Aceptar" ImageUrl="~/Images/button_ok.png"
                        OnClick="btnCerrarPopUp_Click" CausesValidation="false" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
