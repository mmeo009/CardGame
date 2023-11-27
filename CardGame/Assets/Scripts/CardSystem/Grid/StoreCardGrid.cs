using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreCardGrid : MonoBehaviour
{
    enum Type
    {
        NONE,
        SHOP,
        DEMON
    }
    Type type = Type.NONE;

    int limit = 0;
    int limitCount = 0;
    int price = 0;
    string cardId;

    public void CheckThisStage()
    {
        if (GameManager.Instance.sceneName == "Store Scene")
        {
            type = Type.SHOP;
        }
        else if(GameManager.Instance.sceneName == "Demon Scene")
        {
            type = Type.DEMON;
        }
        else
        {
            type = Type.NONE;
        }
    }
    public void SetMyRarity()
    {

    }

    public void SetMyCardId()
    {

    }

    public void SetMyPrice()
    {

    }
}
