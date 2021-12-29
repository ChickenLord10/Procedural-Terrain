﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World 
{

    public Tile[,] tiles;
    int width;
    int height;

    public World(int width, int height)
    {
        this.width = width;
        this.height = height;

        tiles = new Tile[width, height];

        for(int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                tiles[x, y] = new Tile(this, x, y);
            }
        }

    }

    public Tile GetTileAt(int x, int y)
    {
        if(x < 0 || x > width || y < 0 || y > height)
        {
            Debug.LogError("Tile (" + x + "," + y + ") was out of range");
            return null;
        }
        return tiles[x, y];
    }
}
