using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class DeckManager
{
    public int maxHold = 2;
    public int nowHold = 0;
    public GameObject deckInfo;

    public void ShowRemainingCards()
    {
        GameObject _deckInfo = GameObject.Find("RemainingCards");
        if(_deckInfo != null)
        {
            deckInfo = _deckInfo;
            deckInfo.GetComponent<TMP_Text>().text = DeckData.Instance.amountOfCardsInDeck.ToString();
        }
        else
        {
            Debug.Log("숫자를 표시할 수 없어");
        }
    }
    // DefaultDeck 관련 시작
    public void DeckSetting()
    {
        DeckData.Instance.deck = DeckData.Instance.defaultDeck;
    }

    //1,2단계 카드만 덱에 추가할 수 있음
    public void AddCardIntoDefaultDeck(string cardId, int cardAmount = 2)
    {
        Entity_CardData.Param foundCard = Managers.Data.cardsDictionary[cardId];
        char lastId = foundCard.id[foundCard.id.Length - 1];
        int level = 0 ;
        // 아이디를 통해 강화 단계 찾기
        if (lastId != 'A' || lastId != 'J')
        {
            Debug.Log("1단계가 아니야");
        }
        else if(lastId == 'A' || lastId != 'J')
        {
            level = 1;
        }
        else if(lastId == 'B' || lastId == 'T' || lastId == 'N')
        {
            level = 2;
        }
        // 카드를 추가하는 부분
        if(level == 1 || level == 2)
        {
            if (foundCard != null)
            {
                // 덱에 이미 같은 카드가 있는지 확인
                CardInformation existingEntry = DeckData.Instance.defaultDeck.Find(entry => entry.id == cardId);

                if (existingEntry != null)
                {
                    // 새로운 카드라면 덱에 추가
                    CardInformation newEntry = new CardInformation
                    {
                        id = cardId,
                        count = cardAmount,
                        level = 1
                    };
                    DeckData.Instance.defaultDeck.Add(newEntry);
                }
                else
                {
                    // 이미 있는 카드라면 개수를 증가
                    existingEntry.count += cardAmount;
                }

                // 추가된 카드 정보를 출력
                Debug.Log($"추가된 카드 : (카드 이름: {foundCard.cardName}, 개수: {cardAmount}");
            }
            else
            {
                // 추가할 카드가 없다면 오류 메세지 출력
                Debug.Log($"ID {cardId}를 가진 카드를 찾을 수 없습니다.");
            }
        }
    }
    // DefaultDeck 관련 끝

    // 카드를 덱에 추가하는 함수
    public void AddCardToDeckById(string cardId, int cardCount = 1, bool isInf = false , int _level = 0)
    {
        // 해당 카드를 찾아옴
        Entity_CardData.Param foundCard = Managers.Data.cardsDictionary[cardId];

        char lastId = foundCard.id[foundCard.id.Length - 1];
        int level = 1;
        switch(lastId)
        {
            case 'A':
                level = 1;
                break;
            case 'B':
                level = 2;
                break;
            case 'C':
                level = 3;
                break;
            case 'J':
                level = 1;
                break;
            case 'T':
                level = 2;
                break;
            case 'H':
                level = 3;
                break;
            case 'N':
                level = 2;
                break;
        }

        if (foundCard != null)
        {
            // 덱에 이미 같은 카드가 있는지 확인
            CardInformation existingEntry = DeckData.Instance.deck.Find(entry => entry.id == cardId);

            if (existingEntry != null)
            {
                if(isInf == true)
                {
                    if(existingEntry.level == _level)
                    {
                        // 이미 있는 카드라면 개수를 증가
                        existingEntry.count += cardCount;
                    }
                    else
                    {
                        // 새로운 카드라면 덱에 추가
                        CardInformation newEntry = new CardInformation
                        {
                            id = cardId,
                            count = cardCount,
                            level = _level
                        };
                        DeckData.Instance.deck.Add(newEntry);
                    }
                }
                else
                {
                    // 이미 있는 카드라면 개수를 증가
                    existingEntry.count += cardCount;
                }
            }
            else
            {
                // 새로운 카드라면 덱에 추가
                CardInformation newEntry = new CardInformation
                {
                    id = cardId,
                    count = cardCount,
                    level = level
                };
                DeckData.Instance.deck.Add(newEntry);
            }

            // 추가된 카드 정보를 출력
            Debug.Log($"추가된 카드 : (카드 이름: {foundCard.cardName}, 개수: {cardCount}, 강화 단계 : {level.ToString()}");
        }
        else
        {
            // 추가할 카드가 없다면 오류 메세지 출력
            Debug.Log($"ID {cardId}를 가진 카드를 찾을 수 없습니다.");
        }
        // 덱에 남은 카드 수 체크
        CountCardsInDeck();
        ShowRemainingCards();
    }

    public void RemoveCardToDeckById(string cardId, int cardLevel, int cardAmount = 1)
    {
        // 해당 id를 가진 카드를 찾음
        Entity_CardData.Param foundCard = Managers.Data.cardsDictionary[cardId];

        if (foundCard != null)
        {
            // 덱에 이미 같은 카드가 있는지 확인
            CardInformation existingEntry = DeckData.Instance.deck.Find(entry => entry.id == cardId && entry.level == cardLevel);

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
                    DeckData.Instance.deck.Remove(existingEntry);
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
            // 제거할 카드가 없다면 오류 메세지 출력
            Debug.Log($"ID {cardId}를 가진 카드를 찾을 수 없습니다.");
        }
        // 덱에 남은 카드 수 체크
        CountCardsInDeck();
        ShowRemainingCards();
    }

    // 덱에 있는 카드 정보 출력 함수
    public void PrintDeck()
    {
        foreach (var entry in DeckData.Instance.deck)
        {
            Entity_CardData.Param foundCard = Managers.Data.cardsDictionary[entry.id];
            if (foundCard != null)
            {
                Debug.Log($"카드 이름: {foundCard.cardName}, 개수: {entry.count}");
            }
        }
        ShowRemainingCards();
    }

    // 덱에 있는 카드의 수량 체크 함수
    public void CountCardsInDeck()
    {
        int count = 0;
        foreach (var entry in DeckData.Instance.deck)
        {
            Entity_CardData.Param foundCard = Managers.Data.cardsDictionary[entry.id];
            if (foundCard != null)
            {
                count += entry.count;
            }
            DeckData.Instance.amountOfCardsInDeck = count;
        }
    }
}
