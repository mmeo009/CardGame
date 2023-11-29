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
    public void LoadDeckData()      //�ε� �����͸� �����Ҳ���
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
            Debug.Log("���� ī�尡 ��������� �ʾ�.");
        }
    }

    private CardData GetCardDataByIndex(int cardIndex)
    {
        // �ε����� ���� ���� ���� �ִ��� Ȯ���մϴ�.
        if (cardIndex >= 0 && cardIndex < DeckData.Instance.deck.Count)
        {
            return DeckData.Instance.deck[cardIndex];
        }
        else
        {
            // �⺻ ī�� �����͸� ��ȯ�ϰų� ������ ��� ��츦 ó���մϴ�.
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
    public void NextOrPrev(int four)            //��ư�� ���� ��ũ��Ʈ
    {
        if (indexChangeAmount + four < 0 || indexChangeAmount + four + 3 + index > listAmount)
        {
            Debug.Log("�������� ��ȯ�� �� ����");
        }
        else
        {
            indexChangeAmount += four;      //4�� �߰��ϰų� ��
            InvenLoad();            //�ε� �ؿ�
        }
    }
}


