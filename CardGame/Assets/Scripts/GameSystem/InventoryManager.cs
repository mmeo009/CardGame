using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : GenericSingleton<InventoryManager>
{
    public List<CardInformation> myDeck;                // 덱의 정보를 불러와 복사할 리스트
    public CardDataLoad[] invCards;             // 인벤토리에 존재하는 카드들
    public int maxNum = 3;          // 현재 인벤토리 4장중 최대
    public int cardAmount;

    public void LoadMyDeck()
    {           // 덱의 정보를 불러오는 함수
        myDeck.Clear();          // 리스트 정보 초기화
        foreach (var card in DeckData.Instance.defaultDeck)
        {           // defaultDeck의 내용을 복사해옴
            myDeck.Add(card.Clone());
        }
        cardAmount = myDeck.Count;
    }

    public void FindInvCards()
    {           // 카드들을 찾는 함수
        invCards = FindObjectsOfType<CardDataLoad>();
    }

    public void LoadMyCard(int listNum)
    {           // 카드 하나에 정보를 기입하는 함수
        int cardNum = listNum % 4;
        string id = myDeck[listNum].id;
        int level = myDeck[listNum].level;
        int count = myDeck[listNum].count;

        invCards[cardNum].thisCardId = id;
        invCards[cardNum].thisCardLevel = level;
        if(invCards[cardNum].thisCardinfo == null)
        {
            invCards[cardNum].FindChilds(invCards[cardNum].gameObject);
        }
        invCards[cardNum].LoadCardData(id);
        ObjectNameAndParent text = invCards[cardNum].thisCardinfo.Find(name => name.name == "AmountText");
        text.thisObject.GetComponent<TextMeshPro>().text = $"{count}장";
    }

    public void LoadByMaxNum()
    {
        
    }
}
