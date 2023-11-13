using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : GenericSingleton<DragManager>
{
    public Vector3 _target;
    public CardDataLoad carryingCard;

    public bool isEnlargedCardCreated = false;
    private GameObject enlargedCardPrefab;

    public GameObject enlargedCard;

    private float enlargedTimer = 1.0f;


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
        if(!Input.GetMouseButton(0) && !Input.GetMouseButtonDown(0))
        {
            HoldAndEnlargedCard();
        }
        else
        {
            DestroyEnlargedCard();
        }
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
            else if(slot.type != SlotIndex.SlotType.Monster && slot.cardObject)
            {
                if (isEnlargedCardCreated == false)
                {
                    enlargedTimer -= Time.deltaTime;
                    if(enlargedTimer <= 0)
                    {
                        CreateEnlargedCard(slot);
                        enlargedTimer = 1.0f;
                    }
                }
                else if(isEnlargedCardCreated  == true)
                {
                    string id = enlargedCard.GetComponent<CardDataLoad>().thisCardId;
                    
                    if (slot.cardId != id)
                    {
                        DestroyEnlargedCard();
                        CreateEnlargedCard(slot);
                    }
                }
            }
            else
            {
                DestroyEnlargedCard();
            }
        }
        else
        {
            DestroyEnlargedCard();
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
            if (TurnManager.Instance.currentTurn == TurnManager.TurnState.Player)
            {
                if (slot.state == SlotIndex.SlotState.Full && carryingCard == null)
                {
                    slot.cardObject.transform.SetParent(null);
                    carryingCard = slot.cardObject;
                    slot.ChangeState(SlotIndex.SlotState.Empty);
                }
                else if (slot.state == SlotIndex.SlotState.Empty && carryingCard != null)
                {
                    if (slot.type == SlotIndex.SlotType.Default || slot.type == SlotIndex.SlotType.Merge)
                    {
                        slot.GetCardIntoThisSlot(carryingCard);
                        carryingCard = null;
                    }
                    else if (slot.type == SlotIndex.SlotType.Monster)
                    {
                        carryingCard.GetComponent<CardUse>().UsingCard();
                        carryingCard.mySlot.ChangeState(SlotIndex.SlotState.Empty);
                        PlayerData.Instance.ShowMyInfo();
                        carryingCard = null;
                    }
                }
                else if (slot.state == SlotIndex.SlotState.Full && carryingCard != null)
                {
                    if (slot.type != SlotIndex.SlotType.Monster)
                    {
                        OnCarryFail();
                    }
                    else if (slot.type == SlotIndex.SlotType.Monster)
                    {
                        carryingCard.GetComponent<CardUse>().UsingCard();
                        carryingCard.mySlot.ChangeState(SlotIndex.SlotState.Empty);
                        PlayerData.Instance.ShowMyInfo();
                    }
                }
                else
                {
                    if (!carryingCard) return;
                    OnCarryFail();
                }
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

    private void CreateEnlargedCard(SlotIndex slot)
    {
        Vector3 pos = GameObject.FindGameObjectWithTag("EnlargedPosition").transform.position;
        string id = slot.cardObject.thisCardId;
        int level = slot.cardObject.thisCardLevel;
        if (enlargedCardPrefab == null)
        {
            enlargedCardPrefab = Resources.Load<GameObject>("Prefabs/Card");
            GameObject newCard = Instantiate(enlargedCardPrefab, pos, Quaternion.identity);
            Destroy(newCard?.GetComponent<CardUse>());
            newCard.transform.localScale = new Vector3(2,2,2);
            newCard.GetComponent<CardDataLoad>().FindChilds(newCard);
            newCard.GetComponent<CardDataLoad>().thisCardLevel = level;
            newCard.GetComponent<CardDataLoad>().LoadCardData(id);
            enlargedCard = newCard;

        }
        else
        {
            GameObject newCard = Instantiate(enlargedCardPrefab, pos, Quaternion.identity);
            Destroy(newCard?.GetComponent<CardUse>());
            newCard.transform.localScale = new Vector3(2, 2, 2);
            newCard.GetComponent<CardDataLoad>().FindChilds(newCard);
            newCard.GetComponent<CardDataLoad>().thisCardLevel = level;
            newCard.GetComponent<CardDataLoad>().LoadCardData(id);
            enlargedCard = newCard;
        }
        isEnlargedCardCreated = true;
    }
    private void DestroyEnlargedCard()
    {
        if (enlargedCard != null)
        {
            Destroy(enlargedCard);
            enlargedCard = null;
            isEnlargedCardCreated = false;
        }
        else
        {
            if (isEnlargedCardCreated == true)
            {
                isEnlargedCardCreated = false;
            }
        }
    }
}
