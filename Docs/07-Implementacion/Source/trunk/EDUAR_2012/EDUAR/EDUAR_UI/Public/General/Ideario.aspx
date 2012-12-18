<%@ Page Title="Ideario" Language="C#" MasterPageFile="~/EDUARMaster.Master" AutoEventWireup="true"
    CodeBehind="Ideario.aspx.cs" Inherits="EDUAR_UI.Ideario" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%" cellpadding="1" cellspacing="5" border="0">
        <tr>
            <td style="width: 80%; vertical-align: text-top">
                <h2>
                    Misi&oacute;n, Visi&oacute;n y Valores
                    <hr />
                </h2>
            </td>
            <td style="width: 20%; text-align: right" rowspan="2">
                <asp:Image ID="Image1" ImageUrl="~/Images/web/ideario.png" runat="server" ToolTip="Misión, Visión y Valores"
                    AlternateText="Misión, Visión y Valores" />
            </td>
        </tr>
        <tr>
            <td style="width: 80%">
                <p class="ui-widget">
                    [En esta sección se encuentra toda la información a lo que respecta a la misión,
                    visión y valores de la institución al día de la fecha.]
                </p>
            </td>
        </tr>
    </table>
    <table width="100%" cellpadding="1" cellspacing="5" border="0">
        <tr>
            <td style="width: 100%">
                <h3>
                    Misi&oacute;n
                </h3>
                <p class="ui-widget">
                    [En esta sección se encuentra toda la información a lo que respecta a la misión,
                    visión y valores de la institución al día de la fecha. La misión mostrará el motivo,
                    propósito, fin o razón de ser de la existencia de la organización definiendo: *
                    lo que pretende cumplir en su entorno o sistema social en el que actúa, * lo que
                    pretende hacer, * el para quién lo va a hacer.]
                </p>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <h3>
                    Visión</h3>
                <p class="ui-widget">
                    [La visión mostrará el camino al cual se dirige la institución a largo plazo y sirve
                    de rumbo y aliciente para orientar las decisiones estratégicas de crecimiento junto
                    a las de competitividad.]
                </p>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <h3>
                    Valores</h3>
                <p class="ui-widget">
                    [Los Valores mostrarán el conjunto de principios, creencias, reglas que regulan
                    la gestión de la organización. Constituyen la filosofía institucional y el soporte
                    de la cultura organizacional. Muestra el marco de referencia que inspira y regula
                    la vida de la organización.]
                </p>
            </td>
        </tr>
    </table>
</asp:Content>
