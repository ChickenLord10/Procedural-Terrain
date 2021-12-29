using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureGenerator
{
    public static Color[][] ChopUpTiles(Texture2D tileMaptexture, int TileRes)
    {
        int NumTilesPerRow = tileMaptexture.width / TileRes;
        int NumRows = tileMaptexture.height / TileRes;

        Color[][] tiles = new Color[NumTilesPerRow * NumRows][];

        for(int y = 0; y < NumRows; y++)
        {
            for (int x = 0; x < NumTilesPerRow; x++)
            {
                tiles[y * NumTilesPerRow + x] = tileMaptexture.GetPixels(x* TileRes, y * TileRes, TileRes, TileRes);
            }
        }
        return tiles;
    }

    public static Texture2D TextureFromTileMap(Tile[,] tileMap, Texture2D tileMaptexture, int width, int height, int TileRes)
    {
        int NumTilesPerRow = tileMaptexture.width / TileRes;
        Color[][] tiles = ChopUpTiles(tileMaptexture, TileRes);

        Texture2D texture = new Texture2D((width + 1) * TileRes, (height + 1) * TileRes);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color[] pixels = tiles[(int)tileMap[x,y].type];
                texture.SetPixels(x * TileRes, y * TileRes, TileRes, TileRes, pixels);
            }
        }

        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureFromColourMap(Color[] colourMap, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height);

        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colourMap);
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureFromHeightMap(float[,] heightMap)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        Color[] colourMap = new Color[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colourMap[y * height + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
            }
        }

        return TextureFromColourMap(colourMap, width, height);
    }
}
