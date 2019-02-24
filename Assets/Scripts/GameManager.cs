using Andtech;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum State { Uninitialized, Active, Inactive };

public class GameManager : Singleton<GameManager>
{
    public TerrainGenerator terrainTemplate;

    public Player player;
    public SeagullSpawner seagullSpawner;
    public BreadSpawner breadSpawner;
    public EnvironmentSpawner environmentSpawner;
    public NestSpawner nestSpawner;

    [SerializeField] private int mapSize = 5;
    [SerializeField] private int mapDensity = 5;
    [SerializeField] private int mapScale = 5;
    [SerializeField] private int environmentTotal = 5;

    [SerializeField] private int nestTotal = 3;
    [SerializeField] private VectorRange seagullSpeedRange;
    [SerializeField] private VectorRange seagullCooldownRange;
    [SerializeField] private VectorRange seagullFeedingSpeedRange;

    [SerializeField] private int breadTotal = 5;
    [SerializeField] private int lifePoints = 5;

    private List<IDespawnable> despawnableObjects;
    private TerrainGenerator terrain;
    private State gameState = State.Uninitialized;

    private State GameState
    {
        get => gameState;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Initialize();
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            StartGame();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            EndGame();
        }
    }

    void Initialize()
    {
        if(gameState == State.Uninitialized)
        {
            gameState = State.Inactive;

            //Create a list of all objects we want to despawn when game ends.
            despawnableObjects = new List<IDespawnable>();
            DontDestroyOnLoad(this);

            //Create the player prefab.
            player = Instantiate(player) as Player;
            DontDestroyOnLoad(player);

            //Create the breadSpawner.
            breadSpawner = Instantiate(breadSpawner) as BreadSpawner;
            DontDestroyOnLoad(breadSpawner);
            breadSpawner.Initialize(new VectorRange(1, mapSize - 1), mapScale);

            //Create the seagullSpawner.
            seagullSpawner = Instantiate(seagullSpawner) as SeagullSpawner;
            seagullSpawner.Initialize(seagullSpeedRange, seagullFeedingSpeedRange);
            DontDestroyOnLoad(seagullSpawner);

            //Create the environmentSpawner.
            environmentSpawner = Instantiate(environmentSpawner) as EnvironmentSpawner;
            environmentSpawner.Initialize(mapSize, mapScale);
            DontDestroyOnLoad(environmentSpawner);

            //Create the nestSpawner.
            nestSpawner = Instantiate(nestSpawner) as NestSpawner;
            nestSpawner.Initialize(mapSize, mapScale);
            DontDestroyOnLoad(nestSpawner);

            //Start the game.
            StartGame();
        }
        else
        {
            Debug.LogError($"Can't initialize because gamestate is: {GameState}");
        }
    }


    void StartGame()
    {
        if (gameState == State.Inactive)
        {
            gameState = State.Active;
            
            terrain = Instantiate(terrainTemplate) as TerrainGenerator;
            PerlinNoise heightMap = terrain.Initialize(mapSize, mapDensity, mapScale);

            Vector3 randomLocation = new Vector3(Random.Range(mapSize * .10f, mapSize * .90f), Random.Range(mapScale, mapScale * 2f), Random.Range(mapSize * .1f, mapSize * .9f));
            player.Spawn(randomLocation);

            breadSpawner.RefreshMap(heightMap);
            SpawnBread();

            environmentSpawner.RefreshMap(heightMap);
            SpawnEnvironment();

            SpawnNests();

            StartCoroutine(SpawnSeagulls());
        }
        else
        {
            Debug.LogError($"Can't start game because gamestate is: {GameState}");
        }
    }

    void EndGame()
    {
        if(gameState == State.Active)
        {
            StopAllCoroutines();
            Disable(player);

            DisableAll(despawnableObjects);

            Destroy(terrain);
            gameState = State.Inactive;
        }
        else
        {
            Debug.LogError($"Can't end game because gamestate is: {GameState}");
        }
    }

    void DisableAll(List<IDespawnable> list)
    {
        foreach (IDespawnable item in list)
        {
            Disable(item);
        }
    }

    void Disable(IDespawnable item)
    {
        item.Despawn();
    }

    void SpawnEnvironment()
    {
        for (int i = 0; i < environmentTotal; i++)
        {
            despawnableObjects.Add(environmentSpawner.Spawn());
        }
    }
    void SpawnBread()
    {
        for (int i = 0; i < breadTotal; i++)
        {
            despawnableObjects.Add(breadSpawner.Spawn());
        }
    }

    void SpawnNests()
    {
        for(int i = 0; i < nestTotal; i++)
        {
            despawnableObjects.Add(nestSpawner.Spawn());
        }
    }


    IEnumerator SpawnSeagulls()
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
            despawnableObjects.Add(gull);
            elapsedTime -= maxTime;
        }

    }

    public void DecreaseLifePoints()
    {
        lifePoints--;
    }
}
