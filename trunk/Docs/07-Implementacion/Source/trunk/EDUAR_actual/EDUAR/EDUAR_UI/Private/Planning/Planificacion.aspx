<%@ Page Title="Planificación de Clases" Language="C#" MasterPageFile="~/EDUARMaster.Master"
	AutoEventWireup="true" CodeBehind="Planificacion.aspx.cs" Inherits="EDUAR_UI.Planificacion"
	EnableEventValidation="false" %>

<%@ Register Assembly="DayPilot.MonthPicker" Namespace="DayPilot.Web.UI" TagPrefix="DayPilot" %>
<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<DayPilot:MonthPicker ID="MonthPicker1" runat="server"
		StartDate="01/01/2000" FormatMonth="%M" />
	<DayPilot:DayPilotCalendar ID="DayPilotCalendar1" runat="server"  
		ShowHeader="true" ShowHours="true" Days="7" TimeFormat="Clock24Hours" />
	<DayPilot:DayPilotScheduler ID="DayPilotScheduler1" runat="server" 
		BackColor="#FFFFD5" BorderColor="#000000" DurationBarColor="Blue" 
		EventBackColor="#FFFFFF" EventBorderColor="#000000" EventHeight="17" 
		HeaderFontColor="0, 0, 0" HeaderHeight="17" HourBorderColor="#EAD098" 
		HourNameBackColor="#ECE9D8" HourNameBorderColor="#ACA899" HoverColor="#FFED95" 
		NonBusinessBackColor="#FFF4BC" StartDate="2012-04-23" TimeFormat="Clock24Hours">
	</DayPilot:DayPilotScheduler>
</asp:Content>
