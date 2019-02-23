using Andtech;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public TerrainGenerator terrain;
    [SerializeField] private int xSize = 5;
    [SerializeField] private int zSize = 5;
    [SerializeField] private int density = 5;
    [SerializeField] private int scale = 5;
    public Player player;
    public SeagullSpawner spawner;
    [Range(10, 100)] [SerializeField] private float seagullSpeed;
    [Range(0, 0.49f)] [SerializeField] private float seagullSpeedRange;
    [Range(0.01f, 5f)] [SerializeField] private float seagullCooldown;
    [Range(1f, 5f)] [SerializeField] private float seagullFeedingSpeed;

    [SerializeField] private short lifePoints = 5;

    void Start()
    {
        terrain = Instantiate(terrain) as TerrainGenerator;
        terrain.Initialize(xSize, zSize, density, scale);

        player = Instantiate(player) as Player;
        Vector3 randomLocation = new Vector3(Random.Range(xSize * .10f, xSize * .90f), Random.Range(scale, scale * 2f), Random.Range(zSize * .1f, zSize * .9f));
        player.Spawn(randomLocation);
        StartCoroutine(SpawnSeagulls());

    }


    private IEnumerator SpawnSeagulls()
    {
        float elapsedTime = 0f;
        while (true)
        {
            while (elapsedTime <= seagullCooldown)
            {
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            Seagull gull = SeagullSpawner.Current.Spawn();
            float r = seagullSpeed * seagullSpeedRange;
            float s = Random.Range(r, seagullSpeed - r);

            Vector3 p = new Vector3(Random.Range(xSize * .10f, xSize * .90f), Random.Range(scale * 2f, scale * 5f), Random.Range(zSize * .1f, zSize * .9f));

            float c = Random.Range(seagullFeedingSpeed * 0.5f, seagullFeedingSpeed * 2f);
            gull.Spawn(s, p, c);

            gull.gameObject.SetActive(true);
            elapsedTime -= seagullCooldown;
        }

    }

    public void DecreaseLifePoints()
    {
        lifePoints--;
    }
}
