<%@ Page Title="Acceso a Cursos" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="AccesoCursos.aspx.cs" Inherits="EDUAR_UI.AccesoCursos" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Src="~/UserControls/Cursos.ascx" TagName="Curso" TagPrefix="cur" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="tablaInterna" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <h2>
                    <asp:Label ID="lblTituloPrincipal" Text="Cursos del Ciclo Lectivo Actual" runat="server" /></h2>
                <br />
            </td>
        </tr>
    </table>
    <table class="tablaInterna" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:UpdatePanel ID="udpCursos" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DataList RepeatLayout="Table" RepeatColumns="6" RepeatDirection="Horizontal" runat="server"
                            ID="dtlCursos" Width="100%" ShowHeader="true" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <cur:Curso ID="curso" runat="server" titulo='<%# Bind("nombre") %>' idCurso='<%# Bind("idCurso") %>'>
                                </cur:Curso>
                            </ItemTemplate>
                        </asp:DataList>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
