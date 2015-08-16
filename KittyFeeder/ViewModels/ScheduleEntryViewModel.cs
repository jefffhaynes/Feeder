using System;

namespace KittyFeeder
{
	public class ScheduleEntryViewModel
	{
		public DayOfWeek DayOfWeek { get; set; }
		public TimeSpan Time { get; set; }
	}
}

