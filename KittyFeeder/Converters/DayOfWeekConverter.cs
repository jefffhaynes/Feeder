using System;
using Xamarin.Forms;

namespace KittyFeeder.Converters
{
	public class DayOfWeekConverter : IValueConverter
	{
		#region IValueConverter implementation

		public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var dow = (DayOfWeek)value;
			return dow.ToString ();
		}

		public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return Enum.Parse (typeof(DayOfWeek), (string)value);
		}

		#endregion
	}
}

