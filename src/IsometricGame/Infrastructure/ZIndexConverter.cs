using System;
using System.Globalization;
using Avalonia.Data.Converters;
using IsometricGame.ViewModels;

namespace IsometricGame.Infrastructure;

internal class ZIndexConverter : IValueConverter
{
    public static ZIndexConverter Instance { get; } = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is GameObject gameObject)
        {
            return gameObject.Layer;
        }
        return 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}