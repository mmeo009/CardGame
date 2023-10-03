using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CardController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private RectTransform rectTransform;

    public bool isDragging = false;
    public float moreInfoTime = 0.8f;
    public float smoothingDuration = 0.2f;
    public bool onMouse = false;
    public bool isEnlargedCardCreated = false;
    public bool isHolding = false;
    public GameObject myGrid;
    public GameObject enlargedCardPrefab;
    public string id;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if(myGrid.GetComponent<MergeGrid>() == null)
            {
                if (isHolding == false)
                {
                    if (Managers.Deck.nowHold < Managers.Deck.maxHold)
                    {
                        Managers.Deck.nowHold++;
                        isHolding = true;
                    }
                    else
                    {
                        Debug.Log($"홀드는 {Managers.Deck.maxHold}개 까지 가능해");
                    }
                }
                else
                {
                    Managers.Deck.nowHold--;
                    isHolding = false;
                }
                this.gameObject.GetComponent<CardDataLoad>().IsHolding(isHolding);
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(isDragging == false)
        {
            onMouse = true;
        }
        if(isDragging == true)
        {
            onMouse = false;
            moreInfoTime = 0.8f;
            DestroyEnlargedCard();
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        onMouse = false;
        moreInfoTime = 0.8f;
        DestroyEnlargedCard();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(Input.GetMouseButton(0))
        {
            isDragging = true;
            this.transform.SetParent(transform.root);
            this.transform.SetAsLastSibling();
            if (myGrid.GetComponent<GridIndex>() != null)
            {
                myGrid.GetComponent<GridIndex>().ISEmpty();
            }
            else
            {
                myGrid.GetComponent<MergeGrid>().ISEmpty();
            }
            this.GetComponent<Image>().raycastTarget = false;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging == true)
        {
            transform.position = Input.mousePosition;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDragging == true)
        {
            isDragging = false;
            this.transform.SetParent(myGrid.transform);
            if (myGrid.GetComponent<GridIndex>() != null)
            {
                myGrid.GetComponent<GridIndex>().ISEmpty();
            }
            else
            {
                myGrid.GetComponent<MergeGrid>().ISEmpty();
            }
            this.GetComponent<Image>().raycastTarget = true;
        }
    }
    private void CreateEnlargedCard()
    {
        Vector3 pos = new Vector3(853, 361.5f, 0);
        id = this.GetComponent<CardDataLoad>().thisCardId;
        if (enlargedCardPrefab == null)
        {
            enlargedCardPrefab = Resources.Load<GameObject>("Prefabs/EnlargedCard");
            GameObject newCard = Instantiate(enlargedCardPrefab, pos, Quaternion.identity);
            newCard.GetComponent<CardDataLoad>().FindChilds(newCard);
            newCard.GetComponent<CardDataLoad>().LoadCardData(id);
            newCard.transform.SetParent(GameObject.Find("Canvas").transform);
        }
        else
        {
            GameObject newCard = Instantiate(enlargedCardPrefab, pos, Quaternion.identity);
            newCard.GetComponent<CardDataLoad>().FindChilds(newCard);
            newCard.GetComponent<CardDataLoad>().LoadCardData(id);
            newCard.transform.SetParent(GameObject.Find("Canvas").transform);
        }
        isEnlargedCardCreated = true;
    }
    private void DestroyEnlargedCard()
    {
        GameObject en = GameObject.FindWithTag("EnlargedCard");
        if(en != null)
        {
            Destroy(en);
            isEnlargedCardCreated = false;
        }
        else
        {
            if(isEnlargedCardCreated == true)
            {
                isEnlargedCardCreated = false;
            }
        }
    }
    public void ChackMyGrid()
    {
        myGrid = this.gameObject.transform.parent.gameObject;
        if (myGrid.GetComponent<GridIndex>() != null)
        {
            myGrid.GetComponent<GridIndex>().ISEmpty();
        }
        else
        {
            myGrid.GetComponent<MergeGrid>().ISEmpty();
        }
    }

    private void Update()
    {
        if(onMouse == true)
        {
            if(moreInfoTime > 0)
            {
                moreInfoTime -= Time.deltaTime;
            }
            else if(moreInfoTime <= 0 &&isEnlargedCardCreated == false)
            {
                CreateEnlargedCard();
            }
        }
    }

}