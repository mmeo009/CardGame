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
            TurnManager.Instance.TurnEnd();
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
            TurnManager.Instance.TurnStart();
        }
        else if (sceneName == "Store Scene")
        {
            TurnManager.Instance.TurnEnd();
            PlayerData.Instance.DataSet();
            drag.enabled = true;
        }
        else if (sceneName == "Demon Scene")
        {
            TurnManager.Instance.TurnEnd();
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
            for (int i = 1; i < 11; i++)
            {
                string num = i.ToString("000");
                Managers.Deck.AddCardIntoDefaultDeck($"101{num}A", 4);
            }
            for (int i = 12; i < 22; i++)
            {
                string num = i.ToString("000");
                Managers.Deck.AddCardIntoDefaultDeck($"102{num}A", 4);
            }
            for (int i = 23; i < 27; i++)
            {
                string num = i.ToString("000");
                Managers.Deck.AddCardIntoDefaultDeck($"103{num}A", 4);
            }
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
