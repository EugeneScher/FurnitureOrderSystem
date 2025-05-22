using System;
using System.Globalization;
using System.Windows.Data;
using FurnitureOrderSystem.Models.Entities;

namespace FurnitureOrderSystem.Utilities.Converters;

public class OrderItemTotalConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is OrderItem item)
        {
            return (item.UnitPrice * item.Quantity).ToString("C");
        }
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}