using System;
using System.Collections.Generic;

namespace KittyFeeder
{
	public class ScheduleModel
	{
		public ScheduleModel()
		{
			Entries = new List<ScheduleEntryModel> ();
		}

		public List<ScheduleEntryModel> Entries { get; set; }
	}
}

