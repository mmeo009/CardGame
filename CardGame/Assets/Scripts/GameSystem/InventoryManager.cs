using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : GenericSingleton<InventoryManager>
{
    public List<CardInformation> myDeck;                // ���� ������ �ҷ��� ������ ����Ʈ
    public CardDataLoad[] invCards;             // �κ��丮�� �����ϴ� ī���
    public int maxNum = 3;          // ���� �κ��丮 4���� �ִ�
    public int cardAmount;

    public void LoadMyDeck()
    {           // ���� ������ �ҷ����� �Լ�
        myDeck.Clear();          // ����Ʈ ���� �ʱ�ȭ
        foreach (var card in DeckData.Instance.defaultDeck)
        {           // defaultDeck�� ������ �����ؿ�
            myDeck.Add(card.Clone());
        }
        cardAmount = myDeck.Count;
    }

    public void FindInvCards()
    {           // ī����� ã�� �Լ�
        invCards = FindObjectsOfType<CardDataLoad>();
    }

    public void LoadMyCard(int listNum)
    {           // ī�� �ϳ��� ������ �����ϴ� �Լ�
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
        text.thisObject.GetComponent<TextMeshPro>().text = $"{count}��";
    }

    public void LoadByMaxNum()
    {
        
    }
}
