using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
   
    private EnvironmentGenerator environment;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;
    private PerlinNoise noise;

    public Material testMat;

    void Awake()
    {
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        environment = gameObject.GetComponent<EnvironmentGenerator>();
    }

    public void Initialize(int xSize, int zSize, int density, int scale)
    {
        PerlinNoise heightMap = new PerlinNoise(xSize, zSize, density);
        PerlinNoise environmentMap = new PerlinNoise(xSize, zSize, 10);
        meshFilter.mesh = MeshGenerator.GenerateMesh(xSize, zSize, scale, heightMap);
        environment.Initialize(xSize, zSize, scale, heightMap, environmentMap);
        meshRenderer.material = testMat;
        meshCollider = gameObject.AddComponent<MeshCollider>();
    }


}
