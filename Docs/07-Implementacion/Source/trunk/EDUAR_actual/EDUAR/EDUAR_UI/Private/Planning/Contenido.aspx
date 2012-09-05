<%@ Page Title="Carga de Contenidos" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="Contenido.aspx.cs" Inherits="EDUAR_UI.Contenido" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .fadingTooltip
        {
            border-right: darkgray 1px outset;
            border-top: darkgray 1px outset;
            font-size: 12pt;
            border-left: darkgray 1px outset;
            width: auto;
            color: black;
            border-bottom: darkgray 1px outset;
            height: auto;
            background-color: lemonchiffon;
            margin: 3px 3px 3px 3px;
            padding: 3px 3px 3px 3px;
            border-bottom-width: 3px 3px 3px 3px;
        }
    </style>
    <div class="fadingTooltip" id="fadingTooltip" style="z-index: 999; visibility: hidden;
        position: absolute">
    </div>
    <script type="text/javascript">
        var fadingTooltip;
        var wnd_height, wnd_width;
        var tooltip_height, tooltip_width;
        var tooltip_shown = false;
        var transparency = 100;
        var timer_id = 1;
        var tooltiptext;

        // override events
        window.onload = WindowLoading;
        window.onresize = UpdateWindowSize;
        document.onmousemove = AdjustToolTipPosition;

        function DisplayTooltip(tooltip_text) {
            fadingTooltip.innerHTML = tooltip_text;
            tooltip_shown = (tooltip_text != "") ? true : false;
            if (tooltip_text != "") {
                // Get tooltip window height
                tooltip_height = (fadingTooltip.style.pixelHeight) ? fadingTooltip.style.pixelHeight : fadingTooltip.offsetHeight;
                transparency = 0;
                ToolTipFading();
            }
            else {
                clearTimeout(timer_id);
                fadingTooltip.style.visibility = "hidden";
            }
        }

        function AdjustToolTipPosition(e) {
            if (tooltip_shown) {
                // Depending on IE/Firefox, find out what object to use to find mouse position
                var ev;
                if (e)
                    ev = e;
                else
                    ev = event;

                fadingTooltip.style.visibility = "visible";
                offset_y = (ev.clientY + tooltip_height - document.body.scrollTop + 30 >= wnd_height) ? -15 - tooltip_height : 20;
                fadingTooltip.style.left = Math.min(wnd_width - tooltip_width - 10, Math.max(3, ev.clientX + 6)) + document.body.scrollLeft + 'px';
                fadingTooltip.style.top = ev.clientY + offset_y + document.body.scrollTop + 'px';
            }
        }

        function WindowLoading() {
            fadingTooltip = document.getElementById('fadingTooltip');

            // Get tooltip  window width				
            tooltip_width = (fadingTooltip.style.pixelWidth) ? fadingTooltip.style.pixelWidth : fadingTooltip.offsetWidth;

            // Get tooltip window height
            tooltip_height = (fadingTooltip.style.pixelHeight) ? fadingTooltip.style.pixelHeight : fadingTooltip.offsetHeight;

            UpdateWindowSize();
        }

        function ToolTipFading() {
            if (transparency <= 100) {
                fadingTooltip.style.filter = "alpha(opacity=" + transparency + ")";
                fadingTooltip.style.opacity = transparency / 100;
                transparency += 5;
                timer_id = setTimeout('ToolTipFading()', 35);
            }
        }

        function UpdateWindowSize() {
            wnd_height = document.body.clientHeight;
            wnd_width = document.body.clientWidth;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divAccion" runat="server">
        <table class="tablaInterna" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <h2>
                        Carga De Contenidos</h2>
                    <br />
                </td>
                <td align="right" rowspan="2">
                    <asp:UpdatePanel ID="udpBotonera" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:ImageButton ID="btnNuevo" runat="server" ToolTip="Nuevo" ImageUrl="~/Images/botonNuevo.png"
                                Visible="false" />
                            <%--<asp:ImageButton ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" ToolTip="Buscar"
                                ImageUrl="~/Images/botonBuscar.png" />--%>
                            <asp:Panel ID="pnlNuevoContenido" runat="server" Width="500px" Style="display: none;
                                text-align: left" BorderStyle="Groove" CssClass="CajaDialogo">
                                <table class="tablaInterna" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <h2>
                                                <asp:Label ID="lblTitulo" Text="Nuevo Contenido" runat="server" /></h2>
                                            <br />
                                        </td>
                                        <td align="right">
                                            <asp:ImageButton ID="btnGuardar" runat="server" ToolTip="Guardar" ImageUrl="~/Images/PopUp/botonGuardar.png"
                                                OnClick="btnGuardar_Click" CausesValidation="true" />
                                            <asp:ImageButton ID="btnVolver" runat="server" ToolTip="Volver" ImageUrl="~/Images/PopUp/botonVolver.png" />
                                        </td>
                                    </tr>
                                </table>
                                <table class="tablaInterna" cellpadding="1" cellspacing="5">
                                    <tr>
                                        <td class="TD50px">
                                            <asp:Label Text="Descripción:" runat="server" />
                                        </td>
                                        <td class="TD250px">
                                            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="EstiloTxtLargo250" />
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
                                    var descripcion = document.getElementById('<%= txtDescripcion.ClientID %>').value;

                                    if (descripcion.toString().trim() == '')
                                        alert('Por favor, ingrese una descripción válida.');
                                    else
                                        __doPostBack('btnGuardar', 'Click');
                                }

                                function Cancel() {
                                    document.getElementById('<%= txtDescripcion.ClientID %>').value = '';
                                } 
                            </script>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlAsignatura" EventName="SelectedIndexChanged" />
                            <asp:PostBackTrigger ControlID="btnGuardar" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    <div id="divFiltros" runat="server">
        <table class="tablaInterna" cellpadding="1" cellspacing="5">
            <tr>
                <td class="TD50px">
                    <asp:Label ID="lblCurso" runat="server" Text="Curso:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td class="TD140px">
                    <asp:DropDownList ID="ddlCurso" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCurso_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="TD50px">
                    <asp:Label ID="lblAsignatura" runat="server" Text="Asignatura:" CssClass="lblCriterios"></asp:Label>
                </td>
                <td>
                    <asp:UpdatePanel ID="udpAsignatura" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlAsignatura" runat="server" Enabled="false" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlAsignatura_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlCurso" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="udpGrilla" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gvwContenido" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListado"
                    AutoGenerateColumns="false" AllowPaging="true" Width="500px" DataKeyNames="idContenido"
                    OnRowCommand="gvwContenido_RowCommand" OnPageIndexChanging="gvwContenido_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Acciones">
                            <HeaderStyle HorizontalAlign="center" Width="5%" />
                            <ItemStyle HorizontalAlign="center" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnTemas" runat="server" CommandName="Temas" CommandArgument='<%# Bind("idContenido") %>'
                                    ToolTip="Ver Temas" ImageUrl="~/Images/Grillas/action_new.png" />
                                <asp:ImageButton ID="editarEvento" runat="server" CommandName="Editar" CommandArgument='<%# Bind("idContenido") %>'
                                    ToolTip="Editar" ImageUrl="~/Images/Grillas/action_edit.png" />
                                <asp:ImageButton ImageUrl="~/Images/Grillas/action_delete.png" runat="server" ID="btnEliminar"
                                    AlternateText="Eliminar" ToolTip="Eliminar" CommandName="Eliminar" CommandArgument='<%# Bind("idContenido") %>'
                                    OnClientClick="return confirm('¿Desea eliminar el contenido seleccionado?')" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Descripción">
                            <HeaderStyle HorizontalAlign="left" Width="50%" />
                            <ItemStyle HorizontalAlign="left" />
                            <ItemTemplate>
                                <asp:Label ID="lblDescripcionGrilla" runat="server" Text='<%# Bind("descripcion") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlAsignatura" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
