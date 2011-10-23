<%@ Page Title="Bienvenido!" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="Welcome.aspx.cs" Inherits="EDUAR_UI.Welcome" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Src="~/UserControls/Calendario.ascx" TagName="Calendario" TagPrefix="cal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%" cellpadding="1" cellspacing="5" border="0">
        <tr>
            <td style="width: 80%; vertical-align: text-top">
                <h2>
                    Bienvenido!<hr />
                </h2>
            </td>
            <td style="width: 20%; text-align: right" rowspan="2">
                <asp:Image ID="Image1" ImageUrl="~/Images/web/default.png" runat="server" AlternateText="Bienvenido!"
                    ToolTip="Bienvenido!" />
            </td>
        </tr>
        <tr>
            <td style="width: 80%">
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
            </td>
        </tr>
    </table>
    <div id="divAgenda" runat="server" visible="false">
        <table width="100%" cellpadding="1" cellspacing="5" border="0">
            <tr>
                <td style="width: 80%; vertical-align: text-top">
                    <h2>
                        Tu Agenda:<hr />
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
                                <asp:Label ID="lblDescripcionGrilla" runat="server" Text='<%# Bind("descripcion") %>'></asp:Label>
                            </ItemTemplate>
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
