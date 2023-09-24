using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCardAdd : MonoBehaviour
{
    void Start()
    {
        // 카드를 덱에 추가
        DeckData.Instance.AddCardToDeckById("101001A", 2);
        DeckData.Instance.AddCardToDeckById("101002A", 3);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            // 덱에서 카드를 제거
            DeckData.Instance.RemoveCardToDeckById("101001A", 1);
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            // 덱에 있는 카드 정보 출력
            DeckData.Instance.PrintDeck();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            DeckData.Instance.AddCardToDeckById("123321");
            DeckData.Instance.RemoveCardToDeckById("123321");
        }
    }
}
