using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Isometric.ViewModels;
using Avalonia.Controls;

namespace Isometric.Infrastructure;

public class TerrainTileMultiConverter : IMultiValueConverter
{
    private static Dictionary<TerrainTileType, Bitmap> _cache;
    public static TerrainTileMultiConverter Instance { get; } = new();

    public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values != null)
        {
            if (values.Count == 2 && values[0] is TerrainTile terrainTile && values[1] is Image image)
            {
                image.Width = 32;
                image.Height = 32;
                return GetCache()[terrainTile.Type];
            }
        }
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
            t => new Bitmap(AssetLoader.Open(new Uri($"avares://IsometricGame/Assets/{t}.png"))));
    }
}