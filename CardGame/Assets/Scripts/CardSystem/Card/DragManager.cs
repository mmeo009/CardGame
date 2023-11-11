using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    public Vector3 _target;
    public CardDataLoad carryingCard;
    public bool isEnlargedCardCreated = false;

    private GameObject enlargedCardPrefab;


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //���콺 ���� ��
        {
            SendRayCast();
        }

        if (Input.GetMouseButton(0) && carryingCard)    //��� �̵���ų ��
        {
            OnItemSelected();
        }

        if (Input.GetMouseButtonUp(0))  //���콺 ��ư�� ������
        {
            SendRayCast();
        }
    }
    void SendRayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            var slot = hit.transform.GetComponent<SlotIndex>();
            if(slot.state == SlotIndex.SlotState.Full && carryingCard == null)
            {
                slot.cardObject.transform.SetParent(null);
                carryingCard = slot.cardObject;
                slot.ChangeState(SlotIndex.SlotState.Empty);
            }
            else if(slot.state == SlotIndex.SlotState.Empty && carryingCard != null)
            {
                if(slot.type == SlotIndex.SlotType.Default)
                {
                    slot.GetCardIntoThisSlot(carryingCard);
                    carryingCard = null;
                }
            }
            else if(slot.state == SlotIndex.SlotState.Full && carryingCard != null)
            {
                if(slot.type != SlotIndex.SlotType.Monster)
                {
                    if (!carryingCard) return;
                    OnCarryFail();
                }
            }
            else
            {
                if (!carryingCard) return;
                OnCarryFail();
            }
        }

    }
    void OnItemSelected()
    {   //�������� �����ϰ� ���콺 ��ġ�� �̵� 
        _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);  //��ǥ��ȯ
        _target.z = 0;
        var delta = 10 * Time.deltaTime;
        delta *= Vector3.Distance(transform.position, _target);
        carryingCard.transform.position = Vector3.MoveTowards(carryingCard.transform.position, _target, delta);
    }

    void OnCarryFail()
    {
        carryingCard.GetComponent<CardController>().myGrid.GetComponent<SlotIndex>().GetCardIntoThisSlot(carryingCard);
        carryingCard = null;
    }

    private void CreateEnlargedCard()
    {
        Vector3 pos = GameObject.FindGameObjectWithTag("EnlargedPosition").transform.position;
        string id = carryingCard.GetComponent<CardDataLoad>().thisCardId;
        int level = carryingCard.GetComponent<CardDataLoad>().thisCardLevel;
        if (enlargedCardPrefab == null)
        {
            enlargedCardPrefab = Resources.Load<GameObject>("Prefabs/EnlargedCard");
            GameObject newCard = Instantiate(enlargedCardPrefab, pos, Quaternion.identity);
            newCard.transform.localScale = Vector3.one;
            newCard.GetComponent<RectTransform>().sizeDelta = new Vector3(180, 320);
            newCard.GetComponent<CardDataLoad>().FindChilds(newCard);
            newCard.GetComponent<CardDataLoad>().thisCardLevel = level;
            newCard.GetComponent<CardDataLoad>().LoadCardData(id);
            newCard.transform.SetParent(GameObject.Find("Canvas").transform);

        }
        else
        {
            GameObject newCard = Instantiate(enlargedCardPrefab, pos, Quaternion.identity);
            newCard.transform.localScale = Vector3.one;
            newCard.GetComponent<RectTransform>().sizeDelta = new Vector3(180, 320);
            newCard.GetComponent<CardDataLoad>().FindChilds(newCard);
            newCard.GetComponent<CardDataLoad>().thisCardLevel = level;
            newCard.GetComponent<CardDataLoad>().LoadCardData(id);
            newCard.transform.SetParent(GameObject.Find("Canvas").transform);

        }
        isEnlargedCardCreated = true;
    }
}
