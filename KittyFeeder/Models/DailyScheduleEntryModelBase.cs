using System;
using Newtonsoft.Json;

namespace KittyFeeder
{
	public abstract class DailyScheduleEntryModelBase : ScheduleEntryModel
	{
		public DailyScheduleEntryModelBase (ScheduleEntryType type) : base(type)
		{
		}

		public DailyScheduleEntryModelBase(ScheduleEntryType type, TimeSpan time) : this(type)
		{
			Time = time;
		}

		[JsonProperty("time")]
		public TimeSpan Time { get; set; }

		[JsonProperty("time_s")]
		public double TimeOfDay
		{
			get { return Time.TotalSeconds; }
		}
	}
}

