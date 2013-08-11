<%@ Page Title="Planificaci&oacute;n de Contenidos" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="PlanificacionAnual.aspx.cs" Inherits="EDUAR_UI.Planificacion" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="~/UserControls/Calendario.ascx" TagName="Calendario" TagPrefix="cal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divAccion" runat="server">
        <table class="tablaInterna" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <h2>
                        <asp:UpdatePanel runat="server" ID="udpTitulo" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="lblTituloPrincipal" Text="Planificación de Actividades" runat="server" /></h2>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                </td>
                <td align="right" rowspan="2">
                    <asp:UpdatePanel ID="udpBotonera" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:ImageButton ID="btnContenidosPopUp" OnClick="btnContenidosPopUp_Click" runat="server"
                                ToolTip="Contenidos" ImageUrl="~/Images/botonContenidos.png" Visible="false" />
                            <asp:ImageButton ID="btnCursos" runat="server" ToolTip="Cursos" ImageUrl="~/Images/botonLista.png"
                                Visible="false" OnClick="btnCursos_Click" />
                            <asp:ImageButton ID="btnNuevo" runat="server" ToolTip="Nuevo" ImageUrl="~/Images/botonNuevo.png"
                                Visible="false" OnClick="btnNuevo_Click" />
                            <asp:ImageButton ID="btnPDF" runat="server" ToolTip="Exportar a PDF" ImageUrl="~/Images/ExportarPDF.png"
                                Visible="false" OnClick="btnPDF_Click" />
                            <asp:ImageButton ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" ToolTip="Guardar"
                                ImageUrl="~/Images/botonGuardar.png" Visible="false" />
                            <asp:ImageButton ID="btnVolver" OnClick="btnVolver_Click" runat="server" ToolTip="Volver"
                                ImageUrl="~/Images/botonVolver.png" Visible="false" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlAsignatura" EventName="SelectedIndexChanged" />
                            <asp:PostBackTrigger ControlID="btnPDF" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    <asp:UpdatePanel runat="server" ID="udpGrilla" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="divFiltros" runat="server">
                <table class="tablaInterna" cellpadding="1" cellspacing="5">
                    <tr>
                        <td class="TD75px">
                            <asp:Label ID="lblNivel" runat="server" Text="Nivel:" CssClass="lblCriterios"></asp:Label>
                        </td>
                        <td class="TD140px">
                            <asp:DropDownList ID="ddlNivel" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlNivel_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="TD100px">
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
                                    <asp:AsyncPostBackTrigger ControlID="ddlNivel" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
                <asp:UpdatePanel runat="server" ID="udpOrientacion" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table class="tablaInternaSinBorde" cellpadding="1" cellspacing="5">
                            <tr>
                                <td class="TD75px">
                                    <asp:Label ID="lblOrientacion" runat="server" Text="Orientación:" CssClass="lblCriterios"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList ID="ddlOrientacion" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrientacion_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlAsignatura" EventName="SelectedIndexChanged" />
                        <asp:PostBackTrigger ControlID="ddlOrientacion" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdatePanel runat="server" ID="udpAprobacion" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="divAprobacion" runat="server" visible="false">
                            <table class="tablaInternaSinBorde" cellpadding="1" cellspacing="5">
                                <tr>
                                    <td class="TD140px">
                                        <asp:Label ID="lblSolicitarAprobacion" runat="server" Text="Solicitar Aprobación:"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:CheckBox ID="chkSolicitarAprobacion" runat="server" Checked="false" OnCheckedChanged="chkSolicitarAprobacion_CheckedChanged"
                                            AutoPostBack="true" Visible="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TD140px">
                                        <asp:Label ID="lblAprobada" runat="server" Text="Aprobada:"></asp:Label>
                                    </td>
                                    <td class="TD50px">
                                        <asp:CheckBox ID="chkAprobada" runat="server" Checked="false" Enabled="false" OnCheckedChanged="chkAprobada_CheckedChanged"
                                            AutoPostBack="true" Visible="true" />
                                    </td>
                                    <td class="TD250px">
                                        <asp:Label ID="lblFecha" runat="server" Text="Fecha Aprobadación: "></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="chkAprobada" />
                        <asp:PostBackTrigger ControlID="chkSolicitarAprobacion" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:GridView ID="gvwPlanificacion" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListado"
                    AutoGenerateColumns="false" AllowPaging="true" Width="80%" DataKeyNames="idTemaPlanificacion"
                    OnRowCommand="gvwPlanificacion_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Acciones">
                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%# Bind("idTemaPlanificacion") %>'
                                    ToolTip="Planficación" AlternateText="Planficación" ImageUrl="~/Images/Grillas/action_edit.png"
                                    Visible='<%# CheckAprobada(Eval("fechaAprobada"), true) %>' />
                                <asp:ImageButton ID="btnConsultar" runat="server" CommandName="Consultar" CommandArgument='<%# Bind("idTemaPlanificacion") %>'
                                    ToolTip="Consultar" AlternateText="Consultar" ImageUrl="~/Images/Grillas/action_lookup.png"
                                    Visible='<%# CheckAprobada(Eval("fechaAprobada"), false) %>' />
                                <asp:ImageButton ImageUrl="~/Images/Grillas/action_delete.png" runat="server" ID="btnEliminar"
                                    AlternateText="Eliminar" ToolTip="Eliminar" CommandName="Eliminar" CommandArgument='<%# Bind("idTemaPlanificacion") %>'
                                    Visible='<%# CheckAprobada(Eval("fechaAprobada"), true) %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha Inicio">
                            <HeaderStyle HorizontalAlign="Center" Width="15%" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblFechaInicioGrilla" runat="server" Text='<%# Bind("fechaInicioEstimada","{0:d}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha Fin">
                            <HeaderStyle HorizontalAlign="Center" Width="15%" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblFechaFinGrilla" runat="server" Text='<%# Bind("fechaFinEstimada","{0:d}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Descripcion">
                            <HeaderStyle HorizontalAlign="Center" Width="60%" />
                            <ItemStyle HorizontalAlign="left" />
                            <ItemTemplate>
                                <asp:Label ID="lblDescripcion" runat="server" Text='<%# Bind("contenidosConceptuales","{0:d}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlAsignatura" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="udpDivControles" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="divControles" runat="server">
                <table class="tablaInterna" cellpadding="1" cellspacing="5">
                    <tr>
                        <td class="TD140px">
                            <asp:Label ID="lblFechaInicio" runat="server" Text="Fecha Inicio:"></asp:Label>
                        </td>
                        <td>
                            <cal:Calendario ID="calFechaDesde" runat="server" TipoCalendario="SoloFecha" />
                        </td>
                    </tr>
                    <tr>
                        <td class="TD140px">
                            <asp:Label ID="lblFechaFin" runat="server" Text="Fecha Finalización:"></asp:Label>
                        </td>
                        <td>
                            <cal:Calendario ID="calFechaFin" runat="server" TipoCalendario="SoloFecha" />
                        </td>
                    </tr>
                    <tr>
                        <td class="TD250px" colspan="2" style="text-align: center; vertical-align: middle">
                        </td>
                    </tr>
                </table>
                <ajaxtoolkit:Accordion ID="MyAccordion" runat="Server" SelectedIndex="0" HeaderCssClass="accordionHeader"
                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                    AutoSize="None" FadeTransitions="true" TransitionDuration="250" FramesPerSecond="50"
                    RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                    <Panes>
                        <ajaxtoolkit:AccordionPane ID="acpContenidosConceptuales" runat="server" HeaderCssClass="accordionHeader"
                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                            <Header>
                                Contenidos Conceptuales
                            </Header>
                            <Content>
                                <asp:TextBox ID="txtCConceptuales" runat="server" Columns="75" CssClass="txtMultilinea99"
                                    Rows="10" TextMode="MultiLine" />
                            </Content>
                        </ajaxtoolkit:AccordionPane>
                        <ajaxtoolkit:AccordionPane ID="acpContenidosProcedimentales" runat="server" HeaderCssClass="accordionHeader"
                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                            <Header>
                                Contenidos Procedimentales
                            </Header>
                            <Content>
                                <asp:TextBox ID="txtCProcedimentales" runat="server" Columns="75" CssClass="txtMultilinea99"
                                    Rows="10" TextMode="MultiLine" />
                            </Content>
                        </ajaxtoolkit:AccordionPane>
                        <ajaxtoolkit:AccordionPane ID="acpContenidosActitudinales" runat="server" HeaderCssClass="accordionHeader"
                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                            <Header>
                                Contenidos Actitudinales
                            </Header>
                            <Content>
                                <asp:TextBox ID="txtCActitudinales" runat="server" Columns="75" CssClass="txtMultilinea99"
                                    Rows="10" TextMode="MultiLine" />
                            </Content>
                        </ajaxtoolkit:AccordionPane>
                        <ajaxtoolkit:AccordionPane ID="acpEstrategias" runat="server" HeaderCssClass="accordionHeader"
                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                            <Header>
                                Estrategias
                            </Header>
                            <Content>
                                <asp:TextBox ID="txtEstrategias" runat="server" Columns="75" CssClass="txtMultilinea99"
                                    Rows="10" TextMode="MultiLine" />
                            </Content>
                        </ajaxtoolkit:AccordionPane>
                        <ajaxtoolkit:AccordionPane ID="acpCriteriosEvaluacion" runat="server" HeaderCssClass="accordionHeader"
                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                            <Header>
                                Criterios de Evaluación
                            </Header>
                            <Content>
                                <asp:TextBox ID="txtCriteriosEvaluacion" runat="server" Columns="75" CssClass="txtMultilinea99"
                                    Rows="10" TextMode="MultiLine" />
                            </Content>
                        </ajaxtoolkit:AccordionPane>
                        <ajaxtoolkit:AccordionPane ID="acpInstrumentosEvaluación" runat="server" HeaderCssClass="accordionHeader"
                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                            <Header>
                                Instrumentos de Evaluación
                            </Header>
                            <Content>
                                <asp:TextBox ID="txtInstrumentosEvaluacion" runat="server" Columns="75" CssClass="txtMultilinea99"
                                    Rows="10" TextMode="MultiLine" />
                            </Content>
                        </ajaxtoolkit:AccordionPane>
                    </Panes>
                </ajaxtoolkit:Accordion>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    <asp:HiddenField ID="HiddenField2" runat="server" />
    <ajaxtoolkit:ModalPopupExtender ID="mpeCursos" runat="server" PopupControlID="pnlCursos"
        TargetControlID="HiddenField2" RepositionMode="RepositionOnWindowResizeAndScroll"
        BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="pnlCursos">
    </ajaxtoolkit:ModalPopupExtender>
    <asp:Panel ID="pnlCursos" runat="server" Width="600px" Height="300px" Style="display: none;
        text-align: left" CssClass="CajaDialogo">
        <asp:UpdatePanel ID="udpCursosAsociados" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="tablaInterna" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <h2>
                                <asp:Label ID="Label1" Text="Cursos Asociados" runat="server" /></h2>
                            <br />
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="btnGuardarCursos" runat="server" ToolTip="Guardar" ImageUrl="~/Images/PopUp/botonGuardar.png"
                                OnClick="btnGuardarCursos_Click" />
                            <asp:ImageButton ID="btnVolverCursos" runat="server" ToolTip="Cerrar"
                                ImageUrl="~/Images/PopUp/botonCerrar.png" OnClick="btnVolverPopUp_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:GridView ID="gvwCursos" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListadoSize10"
                                AutoGenerateColumns="false" Width="500px" DataKeyNames="idCursoCicloLectivo"
                                OnPageIndexChanging="gvwCursos_PageIndexChanging" OnPageIndexChanged="gvwCursos_PageIndexChanged">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderStyle HorizontalAlign="left" Width="10%" />
                                        <ItemStyle HorizontalAlign="center" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelection" Text='<%# Eval("idCursoCicloLectivo")%>' runat="server"
                                                CssClass="HiddenText" Width="30px" ClientIDMode="Static" Enabled='<%#
