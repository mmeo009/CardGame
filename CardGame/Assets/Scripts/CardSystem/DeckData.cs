using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class DeckData : MonoBehaviour
{
    public Card cardDatabase;

    [System.Serializable]
    public class CardDataEntry
    {
        public string id;
        public int count; // ī�� ����
    }

    // �̱��� �ν��Ͻ�
    public static DeckData Instance { get; private set; }

    // �� ����Ʈ
    public List<CardDataEntry> deck = new List<CardDataEntry>();

    private void Awake()
    {
        // �̱��� �ν��Ͻ��� ����
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            // �̹� �ٸ� �ν��Ͻ��� �����ϸ� �� �ν��Ͻ��� �ı�
            Destroy(gameObject);
        }
    }

    // ī�带 ���� �߰��ϴ� �Լ�
    public void AddCardToDeckById(string cardId, int cardCount = 1)
    {
        // ��ũ���ͺ� ������Ʈ���� �ش� id�� ���� ī�带 ã��
        Card.Param foundCard = cardDatabase.param.Find(card => card.id == cardId);

        if (foundCard != null)
        {
            // ���� �̹� ���� ī�尡 �ִ��� Ȯ��
            CardDataEntry existingEntry = deck.Find(entry => entry.id == cardId);

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
                deck.Add(newEntry);
            }

            // �߰��� ī�� ������ ���
            Debug.Log($"�߰��� ī�� : (ī�� �̸�: {foundCard.cardName}, ����: {cardCount})");
        }
        else
        {
            // �߰��� ī�尡 ��ũ���ĺ� ������Ʈ�� ���ٸ� ���ٸ� ���� �޼��� ���
            Debug.Log($"ID {cardId}�� ���� ī�带 ã�� �� �����ϴ�.");
        }
    }

    public void RemoveCardToDeckById(string cardId, int cardAmount = 1)
    {
        // ��ũ���ͺ� ������Ʈ���� �ش� id�� ���� ī�带 ã��
        Card.Param foundCard = cardDatabase.param.Find(card => card.id == cardId);

        if (foundCard != null)
        {
            // ���� �̹� ���� ī�尡 �ִ��� Ȯ��
            CardDataEntry existingEntry = deck.Find(entry => entry.id == cardId);

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
                    deck.Remove(existingEntry);
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
    }

    // ���� �ִ� ī�� ���� ��� �Լ�
    public void PrintDeck()
    {
        foreach (var entry in deck)
        {
            Card.Param foundCard = cardDatabase.param.Find(card => card.id == entry.id);
            if (foundCard != null)
            {
                Debug.Log($"ī�� �̸�: {foundCard.cardName}, ����: {entry.count}");
            }
        }
    }
}
