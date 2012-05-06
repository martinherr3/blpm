<%@ Page Title="Planificación de Clases" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="Planificacion.aspx.cs" Inherits="EDUAR_UI.Planificacion"
    EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <ext:ResourcePlaceHolder ID="ResourcePlaceHolder1" runat="server" Mode="Style" />
    <link rel="stylesheet" type="text/css" href="../../Shared/resources/css/main.css" />
    <ext:ResourcePlaceHolder ID="ResourcePlaceHolder2" runat="server" Mode="Script" />
    <script type="text/javascript" src="../../Shared/resources/js/common.js"></script>
    <style type="text/css">
        .ext-color-4, .ext-ie .ext-color-4-ad, .ext-opera .ext-color-4-ad
        {
            color: #7F0000;
        }
        .ext-cal-day-col .ext-color-4, .ext-dd-drag-proxy .ext-color-4, .ext-color-4-ad, .ext-color-4-ad .ext-cal-evm, .ext-color-4 .ext-cal-picker-icon, .ext-color-4-x dl, .ext-color-4-x .ext-cal-evb
        {
            background: #7F0000;
        }
        .ext-color-4-x .ext-cal-evb, .ext-color-4-x dl
        {
            border-color: #7C3939;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <ext:Viewport ID="Viewport1" runat="server" Layout="Border">
        <Items>
            <ext:Panel ID="Panel1" runat="server" Height="35" Border="false" Region="North" Cls="app-header"
                BodyCssClass="app-header-content">
                <Content>
                    <div id="app-logo">
                        <div class="logo-top">
                            &nbsp;</div>
                        <div id="logo-body">
                            &nbsp;</div>
                        <div class="logo-bottom">
                            &nbsp;</div>
                    </div>
                    <h1>
                        My Calendar</h1>
                    <span id="app-msg" class="x-hidden"></span>
                </Content>
            </ext:Panel>
            <ext:Panel ID="Panel2" runat="server" Title="..." Layout="Border" Region="Center"
                Cls="app-center">
                <Items>
                    <ext:Panel ID="Panel3" runat="server" Width="176" Region="West" Border="false" Cls="app-west">
                        <Items>
                            <ext:DatePicker ID="DatePicker1" runat="server" Cls="ext-cal-nav-picker">
                                <Listeners>
                                    <Select Fn="CompanyX.setStartDate" Scope="CompanyX" />
                                </Listeners>
                            </ext:DatePicker>
                        </Items>
                        <TopBar>
                            <ext:Toolbar ID="Toolbar1" runat="server">
                                <Items>
                                    <ext:Button ID="Button1" runat="server" Text="Save All Events" Icon="Disk" OnClientClick="CompanyX.record.saveAll();" />
                                </Items>
                            </ext:Toolbar>
                        </TopBar>
                    </ext:Panel>
                    <ext:CalendarPanel ID="CalendarPanel1" runat="server" Region="Center" ActiveIndex="2"
                        Border="false">
                        <GroupStore ID="GroupStore1" runat="server">
                            <Groups>
                                <ext:Group CalendarId="1" Title="Home" />
                                <ext:Group CalendarId="2" Title="Work" />
                                <ext:Group CalendarId="3" Title="School" />
                                <ext:Group CalendarId="4" Title="Other" />
                            </Groups>
                        </GroupStore>
                        <MonthView ID="MonthView1" runat="server" ShowHeader="true" ShowWeekLinks="true"
                            ShowWeekNumbers="true" />
                        <Listeners>
                            <ViewChange Fn="CompanyX.viewChange" Scope="CompanyX" />
                            <EventClick Fn="CompanyX.record.show" Scope="CompanyX" />
                            <DayClick Fn="CompanyX.dayClick" Scope="CompanyX" />
                            <RangeSelect Fn="CompanyX.rangeSelect" Scope="CompanyX" />
                            <EventMove Fn="CompanyX.record.move" Scope="CompanyX" />
                            <EventResize Fn="CompanyX.record.resize" Scope="CompanyX" />
                        </Listeners>
                    </ext:CalendarPanel>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Viewport>
    <ext:EventEditWindow ID="EventEditWindow1" runat="server" Hidden="true" GroupStoreID="GroupStore1">
        <Listeners>
            <EventAdd Fn="CompanyX.record.add" Scope="CompanyX" />
            <EventUpdate Fn="CompanyX.record.update" Scope="CompanyX" />
            <EditDetails Fn="CompanyX.record.edit" Scope="CompanyX" />
            <EventDelete Fn="CompanyX.record.remove" Scope="CompanyX" />
        </Listeners>
    </ext:EventEditWindow>
</asp:Content>
