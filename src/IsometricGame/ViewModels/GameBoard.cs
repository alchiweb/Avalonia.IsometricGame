using System;
using System.Collections.ObjectModel;
using Avalonia;
using IsometricGame.Infrastructure;
using IsometricGame.Models;

namespace IsometricGame.ViewModels;

public class GameBoard : ViewModelBase
{
    public const double CellSize = 32;
    public static GameBoard DesignInstance { get; } = new();
    public static bool Isometric { get; private set; } = false;

    public ObservableCollection<GameObject> GameObjects { get; } = new();

    public TerrainTile[,] Tiles { get; }

    public Player Player { get; }
    public static int Height { get; private set; }
    public static int Width { get; private set; }

    private Random Random { get; } = new();

    public GameBoard(bool isometric = false) : this(20, 15, isometric)
    {
    }
    public static Point GetScreenPoint(int x, int y)
    {
        return GetScreenPoint(x, y, true);
    }
    public static Point GetScreenDetaPoint(double x, double y)
    {
        return GetScreenPoint(x, y, false);
    }
    private static Point GetScreenPoint(double x, double y, bool translate)
    {
        if (Isometric)
            return new Point((x) * CellSize +((translate ? Height-1 : 0)-x-y)*CellSize/2, (x+y) * CellSize/2);
        else
            return new Point(x * CellSize, y * CellSize);
    }

    //public static (int x, int y) GetCellLocation(Point point)
    //{
    //    if (Isometric)
    //        return (x: (point.X - (point.X + point.Y + Height)*CellSize/2)/CellSize, y: 1);//new Point(x * CellSize +(Height-x-y)*CellSize/2, (x+y) * CellSize/2);
    //    else
    //        return new Point(x * CellSize, y * CellSize);
    //}

    public GameBoard(int width, int height, bool isometric)
    {
        Isometric = isometric;
        Width = width;
        Height = height;
        Tiles = new TerrainTile[width, height];
        for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
                GameObjects.Add(
                    Tiles[x, y] =
                        new TerrainTile(GetScreenPoint(x, y), GetTypeForCoords(x, y)));
        GameObjects.Add(
            Player = new Player(this, new CellLocation(width / 2, height / 2), Facing.East));

        for (var c = 0; c < 10;)
        {
            var x = Random.Next(Width - 1);
            var y = Random.Next(Height - 1);
            if (!Tiles[x, y].IsPassable)
                continue;
            c++;
            GameObjects.Add(new Tank(this, new CellLocation(x, y), (Facing)Random.Next(4),
                Random.NextDouble() * 4 + 1));
        }
    }

    private TerrainTileType GetTypeForCoords(int x, int y)
    {
        if (x / 2 == Width / 4)
            return TerrainTileType.Pavement;
        if (y / 2 == Height / 4) return TerrainTileType.Water;

        if (x * y == 0) return TerrainTileType.StoneWall;
        if ((x + 1 - Width) * (y + 1 - Height) == 0) return TerrainTileType.WoodWall;


        //if(Random.NextDouble()<0.1) return TerrainTileType.WoodWall;
        if (Random.NextDouble() < 0.3) return TerrainTileType.Forest;
        return TerrainTileType.Plain;
    }
}