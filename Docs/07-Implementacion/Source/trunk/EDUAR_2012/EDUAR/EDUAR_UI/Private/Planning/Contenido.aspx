﻿<%@ Page Title="Carga de Contenidos" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="Contenido.aspx.cs" Inherits="EDUAR_UI.Contenido" %>

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
                        Carga De Contenidos</h2>
                    <br />
                </td>
                <td align="right" rowspan="2">
                    <asp:UpdatePanel ID="udpBotonera" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:ImageButton ID="btnNuevo" runat="server" ToolTip="Nuevo" ImageUrl="~/Images/botonNuevo.png"
                                Visible="false" OnClick="btnNuevo_Click"/>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlAsignatura" EventName="SelectedIndexChanged" />
                            <%--<asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click" />--%>
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
                                    AlternateText="Eliminar" ToolTip="Eliminar" CommandName="Eliminar" CommandArgument='<%# Bind("idContenido") %>' />
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
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <ajaxtoolkit:ModalPopupExtender ID="mpeContenido" runat="server" PopupControlID="pnlContenidos"
        TargetControlID="HiddenField1" RepositionMode="RepositionOnWindowResizeAndScroll"
        BackgroundCssClass="modalBackground" DropShadow="false" PopupDragHandleControlID="pnlContenidos">
    </ajaxtoolkit:ModalPopupExtender>
    <asp:Panel ID="pnlContenidos" runat="server" Width="600px" Height="150px" Style="display: none;
        text-align: left" BorderStyle="Groove" CssClass="CajaDialogo">
        <asp:UpdatePanel ID="udpContenidosAsociados" runat="server">
            <ContentTemplate>
                <table class="tablaInterna" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <h2>
                                <asp:Label ID="lblTitulo" Text="Nuevo Contenido" runat="server" /></h2>
                            <br />
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="btnGuardar" runat="server" ToolTip="Guardar" ImageUrl="~/Images/PopUp/botonGuardar.png"
                                OnClick="btnGuardar_Click" />
                            <asp:ImageButton ID="btnVolver" runat="server" ToolTip="Volver" ImageUrl="~/Images/PopUp/botonVolver.png"
                                OnClick="btnVolver_Click" />
                        </td>
                    </tr>
                </table>
                <table class="tablaInterna" cellpadding="1" cellspacing="5">
                    <tr>
                        <td class="TD50px">
                            <asp:Label ID="Label1" Text="Descripción:" runat="server" />
                        </td>
                        <td class="TD250px">
                            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="EstiloTxtLargo250" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
