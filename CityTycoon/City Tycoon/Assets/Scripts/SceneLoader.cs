using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string gameScene;
    public string mainMenuScene;
    public string winningScene;

    private void Start()
    {
        GameOver.current.EndGameAction += LoadWinningScreen;
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void LoadWinningScreen()
    {
        SceneManager.LoadScene(winningScene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
