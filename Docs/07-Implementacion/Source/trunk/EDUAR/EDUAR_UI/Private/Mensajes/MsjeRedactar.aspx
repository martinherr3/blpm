<%@ Page Title="" Language="C#" MasterPageFile="~/EDUARMaster.Master" AutoEventWireup="true"
    CodeBehind="MsjeRedactar.aspx.cs" Inherits="EDUAR_UI.MsjeRedactar" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <td class="TDCriterios25">
                Seleccionar:
            </td>
            <td class="TDCriterios75">
                <asp:TextBox ID="txtDestinatario" runat="server" />
                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" enabled="True"
                                servicepath="~/WebServices/SrvDestinatario.asmx" minimumprefixlength="1" servicemethod="obtenerDestinatarios"
                                enablecaching="true" targetcontrolid="txtDestinatario" usecontextkey="True" completionsetcount="10"
                                completioninterval="0" >
                    </cc1:AutoCompleteExtender>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="TextBox1" runat="server" Width="80%" Height="300px" TextMode="MultiLine" />
    <cc1:HtmlEditorExtender ID="HtmlEditorExtender1" TargetControlID="TextBox1"
        runat="server">
        <Toolbar>
            <cc1:Undo />
            <cc1:Redo />
            <cc1:Bold />
            <cc1:Italic />
            <cc1:Underline />
            <cc1:StrikeThrough />
            <cc1:Subscript />
            <cc1:Superscript />
            <cc1:JustifyLeft />
            <cc1:JustifyCenter />
            <cc1:JustifyRight />
            <cc1:JustifyFull />
            <cc1:InsertOrderedList />
            <cc1:InsertUnorderedList />
            <cc1:CreateLink />
            <cc1:UnLink />
            <cc1:RemoveFormat />
            <cc1:SelectAll />
            <cc1:UnSelect />
            <cc1:Delete />
            <cc1:Cut />
            <cc1:Copy />
            <cc1:Paste />
            <cc1:BackgroundColorSelector />
            <cc1:ForeColorSelector />
            <cc1:FontNameSelector />
            <cc1:FontSizeSelector />
            <cc1:Indent />
            <cc1:Outdent />
            <cc1:InsertHorizontalRule />
            <cc1:HorizontalSeparator />
        </Toolbar>
    </cc1:HtmlEditorExtender>
</asp:Content>
