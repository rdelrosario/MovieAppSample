using System;
using Xamarin.Forms;

namespace MovieTimeApp.Helpers.Converters
{
	public class RatingToDisplayRatingConverter : IValueConverter
    {
        private readonly IConfig _config;

        public RatingToDisplayRatingConverter(IConfig config)
        {
            _config = config;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double number;
            if (Double.TryParse($"{value}", out number))
            {
                var rating = (int) (5 * (number / 10)); ;
                return rating == 0? 1: rating;
            }
              
            return number;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}