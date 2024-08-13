using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Isometric.ViewModels;

namespace Isometric.Infrastructure;

public class TerrainTileConverter : IValueConverter
{
    private static Dictionary<TerrainTileType, Bitmap> _cache;
    public static TerrainTileConverter Instance { get; } = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is TerrainTile terrainTile)
            return GetCache()[(TerrainTileType)value];
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    private Dictionary<TerrainTileType, Bitmap> GetCache()
    {
        return _cache ??= Enum.GetValues(typeof(TerrainTileType)).OfType<TerrainTileType>().ToDictionary(
            t => t,
            t => new Bitmap(AssetLoader.Open(new Uri($"avares://Isometric/Assets/{t}.png"))));
    }
}