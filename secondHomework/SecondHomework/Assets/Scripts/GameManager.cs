using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SceneAsset gameScene;
    public SceneAsset mainMenuScene;
    public void LoadGame()
    {
        SceneManager.LoadScene(gameScene.name);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene.name);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
