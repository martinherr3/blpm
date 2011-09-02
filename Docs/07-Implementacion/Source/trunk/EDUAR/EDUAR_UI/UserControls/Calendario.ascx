<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Calendario.ascx.cs"
    Inherits="EDUAR_UI.UserControls.Calendario" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script type="text/javascript" src="/EDUAR_UI/Scripts/jquery.maskedinput-1.3.js"></script>
<script type="text/javascript">
    $(function () {
        $('#date').datepicker(
            { dateFormat: 'dd MM, yy',
                minDate: '+0D',
                maxDate: '+1Y',
//                changeMonth: true,
//                changeYear: true,
                numberOfMonths: 1,
                dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa'],
                monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo',
                    'Junio', 'Julio', 'Agosto', 'Septiembre',
                    'Octubre', 'Noviembre', 'Diciembre'],
                monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr',
                    'May', 'Jun', 'Jul', 'Ago',
                    'Sep', 'Oct', 'Nov', 'Dic'],
                dateFormat: 'dd/mm/yy',
                beforeShowDay: function (day) {
                    var day = day.getDay();
                    if (day == 0 || day == 6) {
                        return [false, "somecssclass"]
                    } else {
                        return [true, "someothercssclass"]
                    }
                } 
            });  


        $("#date").mask("99/99/9999");

        $.mask.definitions['H'] = '[012]';
        $.mask.definitions['N'] = '[012345]';
        $.mask.definitions['n'] = '[0123456789]';
        $("#time").mask("Hn:Nn");
    });
</script>
<script type="text/javascript">
    function valida(valor) {
        //que no existan elementos sin escribir
        if (valor.indexOf("_") == -1) {
            var hora = valor.split(":")[0];
            if (parseInt(hora) > 23) {
                $("#time").val("");
            }
        }
    }</script>
<div id="DivDesdeHasta" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: middle; width: 25%">
                <asp:HiddenField runat="server" ID="hndMensajeErrorValidacion" Value="" />
                <div id="divEtiquetaDesdeDA" runat="server">
                    <asp:Label ID="lblFechaDesde_DA" runat="server" CssClass="lblCriterios"></asp:Label>
                </div>
            </td>
            <td style="width: 25%">
                <div style="vertical-align: middle; overflow: visible">
                    <asp:TextBox ID="txtFechaDesde_DA" runat="server" CssClass="EstiloTxtMedio120" />
                    <cc1:MaskedEditExtender ID="maskEditFechaDesde_DA" runat="server" Mask="99/99/9999"
                        MaskType="Date" TargetControlID="txtFechaDesde_DA" UserDateFormat="DayMonthYear"
                        Enabled="True" ClearTextOnInvalid="True">
                    </cc1:MaskedEditExtender>
                    <asp:Image ID="imgFechaDesde_DA" runat="server" ImageUrl="~/Images/calendario.png"
                        ImageAlign="AbsMiddle" />
                    <cc1:CalendarExtender ID="calExtFechaDesde_DA" runat="server" TargetControlID="txtFechaDesde_DA"
                        Format="dd/MM/yyyy" PopupButtonID="imgFechaDesde_DA">
                    </cc1:CalendarExtender>
                </div>
            </td>
            <td style="vertical-align: middle; width: 25%">
                <div id="divEtiquetaHastaDA" runat="server">
                    <asp:Label ID="lblFechaHasta_DA" runat="server" CssClass="lblCriterios"></asp:Label>
                </div>
            </td>
            <td style="width: 25%">
                <div style="vertical-align: middle; overflow: visible">
                    <asp:TextBox ID="txtFechaHasta_DA" runat="server" CssClass="EstiloTxtMedio120" />
                    <cc1:MaskedEditExtender ID="maskEditFechaHasta_DA" runat="server" Mask="99/99/9999"
                        MaskType="Date" TargetControlID="txtFechaHasta_DA" UserDateFormat="DayMonthYear"
                        Enabled="True" ClearTextOnInvalid="True">
                    </cc1:MaskedEditExtender>
                    <asp:Image ID="imgFechaHasta_DA" runat="server" ImageUrl="~/Images/calendario.png"
                        ImageAlign="AbsMiddle" />
                    <cc1:CalendarExtender ID="calExtFechaHasta_DA" runat="server" TargetControlID="txtFechaHasta_DA"
                        Format="dd/MM/yyyy" PopupButtonID="imgFechaHasta_DA">
                    </cc1:CalendarExtender>
                </div>
            </td>
        </tr>
    </table>
</div>
<div id="DivDesde" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="vertical-align: middle; width: 40%">
                <div id="divEtiquetaDesdeD" runat="server">
                    <asp:Label ID="lblFechaDesde" runat="server" CssClass="lblCriterios"></asp:Label>
                </div>
            </td>
            <td style="vertical-align: middle; width: 60%">
                <div style="vertical-align: middle; overflow: visible">
                    <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="EstiloTxtMedio120"></asp:TextBox>
                    <cc1:MaskedEditExtender ID="txtFechaDesde_MaskedEditExtender" runat="server" Mask="99/99/9999"
                        MaskType="Date" TargetControlID="txtFechaDesde" UserDateFormat="DayMonthYear"
                        Enabled="True" ClearTextOnInvalid="True">
                    </cc1:MaskedEditExtender>
                    <asp:Image ID="imgFechaDesde" runat="server" ImageUrl="~/Images/calendario.png" ImageAlign="AbsMiddle" />
                    <cc1:CalendarExtender ID="calExtDesde" runat="server" TargetControlID="txtFechaDesde"
                        Format="dd/MM/yyyy" PopupButtonID="imgFechaDesde">
                    </cc1:CalendarExtender>
                </div>
            </td>
        </tr>
    </table>
</div>
<div id="DivSoloFecha" runat="server">
    <asp:TextBox ID="txtFecha" runat="server"></asp:TextBox>
    <cc1:MaskedEditExtender ID="txtFecha_MaskedEditExtender" runat="server" Mask="99/99/9999"
        MaskType="Date" TargetControlID="txtFecha" UserDateFormat="DayMonthYear" Enabled="True"
        ClearTextOnInvalid="True">
    </cc1:MaskedEditExtender>
    <asp:Image ID="imgFecha" runat="server" ImageUrl="~/Images/calendario.png" ImageAlign="AbsMiddle" />
    <cc1:CalendarExtender ID="calExtFecha" runat="server" TargetControlID="txtFecha"
        Format="dd/MM/yyyy" PopupButtonID="imgFecha">
    </cc1:CalendarExtender>
    <input id="date" type="text" />
    <input type="text" id="time" onblur="valida(this.value);" />
</div>
