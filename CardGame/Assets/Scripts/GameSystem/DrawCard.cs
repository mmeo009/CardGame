using UnityEngine;
using System.Linq;
using DG.Tweening;

public class DrawCard : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform[] cardGrids;
    public Transform deckUi;
    public int gridNum = 0;
    
    public void TransformChack()
    {
        if (deckUi == null || cardGrids == null)
        {
            if(GameObject.Find("@DeckUI") != null)
            {
                // DeckUi�� ������ ��� Ʈ������ ���� �־���
                deckUi = GameObject.Find("@DeckUI").transform;
            }
            else
            {
                // DeckUi�� �������� ���� ���
                Debug.Log("DeckUi�� �������� �ʽ��ϴ�.");
            }
            // ī�� �׸������ ã�ƿͼ� GridNum ������� �����ؼ� ����
            GameObject[] cardGridGo = GameObject.FindGameObjectsWithTag("Grid").OrderBy(obj => obj.GetComponent<GridIndex>().GridNum).ToArray();

            if(cardGridGo != null)
            {
                // ������� Ʈ�������� ����
                cardGrids = cardGridGo.Select(obj => obj.transform).ToArray();
            }
            else
            {
                // cardGrids�� �������� ���� ���
                Debug.Log("ī�� �׸��尡(����) �������� �ʽ��ϴ�.");
            }
        }
        else
        {
            
        }

        if(cardPrefab == null)
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
                        if (targetGrid.GetComponent<GridIndex>() != null)
                        {
                            targetGrid.GetComponent<GridIndex>().isEmpty = false;
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

    private void CardMoveToGrid(GameObject newCard, float time, Transform targetGrid,int type = 0)
    {
            newCard.transform.DOMove(targetGrid.position, time).SetEase(Ease.Linear).OnComplete(() =>
            {
                // cardGrid�� �ڽ����� ����
                newCard.transform.SetParent(targetGrid);
                if (targetGrid.GetComponent<GridIndex>() != null)
                {
                    targetGrid.GetComponent<GridIndex>().isEmpty = false;
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

    public void CardInToDeck()
    {
    }
}