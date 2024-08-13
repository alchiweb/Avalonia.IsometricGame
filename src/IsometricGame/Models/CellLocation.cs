using Avalonia;
using Isometric.ViewModels;

namespace Isometric.Models;

public readonly record struct CellLocation(int X, int Y)
{
    public Point ToPoint()
    {
        return GameBoard.GetScreenPoint(X, Y);
    }
}