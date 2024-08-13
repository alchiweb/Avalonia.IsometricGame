using Avalonia;
using IsometricGame.ViewModels;

namespace IsometricGame.Models;

public readonly record struct CellLocation(int X, int Y)
{
    public Point ToPoint()
    {
        return GameBoard.GetScreenPoint(X, Y);
    }
}