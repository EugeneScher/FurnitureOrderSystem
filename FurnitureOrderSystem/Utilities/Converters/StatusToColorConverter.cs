using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

public class StatusToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string status)
        {
            return status switch
            {
                "Новый" => Brushes.Blue,
                "В производстве" => Brushes.Orange,
                "Готов" => Brushes.Green,
                "Доставлен" => Brushes.Gray,
                _ => Brushes.Black
            };
        }
        return Brushes.Black;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}