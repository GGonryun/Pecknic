using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadSpawner : Spawner<Bread>
{
    public BreadFactory factory;
    private VectorRange mapSize;
    private float mapScale;
    private PerlinNoise heightMap;

    public void Initialize(VectorRange mapSize, float mapScale, PerlinNoise heightMap)
    {
        this.mapSize = mapSize;
        this.mapScale = mapScale;
        this.heightMap = heightMap;
    }

    public override void Despawn(Bread bread)
    {
        factory.Recycle(bread);
    }

    public override Bread Spawn()
    {
        Bread bread = factory.Get();

        int x = (int) Mathf.Floor(Random.Range(mapSize.min, mapSize.max));
        int z = (int) Mathf.Floor(Random.Range(mapSize.min, mapSize.max));
        float y = mapScale * heightMap[x,z];

        bread.transform.localPosition = new Vector3(x, y, z);

        return bread;
    }
}
