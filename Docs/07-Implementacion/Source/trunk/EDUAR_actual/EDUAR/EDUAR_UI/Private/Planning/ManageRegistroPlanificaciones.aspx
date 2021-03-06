﻿<%@ Page Title="Planificaciones" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="ManageRegistroPlanificaciones.aspx.cs" Inherits="EDUAR_UI.ManageRegistroPlanificaciones" %>

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
                            Administrar Planificaciones</h2>
                        <br />
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="udpFiltrosBusqueda" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table class="tablaInterna" cellpadding="1" cellspacing="5">
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Nivel:" CssClass="lblCriterios"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlNivel" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlNivel_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TD100px">
                                <asp:Label ID="lblAsignatura" runat="server" Text="Asignatura:"></asp:Label>
                            </td>
                            <td valign="top" style="width: 500px">
                                <asp:DropDownList ID="ddlAsignatura" runat="server" Enabled="false" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlAsignatura_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="udpGrilla" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView ID="gvwReporte" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListado"
                AutoGenerateColumns="false" AllowPaging="true" Width="80%" DataKeyNames="idPlanificacionAnual"
                OnRowCommand="gvwReporte_RowCommand" 
                onselectedindexchanging="gvwReporte_SelectedIndexChanging" 
                onrowdatabound="gvwReporte_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Acciones">
                        <HeaderStyle HorizontalAlign="center" Width="5%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="editarPlanificacion" runat="server" CommandName="Editar" CommandArgument='<%# Bind("idPlanificacionAnual") %>'
                                ToolTip="Ver Detalle" ImageUrl="~/Images/Grillas/action_edit.png" />
                            <asp:ImageButton ID="aprobarPlanificacion" runat="server" CommandName="Aprobar" CommandArgument='<%# Bind("idPlanificacionAnual") %>'
                                ToolTip="Aprobar" ImageUrl="~/Images/Grillas/action_enable.png" 
                                Visible='<%# ((DataBinder.Eval(Container.DataItem, "fechaAprobada").ToString() == String.Empty) && 
                                                ((bool)DataBinder.Eval(Container.DataItem, "solicitarAprobacion") == true )) ? true : false %>' />
                            <asp:ImageButton ID="btnLock" runat="server" ToolTip="Planificación aprobada" ImageUrl="~/Images/Grillas/lock.png"
                                Visible='<%# DataBinder.Eval(Container.DataItem, "fechaAprobada").ToString() == String.Empty ? false : true %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Creación">
                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label ID="lblFechaGrilla" runat="server" Text='<%# Bind("fechaCreacion","{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Aprobación">
                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label ID="lblFechaAproadaGrilla" runat="server" Text='<%# Bind("fechaAprobada","{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Asignatura">
                        <HeaderStyle HorizontalAlign="left" Width="20%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblAsignatura" runat="server" Text='<%# Bind("curricula.asignatura.nombre") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nivel">
                        <HeaderStyle HorizontalAlign="Center" Width="7%" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblNivel" runat="server" Text='<%# Bind("curricula.nivel.nombre") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Cursos">
                        <HeaderStyle HorizontalAlign="Center" Width="7%" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblCurso" runat="server" Text='<%# Bind("listaCursos") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Orientación">
                        <HeaderStyle HorizontalAlign="left" Width="30%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblOrientacion" runat="server" Text='<%# Bind("curricula.orientacion.nombre") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Creador">
                        <HeaderStyle HorizontalAlign="left" Width="15%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblCreadorGrilla" runat="server" Text='<%# String.Format("{0} {1}", Eval("creador.nombre"), Eval("creador.apellido")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Adecuación">
                        <HeaderStyle HorizontalAlign="left" Width="5%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label ID="lblPorcentaje" runat="server" Text='<%# Bind("porcentajeCobertura","{0:##0} %") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gvwReporte" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
