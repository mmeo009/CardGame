using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayButton : MonoBehaviour
{
    public void Press(string stage)
    {
        if(stage == "T1")
        {
            PlayerData.Instance.GainingOrLosingValue("currentHealth", (int)Math.Ceiling(PlayerData.Instance.player.maxHealth * 0.1));
            Managers.Stage.SelectLevel();
        }
        else if(stage == "T2")
        {
            PlayerData.Instance.GainingOrLosingValue("maxMana", 1);
            Managers.Stage.SelectLevel();
        }
        else if(stage == "F1")
        {
            if (PlayerData.Instance.player.currentHealth > 7)
            {
                List<CardInformation> cards = new List<CardInformation>();
                foreach (CardInformation cardDummy in DeckData.Instance.defaultDeck)
                {
                    if (cardDummy.count == 1 && cardDummy.level == 1)
                    {
                        cards.Add(cardDummy);
                    }
                }
                if(cards.Count > 0)
                {
                    int num = UnityEngine.Random.Range(0, cards.Count);
                    string id = cards[num].id.Replace("A", "B");
                    Managers.Deck.RemoveCardFromDefaultDeckById(cards[num].id, cards[num].level, 1);
                    Managers.Deck.AddCardIntoDefaultDeck(id, 1);
                    PlayerData.Instance.GainingOrLosingValue("currentHealth", -7);
                    Managers.Stage.SelectLevel();
                }
                else
                {
                    Debug.Log("덱에 강화 가능한 카드가 없셔");
                }
            }
            else
            {
                Debug.Log("체력이 부족해");
            }
        }
        else if(stage == "F2")
        {
            if (PlayerData.Instance.player.currentHealth > 4)
            {
                List<CardInformation> cards = new List<CardInformation>();
                foreach (CardInformation cardDummy in DeckData.Instance.defaultDeck)
                {
                    if (cardDummy.count >= 2 && cardDummy.level == 1)
                    {
                        cards.Add(cardDummy);
                    }
                }
                if (cards.Count > 0)
                {
                    int num = UnityEngine.Random.Range(0, cards.Count);
                    string id = cards[num].id.Replace("A", "B");
                    Managers.Deck.RemoveCardFromDefaultDeckById(cards[num].id, cards[num].level, 2);
                    Managers.Deck.AddCardIntoDefaultDeck(id, 1);
                    PlayerData.Instance.GainingOrLosingValue("currentHealth", -4);
                    Managers.Stage.SelectLevel();
                }
                else
                {
                    Debug.Log("덱에 강화 가능한 카드가 없셔");
                }
            }
            else
            {
                Debug.Log("체력이 부족해");
            }
        }
        else if(stage == "F0")
        {
            Managers.Stage.SelectLevel();
        }
    }
}
