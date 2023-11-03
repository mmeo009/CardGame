using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MonsterGrid : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropedCard = eventData.pointerDrag;
        GridIndex grid = dropedCard.GetComponent<CardController>().myGrid.GetComponent<GridIndex>();
        MergeGrid merge = dropedCard.GetComponent<CardController>().myGrid.GetComponent<MergeGrid>();
        if (grid != null)
        {
            dropedCard.GetComponent<CardUse>().UsingCard();
            PlayerData.Instance.ShowMyInfo();
            grid.ISEmpty();
        }
        else if(merge != null)
        {
            dropedCard.GetComponent<CardUse>().UsingCard();
            PlayerData.Instance.ShowMyInfo();
            merge.ISEmpty();
        }
        else
        {
            Debug.Log("¿Ã∑± ∫Ò±ÿ¿Ã!");
        }
    }
}
