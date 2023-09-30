using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeController : GenericSingleton<MergeController>
{
    public string gridACardId = null;
    public string gridBCardId = null;
    public string _id = null;
    public void MergeCards()
    {
        if (gridACardId != null && gridBCardId != null)
        {
            if (gridACardId == gridBCardId)
            {
                int idLength = gridACardId.Length;
                if (idLength > 0)
                {
                    char lastId = gridACardId[idLength - 1];
                    if (lastId == 'A')
                    {
                        _id = gridACardId.Replace("A", "B");
                    }
                    else if (lastId == 'B')
                    {
                        _id = gridACardId.Replace("B", "C");
                    }
                    else if (lastId == 'J')
                    {
                        _id = gridACardId.Replace("J", "T");
                    }
                    else if (lastId == 'T')
                    {
                        _id = gridACardId.Replace("T", "H");
                    }
                    else
                    {
                        Debug.Log($"ID : {gridACardId}는 강화할 수 없어");
                    }
                }
                gridACardId = null;
                gridBCardId = null;
                if (_id != null)
                {
                    DrawCard.Instance.CreateCardFromNothing(_id);
                    _id = null;
                }
            }
            else
            {
                Debug.Log("카드의 아이디가 일치하지 않아");
            }
        }
        else
        {
            Debug.Log("카드의 아이디가 존재하지 않아");
        }
    }
}
