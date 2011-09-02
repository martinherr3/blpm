<%@ Page Title="Redactar Mensaje" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="MsjeRedactar.aspx.cs" Inherits="EDUAR_UI.MsjeRedactar"
    Theme="Tema" StylesheetTheme="Tema" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Namespace="EDUAR_UI.UserControls" TagPrefix="custom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <select data-placeholder="Seleccione..." style="width: 600px;" multiple="true"
        class="chzn-select" runat="server" id="ddlDestino">
    </select>
    <br />
    <asp:Button Text="Enviar!" runat="server" OnClick="btnSubmit_Click" />
    <script type="text/javascript">        $(".chzn-select").chosen();</script>
</asp:Content>
