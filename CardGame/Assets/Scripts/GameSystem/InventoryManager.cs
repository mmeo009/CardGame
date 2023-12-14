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
        if (listNum > cardAmount)
        {
            CardActive(cardNum, false);         // ī�� ��Ȱ��ȭ
        }
        else
        {
            CardActive(cardNum, true);          // ī�� Ȱ��ȭ

            string id = myDeck[listNum].id;         // ī���� ���̵�
            int level = myDeck[listNum].level;          // ī���� ����
            int count = myDeck[listNum].count;          // ī���� ����

            invCards[cardNum].thisCardId = id;          // ī�忡 ���̵� ��������
            invCards[cardNum].thisCardLevel = level;            // ī�忡 ������ ��������
            if (invCards[cardNum].thisCardinfo.Count <= 0)
            {
                invCards[cardNum].FindChilds(invCards[cardNum].gameObject);             // ���� ģ������ ã�Ƶ��� ���� ������ ��� ã��
            }
            invCards[cardNum].LoadCardData(id);         // ī���� ������ �ҷ���
            ObjectNameAndParent text = invCards[cardNum].thisCardinfo.Find(name => name.name == "AmountText");          // ī���� ������ ǥ���ϴ� ������Ʈ�� ã�ƿ�
            text.thisObject.GetComponent<TextMeshPro>().text = $"{count}��";         // ī���� ������ ǥ���ϴ� ������Ʈ�� ������ ǥ����
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
