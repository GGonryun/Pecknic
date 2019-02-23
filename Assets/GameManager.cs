using Andtech;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public TerrainGenerator terrainTemplate;

    public TerrainGenerator terrain;

    public Player player;
    public SeagullSpawner seagullSpawner;
    public BreadSpawner breadSpawner;
    [SerializeField] private int mapSize = 5;
    [SerializeField] private int mapDensity = 5;
    [SerializeField] private int mapScale = 5;
    [SerializeField] private int breadTotal = 5;
    [SerializeField] private VectorRange seagullSpeedRange;
    [SerializeField] private VectorRange seagullCooldownRange;
    [SerializeField] private VectorRange seagullFeedingSpeedRange;
    [SerializeField] private short lifePoints = 5;

    private List<IDespawnable> seagulls;
    private List<IDespawnable> breads;



    void Initialize()
    {
        breads = new List<IDespawnable>();
        seagulls = new List<IDespawnable>();

        breadSpawner = Instantiate(breadSpawner) as BreadSpawner;

        StartGame();
    }


    void StartGame()
    {
        terrain = Instantiate(terrainTemplate) as TerrainGenerator;
        PerlinNoise heightMap = terrain.Initialize(mapSize, mapDensity, mapScale);

        player = Instantiate(player) as Player;
        Vector3 randomLocation = new Vector3(Random.Range(mapSize * .10f, mapSize * .90f), Random.Range(mapScale, mapScale * 2f), Random.Range(mapSize * .1f, mapSize * .9f));
        player.Spawn(randomLocation);

        
        breadSpawner.Initialize(new VectorRange(1, mapSize - 1), mapScale, heightMap);
        SpawnBread();

        seagullSpawner = Instantiate(seagullSpawner) as SeagullSpawner;
        seagullSpawner.Initialize(seagullSpeedRange, seagullFeedingSpeedRange);
        StartCoroutine(SpawnSeagulls());
    }

    void EndGame()
    {
        StopAllCoroutines();
        DisableAll(breads);
        DisableAll(seagulls);
        Destroy(terrainTemplate.gameObject);
        Destroy(player.gameObject);
    }

    private void DisableAll(List<IDespawnable> list)
    {
        foreach(IDespawnable item in list)
        {
            item.Despawn();
        }
    }

    private void SpawnBread()
    {
        for(int i = 0; i < breadTotal; i++)
        {
            breads.Add(breadSpawner.Spawn());
        }
    }

    private IEnumerator SpawnSeagulls()
    {
        float elapsedTime = 0f;
        while (true)
        {
            float maxTime = Random.Range(seagullCooldownRange.min, seagullCooldownRange.max);
            while (elapsedTime <= maxTime)
            {
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }


            Vector3 newPosition = new Vector3(Random.Range(0, mapSize), Random.Range(mapScale * 2f, mapScale * 5f), Random.Range(0, mapSize));

            Seagull gull = seagullSpawner.Spawn();
            gull.transform.position = newPosition;
            seagulls.Add(gull);
            elapsedTime -= maxTime;
        }

    }

    public void DecreaseLifePoints()
    {
        lifePoints--;
    }
}
