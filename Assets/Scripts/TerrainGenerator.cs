using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
   
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;
    private PerlinNoise noise;

    public Material testMat;

    void Awake()
    {
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
    }

    public PerlinNoise Initialize(int size, int density, int scale)
    {
        PerlinNoise heightMap = new PerlinNoise(size, size, density);
        meshFilter.mesh = MeshGenerator.GenerateMesh(size, size, scale, heightMap);
        meshRenderer.material = testMat;
        meshCollider = gameObject.AddComponent<MeshCollider>();

        return heightMap;
    }


}
