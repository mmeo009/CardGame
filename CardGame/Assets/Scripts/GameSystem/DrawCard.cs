using DG.Tweening;
using System.Linq;
using UnityEngine;

public class DrawCard : GenericSingleton<DrawCard>
{
    public GameObject cardPrefab;
    public Transform[] cardGrids;
    public Transform[] mergeGrids;
    public Transform deckUi;
    public int gridNum = 0;

    public void TransformChack()
    {
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
            // ī�� �׸������ ã�ƿͼ� GridNum ������� �����ؼ� ����
            GameObject[] cardGridGo = GameObject.FindGameObjectsWithTag("Grid").OrderBy(obj => obj.GetComponent<GridIndex>().GridNum).ToArray();

            if (cardGridGo != null)
            {
                // ������� Ʈ�������� ����
                cardGrids = cardGridGo.Select(obj => obj.transform).ToArray();
            }
            else
            {
                // cardGrids�� �������� ���� ���
                Debug.Log("ī�� �׸��尡(����) �������� �ʽ��ϴ�.");
            }
            // ī�� �׸������ ã�ƿͼ� GridNum ������� �����ؼ� ����
            GameObject[] mergeGridGo = GameObject.FindGameObjectsWithTag("Merge").OrderBy(obj => obj.GetComponent<MergeGrid>().GridNum).ToArray();

            if (cardGridGo != null)
            {
                // ������� Ʈ�������� ����
                mergeGrids = mergeGridGo.Select(obj => obj.transform).ToArray();
                Debug.Log(mergeGrids);
            }
            else
            {
                // cardGrids�� �������� ���� ���
                Debug.Log("���� �׸��尡(����) �������� �ʽ��ϴ�.");
            }
        }
        else
        {

        }

