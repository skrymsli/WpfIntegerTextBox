using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace WpfIntegerTextBox.IntegerTextBox
{
    class NumericTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int)
            {
                return ((int)value).ToString(CultureInfo.InvariantCulture);
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var str = value as string;
            if (str != null) str = new string(str.Where(Char.IsDigit).ToArray());
            try
            {
                if (!string.IsNullOrEmpty(str)) return int.Parse(str);
            }
            catch (OverflowException)
            {
                return int.MaxValue;
            }
            return null;
        }
    }
}
