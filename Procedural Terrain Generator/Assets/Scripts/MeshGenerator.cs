using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator 
{
    public static MeshData GenerateFlatTerrainMesh(float[,] heightMap)
    {
        int width = heightMap.GetLength(0)+1;
        int height = heightMap.GetLength(1)+1;
        float topLeftX = (width - 1) / 2f;
        float topLeftZ = (height - 1) / 2f;


        MeshData meshData = new MeshData(width, height);
        int vertexIndex = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, 0, topLeftZ - y);
                meshData.uvs[vertexIndex] = new Vector2((x+0) / (float)width, (y+0) / (float)height);

                if (x < width - 1 && y < height - 1)
                {
                    meshData.AddTriangle(vertexIndex, vertexIndex + width + 1, vertexIndex + width);
                    meshData.AddTriangle(vertexIndex + width + 1, vertexIndex, vertexIndex + 1);
                }

                vertexIndex++;
            }
        }

        return meshData;
    }

    public static MeshData GenerateHeightTerrainMesh(float[,] heightMap, float heightMultiplier, AnimationCurve heightCurve)
    {
        int width = heightMap.GetLength(0)+1;
        int height = heightMap.GetLength(1)+1;
        float topLeftX = (width - 1) / 2f;
        float topLeftZ = (height - 1) / 2f;

        MeshData meshData = GenerateFlatTerrainMesh(heightMap);

        int vertexIndex = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (x < width - 1 && y < height - 1)
                {
                    meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heightCurve.Evaluate(heightMap[x, y]) * heightMultiplier, topLeftZ - y);
                }
                else if(x == width - 1 && y < height - 1)
                {
                    meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heightCurve.Evaluate(heightMap[x-1, y]) * heightMultiplier, topLeftZ - y);
                }
                else if (x < width - 1 && y == height - 1)
                {
                    meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heightCurve.Evaluate(heightMap[x , y - 1]) * heightMultiplier, topLeftZ - y);
                }
                else
                {
                    meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heightCurve.Evaluate(heightMap[x - 1, y - 1]) * heightMultiplier, topLeftZ - y);
                }
                vertexIndex++;

            }
        }

        return meshData;
    }
}

public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;

    int triangleIndex;

    public MeshData(int meshWidth, int meshHeight)
    {
        vertices = new Vector3[(meshWidth+ 1) * (meshHeight+ 1)];
        uvs = new Vector2[(meshWidth + 1) * (meshHeight + 1)];
        triangles = new int[(meshWidth - 0) * (meshHeight - 0) * 6];
    }

    public void AddTriangle(int a, int b, int c)
    {
        triangles[triangleIndex + 0] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;
        triangleIndex += 3;
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        return mesh;
    }
}
