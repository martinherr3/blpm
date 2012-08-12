<%@ Page Title="Planificaci&oacute;n de Clases" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="PlanificacionClases.aspx.cs" Inherits="EDUAR_UI.PlanificacionClases" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var srcElement;

        $(document).ready(function () {

            $("#Button2").click(function () {
                $("div .planificado div").remove();
            });

            $("#Button1").click(function () {
                var data = new Array();
                $("div .planificado div").each(function (index) {
                    data[index] = "'" + this.innerHTML + "'";
                });
                $.ajax({
                    type: 'POST',
                    url: 'shoppingcart.aspx/PlaceOrder',
                    contentType: "application/json; charset=utf-8",
                    data: '{ products:[' + data.join() + ']}',
                    dataType: 'json',
                    success: function (results) { alert(results.d); },
                    error: function () { alert('error'); }
                });
            });

            $("div .divContenidos").each(function () {
                this.addEventListener('dragstart', OnDragStart, false);
            });

            $("div .planificado").each(function () {
                this.addEventListener('dragenter', OnDragEnter, false);
                this.addEventListener('dragleave', OnDragLeave, false);
                this.addEventListener('dragover', OnDragOver, false);
                this.addEventListener('drop', OnDrop, false);
                this.addEventListener('dragend', OnDragEnd, false);
            });

        })

        function OnDragStart(e) {
            this.style.opacity = '0.4';
            srcElement = this;
            e.dataTransfer.effectAllowed = 'move';
            e.dataTransfer.setData('text/html', $(this).find("header")[0].innerHTML);
        }

        function OnDragOver(e) {
            if (e.preventDefault) {
                e.preventDefault();
            }
            $(this).addClass('highlight');
            e.dataTransfer.dropEffect = 'move';
            return false;
        }

        function OnDragEnter(e) {
            $(this).addClass('highlight');
        }

        function OnDragLeave(e) {
            $(this).removeClass('highlight');
        }

        function OnDrop(e) {
            if (e.preventDefault) {
                e.preventDefault();
            }
            srcElement.style.opacity = '1';
            $(this).removeClass('highlight');
            var count = $(this).find("div[data-product-name='" + e.dataTransfer.getData('text/html') + "']").length;
            if (count <= 0) {
                $(this).append("<div class='temaSeleccionado' data-product-name='" + e.dataTransfer.getData('text/html') + "'>" + e.dataTransfer.getData('text/html') + "</div>");
            }
            else {
                alert("This product is already added to your cart!");
            }
            return false;
        }

        function OnDragEnd(e) {
            $("div .planificado").removeClass('highlight');
            this.style.opacity = '1';
        }    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="tablaInterna" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <h2>
                    Planificaci&oacute;n De Clases</h2>
                <br />
            </td>
            <td align="right" rowspan="2">
                <%--<asp:UpdatePanel ID="udpBotonera" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:ImageButton ID="btnNuevo" runat="server" ToolTip="Nuevo" ImageUrl="~/Images/botonNuevo.png"
                                Visible="false" OnClick="btnNuevo_Click" />
                            <asp:ImageButton ID="btnPDF" runat="server" ToolTip="Exportar a PDF" ImageUrl="~/Images/ExportarPDF.png"
                                Visible="false" onclick="btnPDF_Click" />
                            <asp:ImageButton ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" ToolTip="Guardar"
                                ImageUrl="~/Images/botonGuardar.png" Visible="false" />
                            <asp:ImageButton ID="btnVolver" OnClick="btnVolver_Click" runat="server" ToolTip="Volver"
                                ImageUrl="~/Images/botonVolver.png" Visible="false" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlAsignatura" EventName="SelectedIndexChanged" />
                            <asp:PostBackTrigger ControlID="btnPDF" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
            </td>
        </tr>
    </table>
    <table class="tablaInterna" cellpadding="1" cellspacing="5">
        <tr>
            <td class="TD110px">
                <asp:Label ID="lblCurso" runat="server" Text="Curso:" CssClass="lblCriterios"></asp:Label>
            </td>
            <td class="TD140px">
                <asp:DropDownList ID="ddlCurso" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCurso_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td class="TD100px">
                <asp:Label ID="lblAsignatura" runat="server" Text="Asignatura:" CssClass="lblCriterios"></asp:Label>
            </td>
            <td>
                <asp:UpdatePanel ID="udpAsignatura" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlAsignatura" runat="server" Enabled="false" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlAsignatura_SelectedIndexChanged">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlCurso" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="TD110px">
                <asp:Label ID="lblClase" runat="server" Text="Clase del d&iacute;a:" CssClass="lblCriterios"></asp:Label>
            </td>
            <td colspan="3">
                <asp:UpdatePanel ID="udpMeses" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlMeses" OnSelectedIndexChanged="ddlMeses_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:DropDownList runat="server" ID="ddlDia" Enabled="false">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlAsignatura" EventName="SelectedIndexChanged" />
                        <asp:PostBackTrigger ControlID="ddlMeses" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <br />
    <div id="divContenidos" style="width: 100%; border: 0">
        <table class="tablaInterna" cellpadding="1" cellspacing="5">
            <tr>
                <td style="width: 50%">
                    <asp:UpdatePanel ID="udpContenidos" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DataList ID="dtlContenidos" runat="server" Width="100%">
                                <ItemTemplate>
                                    <div class="temasContainer" draggable="true">
                                        <header><%# Eval("titulo")%></header>
                                        <%--<div><asp:Image runat="server" ID="img1" ImageUrl='<%# Eval("ImageUrl") %>' /></div>--%>
                                        <div>
                                            <%# Boolean.Parse(Eval("obligatorio").ToString()) ? "Sí" : "No"%></div>
                                        <%--<div><%# Eval("Cost","Cost : ${0}") %></div>--%>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlAsignatura" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 50%">
                    <div id="divPlanificado" style="border: 1; width: 50%" class="planificado">
                        <input id="Button1" type="button" value="Place Order" />
                        <br />
                        <input id="Button2" type="button" value="Clear Cart" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
