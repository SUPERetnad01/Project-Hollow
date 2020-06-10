using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the game on a higher level
/// </summary>
[AddComponentMenu("Mythirial/Game Manager")]
public class GameManager : Singleton<GameManager>
{
    public GameObject LoadingScreen;

    private GameManager() { }
    private void Awake()
    {
        SceneManager.LoadSceneAsync((int) SceneIndexes.TEST_DESERT, LoadSceneMode.Additive);
    }


    public void LoadGame()
    {
        LoadingScreen.gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync((int) SceneIndexes.TITLE_SCREEN);
        SceneManager.LoadSceneAsync((int) SceneIndexes.TEST_DESERT, LoadSceneMode.Additive);
    }
}
