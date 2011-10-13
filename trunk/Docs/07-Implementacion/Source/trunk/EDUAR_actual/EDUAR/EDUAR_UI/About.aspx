<%@ Page Title="Acerca de nosotros" Language="C#" MasterPageFile="~/EDUARMaster.master"
    AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="EDUAR_UI.About" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table width="100%" cellpadding="1" cellspacing="5" border="0">
        <tr>
            <td style="width: 80%; vertical-align: text-top">
                <h2>
                    Acerca de<hr />
                </h2>
            </td>
            <td style="width: 20%; text-align: right" rowspan="2">
                <asp:Image ID="Image1" ImageUrl="~/Images/Web/Construction.png" runat="server" AlternateText="En Construcción"
                    ToolTip="En Construcción" />
            </td>
        </tr>
        <tr>
            <td style="width: 80%">
                <p class="ui-widget">
                    El presente sitio web forma parte de nuestro Proyecto Final para la carrera Ingeniería
                    en Sistemas de Información. El mismo se encuentra en etapa de construcción y desarrollo.</p>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="1" cellspacing="5" border="0">
        <tr>
            <td style="width: 100%">
                <p class="ui-widget" style="text-align: left">
                    <%=DateTime.Now.ToShortDateString() %>
                    <%=DateTime.Now.ToShortTimeString() %>
                </p>
            </td>
        </tr>
    </table>
</asp:Content>
