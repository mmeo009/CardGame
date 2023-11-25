using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
        string sceneName = scene.name;
        Debug.Log("ÇöÀç ¾À ÀÌ¸§: " + sceneName);

        if (sceneName == "Battle Scene")
        {
            PlayerData.Instance.DataSet();
        }
        else if (sceneName == "Store Scene")
        {

        }
    }
    public void MoveScene(string SceneName) //¾À ÀÌµ¿(¿øÇÏ´Â ¾À ÀÌ¸§)
    {
        SceneManager.LoadScene(SceneName);
    }
}
