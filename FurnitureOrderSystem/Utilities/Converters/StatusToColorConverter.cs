using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace FurnitureOrderSystem.Utilities.Converters;

public class StatusToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string status)
        {
            return status switch
            {
                "Новый" => Brushes.LightGreen,
                "В работе" => Brushes.Orange,
                "Завершен" => Brushes.LightGray,
                "Отменен" => Brushes.LightCoral,
                _ => Brushes.White
            };
        }
        return Brushes.White;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}