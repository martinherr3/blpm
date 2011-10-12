<%@ Page Title="Historia" Language="C#" MasterPageFile="~/EDUARMaster.Master" AutoEventWireup="true"
    CodeBehind="Historia.aspx.cs" Inherits="EDUAR_UI.Historia" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table width="100%" cellpadding="1" cellspacing="5" border="0">
        <tr>
            <td style="width: 80%; vertical-align: text-top">
                <h2>
                    Institución Educativa
                    <hr />
                </h2>
            </td>
            <td style="width: 20%; text-align: right" rowspan="2">
                <asp:Image ImageUrl="~/Images/web/BoxDocument.png" runat="server" ToolTip="Historia"
                    AlternateText="Historia" />
            </td>
        </tr>
        <tr>
            <td style="width: 80%">
                <p class="ui-widget">
                    [En esta sesión se encuentra toda la información a lo que respecta del año de fundación
                    la institución educacional, quienes fueron las personas fundadoras, bajo que misión,
                    visión y valores fue creada en ese momento. Y como fue cambiando al pasar de los
                    años hasta llegar a ser la institución que es hoy.]
                </p>
            </td>
        </tr>
    </table>
</asp:Content>
