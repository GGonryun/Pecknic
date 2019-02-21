using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise
{
    private float[] noise;
    private int xSize, zSize;

    public PerlinNoise(int xSize, int zSize, int density)
    {
        noise = new float[(xSize + 1) * (zSize + 1)];
        this.xSize = xSize;
        this.zSize = zSize;

        for (int z = 0, i = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                float xCoord = (float)x / xSize * density;
                float zCoord = (float)z / zSize * density;
                noise[i] = Mathf.PerlinNoise(xCoord, zCoord);
                Debug.Log($"({xCoord},{zCoord}): {noise[i]}.");
            }
        }
    }

    public float this[int i, int j]
    {
        get
        {
            if( i > xSize)
            {
                return -1;
            }
            if( j > zSize)
            {
                return -1;
            }

            int row = (xSize + 1) * j;
            return noise[i + row];
        }
    }
}

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private int xSize = 5;
    [SerializeField] private int zSize = 5;
    [SerializeField] private int density = 5;
    [SerializeField] private int scale = 5;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private PerlinNoise noise;
    public Material testMat;

    void Awake()
    {
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
    }

    void Start()
    {
        meshFilter.mesh = MeshGenerator.GenerateMesh(xSize, zSize, scale, new PerlinNoise(xSize, zSize, density));
        meshRenderer.material = testMat;
    }
}
