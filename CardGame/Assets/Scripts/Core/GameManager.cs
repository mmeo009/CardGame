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
    public InventoryManager inv;

    private void Awake()
    {
        Managers.Data.GetResources();
        Managers.Data.DataIntoDictionary();
        monsterData = GetComponent<MonsterData>();
        turn = GetComponent<TurnManager>();
        draw = GetComponent<DrawCard>();
        drag = GetComponent<DragManager>();
        inv = GetComponent<InventoryManager>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            MoveScene("Inventory Scene");
        }
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
        Debug.Log("���� �� �̸�: " + name);
        if(sceneName != "Battle Scene" || sceneName != "Store Scene" || sceneName != "Demon Scene" || sceneName != "Inventory Scene")
        {
            TurnManager.Instance.TurnEnd();
            turn.enabled = false;
            draw.enabled = false;
            drag.enabled = false;
        }
        if (sceneName == "Battle Scene")
        {
            inv.enabled = false;
            PlayerData.Instance.DataSet();
            Managers.Stage.BattleStage();
            turn.enabled = true;
            draw.enabled = true;
            drag.enabled = true;
            TurnManager.Instance.TurnStart();
        }
        else if (sceneName == "Store Scene")
        {
            inv.enabled = false;
            TurnManager.Instance.TurnEnd();
            PlayerData.Instance.DataSet();
            drag.enabled = true;
        }
        else if (sceneName == "Demon Scene")
        {
            inv.enabled = false;
            TurnManager.Instance.TurnEnd();
            PlayerData.Instance.DataSet();
            drag.enabled = true;
        }
        else if(sceneName == "Forge Scene")
        {
            inv.enabled = false;
            PlayerData.Instance.DataSet();
        }
        else if(sceneName == "Inventory Scene")
        {
            inv.enabled = true;
            InventoryManager.Instance.LoadMyDeck();
            InventoryManager.Instance.FindInvCards();
            InventoryManager.Instance.LoadByMaxNum();
        }

    }
    public void MoveScene(string nextSceneName) //�� �̵�(���ϴ� �� �̸�)
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
