﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsometricGame.ViewModels
{
    public enum TerrainTileType
    {
        Plain, //passable, shoot-thru
        WoodWall, //impassable, takes 1 shot to bring down
        StoneWall, //impassable, indestructible
        Water1, //impassable, shoot-thru
        Water2, //impassable, shoot-thru
        Pavement, //passable, 2x speed
        Forest //passable at half speed, shoot-thru
    }
}
