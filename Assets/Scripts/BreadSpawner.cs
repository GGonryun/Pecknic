using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadSpawner : Spawner<Bread>
{
    public BreadFactory factory;
    private VectorRange mapSize;
    private float mapScale;
    private PerlinNoise heightMap;

    public void Initialize(VectorRange mapSize, float mapScale)
    {
        this.mapSize = mapSize;
        this.mapScale = mapScale;
    }

    public void RefreshMap(PerlinNoise heightMap)
    {
        this.heightMap = heightMap;
    }

    public override void Despawn(Bread bread)
    {
        factory.Recycle(bread);
    }

    public override Bread Spawn()
    {
        if(heightMap == null)
        {
            throw new System.Exception("BreadSpawner has no heightmap set!");
        }
        Bread bread = factory.Get();
        int x = (int) Mathf.Floor(Random.Range(mapSize.min, mapSize.max));
        int z = (int) Mathf.Floor(Random.Range(mapSize.min, mapSize.max));
        float y = mapScale * heightMap[x,z];
        Vector3 pos = new Vector3(x, y, z);
        bread.Spawn(this, pos);
        return bread;
    }
}
