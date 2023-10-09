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
            deckInfo.GetComponent<TMP_Text>().text = DeckData.Instance.amountOfCardsInDeck.ToString();
        }
        else
        {
            Debug.Log("���ڸ� ǥ���� �� ����");
        }
    }
    // DefaultDeck ���� ����
    public void DeckSetting()
    {
        DeckData.Instance.deck = DeckData.Instance.defaultDeck;
    }

    //1,2�ܰ� ī�常 ���� �߰��� �� ����
    public void AddCardIntoDefaultDeck(string cardId, int cardAmount = 2)
    {
        Entity_CardData.Param foundCard = Managers.Data.cardsDictionary[cardId];
        char lastId = foundCard.id[foundCard.id.Length - 1];
        int level = 0 ;
        // ���̵� ���� ��ȭ �ܰ� ã��
        if (lastId != 'A' || lastId != 'J')
        {
            Debug.Log("1�ܰ谡 �ƴϾ�");
        }
        else if(lastId == 'A' || lastId != 'J')
        {
            level = 1;
        }
        else if(lastId == 'B' || lastId == 'T' || lastId == 'N')
        {
            level = 2;
        }
        // ī�带 �߰��ϴ� �κ�
        if(level == 1 || level == 2)
        {
            if (foundCard != null)
            {
                // ���� �̹� ���� ī�尡 �ִ��� Ȯ��
                CardInformation existingEntry = DeckData.Instance.defaultDeck.Find(entry => entry.id == cardId);

                if (existingEntry != null)
                {
                    // ���ο� ī���� ���� �߰�
                    CardInformation newEntry = new CardInformation
                    {
                        id = cardId,
                        count = cardAmount,
                        level = 1
                    };
                    DeckData.Instance.defaultDeck.Add(newEntry);
                }
                else
                {
                    // �̹� �ִ� ī���� ������ ����
                    existingEntry.count += cardAmount;
                }

                // �߰��� ī�� ������ ���
                Debug.Log($"�߰��� ī�� : (ī�� �̸�: {foundCard.cardName}, ����: {cardAmount}");
            }
            else
            {
                // �߰��� ī�尡 ���ٸ� ���� �޼��� ���
                Debug.Log($"ID {cardId}�� ���� ī�带 ã�� �� �����ϴ�.");
            }
        }
    }
    // DefaultDeck ���� ��

    // ī�带 ���� �߰��ϴ� �Լ�
    public void AddCardToDeckById(string cardId, int cardCount = 1, bool isInf = false , int _level = 0)
    {
        // �ش� ī�带 ã�ƿ�
        Entity_CardData.Param foundCard = Managers.Data.cardsDictionary[cardId];

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
            CardInformation existingEntry = DeckData.Instance.deck.Find(entry => entry.id == cardId);

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
                        CardInformation newEntry = new CardInformation
                        {
                            id = cardId,
                            count = cardCount,
                            level = _level
                        };
                        DeckData.Instance.deck.Add(newEntry);
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
                CardInformation newEntry = new CardInformation
                {
                    id = cardId,
                    count = cardCount,
                    level = level
                };
                DeckData.Instance.deck.Add(newEntry);
            }

            // �߰��� ī�� ������ ���
            Debug.Log($"�߰��� ī�� : (ī�� �̸�: {foundCard.cardName}, ����: {cardCount}, ��ȭ �ܰ� : {level.ToString()}");
        }
        else
        {
            // �߰��� ī�尡 ���ٸ� ���� �޼��� ���
            Debug.Log($"ID {cardId}�� ���� ī�带 ã�� �� �����ϴ�.");
        }
        // ���� ���� ī�� �� üũ
        CountCardsInDeck();
        ShowRemainingCards();
    }

    public void RemoveCardToDeckById(string cardId, int cardLevel, int cardAmount = 1)
    {
        // �ش� id�� ���� ī�带 ã��
        Entity_CardData.Param foundCard = Managers.Data.cardsDictionary[cardId];

        if (foundCard != null)
        {
            // ���� �̹� ���� ī�尡 �ִ��� Ȯ��
            CardInformation existingEntry = DeckData.Instance.deck.Find(entry => entry.id == cardId && entry.level == cardLevel);

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
                    DeckData.Instance.deck.Remove(existingEntry);
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
            // ������ ī�尡 ���ٸ� ���� �޼��� ���
            Debug.Log($"ID {cardId}�� ���� ī�带 ã�� �� �����ϴ�.");
        }
        // ���� ���� ī�� �� üũ
        CountCardsInDeck();
        ShowRemainingCards();
    }

    // ���� �ִ� ī�� ���� ��� �Լ�
    public void PrintDeck()
    {
        foreach (var entry in DeckData.Instance.deck)
        {
            Entity_CardData.Param foundCard = Managers.Data.cardsDictionary[entry.id];
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
        foreach (var entry in DeckData.Instance.deck)
        {
            Entity_CardData.Param foundCard = Managers.Data.cardsDictionary[entry.id];
            if (foundCard != null)
            {
                count += entry.count;
            }
            DeckData.Instance.amountOfCardsInDeck = count;
        }
    }
}
