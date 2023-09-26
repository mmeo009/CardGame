using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCardAdd : MonoBehaviour
{

    private void Start()
    {
        this.gameObject.GetComponent<DrawCard>().TransformChack();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            // 카드를 덱에 추가
            Managers.Deck.AddCardToDeckById("101001A", 2);
            Managers.Deck.AddCardToDeckById("101002A", 3);
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            // 덱에서 카드를 제거
            Managers.Deck.RemoveCardToDeckById("101001A", 1);
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
            this.gameObject.GetComponent<DrawCard>().CreateCard();
        }
    }
}
