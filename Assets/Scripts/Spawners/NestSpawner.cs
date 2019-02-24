using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestSpawner : Spawner<Nest>
{
    [SerializeField] public NestFactory factory;
    private int mapSize;
    private int mapScale;

    public void Initialize(int mapSize, int mapScale)
    {
        factory.Initialize();
        this.mapSize = mapSize;
        this.mapScale = mapScale;
    }

    public override void Despawn(Nest obj)
    {
        factory.Recycle(obj);
    }

    public override Nest Spawn()
    {
        Nest nest = factory.Get();
        float x = Random.Range(0f, mapSize);
        float y = Random.Range(mapScale*5f, mapScale*10f);
        float z = Random.Range(0f, mapSize);

        Vector3 newPosition = new Vector3(x, y, z);

        nest.Spawn(this, newPosition);
        
        return nest;
    }

   
}
