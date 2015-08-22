using System;
using Newtonsoft.Json;

namespace KittyFeeder
{
	public class DailyScheduleEntryModel : DailyScheduleEntryModelBase
	{
		public DailyScheduleEntryModel () : base(ScheduleEntryType.Daily)
		{
		}

		public DailyScheduleEntryModel(TimeSpan time) : base(ScheduleEntryType.Daily, time)
		{
		}
	}
}

