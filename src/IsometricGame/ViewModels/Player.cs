using Isometric.Models;

namespace Isometric.ViewModels;

public class Player : MovingGameObject
{
    public Player(GameBoard field, CellLocation location, Facing facing) : base(field, location, facing)
    {
    }
}