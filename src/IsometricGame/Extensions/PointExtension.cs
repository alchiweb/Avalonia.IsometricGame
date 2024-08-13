using Avalonia;
using Isometric.ViewModels;

namespace Isometric.Extensions
{
    static public class PointExtension
    {
        public static Point ToScreen(this Point point) => point * GameBoard.CellSize;
    }
}
