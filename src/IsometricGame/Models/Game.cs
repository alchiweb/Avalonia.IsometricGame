using System;
using System.Linq;
using Avalonia.Input;
using IsometricGame.ViewModels;

namespace IsometricGame.Models;

public class Game : GameBase
{
    private readonly GameBoard _board;
    private Random Random { get; } = new();

    public Game(GameBoard board)
    {
        _board = board;
    }

    protected override void Tick()
    {
        if (!_board.Player.IsMoving)
        {
            int vertical = 0;
            int horizontal = 0;

            if (Keyboard.IsKeyDown(Key.Up))
                vertical -= 1;
            if (Keyboard.IsKeyDown(Key.Down))
                vertical += 1;
            if (Keyboard.IsKeyDown(Key.Left))
                horizontal -= 1;
            if (Keyboard.IsKeyDown(Key.Right))
                horizontal += 1;

            switch (horizontal, vertical)
            {
                case (_, -1):
                    _board.Player.SetTarget(Facing.North);
                    break;
                case (_, 1):
                    _board.Player.SetTarget(Facing.South);
                    break;
                case (-1, _):
                    _board.Player.SetTarget(Facing.West);
                    break;
                case (1, _):
                    _board.Player.SetTarget(Facing.East);
                    break;
            }
        }

        foreach (var tank in _board.GameObjects.OfType<Tank>())
            if (!tank.IsMoving)
                if (!tank.SetTarget(tank.Facing))
                    if (!tank.SetTarget((Facing)Random.Next(4)))
                        tank.SetTarget(null);

        foreach (var obj in _board.GameObjects.OfType<MovingGameObject>())
            obj.MoveToTarget();
    }
}