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
        if (listNum > cardAmount)
        {
            CardActive(cardNum, false);         // 카드 비활성화
        }
        else
        {
            CardActive(cardNum, true);          // 카드 활성화

            string id = myDeck[listNum].id;         // 카드의 아이디
            int level = myDeck[listNum].level;          // 카드의 레벨
            int count = myDeck[listNum].count;          // 카드의 개수

            invCards[cardNum].thisCardId = id;          // 카드에 아이디를 기입해줌
            invCards[cardNum].thisCardLevel = level;            // 카드에 레벨을 기입해줌
            if (invCards[cardNum].thisCardinfo.Count <= 0)
            {
                invCards[cardNum].FindChilds(invCards[cardNum].gameObject);             // 하위 친구들을 찾아두지 않은 상태일 경우 찾음
            }
            invCards[cardNum].LoadCardData(id);         // 카드의 정보를 불러옴
            ObjectNameAndParent text = invCards[cardNum].thisCardinfo.Find(name => name.name == "AmountText");          // 카드의 개수를 표시하는 오브젝트를 찾아옴
            text.thisObject.GetComponent<TextMeshPro>().text = $"{count}장";         // 카드의 개수를 표시하는 오브젝트에 개수를 표시함
        }
    }
    public void CardActive(int cardNum, bool active)
    {
        invCards[cardNum].gameObject.SetActive(active);
    }

    public void LoadByMaxNum()
    {
        int num = maxNum;
            LoadMyCard(num - 3);
            LoadMyCard(num - 2);
            LoadMyCard(num - 1);
            LoadMyCard(num);
    }
}
