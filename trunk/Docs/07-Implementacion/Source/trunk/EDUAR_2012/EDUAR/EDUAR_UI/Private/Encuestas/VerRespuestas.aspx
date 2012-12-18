<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerRespuestas.aspx.cs"
    Inherits="EDUAR_UI.VerRespuestas" Theme="Tema" StylesheetTheme="Tema" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../App_Themes/Tema/Estilo.css" rel="stylesheet" type="text/css" />
    <title>Respuestas Textuales</title>
    <script type="text/javascript">
        function Imprimir() { window.print(); Cerrar(); }
        function Cerrar() { window.close(); }
    </script>
    <style type="text/css">
        /* Grid Zones */
        .grid
        {
            /*background: #574441;*/
            border: 1px solid #848484;
            width: 100%; /*background-image: url(graf/fondo.gif);*/
            font-size: 11px;
            color: #848484;
            vertical-align: top;
            font-family: Verdana;
            background-color: White;
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
            background-color: White;
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
            background-color: White;
        }
    </style>
</head>
<body style="background-color: White">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server">
    </asp:ScriptManager>
    <table class="tablaInterna" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <h2>
                    <asp:Label ID="lblPregunta" Text="" runat="server" /></h2>
                <br />
            </td>
            <td align="right">
                <%--<asp:ImageButton ID="btnImprimir" runat="server" ImageUrl="~/Images/botonImprimir.png"
                        ToolTip="Imprimir" />--%>
                <asp:ImageButton ID="btnVolver" runat="server" ImageUrl="~/Images/botonVolver.png"
                    ToolTip="Volver" />
            </td>
        </tr>
    </table>
    <table class="tablaInterna" cellpadding="0" cellspacing="0">
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="udpConversacion" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="divConversacion" runat="server">
                <asp:Repeater ID="rptConversacion" runat="server">
                    <ItemTemplate>
                        <div id="divIzquierda" runat="server" class="bubbleNotificacion" style="background: #B0C4DE;
                            border-color: #B0C4DE;">
                            <div style="text-align: left">
                                <asp:Label ID="lblConversacion" Text='<%# Bind("respuestaTextual") %>' runat="server" />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table class="tablaInterna" cellpadding="0" cellspacing="0">
        <tr>
            <td align="right">
                <%--<asp:ImageButton ID="btnImprimir" runat="server" ImageUrl="~/Images/botonImprimir.png"
                        ToolTip="Imprimir" />--%>
                <asp:ImageButton ID="btnVolver2" runat="server" ImageUrl="~/Images/botonVolver.png"
                    ToolTip="Volver" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
