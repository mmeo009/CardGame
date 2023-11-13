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
                // DeckUi�� ������ ��� Ʈ������ ���� �־���
                deckUi = GameObject.Find("DeckUI").transform;
            }
            else
            {
                // DeckUi�� �������� ���� ���
                Debug.Log("DeckUi�� �������� �ʽ��ϴ�.");
            }
            // ī�� �׸��� ���� ã�ƿ�
            SlotIndex[] slots = FindObjectsOfType<SlotIndex>();
            // �⺻ ī�� �׸��� ���� ����
            SlotIndex[] defaultSlots = slots.Where(slot => slot.type == SlotIndex.SlotType.Default).OrderBy(slot => slot.gridNum).ToArray();
            // ���� ī�� �׸��� ���� ����
            SlotIndex[] mergeSlots = slots.Where(slot => slot.type == SlotIndex.SlotType.Merge).OrderBy(slot => slot.gridNum).ToArray();
            if (defaultSlots != null)
            {
                // ������� Ʈ�������� ����
                cardGrids = defaultSlots.Select(obj => obj).ToArray();
            }
            else
            {
                // cardGrids�� �������� ���� ���
                Debug.Log("ī�� �׸��尡(����) �������� �ʽ��ϴ�.");
            }
            if (mergeSlots != null)
            {
                // ������� Ʈ�������� ����
                mergeGrids = mergeSlots.Select(obj => obj).ToArray();
            }
            else
            {
                // cardGrids�� �������� ���� ���
                Debug.Log("���� �׸��尡(����) �������� �ʽ��ϴ�.");
            }
        }
    }
    public void CreateSomeCards()
    {
        if (deckUi != null)
        {
            for (int i = 0; i < cardGrids.Length; i++)
            {
                // ���� �ݺ����� �ε����� ����
                SlotIndex targetGrid = cardGrids[i];
                if (cardGrids[gridNum].GetComponent<SlotIndex>().state == SlotIndex.SlotState.Empty)
                {
                    // ī�� ������ ����
                    GameObject newCard = Instantiate(cardPrefab, deckUi.position, Quaternion.identity);
                    // ī���� ������ �ҷ����� ���� ī�忡 ���� �Է� 
                    newCard.GetComponent<CardDataLoad>().FindChilds(newCard);
                    // ���� ����ִ� ī���� �Ѱ����� �����Ͽ� ī���� ���̵� �ҷ��� �����տ� �־���
                    newCard.GetComponent<CardDataLoad>().PickCardAndIdFromDeck();
                    // cardGrid�� �̵�
                    CardMoveToGrid(newCard, 0.3f, targetGrid);
                    break;
                }
                else
                {
                    Debug.Log($"{cardGrids[i].GetComponent<SlotIndex>().gridNum} �� �׸��忡 �̹� ī�尡 �ֽ��ϴ�.");
                }
            }
        }

    }

    public void CreateCardAllAtOnce()
    {

        float time = 5.0f / cardGrids.Length;

        // deckUi�� ���� �����ϸ� deckUi�� ��ġ���� ����
        if (deckUi != null)
        {
            for (int i = 0; i < cardGrids.Length; i++)
            {

                // ���� �ݺ����� �ε����� ����
                SlotIndex targetGrid = cardGrids[i];
                // ī�� �׸��尡 á���� üũ
                if (cardGrids[gridNum].GetComponent<SlotIndex>().state == SlotIndex.SlotState.Empty)
                {
                    // ī�� ������ ����
                    GameObject newCard = Instantiate(cardPrefab, deckUi.position, Quaternion.identity);
                    // ī���� ������ �ҷ����� ���� ī�忡 ���� �Է� 
                    newCard.GetComponent<CardDataLoad>().FindChilds(newCard);
                    // ���� ����ִ� ī���� �Ѱ����� �����Ͽ� ī���� ���̵� �ҷ��� �����տ� �־���
                    newCard.GetComponent<CardDataLoad>().PickCardAndIdFromDeck();
                    // ī�带 �� ���� �ø�
                    newCard.GetComponent<CardDataLoad>().InDragging();
                    // cardGrid�� �̵�
                    newCard.transform.DOMove(targetGrid.transform.position, time).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        // �׸��忡 ī�带 ����
                        targetGrid.GetComponent<SlotIndex>().GetCardIntoThisSlot(newCard.GetComponent<CardDataLoad>());
                        // ī�� �׸��带 ã�� ī�忡 ����
                        newCard.GetComponent<CardDataLoad>().mySlot = targetGrid.GetComponent<SlotIndex>();
                        // ī�带 ���� ��ġ�� ����
                        newCard.GetComponent<CardDataLoad>().EndDragging();
                    });
                    SoundData.Instance.PlaySound("Card_Draw");
                    // ����� ���
                    CardMoveToGrid(newCard, time, targetGrid);
                }
                else
                {
                    Debug.Log($"{cardGrids[i].GetComponent<SlotIndex>().gridNum} �� �׸��忡 �̹� ī�尡 �ֽ��ϴ�.");
                }
            }
        }
        else
        {
            // DeckUi�� �������� ���� ���
            Debug.Log("DeckUi�� �������� �ʽ��ϴ�.");
        }

    }
    public void CreateCardOneAtTheTime()
    {

        float time = 5.0f / cardGrids.Length - 0.5f;
        // deckUi�� ���� �����ϸ� deckUi�� ��ġ���� ����
        if (deckUi != null)
        {
            if (gridNum < cardGrids.Length)
            {
                // ���� �ݺ����� �ε����� ����
                SlotIndex targetGrid = cardGrids[gridNum];
                // ī�� �׸��尡 á���� üũ
                if (cardGrids[gridNum].GetComponent<SlotIndex>().state == SlotIndex.SlotState.Empty)
                {
                    // ī�� ������ ����
                    GameObject newCard = Instantiate(cardPrefab, deckUi.position, Quaternion.identity);
                    // ī���� ������ �ҷ����� ���� ī�忡 ���� �Է� 
                    newCard.GetComponent<CardDataLoad>().FindChilds(newCard);
                    // ���� ����ִ� ī���� �Ѱ����� �����Ͽ� ī���� ���̵� �ҷ��� �����տ� �־���
                    newCard.GetComponent<CardDataLoad>().PickCardAndIdFromDeck();
                    // ī�带 �� ���� �ø�
                    newCard.GetComponent<CardDataLoad>().InDragging();
                    // cardGrid�� �̵�
                    newCard.transform.DOMove(targetGrid.transform.position, time).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        // �׸��忡 ī�带 ����
                        targetGrid.GetComponent<SlotIndex>().GetCardIntoThisSlot(newCard.GetComponent<CardDataLoad>());
                        // ī�� �׸��带 ã�� ī�忡 ����
                        newCard.GetComponent<CardDataLoad>().mySlot = targetGrid.GetComponent<SlotIndex>();
                        // ī�带 ���� ��ġ�� ����
                        newCard.GetComponent<CardDataLoad>().EndDragging();
                    });
                    SoundData.Instance.PlaySound("Card_Draw");
                    // ����� ���
                    CardMoveToGrid(newCard, time, targetGrid, 1);
                }
                else
                {
                    Debug.Log($"{cardGrids[gridNum].GetComponent<SlotIndex>().gridNum} �� �׸��忡 �̹� ī�尡 �ֽ��ϴ�.");
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
            // DeckUi�� �������� ���� ���
            Debug.Log("DeckUi�� �������� �ʽ��ϴ�.");
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
                // ī�� ������ ����
                GameObject newCard = Instantiate(cardPrefab, mergeGrids[1].transform.position, Quaternion.identity);
                // ī���� ������ �ҷ����� ���� ī�忡 ���� �Է� 
                newCard.GetComponent<CardDataLoad>().FindChilds(newCard);
                // id�� �����ͺ��̽��� �Ѱ����� �����Ͽ� ī���� ���̵� �ҷ��� �����տ� �־���
                if (level != 0)
                {
                    newCard.GetComponent<CardDataLoad>().PickCardIdFromDataBase(id, level);
                }
                else
                {
                    newCard.GetComponent<CardDataLoad>().PickCardIdFromDataBase(id);
                }
                // cardGrid�� �̵�
                CardMoveToGrid(newCard, time, emptyslot, 2);
                break;
            }
            else
            {
                Debug.Log($"�׸��忡 �̹� ī�尡 �ֽ��ϴ�.");
            }
        }
    }
    private void CardMoveToGrid(GameObject newCard, float time, SlotIndex targetGrid, int type = 0)
    {
        newCard.GetComponent<CardDataLoad>().InDragging();
        newCard.transform.DOMove(targetGrid.transform.position, time).SetEase(Ease.Linear).OnComplete(() =>
        {
            // ī�� �׸��忡 ī�带 ����
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
                    GameObject cardToGoHome = cards[j]; // ī�带 Ŭ�������� ����ϱ� ���� ������ �Ҵ�
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
                        // ����� ���
                        Destroy(cardToGoHome); // ������ ����Ͽ� ī�� ����
                    });
                }
            }
        }
    }
}