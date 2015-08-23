using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace KittyFeeder
{
	public class ScheduleModel
	{
		public ScheduleModel()
		{
			Entries = new List<ScheduleEntryModel> ();
		}

		[JsonProperty("entries")]
		public List<ScheduleEntryModel> Entries { get; set; }

		[JsonProperty("utc_offset_s")]
		public double UtcOffset 
		{
			get { return TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).TotalSeconds; }
			set{ }
		}
	}
}