        if (cardPrefab == null)
        {
            cardPrefab = Resources.Load<GameObject>("Prefabs/Card");
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
                Transform targetGrid = cardGrids[i];
                // ī�� �׸��尡 á���� üũ
                if (cardGrids[i].GetComponent<GridIndex>().isEmpty == true)
                {
                    // ī�� ������ ����
                    GameObject newCard = Instantiate(cardPrefab, deckUi.position, Quaternion.identity);
                    // ī���� ������ �ҷ����� ���� ī�忡 ���� �Է� 
                    newCard.GetComponent<CardDataLoad>().FindChilds(newCard);
                    // ���� ����ִ� ī���� �Ѱ����� �����Ͽ� ī���� ���̵� �ҷ��� �����տ� �־���
                    newCard.GetComponent<CardDataLoad>().PickCardAndIdFromDeck();
                    // ������ ī�带 ĵ������ ����
                    newCard.transform.SetParent(GameObject.Find("Canvas").transform);
                    // cardGrid�� �̵�
                    CardMoveToGrid(newCard, time, targetGrid);
                }
                else
                {
                    Debug.Log($"{cardGrids[i].GetComponent<GridIndex>().GridNum} �� �׸��忡 �̹� ī�尡 �ֽ��ϴ�.");
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
                Transform targetGrid = cardGrids[gridNum];
                // ī�� �׸��尡 á���� üũ
                if (cardGrids[gridNum].GetComponent<GridIndex>().isEmpty == true)
                {
                    // ī�� ������ ����
                    GameObject newCard = Instantiate(cardPrefab, deckUi.position, Quaternion.identity);
                    // ī���� ������ �ҷ����� ���� ī�忡 ���� �Է� 
                    newCard.GetComponent<CardDataLoad>().FindChilds(newCard);
                    // ���� ����ִ� ī���� �Ѱ����� �����Ͽ� ī���� ���̵� �ҷ��� �����տ� �־���
                    newCard.GetComponent<CardDataLoad>().PickCardAndIdFromDeck();
                    // ������ ī�带 ĵ������ ����
                    newCard.transform.SetParent(GameObject.Find("Canvas").transform);
                    // cardGrid�� �̵�
                    newCard.transform.DOMove(targetGrid.position, time).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        // cardGrid�� �ڽ����� ����
                        newCard.transform.SetParent(targetGrid);
                        // ī�� �׸��带 ã�� ī�忡 ����
                        newCard.GetComponent<CardController>().ChackMyGrid();
                        if (targetGrid.GetComponent<GridIndex>() != null)
                        {
                            targetGrid.GetComponent<GridIndex>().ISEmpty();
                        }
                    });
                    CardMoveToGrid(newCard, time, targetGrid, 1);
                }
                else
                {
                    Debug.Log($"{cardGrids[gridNum].GetComponent<GridIndex>().GridNum} �� �׸��忡 �̹� ī�尡 �ֽ��ϴ�.");
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
    public void CreateCardFromNothing(string id)
    {
        float time = 0.5f;
        for (int i = 0; i < cardGrids.Length; i++)
        {
            // ���� �ݺ����� �ε����� ����
            Transform targetGrid = cardGrids[i];
            // ī�� �׸��尡 á���� üũ
            if (cardGrids[i].GetComponent<GridIndex>().isEmpty == true)
            {
                if (mergeGrids[0].GetComponent<MergeGrid>().myCard.gameObject != null)
                {
                    GameObject cardA;
                    cardA = mergeGrids[0].GetComponent<MergeGrid>().myCard.gameObject;
                    cardA.transform.SetParent(GameObject.Find("Canvas").transform);
                    mergeGrids[0].GetComponent<MergeGrid>().ISEmpty();
                    Destroy(cardA);
                }
                if (mergeGrids[1].GetComponent<MergeGrid>().myCard.gameObject != null)
                {
                    GameObject cardB;
                    cardB = mergeGrids[1].GetComponent<MergeGrid>().myCard.gameObject;
                    cardB.transform.SetParent(GameObject.Find("Canvas").transform);
                    mergeGrids[1].GetComponent<MergeGrid>().ISEmpty();
                    Destroy(cardB);
                }
                // ī�� ������ ����
                GameObject newCard = Instantiate(cardPrefab, mergeGrids[1].position, Quaternion.identity);
                // ī���� ������ �ҷ����� ���� ī�忡 ���� �Է� 
                newCard.GetComponent<CardDataLoad>().FindChilds(newCard);
                // id�� �����ͺ��̽��� �Ѱ����� �����Ͽ� ī���� ���̵� �ҷ��� �����տ� �־���
                newCard.GetComponent<CardDataLoad>().PickCardIdFromDataBase(id);
                // ������ ī�带 ĵ������ ����
                newCard.transform.SetParent(GameObject.Find("Canvas").transform);
                // cardGrid�� �̵�
                CardMoveToGrid(newCard, time, targetGrid, 2);
                break;
            }
            else
            {
                Debug.Log($"{cardGrids[i].GetComponent<GridIndex>().GridNum} �� �׸��忡 �̹� ī�尡 �ֽ��ϴ�.");
            }
        }
    }


    private void CardMoveToGrid(GameObject newCard, float time, Transform targetGrid, int type = 0)
    {
        newCard.transform.DOMove(targetGrid.position, time).SetEase(Ease.Linear).OnComplete(() =>
        {
            // cardGrid�� �ڽ����� ����
            newCard.transform.SetParent(targetGrid);
            // ī�� �׸��带 ã�� ī�忡 ����
            newCard.GetComponent<CardController>().ChackMyGrid();
            if (targetGrid.GetComponent<GridIndex>() != null)
            {
                targetGrid.GetComponent<GridIndex>().ISEmpty();
            }
            else
            {
                targetGrid.GetComponent<MergeGrid>().ISEmpty();
            }
            if(type == 3)
            {
                CardInToDeck();
            }
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
        GameObject cardA = mergeGrids[0].GetComponent<MergeGrid>().myCard;
        GameObject cardB = mergeGrids[1].GetComponent<MergeGrid>().myCard;

        if (cardA != null && cardB != null)
        {
            for (int i = 0; i < cardGrids.Length; i++)
            {
                // ���� �ݺ����� �ε����� ����
                Transform targetGridA = cardGrids[i];
                // ī�� �׸��尡 á���� üũ
                if (cardGrids[i].GetComponent<GridIndex>().isEmpty == true)
                {
                    cardA.transform.SetParent(GameObject.Find("Canvas").transform);
                    mergeGrids[0].GetComponent<MergeGrid>().ISEmpty();
                    CardMoveToGrid(cardA, 0.5f, targetGridA, 2);
                    for (int j = i; j < cardGrids.Length; j++)
                    {
                        Transform targetGridB = cardGrids[j];
                        if (cardGrids[j].GetComponent<GridIndex>().isEmpty == true)
                        {
                            cardB.transform.SetParent(GameObject.Find("Canvas").transform);
                            mergeGrids[1].GetComponent<MergeGrid>().ISEmpty();
                            CardMoveToGrid(cardB, 0.5f, targetGridB, 3);
                            break;
                        }
                    }
                }
            }
        }
        else if (cardA != null || cardB != null)
        {
            GameObject card;
            int num;
            if (cardA == null)
            {
                card = cardB;
                num = 1;
            }
            else
            {
                card = cardA;
                num = 0;
            }
            for (int i = 0; i < cardGrids.Length; i++)
            {
                // ���� �ݺ����� �ε����� ����
                Transform targetGrid = cardGrids[i];
                // ī�� �׸��尡 á���� üũ
                if (cardGrids[i].GetComponent<GridIndex>().isEmpty == true)
                {
                    card.transform.SetParent(GameObject.Find("Canvas").transform);
                    mergeGrids[num].GetComponent<MergeGrid>().ISEmpty();
                    CardMoveToGrid(card, 0.5f, targetGrid, 3);
                    break;
                }
            }
        }
        else if(cardA == null && cardB == null)
        {
            CardInToDeck();
        }
    }
    public void CardInToDeck()
    {
        GameObject[] cards;
        cards = new GameObject[cardGrids.Length];
        for (int i = 0; i < cardGrids.Length; i++)
        {
            if (cardGrids[i].GetComponent<GridIndex>().myCard != null)
            {
                cards[i] = cardGrids[i].GetComponent<GridIndex>().myCard;
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
                if (cards[j].GetComponent<CardController>().isHolding == false)
                {
                    int gridNum = j;
                    GameObject cardToGoHome = cards[j]; // ī�带 Ŭ�������� ����ϱ� ���� ������ �Ҵ�
                    cardToGoHome.transform.SetParent(GameObject.Find("Canvas").transform);
                    cardToGoHome.transform.DOMove(deckUi.position, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        cardGrids[gridNum].GetComponent<GridIndex>().ISEmpty();
                        Managers.Deck.AddCardToDeckById(cardToGoHome.GetComponent<CardDataLoad>().thisCardId, 1);
                        Destroy(cardToGoHome); // ������ ����Ͽ� ī�� ����
                    });
                }
            }
        }
    }
}