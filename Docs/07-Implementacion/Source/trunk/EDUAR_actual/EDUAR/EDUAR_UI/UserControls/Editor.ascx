<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Editor.ascx.cs" Inherits="EDUAR_UI.UserControls.Editor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:TextBox ID="txtEditor" runat="server" Width="707px" Height="200px" TextMode="MultiLine" CssClass="txtMultilinea"/>
<ajaxToolkit:HtmlEditorExtender ID="HtmlEditorExtender1" TargetControlID="txtEditor" 
    runat="server"   >
    <Toolbar>
        <ajaxToolkit:Undo />
        <ajaxToolkit:Redo />
        <ajaxToolkit:Bold />
        <ajaxToolkit:Italic />
        <ajaxToolkit:Underline />
        <ajaxToolkit:StrikeThrough />
        <ajaxToolkit:Subscript />
        <ajaxToolkit:Superscript />
        <ajaxToolkit:JustifyLeft />
        <ajaxToolkit:JustifyCenter />
        <ajaxToolkit:JustifyRight />
        <ajaxToolkit:JustifyFull />
        <ajaxToolkit:InsertOrderedList />
        <ajaxToolkit:InsertUnorderedList />
        <ajaxToolkit:CreateLink />
        <ajaxToolkit:UnLink />
        <ajaxToolkit:RemoveFormat />
        <ajaxToolkit:SelectAll />
        <ajaxToolkit:UnSelect />
        <ajaxToolkit:Delete />
        <ajaxToolkit:Cut />
        <ajaxToolkit:Copy />
        <ajaxToolkit:Paste />
        <ajaxToolkit:BackgroundColorSelector />
        <ajaxToolkit:ForeColorSelector />
        <ajaxToolkit:FontNameSelector />
        <ajaxToolkit:FontSizeSelector />
        <ajaxToolkit:Indent />
        <ajaxToolkit:Outdent />
        <ajaxToolkit:InsertHorizontalRule />
        <ajaxToolkit:HorizontalSeparator />
    </Toolbar>
</ajaxToolkit:HtmlEditorExtender>
