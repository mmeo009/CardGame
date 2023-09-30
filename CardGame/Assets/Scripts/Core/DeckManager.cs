using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class DeckManager
{
    public int maxHold = 2;
    public int nowHold = 0;

    // ī�带 ���� �߰��ϴ� �Լ�
    public void AddCardToDeckById(string cardId, int cardCount = 1)
    {
        // ��ũ���ͺ� ������Ʈ���� �ش� id�� ���� ī�带 ã��
        Entity_CardData.Param foundCard = CardData.Instance.cardDatabase.param.Find(card => card.id == cardId);

        if (foundCard != null)
        {
            // ���� �̹� ���� ī�尡 �ִ��� Ȯ��
            CardDataEntry existingEntry = CardData.Instance.deck.Find(entry => entry.id == cardId);

            if (existingEntry != null)
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
                    count = cardCount
                };
                CardData.Instance.deck.Add(newEntry);
            }

            // �߰��� ī�� ������ ���
            Debug.Log($"�߰��� ī�� : (ī�� �̸�: {foundCard.cardName}, ����: {cardCount})");
        }
        else
        {
            // �߰��� ī�尡 ��ũ���ĺ� ������Ʈ�� ���ٸ� ���ٸ� ���� �޼��� ���
            Debug.Log($"ID {cardId}�� ���� ī�带 ã�� �� �����ϴ�.");
        }
        // ���� ���� ī�� �� üũ
        CountCardsInDeck();
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
