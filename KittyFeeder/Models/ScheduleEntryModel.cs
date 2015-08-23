using System;
using Newtonsoft.Json;

namespace KittyFeeder
{
	public class ScheduleEntryModel
	{
		public ScheduleEntryModel(){}

		protected ScheduleEntryModel(ScheduleEntryType type)
		{
			Type = type;
		}

		[JsonProperty("type")]
		public ScheduleEntryType Type { get; set; }

		[JsonProperty("portions")]
		public int Portions { get; set; }
	}
}

