using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int MapWidth;
    public int MapHeight;
    public float NoiseScale;

    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;
    public int seed;
    public Vector2 offset;

    public bool autoUpdate;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(MapWidth, MapHeight, seed, NoiseScale, octaves, persistance, lacunarity, offset);

        MapDisplay display = FindObjectOfType<MapDisplay>();
        display.DrawNoiseMap(noiseMap);
    }

    private void OnValidate()
    {
        if(MapWidth < 1) MapWidth = 1;
        if (MapHeight < 1) MapHeight = 1;
        if (lacunarity < 1) lacunarity = 1;
        if (octaves < 1) octaves = 1;

    }
}
