using System;
using Xamarin.Forms;

namespace MovieTimeApp.Helpers.Converters
{
	public class PathToImageUrlConverter : IValueConverter
    {
        private readonly IConfig _config;

        public PathToImageUrlConverter(IConfig config)
        {
            _config = config;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return $"{_config.ImageBaseUrl}{_config.DefaultImageFormat}{value}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}