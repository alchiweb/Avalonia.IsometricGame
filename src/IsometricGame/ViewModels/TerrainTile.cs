using System.Collections.Generic;
using Avalonia;

namespace IsometricGame.ViewModels;

public class TerrainTile : GameObject
{
    private double _speed;
    public double Speed
    {
        get => _speed;
        init => _speed = value;
    }
    private bool _shootThrus;
    public bool ShootThrus
    {
        get => _shootThrus;
        init => _shootThrus = value;
    }
    public bool IsPassable => _speed > 0.1;
    public TerrainTileType Type { get; set; }

    private int _layer;
    public override int Layer => _layer;
    public TerrainTile(Point location, TerrainTileType type, int layer) : base(location)
    {
        Type = type;
        Speed = GetSpeed(Type);
        ShootThrus = GetShootThrus(Type);
        _layer = layer;
    }
    static private double GetSpeed(TerrainTileType terrainTileType)
    {
        switch (terrainTileType)
        {
            case TerrainTileType.Plain:
                return 1;
            case TerrainTileType.Pavement:
                return 2;
            case TerrainTileType.Forest:
                return 0.5;
            default:
                return 0;
        }
    }
    static private bool GetShootThrus(TerrainTileType terrainTileType)
    {
        switch (terrainTileType)
        {
            case TerrainTileType.WoodWall:
            case TerrainTileType.StoneWall:
                return false;
            default:
                return true;
        }
    }
}