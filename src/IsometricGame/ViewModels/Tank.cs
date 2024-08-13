﻿using Isometric.Models;

namespace Isometric.ViewModels;

public class Tank : MovingGameObject
{
    private readonly double _speed;

    public Tank(GameBoard field, CellLocation location, Facing facing, double speed) : base(field, location, facing)
    {
        _speed = speed;
    }

    protected override double SpeedFactor => _speed * base.SpeedFactor;
}