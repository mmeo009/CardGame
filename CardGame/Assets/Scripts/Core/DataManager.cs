using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public Entity_CardData cardDatabase;
	public Dictionary<string, Entity_CardData.Param> cardsDictionary = new Dictionary<string, Entity_CardData.Param>();
	public void GetResources()
    {
        cardDatabase = Resources.Load<Entity_CardData>("CardData"); // "CardData"는 리소스 폴더 내의 경로
        if (cardDatabase == null)
        {
            Debug.LogError("CardDatabase 가 리소스 파일에 없습니다.");
        }
        else
        {
            Debug.Log("찾았다룡.");
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
                    Debug.LogError("중복된 카드 ID를 갖는 카드가 있습니다: " + cardData.id);
                }
            }
        }
        else
        {
            Debug.LogError("CardDatabase가 로드되지 않았습니다.");
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
