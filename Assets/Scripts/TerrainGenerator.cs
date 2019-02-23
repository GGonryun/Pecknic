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

    public PerlinNoise Initialize(int size, int density, int scale)
    {
        PerlinNoise heightMap = new PerlinNoise(size, size, density);
        PerlinNoise environmentMap = new PerlinNoise(size, size, 10);
        meshFilter.mesh = MeshGenerator.GenerateMesh(size, size, scale, heightMap);
        environment.Initialize(size, size, scale, heightMap, environmentMap);
        meshRenderer.material = testMat;
        meshCollider = gameObject.AddComponent<MeshCollider>();

        return heightMap;
    }


}
