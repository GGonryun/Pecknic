using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private int xSize = 5;
    [SerializeField] private int zSize = 5;
    [SerializeField] private int density = 5;
    [SerializeField] private int scale = 5;
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

    void Initialize()
    {
        PerlinNoise heightMap = new PerlinNoise(xSize, zSize, density);
        PerlinNoise environmentMap = new PerlinNoise(xSize, zSize, 10);
        meshFilter.mesh = MeshGenerator.GenerateMesh(xSize, zSize, scale, heightMap);
        environment.Initialize(xSize, zSize, scale, heightMap, environmentMap);
        meshRenderer.material = testMat;
        meshCollider = gameObject.AddComponent<MeshCollider>();
    }


}
