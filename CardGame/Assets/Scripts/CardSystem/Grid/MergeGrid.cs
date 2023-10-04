using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MergeGrid : MonoBehaviour, IDropHandler
{
    public int GridNum;
    public bool isEmpty = true;
    public GameObject myCard;
    public GameObject mergeButton;

    public void Start()
    {
        mergeButton = GameObject.FindWithTag("MergeButton").gameObject;
    }
    public void ISEmpty()
    {
        int count = transform.childCount;

        if (count < 1)
        {
            Debug.Log($"{GridNum}번 조합 그리드 비었음");
            myCard = null;
            isEmpty = true;

            switch (GridNum)
            {
                case 0:
                    mergeButton.GetComponent<MergeController>().gridACardId = null;
                    mergeButton.GetComponent<MergeController>().gridALevel = 0;
                    break;
                case 1:
                    mergeButton.GetComponent<MergeController>().gridBCardId = null;
                    mergeButton.GetComponent<MergeController>().gridBLevel = 0;
                    break;
            }
        }
        else if (count == 1)
        {
            Debug.Log($"{GridNum}번 조합 그리드 차있음");
            myCard = transform.GetChild(0).gameObject;
            string myCardId = myCard.GetComponent<CardDataLoad>().thisCardId;
            isEmpty = false;

            switch (GridNum)
            {
                case 0:
                    mergeButton.GetComponent<MergeController>().gridACardId = myCardId;
                    mergeButton.GetComponent<MergeController>().gridALevel = myCard.GetComponent<CardDataLoad>().thisCardLevel;
                    break;
                case 1:
                    mergeButton.GetComponent<MergeController>().gridBCardId = myCardId;
                    mergeButton.GetComponent<MergeController>().gridBLevel = myCard.GetComponent<CardDataLoad>().thisCardLevel;
                    break;
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedCard = eventData.pointerDrag;

        if (droppedCard != null)
        {
            if(isEmpty == true)
            {
                droppedCard.GetComponent<CardController>().myGrid = gameObject;
                string myCardId = droppedCard.GetComponent<CardDataLoad>().thisCardId;
                Debug.Log(myCardId);
                switch (GridNum)
                {
                    case 0:
                        mergeButton.GetComponent<MergeController>().gridACardId = myCardId;
                        break;
                    case 1:
                        mergeButton.GetComponent<MergeController>().gridBCardId = myCardId;
                        break;
                }
                droppedCard.GetComponent<CardController>().isHolding = false;
                droppedCard.GetComponent<CardDataLoad>().IsHolding(false);
                Managers.Deck.nowHold--;
            }
        }
    }
}