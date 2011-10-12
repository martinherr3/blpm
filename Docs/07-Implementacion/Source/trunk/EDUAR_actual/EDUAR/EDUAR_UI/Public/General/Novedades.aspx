<%@ Page Title="Novedades" Language="C#" MasterPageFile="~/EDUARMaster.Master" AutoEventWireup="true"
    CodeBehind="Novedades.aspx.cs" Inherits="EDUAR_UI.Novedades" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Novedades
    </h2>
    <%--<p class="ui-widget">
        [En esta sesión se publicara las actividades realizadas en la institución, actividades
            próximas a realizar y toda noticias que involucren a la comunidad dela Institución
            educativa]
    </p>
    <hr />
    <h3>
        [Titulo de la Noticia][Fecha de publicacion]</h3>
    <p class="ui-widget">
        [Este espacio será dedicado al contenido de la noticia.]
    </p>
    <hr />
    <h3>
        [Titulo de la Noticia][Fecha de publicacion]</h3>
    <p class="ui-widget">
        [Este espacio será dedicado al contenido de la noticia.]
    </p>
    <hr />--%>
    <asp:UpdatePanel ID="udpNovedades" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
