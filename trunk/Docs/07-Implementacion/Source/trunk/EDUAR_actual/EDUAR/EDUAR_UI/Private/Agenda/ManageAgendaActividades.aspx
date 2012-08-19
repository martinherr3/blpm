<%@ Page Title="Administrar Agenda Actividades" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="ManageAgendaActividades.aspx.cs" Inherits="EDUAR_UI.ManageAgendaActividades"
    Theme="Tema" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Src="~/UserControls/Calendario.ascx" TagName="Calendario" TagPrefix="cal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="tablaInterna" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <h2>
                            <asp:Label ID="lblTitulo" Text="Actividades" runat="server" /></h2>
                        <br />
                    </td>
                    <td align="right">
                        <asp:ImageButton ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" ToolTip="Buscar"
                            ImageUrl="~/Images/botonBuscar.png" />
                        <asp:ImageButton ID="btnEvaluacion" OnClick="btnEvaluacion_Click" runat="server"
                            Visible="false" ToolTip="Evaluaciones" ImageUrl="~/Images/botonEvaluacion.png" />
                        <asp:ImageButton ID="btnExcursion" OnClick="btnExcursion_Click" runat="server" Visible="false"
                            ToolTip="Excursiones" ImageUrl="~/Images/botonExcursion.png" />
                        <asp:ImageButton ID="btnReunion" OnClick="btnReunion_Click" runat="server" Visible="false"
                            ToolTip="Reuniones" ImageUrl="~/Images/botonReunion.png" />
                        <asp:ImageButton ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" ToolTip="Guardar"
                            ImageUrl="~/Images/botonGuardar.png" CausesValidation="true" ValidationGroup="validarEdit" />
                        <asp:ImageButton ID="btnRegistroClases"  runat="server" ToolTip="Registro De Clases" Visible="false"
                            ImageUrl="~/Images/botonAvance.gif" OnClick="btnRegistroClases_Click" />
                        <asp:ImageButton ID="btnVolver" OnClick="btnVolver_Click" runat="server" ToolTip="Volver"
                            ImageUrl="~/Images/botonVolver.png" />
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="udpFiltrosBusqueda" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table class="tablaInterna" cellpadding="1" cellspacing="5">
                        <%--<tr>
                            <td>
                                <h3>
                                    Buscar Agenda de Actividades -
                                    
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="TD100">
                                <asp:ValidationSummary ID="ValidarBusqueda" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="1" cellspacing="5">
                        <tr>
                            <td valign="top" style="width: 50px">
                                <asp:Label ID="lblCurso" runat="server" Text="Curso:"></asp:Label>
                            </td>
                            <td valign="top" style="width: 200px">
                                <asp:DropDownList ID="ddlCurso" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gvwReporte" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="udpGrilla" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView ID="gvwReporte" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListado"
                AutoGenerateColumns="false" AllowPaging="true" Width="500px" DataKeyNames="idAgendaActividad"
                OnRowCommand="gvwReporte_RowCommand" OnPageIndexChanging="gvwReporte_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Acciones">
                        <HeaderStyle HorizontalAlign="center" Width="5%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="editarEvento" runat="server" CommandName="Editar" CommandArgument='<%# Bind("idAgendaActividad") %>'
                                ToolTip="Editar Evento" ImageUrl="~/Images/Grillas/action_edit.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Ciclo Lectivo">
                        <HeaderStyle HorizontalAlign="left" Width="20%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblCicloLectivoGrilla" runat="server" Text='<%# Bind("cursoCicloLectivo.cicloLectivo.nombre") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Curso">
                        <HeaderStyle HorizontalAlign="left" Width="50%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblCursoGrilla" runat="server" Text='<%# Bind("cursoCicloLectivo.curso.nombre") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Activo">
                        <HeaderStyle HorizontalAlign="left" Width="10%" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblActivo" runat="server" Text='<%# Boolean.Parse(Eval("activo").ToString()) ? "Sí" : "No"  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
            <asp:UpdatePanel ID="udpEdit" runat="server" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
                    <table class="tablaInterna" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
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
                                <HeaderStyle HorizontalAlign="center" Width="10%" />
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
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Organizador">
                                <HeaderStyle HorizontalAlign="left" Width="20%" />
                                <ItemStyle HorizontalAlign="left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblOrganizadorGrilla" runat="server" Text='<%# String.Format("{0} {1}", Eval("usuario.nombre"), Eval("usuario.apellido")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Activo">
                                <HeaderStyle HorizontalAlign="center" Width="10%" />
                                <ItemStyle HorizontalAlign="center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblActivo" runat="server" Text='<%# Boolean.Parse(Eval("activo").ToString()) ? "Sí" : "No"  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvwReporte" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
