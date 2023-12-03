using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotIndex : MonoBehaviour
{
    public enum SlotType
    {
        Default,
        Merge,
        Monster,
        Store,
        Demon
    }

    public enum SlotState
    {
        Empty,
        Full
    }

    public int gridNum;
    public string cardId;
    public CardDataLoad cardObject;
    public SlotType type = SlotType.Default;
    public SlotState state = SlotState.Empty;

    public void ChangeState(SlotState _state)
    {
        state = _state;
        if(_state == SlotState.Empty)
        {
            cardObject = null;
            cardId = null;
            if(type == SlotType.Merge)
            {
                SendCardDataToMergeController();
            }
        }
    }

    public void GetCardIntoThisSlot(CardDataLoad card)
    {
        card.transform.position = transform.position;
        card.transform.SetParent(this.transform);
        card.transform.localPosition = Vector3.zero;
        card.transform.localScale = new Vector3(2.403846f, 2.403846f);

        card.mySlot = this;
        card.EndDragging();

        cardId = card.thisCardId;
        cardObject = card;

        ChangeState(SlotState.Full);

        if(type == SlotType.Merge)
        {
            SendCardDataToMergeController();
        }
    }

    public void SendCardDataToMergeController()
    {
        MergeController merge = FindObjectOfType<MergeController>();

        if(gridNum == 0)
        {
            merge.gridACardId = cardId;
        }
        else if(gridNum == 1)
        {
            merge.gridBCardId = cardId;
        }
    }
}
