using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCardAdd : MonoBehaviour
{

    private void Awake()
    {
        Managers.Data.GetResources();
        Managers.Data.DataIntoDictionary();
        DrawCard.Instance.TransformChack(); 
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            // 카드를 덱에 추가
            Managers.Deck.AddCardToDeckById("101006A", 20);
            Managers.Deck.AddCardToDeckById("101006N", 2);
            Managers.Deck.AddCardToDeckById("101006I", 2, true, 3);
            Managers.Deck.AddCardToDeckById("101006I", 2, true, 5);
            Managers.Deck.AddCardToDeckById("101006I", 2, true, 4);
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            Managers.Stage.SelectLevel();
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            DrawCard.Instance.CreateCardAllAtOnce();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            DrawCard.Instance.CreateCardOneAtTheTime();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            DrawCard.Instance.MergeGridToCardGrid();
            DrawCard.Instance.Invoke("CardInToDeck", 0.5f);
        }
    }
}
