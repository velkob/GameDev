using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public SceneAsset gameScene;
    public SceneAsset mainMenuScene;
    public SceneAsset winningScene;

    private void Start()
    {
        GameOver.current.EndGameAction += LoadWinningScreen;
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(gameScene.name);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene.name);
    }

    public void LoadWinningScreen()
    {
        SceneManager.LoadScene(winningScene.name);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
