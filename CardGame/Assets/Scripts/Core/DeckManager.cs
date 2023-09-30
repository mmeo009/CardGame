using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class DeckManager
{
    public int maxHold = 2;
    public int nowHold = 0;

    // 카드를 덱에 추가하는 함수
    public void AddCardToDeckById(string cardId, int cardCount = 1)
    {
        // 스크립터블 오브젝트에서 해당 id를 가진 카드를 찾음
        Entity_CardData.Param foundCard = CardData.Instance.cardDatabase.param.Find(card => card.id == cardId);

        if (foundCard != null)
        {
            // 덱에 이미 같은 카드가 있는지 확인
            CardDataEntry existingEntry = CardData.Instance.deck.Find(entry => entry.id == cardId);

            if (existingEntry != null)
            {
                // 이미 있는 카드라면 개수를 증가
                existingEntry.count += cardCount;
            }
            else
            {
                // 새로운 카드라면 덱에 추가
                CardDataEntry newEntry = new CardDataEntry
                {
                    id = cardId,
                    count = cardCount
                };
                CardData.Instance.deck.Add(newEntry);
            }

            // 추가된 카드 정보를 출력
            Debug.Log($"추가된 카드 : (카드 이름: {foundCard.cardName}, 개수: {cardCount})");
        }
        else
        {
            // 추가할 카드가 스크립쳐블 오브젝트에 없다면 없다면 오류 메세지 출력
            Debug.Log($"ID {cardId}를 가진 카드를 찾을 수 없습니다.");
        }
        // 덱에 남은 카드 수 체크
        CountCardsInDeck();
    }

    public void RemoveCardToDeckById(string cardId, int cardAmount = 1)
    {
        // 스크립터블 오브젝트에서 해당 id를 가진 카드를 찾음
        Entity_CardData.Param foundCard = CardData.Instance.cardDatabase.param.Find(card => card.id == cardId);

        if (foundCard != null)
        {
            // 덱에 이미 같은 카드가 있는지 확인
            CardDataEntry existingEntry = CardData.Instance.deck.Find(entry => entry.id == cardId);

            if (existingEntry != null)
            {
                if(existingEntry.count > cardAmount)
                {
                    // 제거량이 덱에 있는 카드의 양보다 적을경우 덱에있는 카드의 수를 줄임
                    existingEntry.count -= cardAmount;
                    // 제거된 카드 정보를 출력
                    Debug.Log($"제거된 카드 : (카드 이름: {foundCard.cardName}, 제거 량: {cardAmount})");
                }
                else if(existingEntry.count == cardAmount)
                {
                    // 제거량이 덱에 있는 카드와 같을경우 덱에 있는 카드를 제거
                    CardData.Instance.deck.Remove(existingEntry);
                    // 제거된 카드 정보를 출력
                    Debug.Log($"제거된 카드 : (카드 이름: {foundCard.cardName}, 제거 량: {cardAmount})");
                }
                else
                {
                    // 제거량이 덱에 있는 카드의 양보다 많을경우 오류 메세지 출력
                    Debug.Log($"ID{cardId}의 카드 수는{existingEntry.count}입니다. 따라서 더 많은 양을 제거할 수 없습니다.");
                }

            }
            else
            {
                // 덱에 없는 카드일 경우 오류 메세지 출력
                Debug.Log($"ID {cardId}를 가진 카드가 덱에 없습니다.");
            }
        }
        else
        {
            // 제거할 카드가 스크립쳐블 오브젝트에 없다면 없다면 오류 메세지 출력
            Debug.Log($"ID {cardId}를 가진 카드를 찾을 수 없습니다.");
        }
        // 덱에 남은 카드 수 체크
        CountCardsInDeck();
    }

    // 덱에 있는 카드 정보 출력 함수
    public void PrintDeck()
    {
        foreach (var entry in CardData.Instance.deck)
        {
            Entity_CardData.Param foundCard = CardData.Instance.cardDatabase.param.Find(card => card.id == entry.id);
            if (foundCard != null)
            {
                Debug.Log($"카드 이름: {foundCard.cardName}, 개수: {entry.count}");
            }
        }
    }

    // 덱에 있는 카드의 수량 체크 함수
    public void CountCardsInDeck()
    {
        int count = 0;
        foreach (var entry in CardData.Instance.deck)
        {
            Entity_CardData.Param foundCard = CardData.Instance.cardDatabase.param.Find(card => card.id == entry.id);
            if (foundCard != null)
            {
                count += entry.count;
            }
            CardData.Instance.amountOfCardsInDeck = count;
        }
    }
}
