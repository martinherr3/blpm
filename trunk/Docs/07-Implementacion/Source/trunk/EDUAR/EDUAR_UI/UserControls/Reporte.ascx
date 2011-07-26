<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reporte.ascx.cs" Inherits="EDUAR_UI.UserControls.Reporte" %>
<table class="tablaInterna" cellpadding="0" cellspacing="0">
    <tr>
        <td align="right">
            <asp:imagebutton id="btnExcel" runat="server" tooltip="Exportar a Excel" imageurl="~/Images/botonBuscar.png" />
            <asp:imagebutton id="btnPDF" runat="server" tooltip="Exportar a PDF" imageurl="~/Images/botonBuscar.png" />
            <asp:imagebutton id="btnWord" runat="server" tooltip="Exportar a Word" imageurl="~/Images/botonBuscar.png" />
        </td>
    </tr>
</table>
<asp:updatepanel id="udpReporte" runat="server" updatemode="Conditional">
        <ContentTemplate>
            <asp:GridView ID="gvwReporte" runat="server" PagerSettings-Visible="true" BorderStyle="Solid"
                Width="100%" AutoGenerateColumns="False"
                AllowPaging="True" DataKeyNames="">
                <EmptyDataRowStyle HorizontalAlign="Center" />
                <Columns>
                    <%--<asp:TemplateField HeaderText="Usuario">
                        <HeaderStyle HorizontalAlign="left" Width="40%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="columna1" runat="server" Text='<%# Bind("columna1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />--%>
        </Triggers>
    </asp:updatepanel>
