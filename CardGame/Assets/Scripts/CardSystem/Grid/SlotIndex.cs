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

    private void ChangeState(SlotState _state)
    {
        state = _state;
    }

    public void GetCardIntoThisSlot(CardDataLoad card)
    {
        card.transform.SetParent(this.transform);
        card.transform.localPosition = Vector3.zero;
        card.transform.localScale = new Vector3(2.403846f, 2.403846f);

        cardId = card.thisCardId;
        cardObject = card;

        ChangeState(SlotState.Full);
    }


}
