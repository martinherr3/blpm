<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="EDUAR_UI.Error" %>

<!doctype html>
<html lang="es">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>EDU@R 2.0</title>
    <link rel="icon" type="image/png" href="~/favicon.ico" />
    <link href="~/App_Themes/Tema/Estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table class="tablaInterna" style="text-align: center;" cellpadding="1" cellspacing="5"
        width="100%">
        <tr>
            <td align="center" style="width: 100%">
                <h1>
                    Oh oh....<br />
                    Se ha producido un error.
                </h1>
                <br />
                <asp:ImageButton ID="btnVolver" ImageUrl="~/Images/404.png" runat="server" ToolTip="Click para Volver" OnClick="btnVolver_Click"
                    AlternateText="Click para Volver" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <div id="divDetalle" runat="server" style="text-align: justify; width: 100%">
                    <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" Font-Size="Large"></asp:Label>
                    <br />
                    <h5 style="margin: 0px; padding: 1px; width: 100%;">
                        Detalle:</h5>
                    <asp:Label ID="lblDetalle" runat="server" Text="" Font-Size="X-Small"></asp:Label>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
