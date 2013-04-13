<%@ Page Title="Mis Modelos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Modelos.aspx.cs" Inherits="Promethee.Modelos" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/UserControls/Criterio.ascx" TagName="Criterio" TagPrefix="cri" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#<%=gvwModelo.ClientID %> img').click(function () {

                var img = $(this)
                var orderid = $(this).attr('orderid');

                var tr = $('#<%=gvwModelo.ClientID %> tr[orderid =' + orderid + ']')
                tr.toggle();

                if (tr.is(':visible'))
                    img.attr('src', 'Images/Grillas/dialog-fewer.png');
                else
                    img.attr('src', 'Images/Grillas/dialog-more.png');
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="tablaInternaSinBorde" border="0" cellpadding="1" cellspacing="5">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="" Font-Bold="True" Font-Size="Large"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                Mis Modelos
            </td>
        </tr>
    </table>
    <asp:ImageButton ID="btnNuevo" ImageUrl="~/Images/programs.png" runat="server" ToolTip="Nuevo Modelo"
        OnClick="btnNuevo_Click" />
    <asp:GridView ID="gvwModelo" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListado"
        AutoGenerateColumns="false" AllowPaging="false" Width="100%" DataKeyNames="idModelo"
        OnRowCommand="gvwModelo_RowCommand" OnPageIndexChanging="gvwModelo_PageIndexChanging"
        OnRowDataBound="gvwModelo_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="Acciones">
                <HeaderStyle HorizontalAlign="center" Width="15%" />
                <ItemStyle HorizontalAlign="center" />
                <ItemTemplate>
                    <img alt="Detalle" src="Images/Grillas/dialog-more.png" orderid="<%# Eval("idModelo") %>"
                        style="cursor: pointer" />
                    <asp:ImageButton ID="editModelo" runat="server" CommandName="editModelo" CommandArgument='<%# Bind("idModelo") %>'
                        ToolTip="Editar Modelo" ImageUrl="~/Images/Grillas/edit_Model.png" />
                    <asp:ImageButton ID="addAlternativa" runat="server" CommandName="addAlternativa"
                        CommandArgument='<%# Bind("idModelo") %>' ToolTip="Agregar Alternativa" ImageUrl="~/Images/Grillas/add_Alternative.png"
                        Visible='<%# Eval("filename").ToString() != string.Empty ? false : true %>' />
                    <asp:ImageButton ID="addCriterio" runat="server" CommandName="addCriterio" CommandArgument='<%# Bind("idModelo") %>'
                        ToolTip="Agregar Criterio" ImageUrl="~/Images/Grillas/add_Criteria.png" Visible='<%# Eval("filename").ToString() != string.Empty ? false : true %>' />
                    <asp:ImageButton ID="btnDownload" runat="server" ToolTip="Descargar Planilla" ImageUrl="~/Images/Grillas/downloads.png"
                        OnClick="btnDownload_OnClick" Visible='<%# Convert.ToInt32(Eval("criterios")) >= 2 && Convert.ToInt32(Eval("alternativas")) >= 2 ? true : false %>' />
                    <asp:ImageButton ID="btnUpload" CommandName="upload" CommandArgument='<%# Bind("idModelo") %>'
                        runat="server" ToolTip="Cargar Planilla" ImageUrl="~/Images/Grillas/uploads.png"
                        Visible='<%# Convert.ToInt32(Eval("criterios")) >= 2 && Convert.ToInt32(Eval("alternativas")) >= 2 ? true : false %>' />
                    <asp:ImageButton ID="solve" runat="server" CommandName="solve" CommandArgument='<%# Bind("idModelo") %>'
                        ToolTip="Resolver" ImageUrl="~/Images/Grillas/solve.png" Visible='<%# Eval("filename").ToString() != string.Empty ? true:false %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Fecha de Creación">
                <HeaderStyle HorizontalAlign="center" Width="10%" />
                <ItemStyle HorizontalAlign="center" />
                <ItemTemplate>
                    <asp:Label ID="lblFechaGrilla" runat="server" Text='<%# Bind("fechaCreacion","{0:d}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Modelo">
                <HeaderStyle HorizontalAlign="left" Width="40%" />
                <ItemStyle HorizontalAlign="left" />
                <ItemTemplate>
                    <asp:Label ID="lblModeloGrilla" runat="server" Text='<%# Bind("nombre") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Nro de Alternativas">
                <HeaderStyle HorizontalAlign="center" Width="10%" />
                <ItemStyle HorizontalAlign="center" />
                <ItemTemplate>
                    <asp:Label ID="lblAlternativasGrilla" runat="server" Text='<%# Bind("alternativas") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Nro de Criterios">
                <HeaderStyle HorizontalAlign="center" Width="10%" />
                <ItemStyle HorizontalAlign="center" />
                <ItemTemplate>
                    <asp:Label ID="lblCriteriosGrilla" runat="server" Text='<%# Bind("criterios") %>'></asp:Label>
                    <tr style="display: none;" orderid="<%# Eval("idModelo") %>">
                        <td colspan="100%">
                            <div style="position: relative; left: 25px;">
                                <asp:GridView ID="gvwDetalle" runat="server" AutoGenerateColumns="true" BackColor="White"
                                    BorderStyle="Solid" CellPadding="0" ForeColor="Black" GridLines="Vertical" CssClass="DatosLista"
                                    SkinID="gridviewSkinPagerIndicadores">
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:UpdatePanel runat="server" ID="udpModelos" UpdateMode="Conditional">
        <ContentTemplate>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <ajaxtoolkit:ModalPopupExtender ID="mpeModelo" runat="server" PopupControlID="pnlModelos"
        TargetControlID="HiddenField1" RepositionMode="RepositionOnWindowResizeAndScroll"
        BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="pnlModelos">
    </ajaxtoolkit:ModalPopupExtender>
    <asp:Panel ID="pnlModelos" runat="server" Width="600px" Height="200px" Style="display: none;
        text-align: left" BorderStyle="Outset" CssClass="CajaDialogo">
        <asp:UpdatePanel ID="udpModelosAsociados" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <h2>
                    <asp:Label ID="Label2" Text="Modelo" runat="server" /></h2>
                <table class="tablaInterna" cellpadding="1" cellspacing="5">
                    <tr>
                        <td class="TDCriterios30">
                            Nombre
                        </td>
                        <td>
                            <asp:TextBox ID="txtNombre" runat="server" MaxLength="50" CssClass="EstiloTxtLargo350"
                                AutoCompleteType="Disabled" />
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
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnVolverModelo" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:HiddenField ID="HiddenField2" runat="server" />
    <ajaxtoolkit:ModalPopupExtender ID="mpeAlternativas" runat="server" PopupControlID="pnlAlternativas"
        TargetControlID="HiddenField2" RepositionMode="RepositionOnWindowResizeAndScroll"
        BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="pnlAlternativas">
    </ajaxtoolkit:ModalPopupExtender>
    <asp:Panel ID="pnlAlternativas" runat="server" Width="500px" Height="200px" Style="display: none;
        text-align: left" BorderStyle="Outset" CssClass="CajaDialogo">
        <table class="tablaInterna" border="0" cellpadding="1" cellspacing="5">
            <tr>
                <td>
                    <h2>
                        Nueva Alternativa</h2>
                </td>
            </tr>
        </table>
        <table class="tablaInterna" border="0" cellpadding="1" cellspacing="5">
            <tr>
                <td class="TD50px">
                    Nombre
                </td>
                <td class="TD250px">
                    <asp:TextBox ID="txtAlternativa" runat="server" CssClass="EstiloTxtLargo250" MaxLength="50" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label Text="" ID="lblErrorAlternativa" runat="server" ForeColor="Red" Font-Bold="true" />
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <br />
                    <asp:ImageButton ID="btnGuardarAlternativa" runat="server" ToolTip="Aceptar" ImageUrl="~/Images/button_ok.png"
                        OnClick="btnGuardarAlternativa_Click" />
                    <asp:ImageButton ID="btnVolverAlternativa" runat="server" ToolTip="Cancelar" ImageUrl="~/Images/button_cancel.png"
                        OnClick="btnCerrarPopUp_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:HiddenField ID="HiddenField3" runat="server" />
    <ajaxtoolkit:ModalPopupExtender ID="mpeCriterios" runat="server" PopupControlID="pnlCriterios"
        TargetControlID="HiddenField3" RepositionMode="RepositionOnWindowResizeAndScroll"
        BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="pnlCriterios">
    </ajaxtoolkit:ModalPopupExtender>
    <asp:Panel ID="pnlCriterios" runat="server" Width="500px" Height="500px" Style="display: none;
        text-align: left" BorderStyle="Outset" CssClass="CajaDialogo">
        <table class="tablaInterna" border="0" cellpadding="1" cellspacing="5">
            <tr>
                <td>
                    <h2>
                        Nuevo Criterio</h2>
                </td>
            </tr>
        </table>
        <table class="tablaInterna" border="0" cellpadding="1" cellspacing="5">
            <tr>
                <td>
                    <cri:Criterio ID="nuevoCriterio" runat="server" nombreCriterio="nuevoCriterio" esMaximzante="false">
                    </cri:Criterio>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <br />
                    <asp:ImageButton ID="btnGuardarCriterio" runat="server" ToolTip="Aceptar" ImageUrl="~/Images/button_ok.png"
                        OnClick="btnGuardarCriterio_Click" />
                    <asp:ImageButton ID="btnVolverCriterio" runat="server" ToolTip="Cancelar" ImageUrl="~/Images/button_cancel.png"
                        OnClick="btnCerrarPopUp_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:HiddenField ID="HiddenField4" runat="server" />
    <ajaxtoolkit:ModalPopupExtender ID="mpuUpload" runat="server" PopupControlID="pnlUpload"
        TargetControlID="HiddenField2" RepositionMode="RepositionOnWindowResizeAndScroll"
        BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="pnlAlternativas">
    </ajaxtoolkit:ModalPopupExtender>
    <asp:Panel ID="pnlUpload" runat="server" Width="500px" Height="180px" Style="display: none;
        text-align: left" BorderStyle="Outset" CssClass="CajaDialogo">
        <table class="tablaInterna" border="0" cellpadding="1" cellspacing="5">
            <tr>
                <td>
                    <h2>
                        Cargar Planilla</h2>
                </td>
            </tr>
        </table>
        <table class="tablaInterna" border="0" cellpadding="1" cellspacing="5">
            <tr>
                <td class="TD100px" style="vertical-align: text-bottom">
                    Subir Archivo
                </td>
                <td>
                    <asp:FileUpload ID="fuCargarArchivo" runat="server" />
                    <%--<asp:RegularExpressionValidator ID="regexValidator" runat="server" ControlToValidate="fuCargarArchivo"
                        ErrorMessage="<br />Sólo se permiten archivos con extensión XLS" ForeColor="Red"
                        Display="Dynamic" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.txt|.TXT|.xls|.XLS)$">
                    </asp:RegularExpressionValidator>--%>
                    <asp:RegularExpressionValidator ID="regexValidator" runat="server" ControlToValidate="fuCargarArchivo"
                        ErrorMessage="<br />Sólo se permiten archivos XLS" ForeColor="Red" Display="Static"
                        ValidationExpression="^(.*\.xls)|(.*\.XLS)$">
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <br />
                    <asp:ImageButton ID="btnUploadFile" runat="server" ToolTip="Aceptar" ImageUrl="~/Images/button_ok.png"
                        OnClick="btnUpload_OnClick" CausesValidation="true" />
                    <asp:ImageButton ID="ImageButton2" runat="server" ToolTip="Cancelar" ImageUrl="~/Images/button_cancel.png"
                        OnClick="btnCerrarPopUp_Click" CausesValidation="false" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
