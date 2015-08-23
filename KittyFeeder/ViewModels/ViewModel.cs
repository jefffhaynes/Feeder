using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace KittyFeeder
{
	public class ViewModel : INotifyPropertyChanged
	{
		private IFeeder _feeder;
		private ICommand _addMealCommand;
		private ICommand _commitScheduleCommand;

		public ViewModel (IFeeder feeder)
		{
			_feeder = feeder;

			ScheduleEntries = new NotifyTaskCompletion<ObservableCollection<ScheduleEntryViewModel>> (GetSchedule ());

			ScheduleEntries.PropertyChanged += (object sender, PropertyChangedEventArgs e) => 
			{
				if(e.PropertyName == "Result")
				{
					AddMealCommand = new RelayCommand (o => {
						ScheduleEntries.Result.Add (new ScheduleEntryViewModel ());
					});
	
					CommitScheduleCommand = new RelayCommand (async o => await SetSchedule (ScheduleEntries.Result));
				}
			};

			RunCommand = new RelayCommand (o => _feeder.Feed ());
		}

		public ICommand RunCommand { get; set; }

		public ICommand AddMealCommand 
		{
			get { return _addMealCommand; }
			set
			{
				if(_addMealCommand != value)
				{
					_addMealCommand = value;
					OnPropertyChanged ("AddMealCommand");
				}
			}
		}

		public ICommand CommitScheduleCommand
		{
			get { return _commitScheduleCommand; }
			set
			{
				if(_commitScheduleCommand != value)
				{
					_commitScheduleCommand = value;
					OnPropertyChanged ("CommitScheduleCommand");
				}
			}
		}

		public NotifyTaskCompletion<ObservableCollection<ScheduleEntryViewModel>> ScheduleEntries { get; private set; }

		private async Task SetSchedule(IList<ScheduleEntryViewModel> schedule)
		{
			var scheduleEntries = schedule.Select (entry => new WeeklyScheduleEntryModel (Convert(entry.DayOfWeek), entry.Time)).Cast<ScheduleEntryModel>();
			await _feeder.SetSchedule (new ScheduleModel{ Entries = scheduleEntries.ToList () });
		}

		private async Task<ObservableCollection<ScheduleEntryViewModel>> GetSchedule()
		{
			var schedule = await _feeder.GetSchedule ();
			var convertedSchedule = schedule.Entries.Cast<WeeklyScheduleEntryModel> ()
				.Select (entry => new ScheduleEntryViewModel (Convert (entry.DayOfWeek), entry.Time)).ToList ();
			return new ObservableCollection<ScheduleEntryViewModel> (convertedSchedule);
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

		private DayOfWeek Convert(ScheduleEntryDayOfWeek dayOfWeek)
		{
			switch(dayOfWeek)
			{
			case ScheduleEntryDayOfWeek.Sunday:
				return DayOfWeek.Sunday;
			case ScheduleEntryDayOfWeek.Monday:
				return DayOfWeek.Monday;
			case ScheduleEntryDayOfWeek.Tuesday:
				return DayOfWeek.Tuesday;
			case ScheduleEntryDayOfWeek.Wednesday:
				return DayOfWeek.Wednesday;
			case ScheduleEntryDayOfWeek.Thursday:
				return DayOfWeek.Thursday;
			case ScheduleEntryDayOfWeek.Friday:
				return DayOfWeek.Friday;
			case ScheduleEntryDayOfWeek.Saturday:
				return DayOfWeek.Saturday;
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

