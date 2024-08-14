using Avalonia;
using System;
using System.Linq;
using IsometricGame.Models;

namespace IsometricGame.ViewModels;

public abstract class MovingGameObject : GameObject
{
    private readonly GameBoard _field;

    private  int _layer = 0;

    public override int Layer => _layer;

    private Facing _facing;
    public Facing Facing
    {
        get => _facing;
        set
        {
            if (value == _facing) return;
            _facing = value;
            OnPropertyChanged();
        }
    }

    private CellLocation _cellLocation;
    public CellLocation CellLocation
    {
        get => _cellLocation;
        private set
        {
            if (value.Equals(_cellLocation)) return;
            _cellLocation = value;
            if (GameBoard.Isometric)
            {
                var newLayer = _cellLocation.X + _cellLocation.Y;

                if (_layer != newLayer)
                {
                    _layer = _cellLocation.X + _cellLocation.Y;
                    OnPropertyChanged(nameof(Layer));
                }
            }
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsMoving));
        }
    }

    private CellLocation _targetCellLocation;
    public CellLocation TargetCellLocation
    {
        get => _targetCellLocation;
        private set
        {
            if (value.Equals(_targetCellLocation)) return;
            _targetCellLocation = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsMoving));
        }
    }

    public bool IsMoving => TargetCellLocation != CellLocation;

    protected virtual double SpeedFactor => (double)1 / 15;


    protected MovingGameObject(GameBoard field, CellLocation location, Facing facing, int defaultLayer) : base(location.ToPoint())
    {
        _field = field;
        Facing = facing;
        CellLocation = TargetCellLocation = location;
        _layer = defaultLayer;
    }
    public bool SetTarget(CellLocation loc)
    {
        if (IsMoving)
            //We are the bear rolling from the hill
            throw new InvalidOperationException("Unable to change direction while moving");
        if (loc == CellLocation)
            return true;
        Facing = GetDirection(CellLocation, loc);
        if (loc.X < 0 || loc.Y < 0)
            return false;
        if (loc.X >= GameBoard.Width || loc.Y >= GameBoard.Height)
            return false;
        if (!_field.Tiles[loc.X, loc.Y].IsPassable)
            return false;

        if (
            _field.GameObjects.OfType<MovingGameObject>()
            .Any(t => t != this && (t.CellLocation == loc || t.TargetCellLocation == loc)))
            return false;

        TargetCellLocation = loc;
        return true;
    }

    public CellLocation GetTileAtDirection(Facing facing)
    {
        switch(facing)
        {
            case Facing.North:
                return CellLocation with { Y = CellLocation.Y - 1 };
            case Facing.South:
                return CellLocation with { Y = CellLocation.Y + 1 };
            case Facing.West:
                return CellLocation with { X = CellLocation.X - 1 };
            default:
                return CellLocation with { X = CellLocation.X + 1 };
        }
    }

    public bool SetTarget(Facing? facing)
    {
        return SetTarget(facing.HasValue ? GetTileAtDirection(facing.Value) : CellLocation);
    }

    private Facing GetDirection(CellLocation current, CellLocation target)
    {
        if (target.X < current.X)
            return Facing.West;
        if (target.X > current.X)
            return Facing.East;
        if (target.Y < current.Y)
            return Facing.North;
        return Facing.South;
    }

    public void SetLocation(CellLocation loc)
    {
        CellLocation = loc;
        Location = loc.ToPoint();
    }

    public void MoveToTarget()
    {
        if (TargetCellLocation == CellLocation)
            return;
        var speed = //GameBoard.CellSize *
                    (_field.Tiles[CellLocation.X, CellLocation.Y].Speed +
                     _field.Tiles[TargetCellLocation.X, TargetCellLocation.Y].Speed) / 2
                    * SpeedFactor;
        Point speedPoint;
        var pos = Location;
        var direction = GetDirection(CellLocation, TargetCellLocation);
        switch(direction)
        {
            case Facing.North:
                speedPoint = GameBoard.GetScreenDetaPoint(0, -speed);
                break;
            case Facing.South:
                speedPoint = GameBoard.GetScreenDetaPoint(0, speed);
                break;
            case Facing.West:
                speedPoint = GameBoard.GetScreenDetaPoint(-speed, 0);
                break;
            case Facing.East:
                speedPoint = GameBoard.GetScreenDetaPoint(speed, 0);
                break;
            default:
                speedPoint = new(0, 0);
                break;
        }
        var screenPos = GameBoard.GetScreenPoint(CellLocation.X, CellLocation.Y);
        // Firt time to move (if isometric)
        if (GameBoard.Isometric && (speedPoint.Y > 0.0) && screenPos.X == Location.X && screenPos.Y == Location.Y)
        {
            if (this is Player player)
            {

            }
            _layer = TargetCellLocation.X + TargetCellLocation.Y;
            OnPropertyChanged(nameof(Layer));
        }



        Location = Location + speedPoint;
        var targetLocation = GameBoard.GetScreenPoint(TargetCellLocation.X, TargetCellLocation.Y);
        bool reached = false;
        switch(speedPoint.X <= 0, Location.X <= targetLocation.X)
        {
            case (true, true):
            case (false, false):
                switch (speedPoint.Y <= 0, Location.Y <= targetLocation.Y)
                {
                    case (true, true):
                    case (false, false):
                        reached = true;
                        break;
                }
                break;
        }
        if (reached)
        {
            SetLocation(TargetCellLocation);
        }
    }
}