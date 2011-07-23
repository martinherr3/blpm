<%@ Page Title="Novedades" Language="C#" MasterPageFile="~/EDUARMaster.Master" AutoEventWireup="true"
    CodeBehind="Novedades.aspx.cs" Inherits="EDUAR_UI.Novedades" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Novedades
    </h2>
    <p>
        <small>[En esta sesión se publicara las actividades realizadas en la institución, actividades
        próximas a realizar y toda noticias que involucren a la comunidad dela Institución
        educativa]</small>
    </p>
    <hr />
    <h3>
        [Titulo de la Noticia]<small>[Fecha de publicacion]</small></h3>
       
    <p>[Este espacio será dedicado al contenido de la noticia.]
    </p>
    <hr />
    <h3>
        [Titulo de la Noticia]<small>[Fecha de publicacion]</small></h3>
       
    <p>[Este espacio será dedicado al contenido de la noticia.]
    </p>
    <hr />
</asp:Content>
