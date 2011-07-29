<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintReport.aspx.cs" Inherits="EDUAR_UI.PrintReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Impresión de Informes</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="udpReporte" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView ID="grdReporte" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
