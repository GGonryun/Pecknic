using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthBarMenu : MonoBehaviour, IUserInterfaceable
{
    [SerializeField] private GameObject healthTemplate;
    private List<GameObject> healthPoints;



    public void Initialize()
    {
        healthPoints = new List<GameObject>();
    }

    public void StartGame(int health)
    {
        GameManager.Current.LifeLost += LoseHealth;
        for (int i = 0; i < health; i++)
        {
            healthPoints.Add(Instantiate(healthTemplate, this.transform) as GameObject);
        }
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
