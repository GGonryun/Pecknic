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
    public System.EventHandler LifeLost;
    public UserInterfaceManager UIManager;
    public GameObject keyboardManager;

    [SerializeField] private int mapSize = 5;
    [SerializeField] private int mapDensity = 5;
    [SerializeField] private int mapScale = 5;
    [SerializeField] private int environmentTotal = 5;

    [SerializeField] private int nestTotal = 3;
    [SerializeField] private VectorRange seagullSpeedRange;
    [SerializeField] private VectorRange seagullCooldownRange;
    [SerializeField] private VectorRange seagullFeedingSpeedRange;

    [SerializeField] private int breadTotal = 5;
    [SerializeField] private int startingLifePoints = 5;

    private int currentLifePoints = 0;

    private List<IDespawnable> despawnableObjects;
    private TerrainGenerator terrain;
    private State gameState = State.Uninitialized;

    private State GameState
    {
        get => gameState;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            DecreaseLifePoints();
        }
        
    }

    void Start()
    {
        if (gameState == State.Uninitialized)
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

            //Ceate the UI.
            UIManager = Instantiate(UIManager) as UserInterfaceManager;
            UIManager.Initialize();
            DontDestroyOnLoad(UIManager);

            //Create the input manager.
            DontDestroyOnLoad(keyboardManager);
        }
        else
        {
            Debug.LogError($"Can't initialize because gamestate is: {GameState}");
        }
    }

    public void StartGame()
    {
        if (gameState == State.Inactive)
        {
            gameState = State.Active;

            keyboardManager.SetActive(false);

            terrain = Instantiate(terrainTemplate) as TerrainGenerator;
            PerlinNoise heightMap = terrain.Initialize(mapSize, mapDensity, mapScale);


            breadSpawner.RefreshMap(heightMap);
            SpawnBread();

            environmentSpawner.RefreshMap(heightMap);
            SpawnEnvironment();

            SpawnNests();

            UIManager.DisplayGame(startingLifePoints);
            currentLifePoints = startingLifePoints;

            StartCoroutine(SpawnSeagulls());

            float x = Random.Range(mapSize * .10f, mapSize * .90f);
            float z = Random.Range(mapSize * .10f, mapSize * .90f);
            float height = heightMap[(int)Mathf.Floor(x), (int)Mathf.Floor(z)];
            float y = Random.Range(height * mapScale * 1.5f, height * mapScale * 2f);

            Vector3 randomLocation = new Vector3(x, y, z);
            player.Spawn(randomLocation);

        }
        else
        {
            Debug.LogError($"Can't start game because gamestate is: {GameState}");
        }
    }

    public void EndGame()
    {
        if (gameState == State.Active)
        {
            StopAllCoroutines();
            Disable(player);

            DisableAll(despawnableObjects);

            Destroy(terrain);
            gameState = State.Inactive;

            UIManager.GameOver();
            keyboardManager.SetActive(true);
        }
        else
        {
            Debug.LogError($"Can't end game because gamestate is: {GameState}");
        }
    }



    public void ReturnToMenu()
    {
        UIManager.DisplayMenu();
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
            Environment newEnvironmentObject = environmentSpawner.Spawn();
            newEnvironmentObject.gameObject.SetActive(true);
            despawnableObjects.Add(newEnvironmentObject);
        }
    }

    void SpawnBread()
    {
        for (int i = 0; i < breadTotal; i++)
        {
            Bread newBreadObject = breadSpawner.Spawn();
            newBreadObject.gameObject.SetActive(true);
            despawnableObjects.Add(newBreadObject);
        }
    }

    void SpawnNests()
    {
        for (int i = 0; i < nestTotal; i++)
        {
            Nest newNestObject = nestSpawner.Spawn();
            newNestObject.gameObject.SetActive(true);
            despawnableObjects.Add(newNestObject);
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

    void OnLifeLost()
    {
        LifeLost?.Invoke(this, new System.EventArgs());
    }

    public void DecreaseLifePoints()
    {
        currentLifePoints--;
        OnLifeLost();
        if (currentLifePoints <= 0)
        {
            EndGame();
        }
    }
}
