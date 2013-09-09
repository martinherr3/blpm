<%@ Page Title="Temas de Contenidos" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="TemasContenido.aspx.cs" Inherits="EDUAR_UI.TemasContenido" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divAccion" runat="server">
        <table class="tablaInterna" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <h2>
                        <asp:Label ID="lblTemas" Text="" runat="server" /></h2>
                    <br />
                </td>
                <td align="right" rowspan="2">
                    <asp:UpdatePanel ID="udpBotonera" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:ImageButton ID="btnNuevo" runat="server" ToolTip="Nuevo" ImageUrl="~/Images/botonNuevo.png"
                                Visible="true" />
                            <asp:ImageButton ID="btnVolverContenido" OnClick="btnVolverContenido_Click" runat="server"
                                ToolTip="Volver" ImageUrl="~/Images/botonVolver.png" />
                            <asp:Panel ID="pnlNuevoContenido" runat="server" Width="500px" Style="display: none;
                                text-align: left; min-height: 200px; z-index: 500" CssClass="CajaDialogo">
                                <table class="tablaInterna" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <h2>
                                                <asp:Label ID="lblTitulo" Text="Nuevo Tema" runat="server" /></h2>
                                            <br />
                                        </td>
                                        <td align="right">
                                            <asp:ImageButton ID="btnGuardar" runat="server" ToolTip="Guardar" ImageUrl="~/Images/PopUp/botonGuardar.png"
                                                OnClick="btnGuardar_Click" CausesValidation="true" />
                                            <asp:ImageButton ID="btnVolver" runat="server" ToolTip="Cerrar" ImageUrl="~/Images/PopUp/botonCerrar.png" />
                                        </td>
                                    </tr>
                                </table>
                                <table class="tablaInterna" cellpadding="1" cellspacing="5">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblTituloContenido" Text="Contenido:" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblContenido" Text="" runat="server" />
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TD50px">
                                            <asp:Label ID="lblTituloTema" Text="Título:" runat="server" />
                                        </td>
                                        <td class="TD250px">
                                            <asp:TextBox ID="txtTitulo" runat="server" CssClass="EstiloTxtLargo250" MaxLength="100" />
                                        </td>
                                        <td class="TD50px">
                                            <asp:Label ID="lblObligatorio" Text="Obligatorio:" runat="server" />
                                        </td>
                                        <td class="TD50px">
                                            <asp:CheckBox ID="chkObligatorio" Text="" runat="server" Checked="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TD50px" valign="top">
                                            <asp:Label ID="lblDescripción" Text="Descripción:" runat="server" />
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="txtMultilinea99" MaxLength="1000"
                                                TextMode="MultiLine" Height="150px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <ajaxtoolkit:ModalPopupExtender ID="mpeContenido" runat="server" CancelControlID="btnVolver"
                                OkControlID="btnGuardar" OnOkScript="DoPostBack()" OnCancelScript="Cancel()"
                                PopupControlID="pnlNuevoContenido" TargetControlID="btnNuevo" RepositionMode="RepositionOnWindowResizeAndScroll"
                                BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="pnlNuevoContenido">
                            </ajaxtoolkit:ModalPopupExtender>
                            <script type="text/javascript">
                                function DoPostBack() {
                                    var titulo = document.getElementById('<%= txtTitulo.ClientID %>').value;
                                    if (titulo.toString().trim() == '') {
                                        jAlert('Por favor, ingrese un título válido.', 'Mensaje');
                                        var boton = document.getElementById('<%= btnNuevo.ClientID %>');
                                        boton.click();
                                    }
                                    else {
                                        var descripcion = document.getElementById('<%= txtDescripcion.ClientID %>').value;

                                        if (descripcion.toString().trim() == '') {
                                            jAlert('Por favor, ingrese una descripción válida.', 'Mensaje');
                                            var boton = document.getElementById('<%= btnNuevo.ClientID %>');
                                            boton.click();
                                        }
                                        else
                                            __doPostBack('btnGuardar', 'Click');
                                    }
                                }

                                function Cancel() {
                                    document.getElementById('<%= txtTitulo.ClientID %>').value = '';
                                    document.getElementById('<%= txtDescripcion.ClientID %>').value = '';
                                } 
                            </script>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnGuardar" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <table class="tablaInterna" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <div id="divFiltros" runat="server">
        <asp:UpdatePanel ID="udpGrilla" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gvwContenido" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListado"
                    AutoGenerateColumns="false" AllowPaging="true" Width="700px" DataKeyNames="idTemaContenido"
                    OnRowCommand="gvwContenido_RowCommand" OnPageIndexChanging="gvwContenido_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Acciones">
                            <HeaderStyle HorizontalAlign="center" Width="5%" />
                            <ItemStyle HorizontalAlign="center" />
                            <ItemTemplate>
                                <asp:ImageButton ID="editarEvento" runat="server" CommandName="Editar" CommandArgument='<%# Bind("idTemaContenido") %>'
                                    ToolTip="Editar" ImageUrl="~/Images/Grillas/action_edit.png" Visible='<%#DataBinder.Eval(Container.DataItem, "activo") %>' />
                                <asp:ImageButton ImageUrl="~/Images/Grillas/action_delete.png" runat="server" ID="btnEliminar"
                                    AlternateText="Eliminar" ToolTip="Eliminar" CommandName="Eliminar" CommandArgument='<%# Bind("idTemaContenido") %>'
                                    Visible='<%#DataBinder.Eval(Container.DataItem, "activo") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Título">
                            <HeaderStyle HorizontalAlign="left" Width="50%" />
                            <ItemStyle HorizontalAlign="left" />
                            <ItemTemplate>
                                <asp:Label ID="lblDescripcionGrilla" runat="server" Text='<%# Bind("titulo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Obligatorio">
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblObligatorioGrilla" runat="server" Text='<%# Boolean.Parse(Eval("obligatorio").ToString()) ? "Sí" : "No"  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Activo">
                            <HeaderStyle HorizontalAlign="center" Width="10%" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblActivo" runat="server" Text='<%# Boolean.Parse(Eval("activo").ToString()) ? "Sí" : "No"  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
