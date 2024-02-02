using System;
using System.Buffers.Binary;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class TerrainLoader : MonoBehaviour
{

    public Terrain terrain;
    const string FILE_PATH = "C:\\Users\\Anwender\\Downloads\\L32\\N47E009.hgt";

    public int width = 256;
    public int height = 256;
    public int heightmapResolution = 2049;
    public int depth = 20;

    [InspectorButton("GenerateTerrain")]
    public bool generate;

    // Start is called before the first frame update
    void Start()
    {
        terrain.terrainData = GenerateTerrain();
    }

    TerrainData GenerateTerrain()
    {
        TerrainData terrainData = terrain.terrainData;
        terrainData.size = new Vector3(width, depth, height);
        terrainData.heightmapResolution =heightmapResolution;
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    private float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        return LoadHeights();
        heights[0, 0] = 1;
        return heights;
    }

    private float[,] LoadHeights()
    {
        byte[] bytes = File.ReadAllBytes(FILE_PATH);
        int hgtResolution = (int)Mathf.Sqrt(bytes.Length / 2.0f);
        float[,] heights = new float[width + 1, height + 1];
        Debug.Log("hgtResolution: " + hgtResolution);
        for(int x = 0; x <= width; x++)
        {
            for (int z = 0; z <= height; z++)
            {
                heights[width - x, z] = BinaryPrimitives.ReadInt16BigEndian(new ReadOnlySpan<byte>(bytes, (x * hgtResolution + z) * 2,  2)) / 9000f;
                //heights[x, z] = BitConverter.ToInt16(bytes, (x * hgtResolution + z) * 2);
            }
        }
        return heights;
    }
}
