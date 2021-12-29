using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile 
{
    public enum TileType { Deep_Water, Shallow_Water, Sand, Grass, Forest, Mountain }
    public TileType type = TileType.Grass;

    World world;
    int x;
    int y;

    public Tile(World world, int x, int y)
    {
        this.world = world;
        this.x = x;
        this.y = y;
    }

    
}
