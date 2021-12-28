using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColourMapGenerator
{

    public static Color[] GenerateColourMapRegions(float[,] noiseMap, int width, int height, TerrainType[] regions)
    {
        Color[] colourMap = new Color[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float currentHeight = noiseMap[x, y];

                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colourMap[y * width + x] = regions[i].colour;
                        break;
                    }
                }
            }
        }
        return colourMap;
    }
    /*
    public static Color[] GenerateColourMapTiles(TileMapData[] tileMapData, int width, int height, TerrainTile[] tiles, int TileRes, Texture2D terrainTiles)
    {
        
        int numTilesPerRow = terrainTiles.width / TileRes;
        int numRows = terrainTiles.height / TileRes;

        Color[] tileMapColour = new Color[width * TileRes * height * TileRes];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float currentHeight = noiseMap[x, y];

                for (int i = 0; i < tiles.Length; i++)
                {
                    if (currentHeight <= tiles[i].height)
                    {
                        colourMap[y * width + x] = tiles[i].terrainTileTexture;
                        break;
                    }
                }
            }
        }
        return tileMapColour;
        
    }
*/
}
