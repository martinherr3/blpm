<%@ Page Title="Bandeja de Entrada" Language="C#" MasterPageFile="~/EDUARMaster.Master" AutoEventWireup="true"
    CodeBehind="MsjeEntrada.aspx.cs" Inherits="EDUAR_UI.MsjeEntrada" Theme="Tema"
    StylesheetTheme="Tema" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Mensajes Recibidos
        <br />
    </h2>
    <asp:UpdatePanel ID="udpGrilla" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
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
