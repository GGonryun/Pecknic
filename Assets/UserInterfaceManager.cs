using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuNames { Main, Playing, GameOver}

public class UserInterfaceManager : MonoBehaviour
{

    [SerializeField] GameBarMenu gameBar;
    [SerializeField] GameOverMenu gameOver;
    [SerializeField] MainMenu mainMenu;

    public void Initialize()
    {
        gameBar.Initialize();
    }




    public void GameOver()
    {
        gameBar.EndGame();
        gameBar.Disable();
        mainMenu.Disable();

        gameOver.Enable();
    }

    public void DisplayGame(int i)
    {
        gameOver.Disable();
        mainMenu.Disable();

        gameBar.Enable();
        gameBar.StartGame(i);
    }

    public void DisplayMenu()
    {
        gameBar.Disable();
        gameOver.Disable();

        mainMenu.Enable();
    }
}
