using System;
using System.Collections.Generic;
using EDUAR_BusinessLogic.Common;
using EDUAR_Entities;
using EDUAR_UI.Shared;
using Ext.Net;
using Ext.Net.Calendar.Demo;

namespace EDUAR_UI
{
	public partial class Planificacion : EDUARBasePage
	{
		protected void Page_Init(object sender, EventArgs e)
		{
			this.CalendarPanel1.EventStore = new EventStore { ID = "EventStore1" };
			this.CalendarPanel1.EventStore.CustomConfig.Add(new ConfigItem("saveMappings", "false"));
			this.CalendarPanel1.EventStore.SubmitData += EventStore_SubmitData;
		}

		void EventStore_SubmitData(object sender, StoreSubmitDataEventArgs e)
		{
			var events = e.Object<Event>();
			((EventsViewer)this.Page.LoadControl("../../Shared/Common/EventsViewer.ascx")).Render(events);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!X.IsAjaxRequest)
			{
				this.CalendarPanel1.EventStore.Events.AddRange(Data.Events);
			}
		}

		[DirectMethod(Namespace = "CompanyX")]
		public void ShowMsg(string msg)
		{
			X.Msg.Notify("Message", msg).Show();
		}
	}
}