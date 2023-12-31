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
        if(deckInfo != null)
        {
            deckInfo.GetComponent<TMP_Text>().text = Managers.DeckData.amountOfCardsInDeck.ToString();
        }
        else
        {
            GameObject _deckInfo = GameObject.Find("RemainingCards");
            deckInfo = _deckInfo;
            deckInfo.GetComponent<TMP_Text>().text = Managers.DeckData.amountOfCardsInDeck.ToString();
        }
    }
    // DefaultDeck 관련 시작
    public void DeckSetting()
    {
        Managers.DeckData.deck.Clear();
        foreach (var card in Managers.DeckData.defaultDeck)
        {
            Managers.DeckData.deck.Add(card.Clone());
        }
    }

    //1,2단계 카드만 덱에 추가할 수 있음
    public void AddCardIntoDefaultDeck(string cardId, int cardAmount = 2)
    {
        Entity_CardData.Param foundCard = Managers.Data.cardsDictionary[cardId];
        char lastId = foundCard.id[foundCard.id.Length - 1];
        int level = 0 ;
        // 아이디를 통해 강화 단계 찾기
        if(lastId == 'A' || lastId == 'J' || lastId == 'I')
        {
            level = 1;
        }
        else if(lastId == 'B' || lastId == 'T' || lastId == 'N')
        {
            level = 2;
        }
        else if(lastId == 'C' || lastId == 'H')
        {
            level = 3;
        }
        // 카드를 추가하는 부분
        if(level == 1 || level == 2)
        {
            if (foundCard != null)
            {
                // 덱에 이미 같은 카드가 있는지 확인
                CardInformation existingEntry = Managers.DeckData.defaultDeck.Find(entry => entry.id == cardId);

                if (existingEntry == null)
                {
                    // 새로운 카드라면 덱에 추가
                    CardInformation newEntry = new CardInformation
                    {
                        id = cardId,
                        count = cardAmount,
                        level = level
                    };
                    Managers.DeckData.defaultDeck.Add(newEntry);
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
                // 새로운 카드라면 덱에 추가
                CardInformation newEntry = new CardInformation
                {
                    id = cardId,
                    count = cardAmount,
                    level = level
                };
                Managers.DeckData.defaultDeck.Add(newEntry);
            }
        }
        else
        {
            Debug.Log($"{level}은 추가 불가능");
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
            if (isInf == true)
            {
                CardInformation existingInfEntry = Managers.DeckData.deck.Find(entry => entry.id == cardId);

                if (existingInfEntry != null)
                {
                    if (existingInfEntry.level == _level)
                    {
                        // 이미 있는 카드일 경우 카운트 추가.
                        existingInfEntry.count += cardCount;
                    }
                    else
                    {
                        // 아이디는 같지만 레벨이 다를경우 클래스 추가
                        CardInformation newEntry = new CardInformation
                        {
                            id = cardId,
                            count = cardCount,
                            level = _level
                        };
                        Managers.DeckData.deck.Add(newEntry);
                    }
                }
                else
                {
                    // 새로운 카드에 새로운 레벨이면 새 카드 추가
                    CardInformation newEntry = new CardInformation
                    {
                        id = cardId,
                        count = cardCount,
                        level = _level
                    };
                    Managers.DeckData.deck.Add(newEntry);
                }
            }
            else
            {
                // 덱에 이미 같은 카드가 있는지 확인
                CardInformation existingEntry = Managers.DeckData.deck.Find(entry => entry.id == cardId);

                if (existingEntry != null)
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
                        level = level
                    };
                    Managers.DeckData.deck.Add(newEntry);
                }
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
            CardInformation existingEntry = Managers.DeckData.deck.Find(entry => entry.id == cardId && entry.level == cardLevel);

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
                    Managers.DeckData.deck.Remove(existingEntry);
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
        foreach (var entry in Managers.DeckData.deck)
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
        foreach (var entry in Managers.DeckData.deck)
        {
            Entity_CardData.Param foundCard = Managers.Data.cardsDictionary[entry.id];
            if (foundCard != null)
            {
                count += entry.count;
            }
            Managers.DeckData.amountOfCardsInDeck = count;
        }
    }
}
