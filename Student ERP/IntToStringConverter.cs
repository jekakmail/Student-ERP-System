using System;
using System.Globalization;
using System.Windows.Data;

namespace Student_ERP
{

    public class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = string.Empty;
            if (value != null)
            {
                bool sex =  System.Convert.ToBoolean(value);
                result = !sex ? "Муж." : "Жен.";
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}