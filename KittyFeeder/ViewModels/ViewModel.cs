using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace KittyFeeder
{
	public class ViewModel : INotifyPropertyChanged
	{
		private IFeeder _feeder;

		public ViewModel (IFeeder feeder)
		{
			_feeder = feeder;

			RunCommand = new RelayCommand (o => feeder.Feed ());

			AddMealCommand = new RelayCommand (o => {
				ScheduleEntries.Add (new ScheduleEntryViewModel ());
			});

			CommitScheduleCommand = new RelayCommand (o => SetSchedule());

			ScheduleEntries = new ObservableCollection<ScheduleEntryViewModel> ();
		}

		public ICommand RunCommand { get; set; }

		public ICommand AddMealCommand { get; set; }

		public ICommand CommitScheduleCommand { get; set; }

		public ObservableCollection<ScheduleEntryViewModel> ScheduleEntries { get; set; }

		private void SetSchedule()
		{
			
			var scheduleEntries = ScheduleEntries.Select (entry => new WeeklyScheduleEntryModel (Convert(entry.DayOfWeek), entry.Time)).Cast<ScheduleEntryModel>();
			_feeder.SetSchedule (new ScheduleModel{ Entries = scheduleEntries.ToList () });
		}

		private ScheduleEntryDayOfWeek Convert(DayOfWeek dayOfWeek)
		{
			switch(dayOfWeek)
			{
			case DayOfWeek.Sunday:
				return ScheduleEntryDayOfWeek.Sunday;
			case DayOfWeek.Monday:
				return ScheduleEntryDayOfWeek.Monday;
			case DayOfWeek.Tuesday:
				return ScheduleEntryDayOfWeek.Tuesday;
			case DayOfWeek.Wednesday:
				return ScheduleEntryDayOfWeek.Wednesday;
			case DayOfWeek.Thursday:
				return ScheduleEntryDayOfWeek.Thursday;
			case DayOfWeek.Friday:
				return ScheduleEntryDayOfWeek.Friday;
			case DayOfWeek.Saturday:
				return ScheduleEntryDayOfWeek.Saturday;
			default:
				throw new InvalidEnumArgumentException();
			}
		}

		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		protected void OnPropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null)
				handler (this, new PropertyChangedEventArgs (propertyName));
		}
	}
}

