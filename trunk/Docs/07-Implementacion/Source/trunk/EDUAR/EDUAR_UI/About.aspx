<%@ Page Title="Acerca de nosotros" Language="C#" MasterPageFile="~/EDUARMaster.master"
    AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="EDUAR_UI.About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   
    <h2>
        Acerca de
    </h2> <img src="Images/Logo.png" alt="EDU@R 2.0" />
    <p>
       Proyecto Final <%=DateTime.Now.ToString() %>
    </p>
</asp:Content>
