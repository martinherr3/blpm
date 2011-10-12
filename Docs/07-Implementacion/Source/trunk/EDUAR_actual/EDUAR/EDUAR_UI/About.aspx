<%@ Page Title="Acerca de nosotros" Language="C#" MasterPageFile="~/EDUARMaster.master"
    AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="EDUAR_UI.About" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Acerca de
    </h2>
    <p class="ui-widget">
        El presente sitio web forma parte de nuestro Proyecto Final para la carrera Ingeniería
        en Sistemas de Información. El mismo se encuentra en etapa de construcción y desarrollo.</p>
    <asp:Image ImageUrl="~/Images/Construction.png" runat="server" AlternateText="En Construcción" ToolTip="En Construcción" />
    <p class="ui-widget" style="text-align: center">
        <%=DateTime.Now.ToShortDateString() %> <%=DateTime.Now.ToShortTimeString() %>
    </p>
</asp:Content>
