using System;

namespace KittyFeeder
{
	public class ScheduleEntryViewModel
	{
		public ScheduleEntryViewModel()
		{
		}

		public ScheduleEntryViewModel(DayOfWeek dayOfWeek, TimeSpan time)
		{
			DayOfWeek = dayOfWeek;
			Time = time;
		}

		public DayOfWeek DayOfWeek { get; set; }
		public TimeSpan Time { get; set; }
	}
}

