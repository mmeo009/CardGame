using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class DeckManager
{
    public int maxHold = 2;
    public int nowHold = 0;
    public GameObject deckInfo;

    public void ShowRemainingCards()
    {
        GameObject _deckInfo = GameObject.Find("RemainingCards");
        if(_deckInfo != null)
        {
            deckInfo = _deckInfo;
            deckInfo.GetComponent<TMP_Text>().text = CardData.Instance.amountOfCardsInDeck.ToString();
        }
        else
        {
            Debug.Log("���ڸ� ǥ���� �� ����");
        }
    }
    // ī�带 ���� �߰��ϴ� �Լ�
    public void AddCardToDeckById(string cardId, int cardCount = 1, bool isInf = false , int _level = 0)
    {
        // ��ũ���ͺ� ������Ʈ���� �ش� id�� ���� ī�带 ã��
        Entity_CardData.Param foundCard = CardData.Instance.cardDatabase.param.Find(card => card.id == cardId);

        char lastId = foundCard.id[foundCard.id.Length - 1];
        int level = 1;
        switch(lastId)
        {
            case 'A':
                level = 1;
                break;
            case 'B':
                level = 2;
                break;
            case 'C':
                level = 3;
                break;
            case 'J':
                level = 1;
                break;
            case 'T':
                level = 2;
                break;
            case 'H':
                level = 3;
                break;
            case 'N':
                level = 2;
                break;
        }

        if (foundCard != null)
        {
            // ���� �̹� ���� ī�尡 �ִ��� Ȯ��
            CardDataEntry existingEntry = CardData.Instance.deck.Find(entry => entry.id == cardId);

            if (existingEntry != null)
            {
                if(isInf == true)
                {
                    if(existingEntry.level == _level)
                    {
                        // �̹� �ִ� ī���� ������ ����
                        existingEntry.count += cardCount;
                    }
                    else
                    {
                        // ���ο� ī���� ���� �߰�
                        CardDataEntry newEntry = new CardDataEntry
                        {
                            id = cardId,
                            count = cardCount,
                            level = _level
                        };
                        CardData.Instance.deck.Add(newEntry);
                    }
                }
                else
                {
                    // �̹� �ִ� ī���� ������ ����
                    existingEntry.count += cardCount;
                }
            }
            else
            {
                // ���ο� ī���� ���� �߰�
                CardDataEntry newEntry = new CardDataEntry
                {
                    id = cardId,
                    count = cardCount,
                    level = level
                };
                CardData.Instance.deck.Add(newEntry);
            }

            // �߰��� ī�� ������ ���
            Debug.Log($"�߰��� ī�� : (ī�� �̸�: {foundCard.cardName}, ����: {cardCount}, ��ȭ �ܰ� : {level.ToString()}");
        }
        else
        {
            // �߰��� ī�尡 ��ũ���ĺ� ������Ʈ�� ���ٸ� ���ٸ� ���� �޼��� ���
            Debug.Log($"ID {cardId}�� ���� ī�带 ã�� �� �����ϴ�.");
        }

        // ���Ѵ�� �߰��ϴ� ī���� ���
        if (lastId == 'N')
        {
            string infCard = foundCard.id.Replace("N", "A");
            AddCardToDeckById(infCard, 2);
        }
        // ���� ���� ī�� �� üũ
        CountCardsInDeck();
        ShowRemainingCards();
    }

    public void RemoveCardToDeckById(string cardId, int cardAmount = 1)
    {
        // ��ũ���ͺ� ������Ʈ���� �ش� id�� ���� ī�带 ã��
        Entity_CardData.Param foundCard = CardData.Instance.cardDatabase.param.Find(card => card.id == cardId);

        if (foundCard != null)
        {
            // ���� �̹� ���� ī�尡 �ִ��� Ȯ��
            CardDataEntry existingEntry = CardData.Instance.deck.Find(entry => entry.id == cardId);

            if (existingEntry != null)
            {
                if(existingEntry.count > cardAmount)
                {
                    // ���ŷ��� ���� �ִ� ī���� �纸�� ������� �����ִ� ī���� ���� ����
                    existingEntry.count -= cardAmount;
                    // ���ŵ� ī�� ������ ���
                    Debug.Log($"���ŵ� ī�� : (ī�� �̸�: {foundCard.cardName}, ���� ��: {cardAmount})");
                }
                else if(existingEntry.count == cardAmount)
                {
                    // ���ŷ��� ���� �ִ� ī��� ������� ���� �ִ� ī�带 ����
                    CardData.Instance.deck.Remove(existingEntry);
                    // ���ŵ� ī�� ������ ���
                    Debug.Log($"���ŵ� ī�� : (ī�� �̸�: {foundCard.cardName}, ���� ��: {cardAmount})");
                }
                else
                {
                    // ���ŷ��� ���� �ִ� ī���� �纸�� ������� ���� �޼��� ���
                    Debug.Log($"ID{cardId}�� ī�� ����{existingEntry.count}�Դϴ�. ���� �� ���� ���� ������ �� �����ϴ�.");
                }

            }
            else
            {
                // ���� ���� ī���� ��� ���� �޼��� ���
                Debug.Log($"ID {cardId}�� ���� ī�尡 ���� �����ϴ�.");
            }
        }
        else
        {
            // ������ ī�尡 ��ũ���ĺ� ������Ʈ�� ���ٸ� ���ٸ� ���� �޼��� ���
            Debug.Log($"ID {cardId}�� ���� ī�带 ã�� �� �����ϴ�.");
        }
        // ���� ���� ī�� �� üũ
        CountCardsInDeck();
        ShowRemainingCards();
    }

    // ���� �ִ� ī�� ���� ��� �Լ�
    public void PrintDeck()
    {
        foreach (var entry in CardData.Instance.deck)
        {
            Entity_CardData.Param foundCard = CardData.Instance.cardDatabase.param.Find(card => card.id == entry.id);
            if (foundCard != null)
            {
                Debug.Log($"ī�� �̸�: {foundCard.cardName}, ����: {entry.count}");
            }
        }
        ShowRemainingCards();
    }

    // ���� �ִ� ī���� ���� üũ �Լ�
    public void CountCardsInDeck()
    {
        int count = 0;
        foreach (var entry in CardData.Instance.deck)
        {
            Entity_CardData.Param foundCard = CardData.Instance.cardDatabase.param.Find(card => card.id == entry.id);
            if (foundCard != null)
            {
                count += entry.count;
            }
            CardData.Instance.amountOfCardsInDeck = count;
        }
    }
}