planificacionEditar.fechaAprobada.HasValue == true ? false : true %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Curso">
                                        <HeaderStyle HorizontalAlign="left" Width="90%" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCurso" runat="server" Text='<%# Bind("curso.nombre") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <ajaxtoolkit:ModalPopupExtender ID="mpeContenido" runat="server" PopupControlID="pnlContenidos"
        TargetControlID="HiddenField1" RepositionMode="RepositionOnWindowResizeAndScroll"
        BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="pnlContenidos">
    </ajaxtoolkit:ModalPopupExtender>
    <asp:Panel ID="pnlContenidos" runat="server" Width="600px" Height="510px" Style="display: none;
        text-align: left" CssClass="CajaDialogo">
        <asp:UpdatePanel ID="udpContenidosAsociados" runat="server">
            <ContentTemplate>
                <table class="tablaInterna" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <h2>
                                <asp:Label ID="lblTitulo" Text="Contenidos Asociados" runat="server" /></h2>
                            <br />
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="btnGuardarPopUp" runat="server" ToolTip="Guardar" ImageUrl="~/Images/PopUp/botonGuardar.png"
                                OnClick="btnGuardarPopUp_Click" />
                            <asp:ImageButton ID="btnVolverPopUp" runat="server" ToolTip="Volver - Descartar Cambios"
                                ImageUrl="~/Images/PopUp/botonVolver.png" OnClick="btnVolverPopUp_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:GridView ID="gvwContenidos" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListadoSize10"
                                AutoGenerateColumns="false" Width="500px" DataKeyNames="idTemaContenido" OnPageIndexChanging="gvwContenidos_PageIndexChanging"
                                OnPageIndexChanged="gvwContenidos_PageIndexChanged">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelection" Text='<%# Eval("idTemaContenido")%>' runat="server"
                                                CssClass="HiddenText" Width="30px" ClientIDMode="Static" Enabled='<%#
planificacionEditar.fechaAprobada.HasValue == true ? false : true %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tema">
                                        <HeaderStyle HorizontalAlign="left" Width="75%" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTituloContenidos" runat="server" Text='<%# Bind("Titulo") %>' Font-Italic='<%# (bool)Eval("activo") ? false : true %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Obligatorio">
                                        <HeaderStyle HorizontalAlign="center" Width="20%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblObligatorio" runat="server" Text='<%# Boolean.Parse(Eval("Obligatorio").ToString())
? "Sí" : "No" %>' Font-Italic='<%# (bool)Eval("activo") ? false : true %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
