using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace InvoiceMaker.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        ///  Converts a boolean value to a Visibility value.
        ///  If the input value is "true", returns "Visibility.Visible", otherwise, returns "Visibility.Collapsed".
        ///  If the input value is not of type "bool", returns "Visibility.Collapsed".
        /// </summary>
        /// <param name="value"> The input value to convert, which is expected to be of type "bool"</param>
        /// <param name="targetType"> The target type for the conversion. </param>
        /// <param name="parameter"> An optional parameter that can be used in the conversion </param>
        /// <param name="culture"> The culture information </param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
