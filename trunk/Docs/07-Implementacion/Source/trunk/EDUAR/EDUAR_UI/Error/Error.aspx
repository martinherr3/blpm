<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="EDUAR_UI.Error.Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>EDU@R 2.0</title>
    <link rel="icon" type="image/png" href="~/favicon.ico" />
    <style type="text/css">
        h1, h2, h3, h4, h5, h6
        {
            font-size: 1.5em;
            color: #666666;
            font-variant: small-caps;
            text-transform: none;
            font-weight: 200;
            margin-bottom: 0px;
        }
        
        h1
        {
            font-size: 1.6em;
            padding-bottom: 0px;
            margin-bottom: 0px;
        }
        
        h2
        {
            font-size: 1.5em;
            font-weight: 600;
        }
        
        h3
        {
            font-size: 1.2em;
            font-weight: bold;
        }
        
        .tablaInterna
        {
            border: 0;
            border-top: 1px solid silver;
            padding-top: 5px;
        }
    </style>
</head>
<body>
    <table class="tablaInterna" style="text-align: center;" cellpadding="1" cellspacing="5"
        width="100%">
        <tr>
            <td align="center" style="width: 100%">
                <h1>
                    Oh oh....<br />
                    Se ha producido un error.
                </h1>
                <br />
                <asp:ImageButton ImageUrl="~/Images/404.png" runat="server" ToolTip="Click para Volver"
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
</body>
</html>
