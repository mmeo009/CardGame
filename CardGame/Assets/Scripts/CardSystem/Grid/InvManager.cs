using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InvManager : MonoBehaviour
{
    public CardDataLoad[] cards;
    public string id;
    public int listAmount;
    public int index = 0;
    public int indexChangeAmount = 0;
    public void LoadDeckData()      //로드 데이터를 실행할꺼임
    {
        Managers.Deck.DeckSetting();
        listAmount = DeckData.Instance.deck.Count;

        if (listAmount >= 4)
        {
            for (int i = 0; i < cards.Length; i++)
            {

                CardData cardData = GetCardDataByIndex(index + i + indexChangeAmount - 1);

                cards[i].FindChilds(cards[i].gameObject);
                cards[i].thisCardId = cardData.id;
                cards[i].thisCardLevel = cardData.level;
                cards[i].LoadCardData(cards[i].thisCardId);
            }

            InvenLoad();
        }
        else
        {
            Debug.Log("덱에 카드가 충분하지가 않아.");
        }
    }

    private CardData GetCardDataByIndex(int cardIndex)
    {
        // 인덱스가 덱의 범위 내에 있는지 확인합니다.
        if (cardIndex >= 0 && cardIndex < DeckData.Instance.deck.Count)
        {
            return DeckData.Instance.deck[cardIndex];
        }
        else
        {
            // 기본 카드 데이터를 반환하거나 범위를 벗어난 경우를 처리합니다.
            return new CardData();
        }


        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].FindChilds(cards[i].gameObject);
        }

        InvenLoad();
    }
    public void InvenLoad()
    {
        for (int i = 0; i < 4; i++)
        {
            if (listAmount < index + i + indexChangeAmount)
            {
                cards[i].gameObject.SetActive(false);
            }
            else
            {
                cards[i].gameObject.SetActive(true);
                cards[i].thisCardId = DeckData.Instance.deck[index + i + indexChangeAmount - 1].id;
                cards[i].thisCardLevel = DeckData.Instance.deck[index + i + indexChangeAmount - 1].level;
                cards[i].LoadCardData(cards[i].thisCardId);
            }
        }
    }
    public void NextOrPrev(int four)            //버튼에 넣을 스크립트
    {
        if (indexChangeAmount + four < 0 || indexChangeAmount + four + 3 + index > listAmount)
        {
            Debug.Log("페이지를 전환할 수 없어");
        }
        else
        {
            indexChangeAmount += four;      //4를 추가하거나 뺌
            InvenLoad();            //로드 해옴
        }
    }
}


