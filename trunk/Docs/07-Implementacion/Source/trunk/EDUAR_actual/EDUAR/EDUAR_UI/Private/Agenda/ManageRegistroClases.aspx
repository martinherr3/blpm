<%@ Page Title="Registro de Clases" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="ManageRegistroClases.aspx.cs" Inherits="EDUAR_UI.ManageRegistroClases" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="tablaInterna" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <h2>
                    Planificaci&oacute;n De Clases</h2>
                <br />
            </td>
            <td align="right" rowspan="2">
                <%--<asp:UpdatePanel ID="udpBotonera" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:ImageButton ID="btnNuevo" runat="server" ToolTip="Nuevo" ImageUrl="~/Images/botonNuevo.png"
                                Visible="false" OnClick="btnNuevo_Click" />
                            <asp:ImageButton ID="btnPDF" runat="server" ToolTip="Exportar a PDF" ImageUrl="~/Images/ExportarPDF.png"
                                Visible="false" onclick="btnPDF_Click" />
                            <asp:ImageButton ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" ToolTip="Guardar"
                                ImageUrl="~/Images/botonGuardar.png" Visible="false" />
                            <asp:ImageButton ID="btnVolver" OnClick="btnVolver_Click" runat="server" ToolTip="Volver"
                                ImageUrl="~/Images/botonVolver.png" Visible="false" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlAsignatura" EventName="SelectedIndexChanged" />
                            <asp:PostBackTrigger ControlID="btnPDF" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
            </td>
        </tr>
    </table>
    <table class="tablaInterna">
        <tr>
            <td class="TD110px">
                <asp:Label ID="lblCurso" runat="server" Text="Curso:" CssClass="lblCriterios"></asp:Label>
            </td>
            <td class="TD140px">
                <asp:DropDownList ID="ddlCurso" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCurso_SelectedIndexChanged">
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
                        <asp:AsyncPostBackTrigger ControlID="ddlCurso" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="TD110px">
                <asp:Label ID="lblClase" runat="server" Text="Clase del d&iacute;a:" CssClass="lblCriterios"></asp:Label>
            </td>
            <td colspan="3">
                <asp:UpdatePanel ID="udpMeses" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlMeses" OnSelectedIndexChanged="ddlMeses_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:DropDownList runat="server" ID="ddlDia" Enabled="false">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlAsignatura" EventName="SelectedIndexChanged" />
                        <asp:PostBackTrigger ControlID="ddlMeses" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <br />
    <div id="divContenidos" style="width: 100%; border: 0">
        <table class="tablaInterna">
            <tr>
                <td style="width: 50%">
                    <asp:UpdatePanel ID="udpContenidos" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:ListView ID="ltvContenidos" runat="server">
                                <LayoutTemplate>
                                    <div id="Div1" runat="server">
                                        <div id="itemPlaceholder" runat="server">
                                        </div>
                                    </div>
                                </LayoutTemplate>
                                <EmptyDataTemplate>
                                    <div id="Div2" runat="server">
                                        <div id="itemPlaceholder" runat="server">
                                            Sin Datos
                                        </div>
                                    </div>
                                </EmptyDataTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTitulo" runat="server" Text='<%# Eval ("Titulo") %>' />
                                </ItemTemplate>
                            </asp:ListView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlAsignatura" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 50%">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
