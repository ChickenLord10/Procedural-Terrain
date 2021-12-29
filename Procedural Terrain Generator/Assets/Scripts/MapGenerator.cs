using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode { NoiseMap, ColourMap, Mesh, TileMap }
    public DrawMode drawmode;
    
    
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;
    public int seed;
    public Vector2 offset;

    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;
    public Texture2D terrainTiles;
    public int tileResolution;

    public bool autoUpdate;

    public TerrainTile[] tileTypes;
    public TerrainType[] regions;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);


        MapDisplay display = FindObjectOfType<MapDisplay>();
        if(drawmode == DrawMode.NoiseMap)
        {
            Texture2D texture = TextureGenerator.TextureFromHeightMap(noiseMap);
            display.DrawTexture(texture);
        }
        else if (drawmode == DrawMode.ColourMap)
        {
            Color[] colourMap = ColourMapGenerator.GenerateColourMapRegions(noiseMap, mapWidth, mapHeight, regions);
            Texture2D texture = TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight);
            display.DrawTexture(texture);
        }
        else if (drawmode == DrawMode.Mesh)
        {
            Color[] colourMap = ColourMapGenerator.GenerateColourMapRegions(noiseMap, mapWidth, mapHeight, regions);
            MeshData meshData = MeshGenerator.GenerateHeightTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve);
            Texture2D texture = TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight);
            display.DrawMesh(meshData, texture);
        }
        else if (drawmode == DrawMode.TileMap)
        {
            Color[] colourMap = ColourMapGenerator.GenerateColourMapRegions(noiseMap, mapWidth, mapHeight, regions);
            display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight));


            World world = new World(mapWidth, mapHeight);
            TileMapGenerator.GenerateTileMap(world, noiseMap, mapWidth, mapHeight, tileTypes);
            Texture2D texture = TextureGenerator.TextureFromTileMap(world.tiles, terrainTiles, mapWidth, mapHeight, tileResolution);
            MeshData meshData = MeshGenerator.GenerateHeightTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve);
            display.DrawMesh(meshData, texture);
        }
    }

    

    private void OnValidate()
    {
        if(mapWidth < 1) mapWidth = 1;
        if (mapHeight < 1) mapHeight = 1;
        if (lacunarity < 1) lacunarity = 1;
        if (octaves < 1) octaves = 1;

    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color colour;
}

[System.Serializable]
public struct TerrainTile
{
    public Tile.TileType type;
    public string name;
    public float height;
}
