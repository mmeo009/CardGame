using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : MonoBehaviour
{
    public Entity_CardData.Param thisCard;
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
}
