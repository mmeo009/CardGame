using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    public Vector3 _target;
    public CardDataLoad carryingCard;

    public bool isEnlargedCardCreated = false;
    private GameObject enlargedCardPrefab;

    private float enlargedTimer = 2.0f;


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //마우스 누를 때
        {
            SendRayCast();
        }

        if (Input.GetMouseButton(0) && carryingCard)    //잡고 이동시킬 때
        {
            OnItemSelected();
        }

        if (Input.GetMouseButtonUp(0))  //마우스 버튼을 놓을때
        {
            SendRayCast();
        }
        HoldAndEnlargedCard();
    }
    void HoldAndEnlargedCard()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var slot = hit.transform.GetComponent<SlotIndex>();
            if (slot.type == SlotIndex.SlotType.Default && Input.GetMouseButtonDown(1))
            {
                CardDataLoad card = slot.cardObject;
                if (card.isHolding == false)
                {
                    if (Managers.Deck.nowHold < Managers.Deck.maxHold)
                    {
                        Managers.Deck.nowHold++;
                        card.IsHolding(true);
                    }
                    else
                    {
                        Debug.Log($"홀드는 {Managers.Deck.maxHold}개 까지 가능해");
                    }
                }
                else
                {
                    Managers.Deck.nowHold--;
                    card.IsHolding(false);
                }
            }
            else if(slot.type != SlotIndex.SlotType.Monster)
            {
                if (isEnlargedCardCreated == false)
                {
                    enlargedTimer -= Time.deltaTime;
                    if(enlargedTimer <= 0)
                    {
                        //CreateEnlargedCard();
                        enlargedTimer = 2.0f;
                    }
                }
            }
        }
    }
    void SendRayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red, 0.1f);
            Debug.Log("Hit: " + hit.transform.gameObject.name);

            var slot = hit.transform.GetComponent<SlotIndex>();
            if(slot.state == SlotIndex.SlotState.Full && carryingCard == null)
            {
                slot.cardObject.transform.SetParent(null);
                carryingCard = slot.cardObject;
                slot.ChangeState(SlotIndex.SlotState.Empty);
            }
            else if(slot.state == SlotIndex.SlotState.Empty && carryingCard != null)
            {
                if(slot.type == SlotIndex.SlotType.Default || slot.type == SlotIndex.SlotType.Merge)
                {
                    slot.GetCardIntoThisSlot(carryingCard);
                    carryingCard = null;
                }
            }
            else if(slot.state == SlotIndex.SlotState.Full && carryingCard != null)
            {
                if(slot.type != SlotIndex.SlotType.Monster)
                {
                    OnCarryFail();
                }
            }
            else
            {
                OnCarryFail();
            }
        }
        else
        {
            if (!carryingCard) return;
            OnCarryFail();
        }
    }
    void OnItemSelected()
    {   //아이템을 선택하고 마우스 위치로 이동 
        _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);  //좌표변환
        _target.z = 0;
        var delta = 20 * Time.deltaTime;
        delta *= Vector3.Distance(transform.position, _target);
        carryingCard.transform.position = Vector3.MoveTowards(carryingCard.transform.position, _target, delta);
        carryingCard.InDragging();
    }

    void OnCarryFail()
    {
        CardDataLoad _carryingCard = carryingCard;
        SlotIndex _failIndex = carryingCard.GetComponent<CardDataLoad>().mySlot;
        carryingCard = null;
        _failIndex.GetCardIntoThisSlot(_carryingCard);
        _carryingCard.EndDragging();
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
