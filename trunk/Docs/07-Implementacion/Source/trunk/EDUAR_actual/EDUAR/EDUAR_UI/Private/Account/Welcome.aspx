<%@ Page Title="Bienvenido!" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="Welcome.aspx.cs" Inherits="EDUAR_UI.Welcome" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Src="~/UserControls/Calendario.ascx" TagName="Calendario" TagPrefix="cal" %>
<%@ Register Src="~/UserControls/Cursos.ascx" TagName="Curso" TagPrefix="cur" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="tablaInterna" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 80%; vertical-align: text-top">
                <h2>
                    Bienvenido!
                </h2>
                <br />
                <table class="tablaInterna" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 20%; text-align: right" rowspan="2">
                <%--<asp:Image ID="Image1" ImageUrl="~/Images/web/default.png" runat="server" AlternateText="Bienvenido!"
                    ToolTip="Bienvenido!" />--%>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <div id="divMensajes" runat="server">
                    <table width="100%" cellpadding="1" cellspacing="5" border="0">
                        <tr>
                            <td style="width: 30%">
                                <p class="ui-widget">
                                    <asp:Label ID="lblMensajes" Text="Tienes <MENSAJES> <MSJ_STRING> sin leer!!!" runat="server"
                                        Font-Bold="true" /></p>
                            </td>
                            <td style="width: 70%">
                                <p class="ui-widget">
                                    <asp:ImageButton ID="btnMensaje" runat="server" ImageUrl="~/Images/mail-new-message.gif"
                                        OnClick="btnMensaje_Click" /></p>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divEncuesta" runat="server">
                    <table width="100%" cellpadding="1" cellspacing="5" border="0">
                        <tr>
                            <td style="width: 50%">
                                <p class="ui-widget">
                                    <asp:Label ID="lblEncuestas" Text="Tienes <ENCUESTAS> Encuestas pendiente de responder!!!"
                                        runat="server" Font-Bold="true" /></p>
                            </td>
                            <td style="width: 50%">
                                <p class="ui-widget">
                                    <asp:ImageButton ID="btnEncuesta" runat="server" ImageUrl="~/Images/btnEncuesta.png"
                                        OnClick="btnEncuesta_Click" /></p>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <div id="divSecciones" runat="server">
        <table class="tablaInternaSinBorde" border="0" cellpadding="1" cellspacing="5" style="text-align: center">
            <tr>
                <td>
                    <asp:ImageButton ID="btnCursos" ImageUrl="~/Images/Home/Cursos.png" ToolTip="Cursos"
                        runat="server" CommandArgument="Cursos" OnClick="btnPopUP_Click" />
                </td>
                <td>
                    <asp:ImageButton ID="btnEncuestas" ImageUrl="~/Images/Home/Encuestas.png" ToolTip="Encuestas"
                        runat="server" CommandArgument="Encuestas" OnClick="btnPopUP_Click" />
                </td>
                <td>
                    <asp:ImageButton ID="btnReportes" ImageUrl="~/Images/Home/Reportes.png" ToolTip="Reportes"
                        runat="server" CommandArgument="Reportes" OnClick="btnPopUP_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ImageButton ID="btnContenidos" ImageUrl="~/Images/Home/Contenidos.png" ToolTip="Contenidos"
                        runat="server" CommandArgument="Contenidos" OnClick="btnPopUP_Click" />
                </td>
                <td>
                    <asp:ImageButton ID="btnComunicacion" ImageUrl="~/Images/Home/Comunicacion.png" ToolTip="Comunicación"
                        runat="server" CommandArgument="Comunicacion" OnClick="btnPopUP_Click"  />
                </td>
                <td>
                    <asp:ImageButton ID="btnAdministracion" ImageUrl="~/Images/Home/Administracion.png"
                        ToolTip="Administracion" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <!-- Cursos -->
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="mpeCursos" runat="server" PopupControlID="pnlCursos"
        TargetControlID="HiddenField1" RepositionMode="RepositionOnWindowResizeAndScroll"
        BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="pnlCursos">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlCursos" runat="server" Width="1000px" Height="450px" Style="display: none;
        text-align: left" BorderStyle="Outset" CssClass="CajaDialogo">
        <table class="tablaInterna" cellpadding="1" cellspacing="5">
            <tr>
                <td valign="top">
                    <h2>
                        <asp:Label ID="lblTituloPrincipal" Text="Cursos del Ciclo Lectivo Actual" runat="server" /></h2>
                </td>
                <td align="right">
                    <asp:ImageButton ID="btnCerrarCursos" OnClick="btnCerrarPopup_Click" ImageUrl="~/Images/PopUp/botonCerrar.png"
                        ToolTip="Cerrar" runat="server" CommandArgument="Cursos" />
                </td>
            </tr>
        </table>
        <table class="tablaInterna" cellpadding="1" cellspacing="5">
            <tr>
                <td>
                    <asp:UpdatePanel ID="udpCursos" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DataList RepeatLayout="Table" RepeatColumns="6" RepeatDirection="Horizontal"
                                runat="server" ID="dtlCursos" Width="100%" ShowHeader="true" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <cur:Curso ID="curso" runat="server" titulo='<%# Bind("nombre") %>' idCurso='<%# Bind("idCurso") %>'>
                                    </cur:Curso>
                                </ItemTemplate>
                            </asp:DataList>
                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <!-- Encuestas -->
    <asp:HiddenField ID="HiddenField2" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="mpeEncuestas" runat="server" PopupControlID="pnlEncuestas"
        TargetControlID="HiddenField2" RepositionMode="RepositionOnWindowResizeAndScroll"
        BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="pnlEncuestas">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlEncuestas" runat="server" Width="500px" Height="250px" Style="display: none;
        text-align: left" BorderStyle="Outset" CssClass="CajaDialogo">
        <table class="tablaInterna" cellpadding="1" cellspacing="5">
            <tr>
                <td valign="top">
                    <h2>
                        <asp:Label ID="Label1" Text="Encuestas" runat="server" /></h2>
                </td>
                <td align="right">
                    <asp:ImageButton ID="btnCerrarEncuestas" OnClick="btnCerrarPopup_Click" ImageUrl="~/Images/PopUp/botonCerrar.png"
                        ToolTip="Cerrar" runat="server" CommandArgument="Encuestas" />
                </td>
            </tr>
        </table>
        <table class="tablaInterna" cellpadding="1" cellspacing="5">
            <tr>
                <td>
                    <asp:ImageButton ID="btnVerCategorias" ImageUrl="~/Images/Home/VerCategorias.png"
                        ToolTip="Categorías" runat="server" CommandArgument="Categorias" OnClick="btnRedireccion_Click" />
                </td>
                <td>
                    <asp:ImageButton ID="btnVerEncuestas" ImageUrl="~/Images/Home/VerEncuestas.png" ToolTip="Encuestas"
                        runat="server" CommandArgument="Encuestas" OnClick="btnRedireccion_Click" />
                </td>
                <td>
                    <asp:ImageButton ID="btnEscalas" ImageUrl="~/Images/Home/VerEscalas.png" ToolTip="Escalas de Ponderación"
                        runat="server" CommandArgument="Escalas" OnClick="btnRedireccion_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <!-- Reportes -->
    <asp:HiddenField ID="HiddenField3" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="mpeReportes" runat="server" PopupControlID="pnlReportes"
        TargetControlID="HiddenField3" RepositionMode="RepositionOnWindowResizeAndScroll"
        BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="pnlReportes">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlReportes" runat="server" Width="500px" Height="450px" Style="display: none;
        text-align: left" BorderStyle="Outset" CssClass="CajaDialogo">
        <table class="tablaInterna" cellpadding="1" cellspacing="5">
            <tr>
                <td valign="top">
                    <h2>
                        <asp:Label ID="Label2" Text="Reportes" runat="server" /></h2>
                </td>
                <td align="right">
                    <asp:ImageButton ID="btnCerrarReportes" OnClick="btnCerrarPopup_Click" ImageUrl="~/Images/PopUp/botonCerrar.png"
                        ToolTip="Cerrar" runat="server" CommandArgument="Reportes" />
                </td>
            </tr>
        </table>
        <table class="tablaInterna" cellpadding="1" cellspacing="5">
            <tr>
                <td>
                    <asp:ImageButton ID="btnCalificaciones" ImageUrl="~/Images/Home/VerCalificaciones.png"
                        ToolTip="Calificaciones" runat="server" CommandArgument="Calificaciones" OnClick="btnRedireccion_Click" />
                </td>
                <td>
                    <asp:ImageButton ID="btnInasistencias" ImageUrl="~/Images/Home/VerInasistencia.png"
                        ToolTip="Inasistencias" runat="server" CommandArgument="Inasistencias" OnClick="btnRedireccion_Click" />
                </td>
                <td>
                    <asp:ImageButton ID="btnSanciones" ImageUrl="~/Images/Home/VerSanciones.png" ToolTip="Sanciones"
                        runat="server" CommandArgument="Sanciones" OnClick="btnRedireccion_Click" />
                </td>
            </tr>
        </table>
        <table class="tablaInterna" cellpadding="1" cellspacing="5">
            <tr>
                <td valign="top">
                    <h2>
                        <asp:Label ID="Label3" Text="Consolidados" runat="server" /></h2>
                </td>
            </tr>
        </table>
        <table class="tablaInterna" cellpadding="1" cellspacing="5">
            <tr>
                <td>
                    <asp:ImageButton ID="btnConsolidado" ImageUrl="~/Images/Home/VerConsolidado.png"
                        ToolTip="Consolidado" runat="server" CommandArgument="Consolidado" OnClick="btnRedireccion_Click" />
                </td>
                <td>
                    <asp:ImageButton ID="btnIndicadores" ImageUrl="~/Images/Home/VerIndicadores.png"
                        ToolTip="Indicadores" runat="server" CommandArgument="Indicadores" OnClick="btnRedireccion_Click" />
                </td>
                <td>
                    <asp:ImageButton ID="btnHistorico" ImageUrl="~/Images/Home/VerHistorico.png" ToolTip="Histórico"
                        runat="server" CommandArgument="Historico" OnClick="btnRedireccion_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <!-- Reportes -->
    <asp:HiddenField ID="HiddenField4" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="mpeContenidos" runat="server" PopupControlID="pnlContenidos"
        TargetControlID="HiddenField4" RepositionMode="RepositionOnWindowResizeAndScroll"
        BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="pnlContenidos">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlContenidos" runat="server" Width="500px" Height="250px" Style="display: none;
        text-align: left" BorderStyle="Outset" CssClass="CajaDialogo">
        <table class="tablaInterna" cellpadding="1" cellspacing="5">
            <tr>
                <td valign="top">
                    <h2>
                        <asp:Label ID="Label4" Text="Contenidos y Planificacion" runat="server" /></h2>
                </td>
                <td align="right">
                    <asp:ImageButton ID="btnCerrarContenidos" OnClick="btnCerrarPopup_Click" ImageUrl="~/Images/PopUp/botonCerrar.png"
                        ToolTip="Cerrar" runat="server" CommandArgument="Reportes" />
                </td>
            </tr>
        </table>
        <table class="tablaInterna" cellpadding="1" cellspacing="5">
            <tr>
                <td>
                    <asp:ImageButton ID="btnVerCurricula" ImageUrl="~/Images/Home/VerContenidos.png"
                        ToolTip="Contenidos Curriculares" runat="server" CommandArgument="Contenidos"
                        OnClick="btnRedireccion_Click" />
                </td>
                <td>
                    <asp:ImageButton ID="btnPlanificacion" ImageUrl="~/Images/Home/VerPlanificacion.png"
                        ToolTip="Planificacion" runat="server" CommandArgument="Planificacion" OnClick="btnRedireccion_Click" />
                </td>
                <td>
                    <asp:ImageButton ID="btnAprobarPlanificacion" ImageUrl="~/Images/Home/VerAprobaciones.png"
                        ToolTip="Aprobar Planificaciones" runat="server" CommandArgument="Aprobar" OnClick="btnRedireccion_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <!-- Comunicacion -->
    <asp:HiddenField ID="HiddenField5" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="mpeComunicacion" runat="server" PopupControlID="pnlComunicacion"
        TargetControlID="HiddenField5" RepositionMode="RepositionOnWindowResizeAndScroll"
        BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="pnlComunicacion">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlComunicacion" runat="server" Width="500px" Height="250px" Style="display: none;
        text-align: left" BorderStyle="Outset" CssClass="CajaDialogo">
        <table class="tablaInterna" cellpadding="1" cellspacing="5">
            <tr>
                <td valign="top">
                    <h2>
                        <asp:Label ID="Label5" Text="Comunicación" runat="server" /></h2>
                </td>
                <td align="right">
                    <asp:ImageButton ID="btnCerrarComunicacion" OnClick="btnCerrarPopup_Click" ImageUrl="~/Images/PopUp/botonCerrar.png"
                        ToolTip="Cerrar" runat="server" CommandArgument="Comunicacion" />
                </td>
            </tr>
        </table>
        <table class="tablaInterna" cellpadding="1" cellspacing="5">
            <tr>
                <td>
                    <asp:ImageButton ID="btnMensajes" ImageUrl="~/Images/Home/VerMensajes.png" ToolTip="Mensajes"
                        runat="server" CommandArgument="Mensajes" OnClick="btnRedireccion_Click" />
                </td>
                <td>
                    <asp:ImageButton ID="btnCitaciones" ImageUrl="~/Images/Home/VerCitaciones.png"
                        ToolTip="Citaciones" runat="server" CommandArgument="Citaciones" OnClick="btnRedireccion_Click" />
                </td>
                <td>
                    <asp:ImageButton ID="btnNovedades" ImageUrl="~/Images/Home/VerNovedades.png" ToolTip="Novedades Institucionales"
                        runat="server" CommandArgument="Novedades" OnClick="btnRedireccion_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <div id="divAgenda" runat="server" visible="false">
        <table width="100%" cellpadding="1" cellspacing="5" border="0">
            <tr>
                <td style="width: 80%; vertical-align: text-top">
                    <h2>
                        Tu Agenda:
                    </h2>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="udpCampos" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="tablaInterna" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="right">
                            <asp:ImageButton ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" ToolTip="Buscar"
                                ImageUrl="~/Images/botonBuscar.png" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="udpGrilla" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="tablaInterna" cellpadding="1" cellspacing="5">
                    <tr>
                        <td valign="top" colspan="4" class="TDCriterios100">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <cal:Calendario ID="fechas" TipoCalendario="DesdeHasta" runat="server" EtiquetaDesde="Fecha Desde:"
                                        EtiquetaHasta="Fecha Hasta:" TipoAlineacion="Izquierda" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" class="TDCriterios25">
                            <asp:Label ID="lblCurso" runat="server" Text="Curso:"></asp:Label>
                        </td>
                        <td valign="top" class="TDCriterios75" colspan="3">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlCurso" runat="server">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" class="TDCriterios25">
                            <asp:Label ID="lblAlumnos" runat="server" Text="Alumno:"></asp:Label>
                        </td>
                        <td valign="top" class="TDCriterios75" colspan="3">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlAlumnos" runat="server">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gvwAgenda" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListado"
                    AutoGenerateColumns="false" AllowPaging="true" Width="100%" DataKeyNames="idEventoAgenda"
                    OnPageIndexChanging="gvwAgenda_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Evento">
                            <HeaderStyle HorizontalAlign="left" Width="10%" />
                            <ItemStyle HorizontalAlign="left" />
                            <ItemTemplate>
                                <asp:Label ID="lblEventoGrilla" runat="server" Text='<%# Bind("tipoEventoAgenda.descripcion") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha">
                            <HeaderStyle HorizontalAlign="center" Width="5%" />
                            <ItemStyle HorizontalAlign="center" />
                            <ItemTemplate>
                                <asp:Label ID="lblFechaGrilla" runat="server" Text='<%# Bind("fechaEvento","{0:d}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Descripcion">
                            <HeaderStyle HorizontalAlign="left" Width="40%" />
                            <ItemStyle HorizontalAlign="left" />
                            <ItemTemplate>
                                <asp:Label ID="lblDescripcionGrilla" runat="server" Text='<%# TruncateString(DataBinder.Eval(Container.DataItem, "descripcion").ToString())%>'
                                    ToolTip='<%# Bind("descripcion") %>'></asp:Label>
                            </ItemTemplate>
                            <%--<ItemTemplate>
                                <asp:Label ID="lblDescripcion" runat="server" Text='<%# TruncateString(DataBinder.Eval(Container.DataItem, "descripcion").ToString())%>'
                                ToolTip='<%# Bind("descripcion") %>'></asp:Label>
                            </ItemTemplate>--%>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Organizador">
                            <HeaderStyle HorizontalAlign="left" Width="20%" />
                            <ItemStyle HorizontalAlign="left" />
                            <ItemTemplate>
                                <asp:Label ID="lblOrganizadorGrilla" runat="server" Text='<%# String.Format("{0} {1}", Eval("usuario.nombre"), Eval("usuario.apellido")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
