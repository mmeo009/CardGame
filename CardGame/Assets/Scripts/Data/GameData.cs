using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    static GameData s_instance;
    static GameData Instance { get { Init(); return s_instance; } }
    public static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Datas");
            if (go == null)
            {
                go = new GameObject { name = "@Datas" };
                go.AddComponent<GameData>();
            }

            DontDestroyOnLoad(go);  //Scene 이 종료되도 파괴 되지 않게 
            s_instance = go.GetComponent<GameData>();
        }
    }

    CardData _cardD = new CardData();
    public static CardData CardD { get { return Instance?._cardD; } }
}
