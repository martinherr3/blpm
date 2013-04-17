<%@ Page Title="Mis Modelos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Modelos.aspx.cs" Inherits="Promethee.Modelos" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register TagPrefix="ajaxtoolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
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
    <asp:Label ID="lblError" ForeColor="Red" Font-Bold="true" Text="" runat="server" />
    <asp:GridView ID="gvwModelo" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListado"
        AutoGenerateColumns="false" AllowPaging="true" Width="100%" DataKeyNames="idModelo"
        OnRowCommand="gvwModelo_RowCommand" OnPageIndexChanging="gvwModelo_PageIndexChanging"
        OnRowDataBound="gvwModelo_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="Acciones">
                <HeaderStyle HorizontalAlign="center" Width="15%" />
                <%--<HeaderTemplate>
                    <asp:ImageButton ID="nuevoModelo" runat="server" CommandName="nuevoModelo" CommandArgument="0"
                        ToolTip="Nuevo Modelo" ImageUrl="~/Images/programs.png" />
                </HeaderTemplate>--%>
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
                    <%--<asp:ImageButton ID="btnDownload" ImageUrl="~/Images/Grillas/downloads.png" runat="server"
                        Visible='<%# Convert.ToInt32(Eval("criterios")) >= 2 && Convert.ToInt32(Eval("alternativas")) >= 2 ? true : false %>'
                        CommandName="download" CommandArgument='<%# Bind("idModelo") %>' />--%>
                    <asp:ImageButton ID="btnDownload" runat="server" ToolTip="Descargar Planilla" ImageUrl="~/Images/Grillas/downloads.png"
                        OnClick="btnDownload_OnClick" CommandArgument='<%# Bind("idModelo") %>' Visible='<%# Convert.ToInt32(Eval("criterios")) >= 2 && Convert.ToInt32(Eval("alternativas")) >= 2 ? true : false %>' />
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
            <asp:UpdatePanel ID="udpImgPodio" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table class="tablaInternaSinBorde" border="0" cellpadding="1" cellspacing="5">
                        <tr>
                            <td align="center">
                                <asp:Image ID="imgPodio" AlternateText="Resultado" ToolTip="Resultado" runat="server"
                                    Visible="false" Style="text-align: center" /><br />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel runat="server" ID="udpResultado" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="divResultado" runat="server" visible="false">
                        <table class="tablaInternaSinBorde" border="0" cellpadding="1" cellspacing="5">
                            <tr>
                                <td align="center">
                                    <asp:LinkButton Text="[Más Información]" ID="lnkConfig" runat="server" Visible="true" />
                                </td>
                            </tr>
                        </table>
                        <ajaxtoolkit:CollapsiblePanelExtender ID="cpe" runat="Server" TargetControlID="pnlResultado"
                            CollapsedSize="0" ExpandedSize="500" Collapsed="True" ExpandControlID="lnkConfig"
                            CollapseControlID="lnkConfig" AutoCollapse="False" AutoExpand="False" ScrollContents="false"
                            ExpandDirection="Vertical" />
                        <asp:Panel runat="server" ID="pnlResultado">
                            <asp:Label ID="lblResultadoGrilla" Text="Resultados obtenidos en orden Descendente"
                                runat="server" Visible="false" Font-Bold="true" /><br />
                            <asp:Label ID="lblResultado" Text="" runat="server" CssClass="lblCriterios" Visible="false" />
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gvwResultado" runat="server" CssClass="DatosLista" AllowPaging="True"
                                        AllowSorting="false" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None"
                                        ShowFooter="false" OnPageIndexChanging="gvwResultado_PageIndexChanging" PageSize="15">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Height="24px" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                    <br />
                                </ContentTemplate>
                                <Triggers>
                                    <%--<asp:AsyncPostBackTrigger ControlID="gvwModelo" EventName="RowCommand" />--%>
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <%--<asp:AsyncPostBackTrigger ControlID="gvwModelo" EventName="RowCommand" />--%>
                </Triggers>
            </asp:UpdatePanel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <ajaxtoolkit:ModalPopupExtender ID="mpeModelo" runat="server" PopupControlID="pnlModelos"
        TargetControlID="HiddenField1" RepositionMode="RepositionOnWindowResizeAndScroll"
        BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="pnlModelos">
    </ajaxtoolkit:ModalPopupExtender>
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
                    <%-- <asp:UpdatePanel runat="server">
                        <ContentTemplate>--%>
                    <cri:Criterio ID="nuevoCriterio" runat="server" nombreCriterio="" esMaximzante="false">
                    </cri:Criterio>
                    <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
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
        TargetControlID="HiddenField4" RepositionMode="RepositionOnWindowResizeAndScroll"
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
                <td class="TD140px" style="vertical-align: text-bottom">
                    Seleccionar Archivo
                </td>
                <td>
                    <asp:FileUpload ID="fuCargarArchivo" runat="server" />
                    <asp:RegularExpressionValidator ID="regexValidator" runat="server" ControlToValidate="fuCargarArchivo"
                        ErrorMessage="<br />Sólo se permiten archivos XLS" ForeColor="Red" Display="Static"
                        ValidationExpression="^(.*\.xls)|(.*\.XLS)$">
                    </asp:RegularExpressionValidator>
                    <%--<ajaxtoolkit:AsyncFileUpload runat="server" ID="AsyncFileUpload1" Width="350px" UploaderStyle="Traditional"
                        UploadingBackColor="#8FBC8F" ErrorBackColor="#CD5C5C" BackColor="#DCDCDC" CompleteBackColor="#87CEFA"
                        ThrobberID="imgLoading" />
                    <asp:Image ID="imgLoading" runat="server" ImageUrl="~/Images/ajax-loader.gif" />--%>
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
