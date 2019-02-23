using Andtech;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public TerrainGenerator terrain;
    public Player player;
    public SeagullSpawner seagullSpawner;
    [SerializeField] private int mapXSize = 5;
    [SerializeField] private int mapZSize = 5;
    [SerializeField] private int mapDensity = 5;
    [SerializeField] private int mapScale = 5;
    [SerializeField] private VectorRange seagullSpeedRange;
    [SerializeField] private VectorRange seagullCooldownRange;
    [SerializeField] private VectorRange seagullFeedingSpeedRange;

    [SerializeField] private short lifePoints = 5;

    void Start()
    {
        terrain = Instantiate(terrain) as TerrainGenerator;
        terrain.Initialize(mapXSize, mapZSize, mapDensity, mapScale);

        player = Instantiate(player) as Player;
        Vector3 randomLocation = new Vector3(Random.Range(mapXSize * .10f, mapXSize * .90f), Random.Range(mapScale, mapScale * 2f), Random.Range(mapZSize * .1f, mapZSize * .9f));
        player.Spawn(randomLocation);

        seagullSpawner = Instantiate(seagullSpawner) as SeagullSpawner;
        seagullSpawner.Initialize(seagullSpeedRange, seagullFeedingSpeedRange);
        StartCoroutine(SpawnSeagulls());

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

            Seagull gull = seagullSpawner.Spawn();

            Vector3 newPosition = new Vector3(Random.Range(0, mapXSize), Random.Range(mapScale * 2f, mapScale * 5f), Random.Range(0, mapXSize));

            gull.transform.position = newPosition;
            elapsedTime -= maxTime;
        }

    }

    public void DecreaseLifePoints()
    {
        lifePoints--;
    }
}
