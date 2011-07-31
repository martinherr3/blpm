<%@ Page Title="Registrar eventos institucionales" Language="C#" MasterPageFile="~/EDUARMaster.Master" AutoEventWireup="true" 
CodeBehind="RegisterEventoInstitucional.aspx.cs" Inherits="EDUAR_UI.RegisterEventoInstitucional" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Src="~/UserControls/Calendario.ascx" TagName="Calendario" TagPrefix="cal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="ContentHeader" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Registración de eventos institucionales
    </h2>
    
    <div class="eventInfo">
        <fieldset class="register">
            <legend>Información del evento</legend>

            <p>
                <asp:Label runat="server" AssociatedControlID="Lugar" ID="LugarLabel">Lugar:</asp:Label>
                <asp:TextBox runat="server" ID="Lugar" CssClass="textEntry"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Lugar" CssClass="failureNotification" Display="Dynamic"
                    ToolTip="Lugar del evento requerido." ID="LugarRequired" ValidationGroup="RegisterEventValidationGroup"
                    ErrorMessage="El lugar del evento es Requerido.">*
                </asp:RequiredFieldValidator>
            </p>

 <%--           <p>
                Fecha: <cal:Calendario ID="fechas" TipoCalendario="SoloFecha" runat="server" TipoAlineacion="Izquierda" />
            </p>--%>
            <p>
                <asp:Label runat="server" AssociatedControlID="Fecha" ID="FechaLabel">Fecha:</asp:Label>
                <asp:TextBox runat="server" ID="Fecha" CssClass="textEntry" MaxLength="10"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="Fecha" Format="dd/MM/yyyy"/>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Fecha" CssClass="failureNotification" Display="Dynamic"
                    ToolTip="Fecha del evento requerida." ID="FechaRequired" ValidationGroup="RegisterEventValidationGroup"
                    ErrorMessage="La fecha del evento es Requerida.">*
                </asp:RequiredFieldValidator>
            </p>

            <p>
                <asp:Label runat="server" AssociatedControlID="Hora" ID="HoraLabel">Hora:</asp:Label>
                <asp:TextBox runat="server" ID="Hora" CssClass="textEntry" MaxLength="5" ></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Hora" CssClass="failureNotification" Display="Dynamic"
                    ToolTip="Hora del evento requerida." ID="HoraRequired" ValidationGroup="RegisterEventValidationGroup"
                    ErrorMessage="La hora del evento es Requerida.">*
                </asp:RequiredFieldValidator>
            </p>

            <p>
                <asp:Label runat="server" AssociatedControlID="Titulo" ID="TituloLabel">Título:</asp:Label>
                <asp:TextBox runat="server" ID="Titulo" CssClass="textEntry"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Titulo" CssClass="failureNotification" Display="Dynamic"
                    ToolTip="Título del evento requerido." ID="TituloRequired" ValidationGroup="RegisterEventValidationGroup"
                    ErrorMessage="El título del evento es Requerido.">*
                </asp:RequiredFieldValidator>
            </p>

            <p>
                <asp:Label runat="server" AssociatedControlID="Detalle" ID="DetalleLabel">Detalle:</asp:Label>
                <asp:TextBox runat="server" ID="Detalle" CssClass="textEntry" TextMode="MultiLine" Columns="80"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Detalle" CssClass="failureNotification" Display="Dynamic"
                    ToolTip="Detalle del evento requerido." ID="DetalleRequired" ValidationGroup="RegisterEventValidationGroup"
                    ErrorMessage="El detalle del evento es Requerido.">*
                </asp:RequiredFieldValidator>
            </p>

        </fieldset>

        <p class="submitButton">
            <asp:Button ID="btnRegisterEvent" runat="server" Text="Registrar Evento"
                ValidationGroup="RegisterEventValidationGroup" OnClick="btnRegisterEvent_Click" />
        </p>
    </div>
</asp:Content>

