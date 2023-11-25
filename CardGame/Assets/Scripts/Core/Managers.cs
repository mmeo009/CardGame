using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }
    public static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);  //Scene 이 종료되도 파괴 되지 않게 
            s_instance = go.GetComponent<Managers>();
        }
    }

    DeckManager _deck = new DeckManager();
    DataManager _data = new DataManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    StageManager _stage = new StageManager();
    GameManager _game = new GameManager();

    public static DeckManager Deck { get { return Instance?._deck; } }
    public static DataManager Data { get { return Instance?._data;  } }
    public static PoolManager Pool { get { return Instance?._pool; } }
    public static ResourceManager Resource { get { return Instance?._resource; } }
    public static StageManager Stage { get { return Instance?._stage; } }
    public static GameManager Game { get { return Instance?._game; } }

}
