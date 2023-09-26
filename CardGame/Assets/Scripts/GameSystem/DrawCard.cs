using UnityEngine;
using System.Linq;
using DG.Tweening;

public class DrawCard : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform[] cardGrids;
    public Transform deckUi;

    
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

    }

    public void CreateCard()
    {

        float time = 2.0f / cardGrids.Length;

        // deckUi가 씬에 존재하면 deckUi의 위치에서 생성
        if (deckUi != null)
        {
            for (int i = 0; i < cardGrids.Length; i++)
            {
                // 카드 프리팹 생성
                GameObject newCard = Instantiate(cardPrefab, deckUi.position, Quaternion.identity);
                // 생성된 카드를 캔버스에 넣음
                newCard.transform.SetParent(GameObject.Find("Canvas").transform);
                // 현재 반복중인 인덱스를 저장
                Transform targetGrid = cardGrids[i];
                // cardGrid로 이동
                newCard.transform.DOMove(targetGrid.position, time).SetEase(Ease.Linear).OnComplete(() =>
                {
                    // cardGrid의 자식으로 설정
                    newCard.transform.SetParent(targetGrid);
                });
            }
        }
        else
        {
            // DeckUi가 존재하지 않을 경우
            Debug.Log("DeckUi가 존재하지 않습니다.");
        }

    }

    public void CardInToDeck()
    {
    }
}