<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Cursos.ascx.cs" Inherits="EDUAR_UI.UserControls.Cursos" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div id="divCurso" runat="server" class="DivIndicador" style="width: 140px">
            <table border="0" cellpadding="2" cellspacing="0" width="100%">
                <tr>
                    <td align="center" style="width: 100%; border-bottom: 1px solid #000000">
                        <asp:Label ID="lblTitulo" Text="" runat="server" CssClass="lblTitulo" />
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="2" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 64px; height: 64px;">
                        <asp:ImageButton ImageUrl="~/Images/botonActividades.png" ID="btnActividad" runat="server"
                            AlternateText="Agenda" ToolTip="Agenda" OnClick="btnActividad_Click" />
                    </td>
                    <td style="width: 64px; height: 64px;">
                        <asp:ImageButton ImageUrl="~/Images/botonPlanificacion.png" ID="btnPlanificacion"
                            runat="server" AlternateText="Planificación" ToolTip="Planificación" OnClick="btnPlanficacion_Click"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 64px; height: 64px;">
                        <asp:ImageButton ImageUrl="~/Images/botonNotificacion.png" ID="btnNotificacion" runat="server"
                            AlternateText="Notificaciones" ToolTip="Notificaciones" OnClick="btnNotificacion_Click"/>
                    </td>
                    <td style="width: 64px; height: 64px;">
                        <asp:ImageButton ImageUrl="~/Images/botonIndicadores.png" ID="btnIndicador" runat="server"
                            AlternateText="Indicadores" ToolTip="Indicadores" OnClick="btnIndicadorClick"/>
                    </td>
                </tr>
            </table>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
