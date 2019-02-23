using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour, IDespawnable
{
   
    private EnvironmentGenerator environment;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;
    private PerlinNoise noise;
    private List<GameObject> environmentObjects;


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
        PerlinNoise environmentMap = new PerlinNoise(size, size, Random.Range(0,20));
        meshFilter.mesh = MeshGenerator.GenerateMesh(size, size, scale, heightMap);
        environmentObjects = environment.Initialize(size, size, scale, heightMap, environmentMap);
        meshRenderer.material = testMat;
        meshCollider = gameObject.AddComponent<MeshCollider>();

        return heightMap;
    }

    void IDespawnable.Despawn()
    {
        foreach(GameObject obj in environmentObjects)
        {
            Destroy(obj);
        }
    }
}
