using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : GenericSingleton<CardData>
{
    public Entity_CardData cardDatabase; // 아래 Awake 메서드에서 할당할 것입니다
    public int amountOfCardsInDeck;     //덱에 들어있는 카드의 양
    // 덱 리스트
    public List<CardDataEntry> deck = new List<CardDataEntry>();
    private void Awake()
    {
        // Resources 폴더에서 스크립터블 오브젝트 불러오기
        cardDatabase = Resources.Load<Entity_CardData>("CardData"); // "CardData"는 리소스 폴더 내의 경로입니다
        if (cardDatabase == null)
        {
            Debug.LogError("CardDatabase 가 리소스 파일에 없습니다.");
        }
        else
        {
            Debug.Log("찾았다룡.");
        }
    }
}
[System.Serializable]
public class CardDataEntry
{
    public string id;
    public int count; // 카드 개수
}
