using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testc : MonoBehaviour
{
    public CardDataLoad[] cards;
    public string id;
    public int ListAmount;
    public int index = 0;
    public int indexChangeAmount;
    
    
    public void LoadDeckData()
    {
        Managers.Deck.DeckSetting();
       string id = DeckData.Instance.deck[0].id;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            cards[0].thisCardId = id;
            cards[0].thisCardLevel = 1;
            cards[0].LoadCardData(id);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            cards[1].thisCardId = id;
            cards[1].thisCardLevel = 1;
            cards[1].LoadCardData(id);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            cards[2].thisCardId = id;
            cards[2].thisCardLevel = 1;
            cards[2].LoadCardData(id);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            cards[3].thisCardId = id;
            cards[3].thisCardLevel = 1;
            cards[3].LoadCardData(id);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
           for(int i = 0; i < cards.Length; i++)
            {
                cards[i].FindChilds(cards[i].gameObject);
            }
        }
       
    }

   
}
