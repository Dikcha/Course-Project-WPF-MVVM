using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CourceProject.Converters
{
    public class TotalSecondsToTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "0:00";

            int minutes = (int)value / 60;
            int seconds = (int)value - minutes * 60;
            string secondFormat = seconds < 10 ? '0' + seconds.ToString() : seconds.ToString();
            return $"{minutes}:{ secondFormat }";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
