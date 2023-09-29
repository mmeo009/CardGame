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
                // DeckUi가 존재할 경우 트랜스폼 값을 넣어줌
                deckUi = GameObject.Find("@DeckUI").transform;
            }
            else
            {
                // DeckUi가 존재하지 않을 경우
                Debug.Log("DeckUi가 존재하지 않습니다.");
            }
            // 카드 그리드들을 찾아와서 GridNum 순서대로 정렬해서 넣음
            GameObject[] cardGridGo = GameObject.FindGameObjectsWithTag("Grid").OrderBy(obj => obj.GetComponent<GridIndex>().GridNum).ToArray();

            if(cardGridGo != null)
            {
                // 순서대로 트랜스폼에 넣음
                cardGrids = cardGridGo.Select(obj => obj.transform).ToArray();
            }
            else
            {
                // cardGrids가 존재하지 않을 경우
                Debug.Log("카드 그리드가(들이) 존재하지 않습니다.");
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

        // deckUi가 씬에 존재하면 deckUi의 위치에서 생성
        if (deckUi != null)
        {
            for (int i = 0; i < cardGrids.Length; i++)
            {

                // 현재 반복중인 인덱스를 저장
                Transform targetGrid = cardGrids[i];
                // 카드 그리드가 찼는지 체크
                if (cardGrids[i].GetComponent<GridIndex>().isEmpty == true)
                {
                    // 카드 프리팹 생성
                    GameObject newCard = Instantiate(cardPrefab, deckUi.position, Quaternion.identity);
                    // 카드의 정보를 불러오기 위해 카드에 값을 입력 
                    newCard.GetComponent<CardDataLoad>().FindChilds(newCard);
                    // 덱에 들어있는 카드중 한가지를 선택하여 카드의 아이디를 불러와 프리팹에 넣어줌
                    newCard.GetComponent<CardDataLoad>().PickCardAndIdFromDeck();
                    // 생성된 카드를 캔버스에 넣음
                    newCard.transform.SetParent(GameObject.Find("Canvas").transform);
                    // cardGrid로 이동
                    CardMoveToGrid(newCard, time, targetGrid);
                }
                else 
                {
                    Debug.Log($"{cardGrids[i].GetComponent<GridIndex>().GridNum} 번 그리드에 이미 카드가 있습니다.");
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
                Transform targetGrid = cardGrids[gridNum];
                // 카드 그리드가 찼는지 체크
                if (cardGrids[gridNum].GetComponent<GridIndex>().isEmpty == true)
                {
                    // 카드 프리팹 생성
                    GameObject newCard = Instantiate(cardPrefab, deckUi.position, Quaternion.identity);
                    // 카드의 정보를 불러오기 위해 카드에 값을 입력 
                    newCard.GetComponent<CardDataLoad>().FindChilds(newCard);
                    // 덱에 들어있는 카드중 한가지를 선택하여 카드의 아이디를 불러와 프리팹에 넣어줌
                    newCard.GetComponent<CardDataLoad>().PickCardAndIdFromDeck();
                    // 생성된 카드를 캔버스에 넣음
                    newCard.transform.SetParent(GameObject.Find("Canvas").transform);
                    // cardGrid로 이동
                    newCard.transform.DOMove(targetGrid.position, time).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        // cardGrid의 자식으로 설정
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
                    Debug.Log($"{cardGrids[gridNum].GetComponent<GridIndex>().GridNum} 번 그리드에 이미 카드가 있습니다.");
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

    private void CardMoveToGrid(GameObject newCard, float time, Transform targetGrid,int type = 0)
    {
            newCard.transform.DOMove(targetGrid.position, time).SetEase(Ease.Linear).OnComplete(() =>
            {
                // cardGrid의 자식으로 설정
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