using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TileMapGenerator
{


    static public void GenerateTileMap(World world, float[,] noiseMap, int width, int height, TerrainTile[] tileTypes)
    {
        //Tile[,] tileMap = world.tiles;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float currentHeight = noiseMap[x, y];

                for (int i = 0; i < tileTypes.Length; i++)
                {
                    if (currentHeight <= tileTypes[i].height)
                    {
                        //Debug.Log("ï = " + i.ToString());
                        //Debug.Log(world.tiles[x, y].type.ToString());
                        //Debug.Log(tileTypes[i].type.ToString());
                        world.tiles[x, y].type = tileTypes[i].type;
                        break;
                    }
                }
            }
        }
    }
}
