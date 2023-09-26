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

    }

    public void CreateCard()
    {

        float time = 2.0f / cardGrids.Length;

        // deckUi�� ���� �����ϸ� deckUi�� ��ġ���� ����
        if (deckUi != null)
        {
            for (int i = 0; i < cardGrids.Length; i++)
            {
                // ī�� ������ ����
                GameObject newCard = Instantiate(cardPrefab, deckUi.position, Quaternion.identity);
                // ������ ī�带 ĵ������ ����
                newCard.transform.SetParent(GameObject.Find("Canvas").transform);
                // ���� �ݺ����� �ε����� ����
                Transform targetGrid = cardGrids[i];
                // cardGrid�� �̵�
                newCard.transform.DOMove(targetGrid.position, time).SetEase(Ease.Linear).OnComplete(() =>
                {
                    // cardGrid�� �ڽ����� ����
                    newCard.transform.SetParent(targetGrid);
                });
            }
        }
        else
        {
            // DeckUi�� �������� ���� ���
            Debug.Log("DeckUi�� �������� �ʽ��ϴ�.");
        }

    }

    public void CardInToDeck()
    {
    }
}