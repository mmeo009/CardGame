using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCardAdd : MonoBehaviour
{

    private void Awake()
    {
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            Managers.Stage.SelectLevel();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            //Managers.Deck.AddCardToDeckById("104028A", 4);      //ȯ�� "104027A" ȭ��(2��)
            MonsterData.Instance.PickPattern();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            //Managers.Deck.AddCardToDeckById("104028A", 4);      //ȯ�� "104027A" ȭ��(2��)
            //MonsterData.Instance.PickPattern("401013A");
            PlayerData.Instance.GainingOrLosingValue("currentHealth", 1);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            //Managers.Deck.AddCardToDeckById("104028A", 4);      //ȯ�� "104027A" ȭ��(2��)
            PlayerData.Instance.GainingOrLosingValue("currentHealth", -1);
        }


    }
}
