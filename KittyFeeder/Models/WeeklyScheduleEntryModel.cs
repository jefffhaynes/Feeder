using System;
using Newtonsoft.Json;

namespace KittyFeeder
{
	public class WeeklyScheduleEntryModel : DailyScheduleEntryModelBase
	{
		public WeeklyScheduleEntryModel () : base(ScheduleEntryType.Weekly)
		{
		}

		public WeeklyScheduleEntryModel(ScheduleEntryDayOfWeek dayOfWeek, TimeSpan time) : base(ScheduleEntryType.Weekly, time)
		{
			DayOfWeek = dayOfWeek;
		}

		[JsonProperty("day_of_week")]
		public ScheduleEntryDayOfWeek DayOfWeek { get; set; }
	}
}

