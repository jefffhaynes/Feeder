using System;
using Newtonsoft.Json;

namespace KittyFeeder
{
	public abstract class ScheduleEntryModel
	{
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

