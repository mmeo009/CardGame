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
            Debug.Log("�ش� ���̵��� ī�尡 ���ӿ� �������� �ʽ��ϴ�.");
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
                    if(cardMethod == 0)             // �ƹ��͵� �ƴ� ����
                    {
                        Debug.Log("�ƹ� �ɷµ� ����");
                    }
                    if(cardMethod == 1)             // ������ ����
                    {
                        if(myMana >= cardCost)
                        {

                        }
                        else
                        {
                            Debug.Log("������ ��� �� �� ����");
                        }
                    }
                    if(cardMethod == 2)             // ������ ����
                    {
                        if (myMana >= cardCost)
                        {

                        }
                        else
                        {
                            Debug.Log("������ ��� �� �� ����");
                        }
                    }
                    if(cardMethod == 3)             // ������ ���ƿ��� ����
                    {
                        if (myMana >= cardCost)
                        {

                        }
                        else
                        {
                            Debug.Log("������ ��� �� �� ����");
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
