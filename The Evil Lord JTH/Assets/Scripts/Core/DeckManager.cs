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
        if(deckInfo != null)
        {
            deckInfo.GetComponent<TMP_Text>().text = Managers.DeckData.amountOfCardsInDeck.ToString();
        }
        else
        {
            GameObject _deckInfo = GameObject.Find("RemainingCards");
            deckInfo = _deckInfo;
            deckInfo.GetComponent<TMP_Text>().text = Managers.DeckData.amountOfCardsInDeck.ToString();
        }
    }
    // DefaultDeck ���� ����
    public void DeckSetting()
    {
        Managers.DeckData.deck.Clear();
        foreach (var card in Managers.DeckData.defaultDeck)
        {
            Managers.DeckData.deck.Add(card.Clone());
        }
    }

    //1,2�ܰ� ī�常 ���� �߰��� �� ����
    public void AddCardIntoDefaultDeck(string cardId, int cardAmount = 2)
    {
        Entity_CardData.Param foundCard = Managers.Data.cardsDictionary[cardId];
        char lastId = foundCard.id[foundCard.id.Length - 1];
        int level = 0 ;
        // ���̵� ���� ��ȭ �ܰ� ã��
        if(lastId == 'A' || lastId == 'J' || lastId == 'I')
        {
            level = 1;
        }
        else if(lastId == 'B' || lastId == 'T' || lastId == 'N')
        {
            level = 2;
        }
        else if(lastId == 'C' || lastId == 'H')
        {
            level = 3;
        }
        // ī�带 �߰��ϴ� �κ�
        if(level == 1 || level == 2)
        {
            if (foundCard != null)
            {
                // ���� �̹� ���� ī�尡 �ִ��� Ȯ��
                CardInformation existingEntry = Managers.DeckData.defaultDeck.Find(entry => entry.id == cardId);

                if (existingEntry == null)
                {
                    // ���ο� ī���� ���� �߰�
                    CardInformation newEntry = new CardInformation
                    {
                        id = cardId,
                        count = cardAmount,
                        level = level
                    };
                    Managers.DeckData.defaultDeck.Add(newEntry);
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
                // ���ο� ī���� ���� �߰�
                CardInformation newEntry = new CardInformation
                {
                    id = cardId,
                    count = cardAmount,
                    level = level
                };
                Managers.DeckData.defaultDeck.Add(newEntry);
            }
        }
        else
        {
            Debug.Log($"{level}�� �߰� �Ұ���");
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
            if (isInf == true)
            {
                CardInformation existingInfEntry = Managers.DeckData.deck.Find(entry => entry.id == cardId);

                if (existingInfEntry != null)
                {
                    if (existingInfEntry.level == _level)
                    {
                        // �̹� �ִ� ī���� ��� ī��Ʈ �߰�.
                        existingInfEntry.count += cardCount;
                    }
                    else
                    {
                        // ���̵�� ������ ������ �ٸ���� Ŭ���� �߰�
                        CardInformation newEntry = new CardInformation
                        {
                            id = cardId,
                            count = cardCount,
                            level = _level
                        };
                        Managers.DeckData.deck.Add(newEntry);
                    }
                }
                else
                {
                    // ���ο� ī�忡 ���ο� �����̸� �� ī�� �߰�
                    CardInformation newEntry = new CardInformation
                    {
                        id = cardId,
                        count = cardCount,
                        level = _level
                    };
                    Managers.DeckData.deck.Add(newEntry);
                }
            }
            else
            {
                // ���� �̹� ���� ī�尡 �ִ��� Ȯ��
                CardInformation existingEntry = Managers.DeckData.deck.Find(entry => entry.id == cardId);

                if (existingEntry != null)
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
                        level = level
                    };
                    Managers.DeckData.deck.Add(newEntry);
                }
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
            CardInformation existingEntry = Managers.DeckData.deck.Find(entry => entry.id == cardId && entry.level == cardLevel);

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
                    Managers.DeckData.deck.Remove(existingEntry);
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
        foreach (var entry in Managers.DeckData.deck)
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
        foreach (var entry in Managers.DeckData.deck)
        {
            Entity_CardData.Param foundCard = Managers.Data.cardsDictionary[entry.id];
            if (foundCard != null)
            {
                count += entry.count;
            }
            Managers.DeckData.amountOfCardsInDeck = count;
        }
    }
}