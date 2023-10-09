using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckData : GenericSingleton<DeckData>
{
    public int amountOfCardsInDeck;     //���� ����ִ� ī���� ��
    // �� ����Ʈ
    public List<CardInformation> deck = new List<CardInformation>();
    public List<CardInformation> defaultDeck = new List<CardInformation>();

}
[System.Serializable]
public class CardInformation
{
    public string id;
    public int count; // ī�� ����
    public int level; // ��ȭ �ܰ�
    public CardInformation Clone()
    {
        return new CardInformation
        {
            id = this.id,
            count = this.count,
            level = this.level,
            // �ٸ� �Ӽ��鵵 ����
        };
    }
}
