using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

			ScheduleEntries = new ObservableCollection<ScheduleEntryViewModel> ();
		}

		public ICommand RunCommand { get; set; }

		public ICommand AddMealCommand { get; set; }

		public ObservableCollection<ScheduleEntryViewModel> ScheduleEntries { get; set; }

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

