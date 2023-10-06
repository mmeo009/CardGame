using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public Entity_CardData cardDatabase;
	public Dictionary<string, Entity_CardData.Param> cardsDictionary = new Dictionary<string, Entity_CardData.Param>();
	public void GetResources()
    {
        cardDatabase = Resources.Load<Entity_CardData>("CardData"); // "CardData"�� ���ҽ� ���� ���� ���
        if (cardDatabase == null)
        {
            Debug.LogError("CardDatabase �� ���ҽ� ���Ͽ� �����ϴ�.");
        }
        else
        {
            Debug.Log("ã�Ҵٷ�.");
        }
	}
    public void DataIntoDictionary()
    {
        if (cardDatabase != null)
        {
            foreach (Entity_CardData.Param cardData in cardDatabase.param)
            {
                if (!cardsDictionary.ContainsKey(cardData.id))
                {
                    cardsDictionary.Add(cardData.id, cardData);
                }
                else
                {
                    Debug.LogError("�ߺ��� ī�� ID�� ���� ī�尡 �ֽ��ϴ�: " + cardData.id);
                }
            }
        }
        else
        {
            Debug.LogError("CardDatabase�� �ε���� �ʾҽ��ϴ�.");
        }
    }
    public void DebugCardDatas()
    {
        foreach (var cardData in cardsDictionary.Values)
        {
            Debug.Log("Card ID: " + cardData.id);
            Debug.Log("Item Code: " + cardData.cardCost);
            Debug.Log("Card Name: " + cardData.cardName);
        }
    }
}
