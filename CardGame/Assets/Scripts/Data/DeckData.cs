using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckData : GenericSingleton<DeckData>
{
    public int amountOfCardsInDeck;     //덱에 들어있는 카드의 양
    // 덱 리스트
    public List<CardInformation> deck = new List<CardInformation>();
    public List<CardInformation> defaultDeck = new List<CardInformation>();

}
[System.Serializable]
public class CardInformation
{
    public string id;
    public int count; // 카드 개수
    public int level; // 강화 단계
}
