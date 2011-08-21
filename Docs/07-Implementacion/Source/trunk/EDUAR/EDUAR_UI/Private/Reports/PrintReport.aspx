<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintReport.aspx.cs" Inherits="EDUAR_UI.PrintReport" Theme="Tema" StylesheetTheme="Tema" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Impresión de Informes</title>
    <script type="text/javascript">
        function Imprimir() { window.print(); Cerrar(); }
        function Cerrar() { window.close(); }
    </script>
    <style type="text/css">
        /* Grid Zones */
        .grid
        {
            background: #574441;
            border: 1px solid #848484;
            width: 100%; /*background-image: url(graf/fondo.gif);*/
            font-size: 11px;
            color: #848484;
            vertical-align: top;
            font-family: Verdana;
        }
        .gridheader
        {
            color: #8B908C;
            font-family: Verdana,Tahoma,Arial,Helvetica,sans-serif,Symbol;
            font-size: 12px;
            font-style: normal;
            font-weight: bold;
            text-decoration: none;
            white-space: nowrap;
            text-transform: uppercase;
        }
        .gridrow
        {
            color: #8B908C;
            font-family: Verdana,Tahoma,Arial,Helvetica,sans-serif,Symbol;
            font-size: 12px;
            font-style: normal;
            font-weight: normal;
            text-decoration: none;
            white-space: nowrap;
        }
        .gridborder
        {
            border: 1px solid #848484;
        }
        .tablaInterna
        {
            width: 100%;
            border: 0;
            border-top: 1px solid silver;
            padding-top: 5px;
        }
        @media print
        {
            .noImprimir
            {
                display: none;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server">
    </asp:ScriptManager>
    <div class="noImprimir">
        <table class="tablaInterna" cellpadding="0" cellspacing="0">
            <tr>
                <td align="right">
                    <asp:ImageButton ID="btnImprimir" runat="server" ImageUrl="~/Images/botonImprimir.png"
                        ToolTip="Imprimir" />
                    <asp:ImageButton ID="btnVolver" runat="server" ImageUrl="~/Images/botonVolver.png"
                        ToolTip="Volver" />
                </td>
            </tr>
        </table>
    </div>
    <table class="tablaInterna" cellpadding="1" cellspacing="5">
        <tr>
            <td align="center">
                <!--FontFactory.HELVETICA_BOLD, 24, Font.BOLD, BaseColor.BLUE);-->
                <asp:Label ID="lblTitulo" runat="server" Font-Names="Helvetica" Font-Bold="true"
                    ForeColor="Blue" Font-Size="Large"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <!--FontFactory.HELVETICA, 15, Font.BOLDITALIC-->
                <asp:Label ID="lblInforme" runat="server" Font-Names="Helvetica" Font-Italic="true"
                    Font-Bold="true" ForeColor="Black" Font-Size="Medium"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <!--FontFactory.HELVETICA, 15, Font.BOLDITALIC-->
                <asp:Label ID="lblFecha" runat="server" Font-Names="Helvetica" Font-Italic="true"
                    Font-Bold="true" ForeColor="Black" Font-Size="Medium"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="lblUsuario" Text="" runat="server" Font-Names="Helvetica" Font-Bold="false"
                    ForeColor="Black" Font-Size="Medium" />
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Literal ID="lblFiltro" Text="" runat="server" />
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="udpReporte" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView ID="gvwReporte" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerReporte"
                AutoGenerateColumns="false" AllowPaging="false" Width="100%">
                <HeaderStyle CssClass="gridheader" />
                <RowStyle CssClass="gridrow" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
