using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuNames { Main, Playing, GameOver}

public class UserInterfaceManager : MonoBehaviour
{

    [SerializeField] HealthBarMenu healthBar;
    [SerializeField] GameOverMenu gameOver;
    [SerializeField] MainMenu mainMenu;

    public void Initialize()
    {
        healthBar.Initialize();
    }




    public void GameOver()
    {
        healthBar.EndGame();
        healthBar.Disable();
        mainMenu.Disable();

        gameOver.Enable();
    }

    public void DisplayGame(int i)
    {
        gameOver.Disable();
        mainMenu.Disable();

        healthBar.Enable();
        healthBar.StartGame(i);
    }

    public void DisplayMenu()
    {
        healthBar.Disable();
        gameOver.Disable();

        mainMenu.Enable();
    }
}
