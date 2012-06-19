<%@ Page Title="Planificaci&oacute;n de Contenidos" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="Planificacion.aspx.cs" Inherits="EDUAR_UI.Planificacion" %>

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
                        Planificaci&oacute;n de Contenidos</h2>
                    <br />
                </td>
                <td align="right" rowspan="2">
                    <asp:UpdatePanel ID="udpBotonera" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:ImageButton ID="btnNuevo" runat="server" ToolTip="Nuevo" ImageUrl="~/Images/botonNuevo.png"
                                Visible="false" OnClick="btnNuevo_Click" />
                            <asp:ImageButton ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" ToolTip="Guardar"
                                ImageUrl="~/Images/botonGuardar.png" Visible="false" />
                            <asp:ImageButton ID="btnVolver" OnClick="btnVolver_Click" runat="server" ToolTip="Volver"
                                ImageUrl="~/Images/botonVolver.png" Visible="false" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlAsignatura" EventName="SelectedIndexChanged" />
                            <%--<asp:PostBackTrigger ControlID="btnGuardar" />--%>
                            <%--<asp:PostBackTrigger ControlID="btnBuscar" />--%>
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
                        <td class="TD140px">
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
                <asp:GridView ID="gvwPlanificacion" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListado"
                    AutoGenerateColumns="false" AllowPaging="true" Width="500px" DataKeyNames="idTemaPlanificacion"
                    OnRowCommand="gvwPlanificacion_RowCommand" OnPageIndexChanging="gvwPlanificacion_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Acciones">
                            <HeaderStyle HorizontalAlign="center" Width="5%" />
                            <ItemStyle HorizontalAlign="center" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnTemas" runat="server" CommandName="Editar" CommandArgument='<%# Bind("idTemaPlanificacion") %>'
                                    ToolTip="Ver Contenidos" ImageUrl="~/Images/Grillas/action_new.png" />
                                <%--<asp:ImageButton ID="editarEvento" runat="server" CommandName="Editar" CommandArgument='<%# Bind("idTemaPlanificacion") %>'
                                ToolTip="Editar" ImageUrl="~/Images/Grillas/action_edit.png" />--%>
                                <asp:ImageButton ImageUrl="~/Images/Grillas/action_delete.png" runat="server" ID="btnEliminar"
                                    AlternateText="Eliminar" ToolTip="Eliminar" CommandName="Eliminar" CommandArgument='<%# Bind("idTemaPlanificacion") %>'
                                    OnClientClick="return confirm('¿Desea eliminar la planificación seleccionada?')" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Asignatura">
                            <HeaderStyle HorizontalAlign="left" Width="50%" />
                            <ItemStyle HorizontalAlign="left" />
                            <ItemTemplate>
                                <asp:Label ID="lblAsignatura" runat="server" Text='<%# Bind("asignatura.nombre") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha Inicio">
                            <HeaderStyle HorizontalAlign="left" Width="50%" />
                            <ItemStyle HorizontalAlign="left" />
                            <ItemTemplate>
                                <asp:Label ID="lblFechaInicioGrilla" runat="server" Text='<%# Bind("fechaInicioEstimada","{0:d}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha Fin">
                            <HeaderStyle HorizontalAlign="left" Width="50%" />
                            <ItemStyle HorizontalAlign="left" />
                            <ItemTemplate>
                                <asp:Label ID="lblFechaFinGrilla" runat="server" Text='<%# Bind("fechaFinEstimada","{0:d}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
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
                            <asp:Label ID="lblAprobada" runat="server" Text="Aprobada:"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkAprobada" runat="server" Checked="false" Enabled="false" />
                        </td>
                    </tr>
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
                        <td class="TD250px" colspan="2">
                            <asp:Label ID="lblCConceptuales" runat="server" Text="Contenidos Conceptuales"></asp:Label><br />
                            <asp:TextBox ID="txtCConceptuales" runat="server" TextMode="MultiLine" Columns="75"
                                Rows="10" />
                        </td>
                    </tr>
                    <tr>
                        <td class="TD250px" colspan="2">
                            <asp:Label ID="lblCProcedimentales" runat="server" Text="Contenidos Procedimentales"></asp:Label><br />
                            <asp:TextBox ID="txtCProcedimentales" runat="server" TextMode="MultiLine" Columns="75"
                                Rows="10" />
                        </td>
                    </tr>
                    <tr>
                        <td class="TD250px" colspan="2">
                            <asp:Label ID="lblCActitudinales" runat="server" Text="Contenidos Actitudinales"></asp:Label><br />
                            <asp:TextBox ID="txtCActitudinales" runat="server" TextMode="MultiLine" Columns="75"
                                Rows="10" />
                        </td>
                    </tr>
                    <tr>
                        <td class="TD250px" colspan="2">
                            <asp:Label ID="lblEstrategias" runat="server" Text="Estrategias"></asp:Label><br />
                            <asp:TextBox ID="txtEstrategias" runat="server" TextMode="MultiLine" Columns="75"
                                Rows="10" />
                        </td>
                    </tr>
                    <tr>
                        <td class="TD250px" colspan="2">
                            <asp:Label ID="lblCriteriosEvaluacion" runat="server" Text="Criterios de Evaluación"></asp:Label><br />
                            <asp:TextBox ID="txtCriteriosEvaluacion" runat="server" TextMode="MultiLine" Columns="75"
                                Rows="10" />
                        </td>
                    </tr>
                    <tr>
                        <td class="TD250px" colspan="2">
                            <asp:Label ID="lblInstrumentosEvaluación" runat="server" Text="Instrumentos de Evaluación"></asp:Label><br />
                            <asp:TextBox ID="txtInstrumentosEvaluacion" runat="server" TextMode="MultiLine" Columns="75"
                                Rows="10" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
