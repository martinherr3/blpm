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
                        <asp:Label ID="lblTituloPrincipal" Text="Planificación de Actividades" runat="server" /></h2>
                    <br />
                </td>
                <td align="right" rowspan="2">
                    <asp:UpdatePanel ID="udpBotonera" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:ImageButton ID="btnContenidosPopUp" OnClick="btnContenidosPopUp_Click" runat="server"
                                ToolTip="Asociar Contenidos" ImageUrl="~/Images/botonContenidos.png" Visible="false" />
                            <asp:ImageButton ID="btnNuevo" runat="server" ToolTip="Nuevo" ImageUrl="~/Images/botonNuevo.png"
                                Visible="false" OnClick="btnNuevo_Click" />
                            <asp:ImageButton ID="btnPDF" runat="server" ToolTip="Exportar a PDF" ImageUrl="~/Images/ExportarPDF.png"
                                Visible="false" OnClick="btnPDF_Click" />
                            <asp:ImageButton ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" ToolTip="Guardar"
                                ImageUrl="~/Images/botonGuardar.png" Visible="false" />
                            <asp:ImageButton ID="btnVolver" OnClick="btnVolver_Click" runat="server" ToolTip="Volver"
                                ImageUrl="~/Images/botonVolver.png" Visible="false" />
                            <asp:ImageButton ID="btnVolverAnterior" OnClick="btnVolverAnterior_Click" runat="server"
                                ToolTip="Volver" ImageUrl="~/Images/botonVolver.png" Visible="true" />
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
                        <%--<td class="TD110px">
                            <asp:Label ID="lblCurso" runat="server" Text="Curso:" CssClass="lblCriterios"></asp:Label>
                        </td>
                        <td class="TD140px">
                            <asp:DropDownList ID="ddlCurso" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCurso_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>--%>
                        <td class="TD100px">
                            <asp:Label ID="lblAsignatura" runat="server" Text="Asignatura:" CssClass="lblCriterios"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="udpAsignatura" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlAsignatura" runat="server" Enabled="true" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlAsignatura_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <%--<asp:AsyncPostBackTrigger ControlID="ddlCurso" EventName="SelectedIndexChanged" />--%>
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:UpdatePanel runat="server" ID="udpAprobacion" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="divAprobacion" runat="server" visible="false">
                            <table class="tablaInterna" cellpadding="1" cellspacing="5">
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
                            <HeaderStyle HorizontalAlign="Center" Width="45%" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblFechaInicioGrilla" runat="server" Text='<%# Bind("fechaInicioEstimada","{0:d}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha Fin">
                            <HeaderStyle HorizontalAlign="Center" Width="45%" />
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblFechaFinGrilla" runat="server" Text='<%# Bind("fechaFinEstimada","{0:d}") %>'></asp:Label>
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
                            <%--<asp:Button ID="btnContenidosPopUp" runat="server" Text="Asociar Contenidos"
CssClass="button" OnClick="btnContenidosPopUp_Click" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="TD250px" colspan="2">
                            <asp:Label ID="lblCConceptuales" runat="server" Text="Contenidos
Conceptuales"></asp:Label><br />
                            <asp:TextBox ID="txtCConceptuales" runat="server" TextMode="MultiLine" Columns="75"
                                Rows="10" CssClass="txtMultilinea99" />
                        </td>
                    </tr>
                    <tr>
                        <td class="TD250px" colspan="2">
                            <asp:Label ID="lblCProcedimentales" runat="server" Text="Contenidos Procedimentales"></asp:Label><br />
                            <asp:TextBox ID="txtCProcedimentales" runat="server" TextMode="MultiLine" Columns="75"
                                Rows="10" CssClass="txtMultilinea99" />
                        </td>
                    </tr>
                    <tr>
                        <td class="TD250px" colspan="2">
                            <asp:Label ID="lblCActitudinales" runat="server" Text="Contenidos Actitudinales"></asp:Label><br />
                            <asp:TextBox ID="txtCActitudinales" runat="server" TextMode="MultiLine" Columns="75"
                                Rows="10" CssClass="txtMultilinea99" />
                        </td>
                    </tr>
                    <tr>
                        <td class="TD250px" colspan="2">
                            <asp:Label ID="lblEstrategias" runat="server" Text="Estrategias"></asp:Label><br />
                            <asp:TextBox ID="txtEstrategias" runat="server" TextMode="MultiLine" Columns="75"
                                Rows="10" CssClass="txtMultilinea99" />
                        </td>
                    </tr>
                    <tr>
                        <td class="TD250px" colspan="2">
                            <asp:Label ID="lblCriteriosEvaluacion" runat="server" Text="Criterios de Evaluación"></asp:Label><br />
                            <asp:TextBox ID="txtCriteriosEvaluacion" runat="server" TextMode="MultiLine" Columns="75"
                                Rows="10" CssClass="txtMultilinea99" />
                        </td>
                    </tr>
                    <tr>
                        <td class="TD250px" colspan="2">
                            <asp:Label ID="lblInstrumentosEvaluación" runat="server" Text="Instrumentos
de Evaluación"></asp:Label><br />
                            <asp:TextBox ID="txtInstrumentosEvaluacion" runat="server" TextMode="MultiLine" Columns="75"
                                Rows="10" CssClass="txtMultilinea99" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <ajaxtoolkit:ModalPopupExtender ID="mpeContenido" runat="server" PopupControlID="pnlContenidos"
        TargetControlID="HiddenField1" RepositionMode="RepositionOnWindowResizeAndScroll"
        BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="pnlContenidos">
    </ajaxtoolkit:ModalPopupExtender>
    <asp:Panel ID="pnlContenidos" runat="server" Width="600px" Height="400px" Style="display: none;
        text-align: left" BorderStyle="Groove" CssClass="CajaDialogo">
        <asp:UpdatePanel ID="udpContenidosAsociados" runat="server">
            <ContentTemplate>
                <table class="tablaInterna" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <h2>
                                <asp:Label ID="lblTitulo" Text="Asociar
Contenidos" runat="server" /></h2>
                            <br />
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="btnGuardarPopUp" runat="server" ToolTip="Guardar" ImageUrl="~/Images/PopUp/botonGuardar.png"
                                OnClick="btnGuardarPopUp_Click" />
                            <asp:ImageButton ID="btnVolverPopUp" runat="server" ToolTip="Volver <br />Descartar Cambios"
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
planificacionEditar.fechaAprobada.HasValue == true ? false : true%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tema">
                                        <HeaderStyle HorizontalAlign="left" Width="75%" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTituloContenidos" runat="server" Text='<%# Bind("Titulo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Obligatorio">
                                        <HeaderStyle HorizontalAlign="center" Width="20%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblObligatorio" runat="server" Text='<%# Boolean.Parse(Eval("Obligatorio").ToString())
? "Sí" : "No" %>'></asp:Label>
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
