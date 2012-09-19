<%@ Page Title="" Language="C#" MasterPageFile="~/EDUARMaster.Master" AutoEventWireup="true"
    CodeBehind="RegNovedadAula.aspx.cs" Inherits="EDUAR_UI.Private.Novedades.RegNovedadAula" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="tablaInterna" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <h2>
                    Novedades Aulicas</h2>
                <br />
            </td>
            <td align="right">
                <%-- <asp:ImageButton ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" ToolTip="Buscar"
                            ImageUrl="~/Images/botonBuscar.png" ValidationGroup="ValidarBusqueda" />--%>
                <asp:ImageButton ID="btnNuevo" OnClick="btnNuevo_Click" runat="server" ToolTip="Nuevo"
                    ImageUrl="~/Images/botonNuevo.png" Visible="false" />
                <asp:ImageButton ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" ToolTip="Guardar"
                    ImageUrl="~/Images/botonGuardar.png" Visible="false" />
                <%-- <asp:ImageButton ID="btnVolver" OnClick="btnVolver_Click" runat="server" ToolTip="Volver"
                            ImageUrl="~/Images/botonVolver.png" />--%>
            </td>
        </tr>
    </table>
    <table class="tablaInterna" cellpadding="1" cellspacing="5">
        <tr>
            <td class="TD140px">
                <asp:Label ID="lblCurso" runat="server" Text="Curso:" CssClass="lblCriterios"></asp:Label>
            </td>
            <td class="TD140px">
                <asp:DropDownList ID="ddlCurso" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCurso_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="udpNueva" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="tablaInternaSinBorde" cellpadding="1" cellspacing="5">
                <tr>
                    <td class="TD140px">
                        <asp:Label ID="lblEstado" runat="server" Text="Estado:" CssClass="lblCriterios"></asp:Label>
                    </td>
                    <td class="TD140px">
                        <asp:DropDownList ID="ddlEstado" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="TD140px">
                        <asp:Label ID="lblTipo" runat="server" Text="Tipo Novedad:" CssClass="lblCriterios"></asp:Label>
                    </td>
                    <td class="TD140px">
                        <asp:DropDownList ID="ddlNovedad" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="TD140px" style="vertical-align: text-top">
                        <asp:Label ID="lblObservaciones" runat="server" Text="Comentario:"></asp:Label><br />
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine" Columns="75"
                            Rows="10" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
