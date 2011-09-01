<%@ Page Title="Redactar Mensaje" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="MsjeRedactar.aspx.cs" Inherits="EDUAR_UI.MsjeRedactar"
    Theme="Tema" StylesheetTheme="Tema" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%--<%@ Register Src="~/UserControls/TextoHTML.ascx" TagName="TextoHTML" TagPrefix="edi" %>--%>
<%@ Register Namespace="EDUAR_UI.UserControls" TagPrefix="custom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .ui-autocomplete-loading
        {
            background: white url('../../Images/loading.gif') right center no-repeat;
        }
        
        /* RESET */html, body, div, span, h1, h2, h3, h4, h5, h6, p, blockquote, a, font, img, dl, dt, dd, ol, ul, li, legend, table, tbody, tr, th, td
        {
            margin: 0px;
            padding: 0px;
            border: 0;
            outline: 0;
            font-weight: inherit;
            font-style: inherit;
            font-size: 100%;
            font-family: inherit;
            list-style: none;
        }
        a img
        {
            border: none;
        }
        ol li
        {
            list-style: decimal outside;
        }
        fieldset
        {
            border: 0;
            padding: 0;
        }
        body
        {
            font-family: sans-serif;
            font-size: 1em;
        }
        div#container
        {
            width: 780px;
            margin: 0 auto;
            padding: 1em 0;
        }
        p
        {
            margin: 1em 0;
            max-width: 700px;
        }
        h1 + p
        {
            margin-top: 0;
        }
        h1, h2
        {
            font-family: Georgia, Times, serif;
        }
        h1
        {
            font-size: 2em;
            margin-bottom: .75em;
        }
        h2
        {
            font-size: 1.5em;
            margin: 2.5em 0 .5em;
            border-bottom: 1px solid #999;
            padding-bottom: 5px;
        }
        h3
        {
            font-weight: bold;
        }
        ul li
        {
            list-style: disc;
            margin-left: 1em;
        }
        ol li
        {
            margin-left: 1.25em;
        }
        div.side-by-side
        {
            width: 100%;
            margin-bottom: 1em;
        }
        div.side-by-side > div
        {
            float: left;
            width: 50%;
        }
        div.side-by-side > div > em
        {
            margin-bottom: 10px;
            display: block;
        }
        a
        {
            color: orange;
            text-decoration: underline;
        }
        .faqs em
        {
            display: block;
        }
        .clearfix:after
        {
            content: "\0020";
            display: block;
            height: 0;
            clear: both;
            overflow: hidden;
            visibility: hidden;
        }
        footer
        {
            margin-top: 2em;
            border-top: 1px solid #666;
            padding-top: 5px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $('#date').datepicker();

            function split(val) {
                return val.split(/,\s*/);
            }
            function extractLast(term) {
                return split(term).pop();
            }

            $("#tbNameFilter").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "/Services/AutocompleteService.asmx/Aoi_Autocomplete",
                        data: "{ 'q': '" + request.term + "', 'limit': '10' }",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.Name,
                                    value: item.Name
                                }
                            }))
                        }
                    });
                },
                minLength: 1
            });

            $("#birds")
            // don't navigate away from the field on tab when selecting an item
			.bind("keydown", function (event) {
			    if (event.keyCode === $.ui.keyCode.TAB &&
						$(this).data("autocomplete").menu.active) {
			        event.preventDefault();
			    }
			})
			.autocomplete({
			    source: function (request, response) {
			        $.ajax({
			            url: "../../WebServices/SrvDestinatario.asmx/obtenerDestinatarios",
			            data: "{ 'prefixText': '" + extractLast(request.term) + "', 'limite': 10 }",
			            dataType: "json",
			            type: "POST",
			            contentType: "application/json; charset=utf-8",
			            dataFilter: function (data) { return data; },
			            success: function (data) {
			                response($.map(data.d, function (item) {
			                    return {
			                        value: item.apellido + " " + item.nombre
			                    }
			                }))
			            },
			            error: function (XMLHttpRequest, textStatus, errorThrown) {
			                alert(textStatus);
			            }
			        });

			        minLength: 2
			    },
			    search: function () {
			        // custom minLength
			        var term = extractLast(this.value);
			        if (term.length < 2) {
			            return false;
			        }
			    },
			    focus: function () {
			        // prevent value inserted on focus
			        return false;
			    },
			    select: function (event, ui) {
			        var terms = split(this.value);
			        // remove the current input
			        terms.pop();
			        // add the selected item
			        terms.push(ui.item.value);
			        // add placeholder to get the comma-and-space at the end
			        terms.push("");
			        this.value = terms.join(", ");
			        return false;
			    }
			});
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <input type="text" name="date" id="date" />
    <div class="demo">
        <div class="ui-widget">
            <label for="birds">
                Birds:
            </label>
            <input id="birds" size="50" />
        </div>
    </div>
    <div class="demo">
        <div class="ui-widget">
            <label for="birds">
                Birds:
            </label>
            <select id="tbNameFilter">
                <%--<option value="">text</option>--%>
            </select>
        </div>
    </div>
    <select data-placeholder="Seleccione..." style="width: 350px;" multiple class="chzn-select"
        runat="server" id="ddlDestino"></select>

    <asp:Button Text="submit" runat="server" OnClick="btnSubmit_Click" />
        <script src="/EDUAR_UI/Scripts/jquery-1.6.2.min.js" type="text/javascript"></script>
        <script src="/EDUAR_UI/chosen/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript">            $(".chzn-select").chosen();</script>
</asp:Content>
