using System;
using Newtonsoft.Json;

namespace KittyFeeder
{
	public class OneTimeScheduleEntryModel : ScheduleEntryModel
	{
		public OneTimeScheduleEntryModel () : base(ScheduleEntryType.OneTime)
		{
		}

		public OneTimeScheduleEntryModel(DateTime dateTime) : this()
		{
			DateTime = dateTime;
		}

		[JsonProperty("time")]
		public DateTime DateTime { get; set; }

		[JsonProperty("time_s")]
		public double UnixTime
		{
			get { return GetUnixTimestamp (DateTime); }
		}

		private double GetUnixTimestamp(DateTime dateTime)
		{
			return DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
		}
	}
}

