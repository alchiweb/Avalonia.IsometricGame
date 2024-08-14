using IsometricGame.Models;

namespace IsometricGame.ViewModels;

public class Player : MovingGameObject
{
    public Player(GameBoard field, CellLocation location, Facing facing) : base(field, location, facing, int.MaxValue)
    {
    }
}