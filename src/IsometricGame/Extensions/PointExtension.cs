using Avalonia;
using IsometricGame.ViewModels;

namespace IsometricGame.Extensions
{
    static public class PointExtension
    {
        public static Point ToScreen(this Point point) => point * GameBoard.CellSize;
    }
}
