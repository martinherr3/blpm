﻿<%@ Page Title="Redactar Mensaje" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="MsjeRedactar.aspx.cs" Inherits="EDUAR_UI.MsjeRedactar"
    Theme="Tema" StylesheetTheme="Tema" ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Editor.ascx" TagName="Editor" TagPrefix="edi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <h2>
                <asp:Label ID="lblTitulo" Text="Nuevo Mensaje" runat="server" /></h2>
            <br />
            <table class="tablaInterna" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="right">
                        <asp:ImageButton ID="btnEnviar" OnClick="btnEnviar_Click" runat="server" ToolTip="Enviar"
                            ImageUrl="~/Images/botonEnviarMail.png" />
                        <asp:ImageButton ID="btnVolver" OnClick="btnVolver_Click" runat="server" ToolTip="Volver"
                            ImageUrl="~/Images/botonVolver.png" />
                    </td>
                </tr>
            </table>
            <table class="tablaInterna" width="100%">
                <tr>
                    <td class="TD20">
                        Destinatarios
                    </td>
                    <td class="TD80">
                        <select data-placeholder="Seleccione los destinatarios" style="width: 600px;" multiple="true"
                            class="chzn-select" runat="server" id="ddlDestino">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="TD20">
                        Asunto
                    </td>
                    <td class="TD80">
                        <input id="txtAsunto" type="text" runat="server" style="width: 595px; background-color: #FFFFFF;
                            background-image: -moz-linear-gradient(center bottom , white 85%, #EEEEEE 99%);
                            border: 1px solid #AAAAAA; cursor: text; height: 26px !important; margin: 0;
                            overflow: hidden; padding: 0; position: relative; font-family: sans-serif; font-size: 1em" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 100%">
                        <edi:Editor ID="textoMensaje" runat="server" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">        $(".chzn-select").chosen();</script>
</asp:Content>
