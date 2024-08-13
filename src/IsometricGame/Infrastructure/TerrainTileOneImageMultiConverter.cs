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
using Avalonia.Media;

namespace Isometric.Infrastructure;

public class TerrainTileOneImageMultiConverter : IMultiValueConverter
{
    private static Bitmap? _bitmapImage = null;
    public static Bitmap? BitmapImage
    {
        get {
            if (_bitmapImage == null)
            {
                _bitmapImage = new Bitmap(AssetLoader.Open(new Uri($"avares://Isometric/Assets/Spritesheet.png"))).CreateScaledBitmap(new PixelSize(600, 522));
            }
            return _bitmapImage;
        }
    }
    public static TerrainTileOneImageMultiConverter Instance { get; } = new();

    public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values != null)
        {
            if (values.Count == 2 && values[0] is TerrainTile terrainTile && values[1] is Image image)
            {
                image.Width = 32;
                image.Height = 32;
                PixelRect rect;
                switch(terrainTile.Type)
                {
                    case TerrainTileType.Plain:
                        rect = new PixelRect(382, 46, 32, 32);
                        break;
                    case TerrainTileType.Forest:
                        rect = new PixelRect(406, 0, 32, 32);
                        break;
                    case TerrainTileType.Pavement:
                        rect = new PixelRect(215, 432, 32, 32);
                        break;
                    case TerrainTileType.WoodWall:
                        rect = new PixelRect(0, 258, 32, 32);
                        break;
                    case TerrainTileType.StoneWall:
                        rect = new PixelRect(0, 285, 32, 32);
                        break;
                    default:
                        rect = new PixelRect(214, 73, 32, 32);
                        break;
                }

                return new CroppedBitmap(
                   BitmapImage,
                   rect);
            }
        }
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

}