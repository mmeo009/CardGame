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
            Managers.Deck.AddCardToDeckById("101001A", 4);
            Managers.Deck.AddCardToDeckById("101002A", 4);
            Managers.Deck.AddCardToDeckById("101003A", 4);
            Managers.Deck.AddCardToDeckById("101004A", 4);
            Managers.Deck.AddCardToDeckById("101005A", 4);
            Managers.Deck.AddCardToDeckById("101006A", 4);
            Managers.Deck.AddCardToDeckById("101008A", 4);
            Managers.Deck.AddCardToDeckById("101009A", 4);
            Managers.Deck.AddCardToDeckById("101011J", 4);
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            int num = Random.Range(1, 5);
            // 덱에서 카드를 제거
            Managers.Deck.RemoveCardToDeckById($"10100{num}A", 1);
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
            DrawCard.Instance.Invoke("CardInToDeck", 0.5f);
        }
    }
}
