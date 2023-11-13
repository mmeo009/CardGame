using DG.Tweening;
using System.Linq;
using UnityEngine;

public class DrawCard : GenericSingleton<DrawCard>
{
    public GameObject cardPrefab;
    public SlotIndex[] cardGrids;
    public SlotIndex[] mergeGrids;
    public Transform deckUi;
    public int gridNum = 0;
    private AudioClip draw, shuffle;

    public void TransformChack()
    {
        if (cardPrefab == null)
        {
            cardPrefab = Resources.Load<GameObject>("Prefabs/Card");
        }
        if (deckUi == null || cardGrids == null)
        {
            if (GameObject.Find("DeckUI") != null)
            {
                // DeckUi가 존재할 경우 트랜스폼 값을 넣어줌
                deckUi = GameObject.Find("DeckUI").transform;
            }
            else
            {
                // DeckUi가 존재하지 않을 경우
                Debug.Log("DeckUi가 존재하지 않습니다.");
            }
            // 카드 그리드 들을 찾아옴
            SlotIndex[] slots = FindObjectsOfType<SlotIndex>();
            // 기본 카드 그리드 들을 정렬
            SlotIndex[] defaultSlots = slots.Where(slot => slot.type == SlotIndex.SlotType.Default).OrderBy(slot => slot.gridNum).ToArray();
            // 머지 카드 그리드 들을 정렬
            SlotIndex[] mergeSlots = slots.Where(slot => slot.type == SlotIndex.SlotType.Merge).OrderBy(slot => slot.gridNum).ToArray();
            if (defaultSlots != null)
            {
                // 순서대로 트랜스폼에 넣음
                cardGrids = defaultSlots.Select(obj => obj).ToArray();
            }
            else
            {
                // cardGrids가 존재하지 않을 경우
                Debug.Log("카드 그리드가(들이) 존재하지 않습니다.");
            }
            if (mergeSlots != null)
            {
                // 순서대로 트랜스폼에 넣음
                mergeGrids = mergeSlots.Select(obj => obj).ToArray();
            }
            else
            {
                // cardGrids가 존재하지 않을 경우
                Debug.Log("마지 그리드가(들이) 존재하지 않습니다.");
            }
        }
    }
    public void CreateSomeCards()
    {
        if (deckUi != null)
        {
            for (int i = 0; i < cardGrids.Length; i++)
            {
                // 현재 반복중인 인덱스를 저장
                SlotIndex targetGrid = cardGrids[i];
                if (cardGrids[gridNum].GetComponent<SlotIndex>().state == SlotIndex.SlotState.Empty)
                {
                    // 카드 프리팹 생성
                    GameObject newCard = Instantiate(cardPrefab, deckUi.position, Quaternion.identity);
                    // 카드의 정보를 불러오기 위해 카드에 값을 입력 
                    newCard.GetComponent<CardDataLoad>().FindChilds(newCard);
                    // 덱에 들어있는 카드중 한가지를 선택하여 카드의 아이디를 불러와 프리팹에 넣어줌
                    newCard.GetComponent<CardDataLoad>().PickCardAndIdFromDeck();
                    // cardGrid로 이동
                    CardMoveToGrid(newCard, 0.3f, targetGrid);
                    break;
                }
                else
                {
                    Debug.Log($"{cardGrids[i].GetComponent<SlotIndex>().gridNum} 번 그리드에 이미 카드가 있습니다.");
                }
            }
        }

    }

