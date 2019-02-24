using UnityEngine;

public class EnvironmentSpawner : Spawner<Environment>
{
    [SerializeField] private EnvironmentFactory factory;
    private int mapSize;
    private int scale;
    private PerlinNoise heightMap;

    public void Initialize(int mapSize, int scale)
    {
        factory.Initialize();
        this.mapSize = mapSize;
        this.scale = scale;
    }

    public void RefreshMap(PerlinNoise heightMap)
    {
        this.heightMap = heightMap;
    }


    public override void Despawn(Environment obj)
    {
        factory.Recycle(obj);
    }

    public override Environment Spawn()
    {
        Environment environment = factory.Get();
        int x = Random.Range(1, mapSize - 1);
        int z = Random.Range(1, mapSize - 1);
        float y = scale * heightMap[x, z];

        Vector3 position = new Vector3(x,y,z);
        environment.Spawn(this, position);

        return environment;
    }
}
