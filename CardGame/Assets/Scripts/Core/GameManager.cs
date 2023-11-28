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
        if(sceneName != "Battle Scene")
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
