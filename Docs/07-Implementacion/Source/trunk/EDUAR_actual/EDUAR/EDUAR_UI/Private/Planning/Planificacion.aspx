<%@ Page Language="C#" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<script runat="server">
    [DirectMethod(Namespace = "CompanyX")]
    public void ShowMsg(string msg)
    {
        X.Msg.Notify("Message", msg).Show();
    }
</script>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Remote Data Calendar - Ext.NET Examples</title>
    
    <ext:ResourcePlaceHolder ID="ResourcePlaceHolder1" runat="server" Mode="Style" />
    <link rel="stylesheet" type="text/css" href="../../Shared/resources/css/main.css" />
    
    <ext:ResourcePlaceHolder ID="ResourcePlaceHolder2" runat="server" Mode="Script" />
    <script type="text/javascript" src="../../Shared/resources/js/common.js"></script>
    <script type="text/javascript" src="../../Shared/resources/js/override.js"></script>
</head>
<body>
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" 
            runat="server" 
            ScriptMode="Debug"
            IDMode="Explicit" 
            InitScriptMode="Linked" 
            RemoveViewState="true"
            Namespace="CompanyX"
            />
        
        <ext:Viewport ID="Viewport1" runat="server" Layout="Border">
            <Items>
                <ext:Panel ID="Panel1" 
                    runat="server" 
                    Height="35" 
                    Border="false" 
                    Region="North" 
                    Cls="app-header" 
                    BodyCssClass="app-header-content">
                    <Content>
                        <div id="app-logo">
                            <div class="logo-top">&nbsp;</div>
                            <div id="logo-body">&nbsp;</div>
                            <div class="logo-bottom">&nbsp;</div>
                        </div>
                        <h1>My Calendar</h1>
                        <span id="app-msg" class="x-hidden"></span>
                    </Content>
                </ext:Panel>
                
                <ext:Panel 
                    ID="Panel2" 
                    runat="server" 
                    Title="..." 
                    Layout="Border" 
                    Region="Center" 
                    Cls="app-center">
                    <Items>
                        <ext:Panel ID="Panel3" 
                            runat="server" 
                            Width="176" 
                            Region="West" 
                            Border="false" 
                            Cls="app-west">
                            <Items>
                                <ext:DatePicker 
                                    ID="DatePicker1" 
                                    runat="server" 
                                    Cls="ext-cal-nav-picker">
                                </ext:DatePicker>
                            </Items>
                            <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <ext:Button 
                                            ID="Button1"
                                            runat="server" 
                                            Text="Save All Events" 
                                            Icon="Disk" 
                                            />
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                        </ext:Panel>
                        
                        <ext:CalendarPanel
                            ID="CalendarPanel1" 
                            runat="server"
                            Region="Center"
                            ActiveIndex="2"
                            Border="false">
                            <GroupStore ID="GroupStore1" runat="server">
                                <Groups>
                                    <ext:Group CalendarId="1" Title="Home" />
                                    <ext:Group CalendarId="2" Title="Work" />
                                    <ext:Group CalendarId="3" Title="School" />
                                </Groups>
                            </GroupStore>
                            <EventStore 
                                ID="EventStore1" 
                                runat="server" 
                                DateFormat="M$"
                                SaveMappings="false">
                                <Reader>
                                    <ext:JsonReader Root="d" />
                                </Reader>
                            </EventStore>
                            <MonthView ID="MonthView1" 
                                runat="server" 
                                ShowHeader="true" 
                                ShowWeekLinks="true" 
                                ShowWeekNumbers="true" 
                                />  
                        </ext:CalendarPanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
        
        <ext:EventEditWindow 
            ID="EventEditWindow1" 
            runat="server"
            Hidden="true"
            GroupStoreID="GroupStore1">
            <Listeners>
                <EventAdd    Fn="CompanyX.record.add" Scope="CompanyX" />
                <EventUpdate Fn="CompanyX.record.update" Scope="CompanyX" />
                <EditDetails Fn="CompanyX.record.edit" Scope="CompanyX" />
                <EventDelete Fn="CompanyX.record.remove" Scope="CompanyX" />
            </Listeners>
        </ext:EventEditWindow>
    </form>
</body>
</html>
