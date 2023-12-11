using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public CardDataLoad[] cards;
    private void Start()
    {
        cards[0].thisCardId = "101011J";
        cards[1].thisCardId = "101011T";
        cards[2].thisCardId = "101011H";
        cards[3].thisCardId = "102022J";
        cards[4].thisCardId = "102022T";
        cards[5].thisCardId = "102022H";

        for(int i = 0; i < cards.Length; i++)
        {
            cards[i].FindChilds(cards[i].gameObject);
            cards[i].LoadCardData(cards[i].thisCardId);
        }
    }

}
