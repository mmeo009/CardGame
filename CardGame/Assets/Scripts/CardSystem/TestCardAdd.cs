using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCardAdd : MonoBehaviour
{

    private void Start()
    {
        DrawCard.Instance.GetComponent<DrawCard>().TransformChack();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            // 카드를 덱에 추가
            Managers.Deck.AddCardToDeckById("101001B", 2);
            Managers.Deck.AddCardToDeckById("101002B", 3);
            Managers.Deck.AddCardToDeckById("101003B", 5);
            Managers.Deck.AddCardToDeckById("101004B", 6);
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            int num = Random.Range(1, 5);
            // 덱에서 카드를 제거
            Managers.Deck.RemoveCardToDeckById($"10100{num}C", 1);
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            // 덱에 있는 카드 정보 출력
            Managers.Deck.PrintDeck();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Managers.Deck.AddCardToDeckById("123321");
            Managers.Deck.RemoveCardToDeckById("123321");
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
        }
    }
}
