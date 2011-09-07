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
            font-weight: normal;
            /*color: #8B908C;*/
            color:Black;
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
            font-weight: normal;
             /*color: #8B908C;*/
            color:Black;
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
            <ajaxtoolkit:Accordion ID="MyAccordion" runat="Server" SelectedIndex="0" HeaderCssClass="accordionHeader"
                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                AutoSize="Fill" FadeTransitions="true" TransitionDuration="250" FramesPerSecond="40"
                RequireOpenedPane="false" SuppressHeaderPostbacks="true" Width="100%">
                <HeaderTemplate>
                    <table class="tablaInternaMensajes" cellpadding="1" cellspacing="5">
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
                </HeaderTemplate>
                <ContentTemplate>
                    <p>
                        <%# Server.HtmlDecode(Eval("textoMensaje").ToString()) %></p>
                </ContentTemplate>
            </ajaxtoolkit:Accordion>
            <hr />
            <asp:GridView ID="gvwReporte" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListado"
                AutoGenerateColumns="false" AllowPaging="false" Width="100%" DataKeyNames="idMensaje"
                OnRowCommand="gvwReporte_RowCommand" OnPageIndexChanging="gvwReporte_PageIndexChanging"
                AllowSorting="true">
                <Columns>
                    <asp:TemplateField HeaderText="Acciones">
                        <HeaderStyle HorizontalAlign="center" Width="5%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="leerMensaje" runat="server" CommandName="Abrir" CommandArgument='<%# Bind("idMensaje") %>'
                                ToolTip="Abrir Mensaje" ImageUrl="~/Images/Grillas/action-open.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remitente">
                        <HeaderStyle HorizontalAlign="left" Width="20%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblOrganizador" runat="server" Text='<%# String.Format("{0} {1}", Eval("remitente.nombre"), Eval("remitente.apellido")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Asunto">
                        <HeaderStyle HorizontalAlign="left" Width="50%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblMotivoGrilla" runat="server" Text='<%# Bind("asuntoMensaje") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fecha">
                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label ID="lblFechaGrilla" runat="server" Text='<%# (Eval("fechaEnvio","{0:d}") == DateTime.Now.ToShortDateString()) ?  Eval("horaEnvio","{0:HH:mm}") : Eval("fechaEnvio","{0:d}") %>'></asp:Label>
                            <%--<asp:Label ID="Label1" runat="server" Text='<%# String.Format("{0} {1} hs.", Eval("fechaEnvio","{0:d}"), Eval("horaEnvio","{0:HH:mm}")) %>'></asp:Label>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
