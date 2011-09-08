<%@ Page Title="Bandeja de Entrada" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="MsjeEntrada.aspx.cs" Inherits="EDUAR_UI.MsjeEntrada"
    Theme="Tema" StylesheetTheme="Tema" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .accordionHeaderSelected
        {
            background-color: #A2B5CD;
            padding-left: 10px;
            padding-top: 5px;
            border: solid 1px Silver;
            font-style: normal;
            font-weight: bold;
            height: 35px;
            cursor: pointer;
            font-family: Verdana, Tahoma, Arial, Helvetica, sans-serif, Symbol;
            font-size: 12px;
            font-style: normal;
            font-weight: normal; /*color: #8B908C;*/
            color: Black;
            border: 1px solid #dbdddc;
            text-decoration: none;
            overflow: hidden; /* text-overflow: ellipsis;*/
            white-space: nowrap;
            padding: 0px;
            margin: 0px;
        }
        .accordionHeader
        {
            background-color: #F5F5F5;
            padding-left: 10px;
            padding-top: 5px;
            border: solid 1px Silver;
            font-style: normal;
            font-weight: normal;
            height: 35px;
            cursor: pointer;
            font-family: Verdana, Tahoma, Arial, Helvetica, sans-serif, Symbol;
            font-size: 12px;
            font-style: normal;
            font-weight: normal; /*color: #8B908C;*/
            color: Black;
            border: 1px solid #dbdddc;
            text-decoration: none;
            overflow: hidden; /* text-overflow: ellipsis;*/
            white-space: nowrap;
            padding: 0px;
            margin: 0px;
        }
        .accordionContent
        {
            background-color: transparent;
            padding: 10px 25px 25px 25px; /*font-size: medium; color:Snow;  */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Mensajes Recibidos
        <br />
    </h2>
    <asp:UpdatePanel ID="udpGrilla" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <hr />
            <ajaxtoolkit:Accordion ID="MyAccordion" runat="Server" SelectedIndex="-1" HeaderCssClass="accordionHeader"
                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                AutoSize="None" FadeTransitions="true" TransitionDuration="250" FramesPerSecond="40"
                RequireOpenedPane="false" Width="100%" OnItemCommand="MyAccordion_ItemCommand"
                SuppressHeaderPostbacks="false">
                <HeaderTemplate>
                    <table class="tablaInternaMensajes" cellpadding="1" cellspacing="5">
                        <tr>
                            <td style="vertical-align: text-top">
                                <asp:ImageButton ImageUrl="~/Images/Grillas/mail-reply-2.png" runat="server" ID="btnResponder"
                                    AlternateText="Responder" ToolTip="Responder" ImageAlign="TextTop" CommandName="Responder"
                                    CommandArgument='<%# Bind("idMensajeDestinatario") %>' />
                                <asp:ImageButton ImageUrl="~/Images/Grillas/edit-delete-2.png" runat="server" ID="btnEliminar"
                                    AlternateText="Eliminar" ToolTip="Eliminar" ImageAlign="TextTop" CommandName="Eliminar"
                                    CommandArgument='<%# Bind("idMensajeDestinatario") %>' />
                            </td>
                            <td style="width: 95%; vertical-align: middle">
                                <asp:LinkButton ID="lnkEncabezado" runat="server" CommandName="Leer" CommandArgument='<%# Bind("idMensajeDestinatario") %>'>
                        <table id="<%# String.Format("tbl_{0}", Eval("idMensajeDestinatario").ToString()) %>" class="tablaInternaMensajes"
                            cellpadding="1" cellspacing="5" style="font-weight: <%# Boolean.Parse(Eval("leido").ToString()) ? "normal" : "bold"  %>">
                            <tr>
                                <td class="TDCriterios30">
                                 
                                    <%# String.Format("{0} {1}", Eval("remitente.nombre"), Eval("remitente.apellido")) %>
                                </td>
                                <td class="TDCriterios60">
                                    <%# Eval("asuntoMensaje") %>
                                </td>
                                <td class="TD10" align="center">
                                    <%# (Eval("fechaEnvio","{0:d}") == DateTime.Now.ToShortDateString()) ?  Eval("horaEnvio","{0:HH:mm}") : Eval("fechaEnvio","{0:d}") %>
                                </td>
                            </tr>
                        </table>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <ContentTemplate>
                    <p>
                        <%# Server.HtmlDecode(Eval("textoMensaje").ToString()) %></p>
                </ContentTemplate>
            </ajaxtoolkit:Accordion>
            <hr />
            <table class="tablaInternaMensajes" cellpadding="5" cellspacing="1">
                <tr>
                    <td class="TDCriterios40">
                        Mostrar
                        <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>15</asp:ListItem>
                        </asp:DropDownList>
                        por Página
                    </td>
                    <td class="TD30" align="center">
                        <asp:Label ID="lblCantidad" Text="" runat="server" />
                    </td>
                    <td class="TD30" align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="lnkbtnFirst" OnClick="lnkbtnFirst_Click" ImageUrl="~/Images/Paginador/go-frist-view.png"
                                        runat="server" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="lnkbtnPrevious" OnClick="lnkbtnPrevious_Click" ImageUrl="~/Images/Paginador/go-previous-view.png"
                                        runat="server" />
                                </td>
                                <td>
                                    <asp:DataList ID="dlPaging" runat="server" OnItemCommand="dlPaging_ItemCommand" OnItemDataBound="dlPaging_ItemDataBound"
                                        RepeatDirection="Horizontal">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnPaging" runat="server" CommandArgument='<%# Eval("PageIndex") %>'
                                                CommandName="lnkbtnPaging" Text='<%# Eval("PageText") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                                <td>
                                    <asp:ImageButton ID="lnkbtnNext" OnClick="lnkbtnNext_Click" ImageUrl="~/Images/Paginador/go-next-view.png"
                                        runat="server" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="lnkbtnLast" OnClick="lnkbtnLast_Click" ImageUrl="~/Images/Paginador/go-last-view.png"
                                        runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
