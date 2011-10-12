<%@ Page Title="Página principal" Language="C#" MasterPageFile="~/EDUARMaster.master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EDUAR_UI.Default" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        EDU@R 2.0
    </h2>
    <p class="ui-widget">
        Sistema de Comunicación y Explotación de Información Educativa.
    </p>
    <asp:Image ImageUrl="~/Images/prueba.gif" runat="server" />
</asp:Content>
