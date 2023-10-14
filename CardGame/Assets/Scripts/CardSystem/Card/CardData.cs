using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : MonoBehaviour
{
    public Entity_CardData.Param thisCard;
    public PlayerData player;
	public void GetData(string id)
    {
        Entity_CardData.Param foundCard = Managers.Data.cardsDictionary[id];
        if(foundCard != null)
        {
            thisCard = foundCard;
		}
        else
        {
            Debug.Log("해당 아이디의 카드가 게임에 존재하지 않습니다.");
        }
    }
    public void UsingCard()
    {
        player = PlayerData.Instance;
        if (thisCard != null)
        {
            int cardType = thisCard.id[3];
            char cardLevel = thisCard.id[thisCard.id.Length - 1];
            int cardPlusData = thisCard.stat_05;
            int cardCost = thisCard.cardCost;
            int cardMethod = thisCard.usingMethod;
            string cardCC01 = thisCard.cc_1;
            string cardCC02 = thisCard.cc_2;
            int myMana = player.player.maxMana;
            if (cardLevel != 'N')
            {
                if (cardType == 1)
                {
                    if(cardMethod == 0)             // 아무것도 아닌 무기
                    {
                        Debug.Log("아무 능력도 없당");
                    }
                    if(cardMethod == 1)             // 때리는 무기
                    {
                        if(myMana >= cardCost)
                        {

                        }
                        else
                        {
                            Debug.Log("마나가 없어서 쓸 수 없다");
                        }
                    }
                    if(cardMethod == 2)             // 던지는 무기
                    {
                        if (myMana >= cardCost)
                        {

                        }
                        else
                        {
                            Debug.Log("마나가 없어서 쓸 수 없다");
                        }
                    }
                    if(cardMethod == 3)             // 던지고 돌아오는 무기
                    {
                        if (myMana >= cardCost)
                        {

                        }
                        else
                        {
                            Debug.Log("마나가 없어서 쓸 수 없다");
                        }
                    }
                }
                else if (cardType == 2)
                {

                }
                else if (cardType == 3)
                {

                }
                else if (cardType == 4)
                {

                }
            }
        }
        else
        {
            GetComponent<CardDataLoad>().PickCardAndIdFromDeck();
        }
        
    }
}
