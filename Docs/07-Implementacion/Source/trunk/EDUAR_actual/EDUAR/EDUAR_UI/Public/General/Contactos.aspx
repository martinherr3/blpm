<%@ Page Title="Contacto" Language="C#" MasterPageFile="~/EDUARMaster.Master" AutoEventWireup="true"
    CodeBehind="Contactos.aspx.cs" Inherits="EDUAR_UI.Contactos" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Información General
    </h2>
    <h3>
        Direccion</h3>
    <p class="ui-widget">
        Calle Pública S/N
    </p>
    <h3>
        Teléfonos</h3>
    <p class="ui-widget">
        0351 - 4123456
    </p>
    <h3>
        Email</h3>
    <p class="ui-widget">
        <a href="mailto:proyecto.eduar2.0@gmail.com">Solicita Información</a>
    </p>
</asp:Content>
