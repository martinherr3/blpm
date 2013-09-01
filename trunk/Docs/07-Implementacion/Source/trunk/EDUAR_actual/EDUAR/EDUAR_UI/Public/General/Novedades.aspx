<%@ Page Title="Novedades" Language="C#" MasterPageFile="~/EDUARMaster.Master" AutoEventWireup="true"
    CodeBehind="Novedades.aspx.cs" Inherits="EDUAR_UI.Novedades" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%" cellpadding="1" cellspacing="5" border="0">
        <tr>
            <td style="width: 80%; vertical-align: text-top">
                <h2>
                    Novedades
                    <hr />
                </h2>
            </td>
            <td style="width: 20%; text-align: right" rowspan="2">
                <asp:Image ID="Image1" ImageUrl="~/Images/web/appointment-new.png" runat="server"
                    ToolTip="Eventos Institucionales" AlternateText="Eventos Institucionales" />
            </td>
        </tr>
        <tr>
            <td style="width: 80%">
                <p class="ui-widget">
                    [En esta sección se encuentra información relativa a los eventos institucionales.]
                </p>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="1" cellspacing="5" border="0">
        <tr>
            <td style="width: 100%">
                <asp:UpdatePanel ID="udpNovedades" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table class="tablaInternaSinBorde" border="0" cellpadding="1" cellspacing="5">
                            <tr>
                                <td align="right">
                                    [<asp:LinkButton ID="lnkFiltrado" Text="Mostrar Todos" runat="server" CommandArgument="Todo"
                                        OnCommand="lnkFiltrado_Command" />]
                                </td>
                            </tr>
                        </table>
                        <asp:Repeater ID="rptNovedades" runat="server">
                            <ItemTemplate>
                                <div style="text-align: left">
                                    <h3>
                                        <asp:Label ID="lblFecha" Text='<%# Bind("fecha","{0:d}") %>' runat="server" Font-Italic="true" />
                                        -
                                        <asp:Label ID="lblTitulo" Text='<%# Bind("titulo") %>' runat="server" Font-Italic="true" /></h3>
                                    <br />
                                </div>
                                <div style="text-align: left">
                                    <asp:Label ID="lblLugar" Text='<%# Bind("lugar") %>' runat="server" Font-Bold="false" />
                                </div>
                                <div style="text-align: left">
                                    <asp:Label ID="lblDetalle" Text='<%# Bind("detalle") %>' runat="server" /><br />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <table class="tablaInternaSinBorde" border="0" cellpadding="1" cellspacing="5">
                            <tr>
                                <td align="right">
                                    [<asp:LinkButton ID="lnkFiltrado2" Text="Mostrar Todos" runat="server" CommandArgument="Todo"
                                        OnCommand="lnkFiltrado_Command" />]
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkFiltrado" />
                        <asp:PostBackTrigger ControlID="lnkFiltrado2" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
