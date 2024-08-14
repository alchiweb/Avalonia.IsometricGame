using System;
using System.Globalization;
using IsometricGame.Models;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media;
using IsometricGame.ViewModels;

namespace IsometricGame.Infrastructure;

internal class DirectionToMatrixConverter : IValueConverter
{
    public static DirectionToMatrixConverter Instance { get; } = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var direction = (Facing)value;
        var matrix = Matrix.Identity;
        if (GameBoard.Isometric)
        {
            double skewY = 0;
            double rotation = 0;
            switch (direction)
            {
                case Facing.South:
                    skewY = -0.523599;
                    rotation = 1.0472;
                    break;
                case Facing.East:
                    skewY = 0.523599;
                    rotation = -1.0472;
                    break;
                case Facing.North:
                    skewY = -0.523599;
                    rotation = -2.0944;
                    break;
                case Facing.West:
                    skewY = 0.523599;
                    rotation = 2.0944;
                    break;
            }
            matrix = Matrix.CreateSkew(0, skewY).Append(Matrix.CreateRotation(rotation));
        }
        else
        {
            if (direction == Facing.North) matrix = Matrix.CreateScale(1, -1);
            if (direction == Facing.West) matrix = Matrix.CreateRotation(1.5708);
            if (direction == Facing.East) matrix = Matrix.CreateRotation(1.5708) * Matrix.CreateScale(-1, 1);
        }
        return new MatrixTransform(matrix);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}