using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CardController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private RectTransform rectTransform;
    private Vector2 offset;
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
        }
        offset = rectTransform.anchoredPosition - eventData.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            rectTransform.anchoredPosition = eventData.position + offset;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            isDragging = false;

            // 드래그가 끝났을 때 부드럽게 위치 조정
            Vector2 targetPosition = ClampToCanvas(rectTransform.anchoredPosition);
            rectTransform.DOAnchorPos(targetPosition, smoothingDuration);
        }
    }
    private Vector2 ClampToCanvas(Vector2 position)
    {
        // 캔버스 영역 내에서 움직이도록 제한
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();
            Vector2 minPosition = canvasRect.rect.min - rectTransform.rect.min;
            Vector2 maxPosition = canvasRect.rect.max - rectTransform.rect.max;

            position.x = Mathf.Clamp(position.x, minPosition.x, maxPosition.x);
            position.y = Mathf.Clamp(position.y, minPosition.y, maxPosition.y);
        }

        return position;
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