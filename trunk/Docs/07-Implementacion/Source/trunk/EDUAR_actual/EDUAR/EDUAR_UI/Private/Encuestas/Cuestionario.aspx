<%@ Page Title="Cuestionario" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="Cuestionario.aspx.cs" Inherits="EDUAR_UI.Cuestionario"
    Theme="Tema" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1><asp:Label ID="lblNombreEncuesta" runat="server"/></h1>
    <p></p>

    <asp:UpdatePanel ID="udpFormulario" runat="server">
    <ContentTemplate>
            <cc1:Accordion ID="CuestionarioAccordion" 
                CssClass="accordion" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"  
                ContentCssClass="accordionContent" RequireOpenedPane="false" SelectedIndex="-1" runat="server">
                <Panes>
                    <%--LOS COMPONENTES DEL ACCORDION SON AGREGADOS DINÁMICAMENTE DESDE CÓDIGO--%> 
                </Panes>
            </cc1:Accordion>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
