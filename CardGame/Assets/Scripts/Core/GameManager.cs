using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingleton<GameManager>
{
    public string sceneName;
    public bool isFirst = false;

    private void Awake()
    {
        Managers.Data.GetResources();
        Managers.Data.DataIntoDictionary();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string name = scene.name;
        sceneName = name;
        Debug.Log("ÇöÀç ¾À ÀÌ¸§: " + name);
        if (sceneName == "Battle Scene")
        {
            PlayerData.Instance.DataSet();
            Managers.Stage.BattleStage();
        }
        else if (sceneName == "Store Scene")
        {

        }
    }
    public void MoveScene(string sceneName) //¾À ÀÌµ¿(¿øÇÏ´Â ¾À ÀÌ¸§)
    {
        if(sceneName == "first")
        {
            Managers.Stage.SelectLevel();
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
    public void EndClick()
    {
        Application.Quit();
    }
}
