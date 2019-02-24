using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameBarMenu : MonoBehaviour, IUserInterfaceable
{
    [SerializeField] private GameObject healthTemplate;
    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private Transform healthBarObject;

    private int score = 0;
    private List<GameObject> healthPoints;

    public void Initialize()
    {
        healthPoints = new List<GameObject>();
    }

    public void StartGame(int health)
    {
        GameManager.Current.LifeLost += LoseHealth;
        GameManager.Current.ScoreIncreased += IncreasedScore;
        score = 0;
        for (int i = 0; i < health; i++)
        {
            healthPoints.Add(Instantiate(healthTemplate, healthBarObject) as GameObject);
        }
    }

    public void IncreasedScore(object sender, System.EventArgs e)
    {
        scoreDisplay.text = (++score).ToString();
    }

    public void LoseHealth(object sender, System.EventArgs e)
    {
        int lastIndex = healthPoints.Count - 1;
        GameObject healthPoint = healthPoints[lastIndex];
        healthPoints.RemoveAt(lastIndex);

        Destroy(healthPoint);
    }

    public void EndGame()
    {
        GameManager.Current.LifeLost -= LoseHealth;
        GameManager.Current.ScoreIncreased -= IncreasedScore;
        foreach (GameObject obj in healthPoints)
        {
            Destroy(obj);
        }
        healthPoints.Clear();
    }

    public void Enable()
    {
        this.gameObject.SetActive(true);

    }

    public void Disable()
    {
        this.gameObject.SetActive(false);
    }
}
