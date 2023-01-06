﻿using MonoGame.Extended.Tiled;
using SAE_DEV.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_DEV
{
    internal class Collision
    {
        public static string _mapLayer = "Batiment";
        public static bool IsCollision(ushort x, ushort y)
        {
            TiledMapTileLayer _collision = Monde._tiledMap.GetLayer<TiledMapTileLayer>(_mapLayer);
            TiledMapTile? tile;
            if (_collision.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }

    }
}
