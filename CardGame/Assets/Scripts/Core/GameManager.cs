using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingleton<GameManager>
{
    public string sceneName;
    public bool isFirst = false;
    public DrawCard draw;
    public TurnManager turn;
    public MonsterData monsterData;
    public DragManager drag;

    private void Awake()
    {
        Managers.Data.GetResources();
        Managers.Data.DataIntoDictionary();
        monsterData = GetComponent<MonsterData>();
        turn = GetComponent<TurnManager>();
        draw = GetComponent<DrawCard>();
        drag = GetComponent<DragManager>();
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
        if(sceneName != "Battle Scene" || sceneName != "Store Scene" || sceneName != "Demon Scene")
        {
            turn.enabled = false;
            draw.enabled = false;
            drag.enabled = false;
        }
        if (sceneName == "Battle Scene")
        {
            PlayerData.Instance.DataSet();
            Managers.Stage.BattleStage();
            turn.enabled = true;
            draw.enabled = true;
            drag.enabled = true;
        }
        else if (sceneName == "Store Scene")
        {
            PlayerData.Instance.DataSet();
            drag.enabled = true;
        }
        else if (sceneName == "Demon Scene")
        {
            PlayerData.Instance.DataSet();
            drag.enabled = true;
        }
        else if(sceneName == "Forge Scene")
        {
            PlayerData.Instance.DataSet();
        }

    }
    public void MoveScene(string nextSceneName) //¾À ÀÌµ¿(¿øÇÏ´Â ¾À ÀÌ¸§)
    {
        if (nextSceneName == "first")
        {
            Managers.Stage.SelectLevel();
        }
        else
        {
            if (sceneName != nextSceneName)
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
    public void EndClick()
    {
        Application.Quit();
    }
}