    public void CreateCardAllAtOnce()
    {

        float time = 5.0f / cardGrids.Length;

        // deckUi가 씬에 존재하면 deckUi의 위치에서 생성
        if (deckUi != null)
        {
            for (int i = 0; i < cardGrids.Length; i++)
            {

                // 현재 반복중인 인덱스를 저장
                SlotIndex targetGrid = cardGrids[i];
                // 카드 그리드가 찼는지 체크
                if (cardGrids[gridNum].GetComponent<SlotIndex>().state == SlotIndex.SlotState.Empty)
                {
                    // 카드 프리팹 생성
                    GameObject newCard = Instantiate(cardPrefab, deckUi.position, Quaternion.identity);
                    // 카드의 정보를 불러오기 위해 카드에 값을 입력 
                    newCard.GetComponent<CardDataLoad>().FindChilds(newCard);
                    // 덱에 들어있는 카드중 한가지를 선택하여 카드의 아이디를 불러와 프리팹에 넣어줌
                    newCard.GetComponent<CardDataLoad>().PickCardAndIdFromDeck();
                    // 카드를 멘 위로 올림
                    newCard.GetComponent<CardDataLoad>().InDragging();
                    // cardGrid로 이동
                    newCard.transform.DOMove(targetGrid.transform.position, time).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        // 그리드에 카드를 넣음
                        targetGrid.GetComponent<SlotIndex>().GetCardIntoThisSlot(newCard.GetComponent<CardDataLoad>());
                        // 카드 그리드를 찾아 카드에 넣음
                        newCard.GetComponent<CardDataLoad>().mySlot = targetGrid.GetComponent<SlotIndex>();
                        // 카드를 원래 위치로 내림
                        newCard.GetComponent<CardDataLoad>().EndDragging();
                    });
                    SoundData.Instance.PlaySound("Card_Draw");
                    // 오디오 재생
                    CardMoveToGrid(newCard, time, targetGrid);
                }
                else
                {
                    Debug.Log($"{cardGrids[i].GetComponent<SlotIndex>().gridNum} 번 그리드에 이미 카드가 있습니다.");
                }
            }
        }
        else
        {
            // DeckUi가 존재하지 않을 경우
            Debug.Log("DeckUi가 존재하지 않습니다.");
        }

    }
    public void CreateCardOneAtTheTime()
    {

        float time = 5.0f / cardGrids.Length - 0.5f;
        // deckUi가 씬에 존재하면 deckUi의 위치에서 생성
        if (deckUi != null)
        {
            if (gridNum < cardGrids.Length)
            {
                // 현재 반복중인 인덱스를 저장
                SlotIndex targetGrid = cardGrids[gridNum];
                // 카드 그리드가 찼는지 체크
                if (cardGrids[gridNum].GetComponent<SlotIndex>().state == SlotIndex.SlotState.Empty)
                {
                    // 카드 프리팹 생성
                    GameObject newCard = Instantiate(cardPrefab, deckUi.position, Quaternion.identity);
                    // 카드의 정보를 불러오기 위해 카드에 값을 입력 
                    newCard.GetComponent<CardDataLoad>().FindChilds(newCard);
                    // 덱에 들어있는 카드중 한가지를 선택하여 카드의 아이디를 불러와 프리팹에 넣어줌
                    newCard.GetComponent<CardDataLoad>().PickCardAndIdFromDeck();
                    // 카드를 멘 위로 올림
                    newCard.GetComponent<CardDataLoad>().InDragging();
                    // cardGrid로 이동
                    newCard.transform.DOMove(targetGrid.transform.position, time).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        // 그리드에 카드를 넣음
                        targetGrid.GetComponent<SlotIndex>().GetCardIntoThisSlot(newCard.GetComponent<CardDataLoad>());
                        // 카드 그리드를 찾아 카드에 넣음
                        newCard.GetComponent<CardDataLoad>().mySlot = targetGrid.GetComponent<SlotIndex>();
                        // 카드를 원래 위치로 내림
                        newCard.GetComponent<CardDataLoad>().EndDragging();
                    });
                    SoundData.Instance.PlaySound("Card_Draw");
                    // 오디오 재생
                    CardMoveToGrid(newCard, time, targetGrid, 1);
                }
                else
                {
                    Debug.Log($"{cardGrids[gridNum].GetComponent<SlotIndex>().gridNum} 번 그리드에 이미 카드가 있습니다.");
                    PassGrid();
                }
            }
            else
            {
                gridNum = 0;
            }
        }
        else
        {
            // DeckUi가 존재하지 않을 경우
            Debug.Log("DeckUi가 존재하지 않습니다.");
        }
    }
    public void CreateCardFromNothing(string id, int level = 0)
    {
        float time = 0.5f;
        if (mergeGrids[0].GetComponent<SlotIndex>().state != SlotIndex.SlotState.Empty)
        {
            GameObject cardA;
            cardA = mergeGrids[0].GetComponent<SlotIndex>().cardObject.gameObject;
            cardA.transform.SetParent(null);
            mergeGrids[0].GetComponent<SlotIndex>().ChangeState(SlotIndex.SlotState.Empty);
            Destroy(cardA);
        }
        if (mergeGrids[1].GetComponent<SlotIndex>().state != SlotIndex.SlotState.Empty)
        {
            GameObject cardB;
            cardB = mergeGrids[1].GetComponent<SlotIndex>().cardObject.gameObject;
            cardB.transform.SetParent(null);
            mergeGrids[1].GetComponent<SlotIndex>().ChangeState(SlotIndex.SlotState.Empty);
            Destroy(cardB);
        }
        foreach (SlotIndex slot in cardGrids)
        {
            if (slot.state == SlotIndex.SlotState.Empty)
            {
                SlotIndex emptyslot = slot;
                // 카드 프리팹 생성
                GameObject newCard = Instantiate(cardPrefab, mergeGrids[1].transform.position, Quaternion.identity);
                // 카드의 정보를 불러오기 위해 카드에 값을 입력 
                newCard.GetComponent<CardDataLoad>().FindChilds(newCard);
                // id를 데이터베이스중 한가지를 선택하여 카드의 아이디를 불러와 프리팹에 넣어줌
                if (level != 0)
                {
                    newCard.GetComponent<CardDataLoad>().PickCardIdFromDataBase(id, level);
                }
                else
                {
                    newCard.GetComponent<CardDataLoad>().PickCardIdFromDataBase(id);
                }
                // cardGrid로 이동
                CardMoveToGrid(newCard, time, emptyslot, 2);
                break;
            }
            else
            {
                Debug.Log($"그리드에 이미 카드가 있습니다.");
            }
        }
    }
    private void CardMoveToGrid(GameObject newCard, float time, SlotIndex targetGrid, int type = 0)
    {
        newCard.GetComponent<CardDataLoad>().InDragging();
        newCard.transform.DOMove(targetGrid.transform.position, time).SetEase(Ease.Linear).OnComplete(() =>
        {
            // 카드 그리드에 카드를 넣음
            targetGrid.GetComponent<SlotIndex>().GetCardIntoThisSlot(newCard.GetComponent<CardDataLoad>());
            newCard.GetComponent<CardDataLoad>().mySlot = targetGrid.GetComponent<SlotIndex>();
            newCard.GetComponent<CardDataLoad>().EndDragging();
        });
        if (type == 1)
        {
            gridNum++;
            Invoke("CreateCardOneAtTheTime", 0.5f);
        }
    }
    private void PassGrid()
    {
        gridNum++;
        Invoke("CreateCardOneAtTheTime", 0.5f);
    }
    public void MergeGridToCardGrid()
    {
        foreach (SlotIndex _mergeGrids in mergeGrids)
        {
            CardDataLoad card =  _mergeGrids.cardObject;
            if(card != null)
            {
                for (int i = 0; i < cardGrids.Length; i++)
                {
                    SlotIndex grid = cardGrids[i];
                    if (grid.GetComponent<SlotIndex>().state == SlotIndex.SlotState.Empty)
                    {
                        _mergeGrids.GetComponent<SlotIndex>().ChangeState(SlotIndex.SlotState.Empty);
                        CardMoveToGrid(card.gameObject, 0.3f, grid);
                        break;
                    }
                }
            }
        }
    }
    public void CardInToDeck()
    {
        GameObject[] cards;
        cards = new GameObject[cardGrids.Length];
        for (int i = 0; i < cardGrids.Length; i++)
        {
            if (cardGrids[i].GetComponent<SlotIndex>().cardObject != null)
            {
                cards[i] = cardGrids[i].GetComponent<SlotIndex>().cardObject.gameObject;
                Debug.Log(cards[i].GetComponent<CardDataLoad>().thisCardId);
            }
            else
            {
                cards[i] = null;
            }
        }
        for (int j = 0; j < cards.Length; j++)
        {
            if (cards[j] != null)
            {
                if (cards[j].GetComponent<CardDataLoad>().isHolding == false)
                {
                    int gridNum = j;
                    GameObject cardToGoHome = cards[j]; // 카드를 클로저에서 사용하기 위해 변수에 할당
                    cardToGoHome.transform.SetParent(null);
                    cardToGoHome.GetComponent<CardDataLoad>().InDragging();
                    cardToGoHome.transform.DOMove(deckUi.position, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        string id = cardToGoHome.GetComponent<CardDataLoad>().thisCardId;
                        char lastId = id[id.Length - 1];
                        cardGrids[gridNum].GetComponent<SlotIndex>().ChangeState(SlotIndex.SlotState.Empty);
                        if (lastId == 'I')
                        {
                            Managers.Deck.AddCardToDeckById(id, 1, true, cardToGoHome.GetComponent<CardDataLoad>().thisCardLevel);
                        }
                        else
                        {
                            Managers.Deck.AddCardToDeckById(id, 1);
                        }
                        SoundData.Instance.PlaySound("Card_Shuffle");
                        // 오디오 재생
                        Destroy(cardToGoHome); // 변수를 사용하여 카드 삭제
                    });
                }
            }
        }
    }
}