using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using IsometricGame.ViewModels;
using Avalonia.Controls;

namespace IsometricGame.Infrastructure;

public class ImageMultiConverter : IMultiValueConverter
{
    private static Dictionary<TerrainTileType, Bitmap> _cache;
    public static ImageMultiConverter Instance { get; } = new();

    private static Bitmap _playerBitmap;
    private static Bitmap _tankBitmap;
    private const string IsoPrefixe = "Iso-";
    public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
    {
        Bitmap? bitmap = null;

        if (values != null && values.Count == 2 && values[0] is Image image)
        {
            if (!GameBoard.Isometric)
            {
                image.Width = GameBoard.CellSize;
                image.Height = GameBoard.CellSize;
            }
            switch (values[1])
            {
                case TerrainTile terrainTile:
                    bitmap = GetCache()[terrainTile.Type];
                    if (GameBoard.Isometric)
                    {
                        image.Width = GameBoard.CellSize + 6;//bitmap.PixelSize.Width;
                        image.Height = GameBoard.CellSize + 32;//bitmap.PixelSize.Height;
                    }
                    break;
                case Player player:
                    bitmap = _playerBitmap ??= GetBitmap(nameof(Player));
                    if (GameBoard.Isometric)
                    {
                        image.Width = GameBoard.CellSize/2 + 16;//bitmap.PixelSize.Width;
                        image.Height = GameBoard.CellSize/2 + 12;//bitmap.PixelSize.Height;
                        if (GameBoard.CellSize >= 64)
                            image.Margin = new Thickness(10, 25, 0, 0);
                        else if (GameBoard.CellSize >= 32)
                            image.Margin = new Thickness(6, 15, 0, 0);
                    }
                    break;
                case Tank tank:
                    bitmap = _tankBitmap ??= GetBitmap(nameof(Tank));
                    if (GameBoard.Isometric)
                    {
                        image.Width = GameBoard.CellSize/2;//bitmap.PixelSize.Width;
                        image.Height = GameBoard.CellSize/2+2;//bitmap.PixelSize.Height;
                        if (GameBoard.CellSize >= 64)
                            image.Margin = new Thickness(20, 34, 0, 0);
                        else if (GameBoard.CellSize >= 32)
                            image.Margin = new Thickness(14, 24, 0, 0);
                    }
                    break;
            }
        }
        return bitmap;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    private static Dictionary<TerrainTileType, Bitmap> GetCache()
    {
        return _cache ??= Enum.GetValues(typeof(TerrainTileType)).OfType<TerrainTileType>().ToDictionary(
            t => t,
            t => GetBitmap(t.ToString()));
    }
    private static Bitmap GetBitmap(string partfilename)
    {
        return new Bitmap(AssetLoader.Open(new Uri($"avares://IsometricGame/Assets/{(GameBoard.Isometric ? $"{IsoPrefixe}" : "")}{partfilename}.png")))/*?.CreateScaledBitmap(new PixelSize(70,96))*/;
    }
}