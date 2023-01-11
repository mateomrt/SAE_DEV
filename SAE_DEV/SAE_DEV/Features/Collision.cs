using MonoGame.Extended.Tiled;
using SAE_DEV.Screens;

namespace SAE_DEV
{
    internal class Collision
    {
        //On vérifie ici si il y a des collisions avec les tuiles alentours
        public static bool IsCollision(ushort x, ushort y)
        {
            TiledMapTileLayer mapLayer = Monde._tiledMap.GetLayer<TiledMapTileLayer>("Batiment");
            TiledMapTile? tile;
            if (mapLayer.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }

       
    }
}
