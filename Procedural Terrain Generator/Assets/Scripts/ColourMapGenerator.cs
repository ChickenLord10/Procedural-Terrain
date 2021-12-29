using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColourMapGenerator
{

    public static Color[] GenerateColourMapRegions(float[,] noiseMap, int width, int height, TerrainType[] regions)
    {
        Color[] colourMap = new Color[(width) * (height)];

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

}
