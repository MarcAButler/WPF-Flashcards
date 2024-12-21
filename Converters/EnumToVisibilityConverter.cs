using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPF_Flashcards.Converters
{
    public class EnumToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return Visibility.Collapsed;

            string parameterString = parameter.ToString();
            bool negate = false;

            if (parameterString.StartsWith("!"))
            {
                negate = true;
                parameterString = parameterString.Substring(1);
            }

            if (Enum.IsDefined(value.GetType(), value) == false)
                return Visibility.Collapsed;

            object parameterValue = Enum.Parse(value.GetType(), parameterString);

            if (negate)
                return value.Equals(parameterValue) ? Visibility.Collapsed : Visibility.Visible;
            else
                return value.Equals(parameterValue) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
