using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CardUse : MonoBehaviour
{
    public Entity_CardData.Param thisCard;          // 이 카드의 데이터를 저장
    public PlayerData player;                       // 플레이어의 데이터를 저장
    public CardController cardController;
	public void GetData(string id)
    {
        Entity_CardData.Param foundCard = Managers.Data.cardsDictionary[id];        // 카드를 데이터에서 찾아옴
        if(foundCard != null)                   // 카드의 아이디가 존재할 경우
        {
            thisCard = foundCard;               // 변수에 할당
		}
        else                                    // 존재 하지 않을 경우 로그 출력
        {
            Debug.Log("해당 아이디의 카드가 게임에 존재하지 않습니다.");
        }
        player = PlayerData.Instance;           //  플레이어를 불러와서 할당

        cardController = GetComponent<CardController>();        // CardController를 불러와서 할당
    }
    public void UsingCard()                     // 카드를 사용하는 함수
    {

        if (player == null)                      // 플레이어가 존재하지 않을 경우
        {
            player = PlayerData.Instance;       // 플레이어 불러와서 할당
        }
        if (thisCard != null)                   // 아이디가 존재할경우
        {
            if (player.isUsingCard == false)
            {
                player.UsingDelay(0);        // 다른 카드 사용 불가 상태로 만듬
                int cardType = (int)Char.GetNumericValue(thisCard.id[2]);       // 카드의 타입(공격, 방어, 스킬, 상태이상)
                char cardLevel = thisCard.id[thisCard.id.Length - 1];           // 카드의 강화 레벨
                int cardPlusData = thisCard.stat_05;                            // 카드의 사용 관련 데이터(cc기, 적중확률 등)
                int cardCost = thisCard.cardCost;                               // 카드의 마나 값
                int cardMethod = thisCard.usingMethod;                          // 카드의 사용 방법
                string cardCC01 = thisCard.cc_1;                                // 카드의 상태이상 1번
                string cardCC02 = thisCard.cc_2;                                // 카드의 상태이상 2번
                int myMana = player.player.currentMana;                         // 플레이어의 현제 마나
                if (cardLevel == 'A' || cardLevel == 'B' || cardLevel == 'C' || cardLevel == 'N')
                {
                    if (cardType == 1)
                    {
                        if (cardMethod == 0)             // 아무것도 아닌 무기
                        {
                            Debug.Log("아무 능력도 없당");
                        }
                        if (cardMethod == 1)             // 때리는 무기
                        {
                            if (myMana >= cardCost)
                            {
                                player.CalculatePorbability();              // 명중률 계산 함수
                                player.GainingOrLosingValue("currentMana", -cardCost);            // 마나 감소
                                int num = UnityEngine.Random.Range(0, 100);
                                if (num <= player.player.hitProbability)    // 명중 확률 
                                {
                                    if (thisCard.adPower != 0)           // ad공격 카드일 경우
                                    {
                                        MonsterData.Instance.GetDamage(player.player.adDamage + thisCard.adPower);
                                    }
                                    else if (thisCard.apPower != 0)      // ap공격 카드일 경우
                                    {
                                        MonsterData.Instance.GetDamage(player.player.apDamage + thisCard.apPower);
                                    }
                                    MonsterData.Instance.monsterAnim.GetDamaged();        // 몬스터 타격 이펙트 출력
                                    Debug.Log(num + "/" + player.player.hitProbability);      // 명중률 관련 출력
                                    CCUse(cardCC01, cardCC02, cardPlusData);
                                }
                                else
                                {
                                    Debug.Log("감나빗");       // 빗나갔을 경우 출력
                                }
                                player.UsingDelay(0.5f);        // 카드 사용 딜레이
                                SoundData.Instance.PlaySound(thisCard.itemCode);        // 카드 사용 사운드 재생
                                Destroy(gameObject);        // 카드 오브젝트를 제거
                            }
                            else
                            {
                                Debug.Log("마나가 없어서 쓸 수 없다");        // 마나가 없을 경우 출력
                            }
                        }
                        if (cardMethod == 2)             // 던지는 무기
                        {
                            if (myMana >= cardCost)
                            {
                                player.CalculatePorbability();      // 명중률 계산 함수
                                player.GainingOrLosingValue("currentMana", -cardCost);            // 마나 감소
                                int num = UnityEngine.Random.Range(0, 100);
                                if (num <= player.player.hitProbability / 2)      // 명중확률 ( 원거리 무기라 절반값으로 계산)
                                {
                                    if (thisCard.adPower != 0)      // ad공격 카드일 경우
                                    {
                                        MonsterData.Instance.GetDamage(player.player.adDamage + thisCard.adPower);
                                    }
                                    else if (thisCard.apPower != 0)     // ap공격 카드일 경우
                                    {
                                        MonsterData.Instance.GetDamage(player.player.apDamage + thisCard.apPower);
                                    }
                                    MonsterData.Instance.monsterAnim.GetDamaged();        // 몬스터 타격 이펙트 출력
                                    Debug.Log(num + "/" + player.player.hitProbability / 2);        // 명중률 관련 출력
                                    CCUse(cardCC01, cardCC02, cardPlusData);
                                }
                                else
                                {
                                    Debug.Log("감나빗");       // 빗나갔을 경우 출력
                                }
                                player.UsingDelay(1.0f);        // 카드 사용 딜레이
                                SoundData.Instance.PlaySound(thisCard.itemCode);        // 카드 사용 사운드 재생
                                Destroy(gameObject);        // 카드 오브젝트 제거
                            }
                            else
                            {
                                Debug.Log("마나가 없어서 쓸 수 없다");        // 마나가 없을 경우 출력
                            }
                        }
                        if (cardMethod == 3)             // 던지고 돌아오는 무기
                        {
                            if (myMana >= cardCost)
                            {
                                player.CalculatePorbability();      // 명중률 계산 함수
                                player.GainingOrLosingValue("currentMana", -cardCost);            // 마나 감소
                                int num = UnityEngine.Random.Range(0, 100);
                                if (num <= player.player.hitProbability)        // 명중확률 ( 던지고 돌아오는 무기이기에 그대로 계산)
                                {
                                    if (thisCard.adPower != 0)      // ad무기일 경우
                                    {
                                        MonsterData.Instance.GetDamage(player.player.adDamage + thisCard.adPower);      // 몬스터 체력 감소
                                        MonsterData.Instance.monsterAnim.GetDamaged();        // 몬스터 타격 이펙트 출력
                                        CCUse(cardCC01, cardCC02, cardPlusData);
                                        if (num / 2 <= player.player.hitProbability)      // 이후 자신에게 돌아오는 확률 계산
                                        {
                                            player.GainingOrLosingValue("currentHealth", -(player.player.adDamage + thisCard.adPower) / 2);        //내 체력 감소
                                        }
                                        else
                                        {
                                            Debug.Log("나한테 감나빗");       // 나한테 빗나갔을 경우 출력
                                        }
                                    }
                                    else if (thisCard.apPower != 0)     //ap무기일 경우
                                    {
                                        MonsterData.Instance.GetDamage(player.player.apDamage + thisCard.apPower);
                                        MonsterData.Instance.monsterAnim.GetDamaged();        // 몬스터 타격 이펙트 출력
                                        CCUse(cardCC01, cardCC02, cardPlusData);
                                        if (num / 2 <= player.player.hitProbability)
                                        {
                                            player.GainingOrLosingValue("currentHealth", -(player.player.apDamage + thisCard.apPower) / 2);
                                        }
                                        else
                                        {
                                            Debug.Log("나한테 감나빗");
                                        }
                                    }
                                    Debug.Log(num + "/" + player.player.hitProbability);        // 명중률 관련 계산
                                }
                                else
                                {
                                    Debug.Log("감나빗");       // 빗나갔을 경우 출력
                                }
                                player.UsingDelay(1.5f);        // 카드 사용 딜레이
                                SoundData.Instance.PlaySound(thisCard.itemCode);        // 카드 사용 사운드 재생
                                Destroy(gameObject);        // 카드 오브젝트 제거
                            }
                            else
                            {
                                Debug.Log("마나가 없어서 쓸 수 없다");        // 마나가 없을 경우 출력
                            }
                        }
                    }
                    else if (cardType == 2)     // 방어카드 관련
                    {
                        if (cardMethod == 0)        // 방어 카드 중 아무 능력이 없는 카드
                        {
                            Debug.Log("아무 능력도 없당");
                        }
                        else if (cardMethod == 1)       // 방어력 증가 카드
                        {
                            if (myMana >= cardCost)
                            {
                                player.GainingOrLosingValue("currentMana", -cardCost);            // 마나 감소
                                player.GainingOrLosingValue("shield", (player.player.apPower * thisCard.adPower));       // 플레이어 방어력 증가
                                CCUse(cardCC01, cardCC02, cardPlusData);
                                player.UsingDelay(0.5f);        // 카드 사용 딜레이
                                SoundData.Instance.PlaySound(thisCard.itemCode);        // 카드 사용 사운드 재생
                                Destroy(gameObject);        //카드 오브젝트 제거
                            }
                            else
                            {
                                Debug.Log("마나가 없어서 쓸 수 없다");        // 마나가 없을 경우 출력
                            }
                        }
                        else if (cardMethod == 2)        // 체력 회복 카드
                        {
                            if (myMana >= cardCost)
                            {
                                player.GainingOrLosingValue("currentMana", -cardCost);            // 마나 감소
                                player.GainingOrLosingValue("currentHealth", (player.player.apPower * thisCard.apPower));       // 플레이어 체력 증가
                                CCUse(cardCC01, cardCC02, cardPlusData);
                                player.UsingDelay(0.5f);        // 카드 사용 딜레이
                                SoundData.Instance.PlaySound(thisCard.itemCode);        // 카드 사용 사운드 재생
                                Destroy(gameObject);        // 카드 오브젝트 제거
                            }
                            else
                            {
                                Debug.Log("마나가 없어서 쓸 수 없다");        // 마나가 없을 경우 출력
                            }
                        }
                        else if (cardMethod == 3)       // 체력을 회복하고 만약 체력이 가득차면 남은 양을 방어력으로 전환하는 카드
                        {
                            if (myMana >= cardCost)
                            {
                                player.GainingOrLosingValue("currentMana", -cardCost);            // 마나 감소
                                player.GainingOrLosingValue("currentHealth", (player.player.apPower * thisCard.fixedPower), true);     // 플레이어 체력증가 후 가능하다면 방어력 증가
                                CCUse(cardCC01, cardCC02, cardPlusData);
                                player.UsingDelay(0.5f);        // 카드 사용 딜레이
                                SoundData.Instance.PlaySound(thisCard.itemCode);        // 카드 사용 사운드 재생
                                Destroy(gameObject);        // 카드 오브젝트 제거
                            }
                            else
                            {
                                Debug.Log("마나가 없어서 쓸 수 없다");        // 마나가 없을 경우 출력
                            }
                        }
                        else if (cardMethod == 4)       // 천천히 사라지는 체력을 회복하는 카드
                        {
                            if (myMana >= cardCost)
                            {
                                player.GainingOrLosingValue("currentMana", -cardCost);            // 마나 감소
                                player.GainingOrLosingValue("currentHealth", (player.player.apPower * thisCard.fixedPower));        // 플레이어의 체력을 회복
                                player.GainingOrLosingValue("temporary", 2, false, (player.player.apPower * thisCard.fixedPower));      // 천천히 사라지는 체력 상태이상을 부여
                                CCUse(cardCC01, cardCC02, cardPlusData);
                                player.UsingDelay(0.5f);        // 카드 사용 딜레이
                                SoundData.Instance.PlaySound(thisCard.itemCode);        // 카드 사용 사운드 재생
                                Destroy(gameObject);        // 카등 오브젝트 제거
                            }
                            else
                            {
                                Debug.Log("마나가 없어서 쓸 수 없다");        // 마나가 없을 경우 출력
                            }
                        }
                        else if (cardMethod == 5)        // 무적이 되는 카드
                        {
                            if (myMana >= cardCost)
                            {
                                player.GainingOrLosingValue("currentMana", -cardCost);            // 마나 감소
                                player.GainingOrLosingValue("god", 0, true);        // 무적 상태이상을 부여
                                CCUse(cardCC01, cardCC02, cardPlusData);
                                player.UsingDelay(0.5f);        // 카드 사용 딜레이
                                SoundData.Instance.PlaySound(thisCard.itemCode);        // 카드 사용 사운드 재생
                                Destroy(gameObject);        // 카드 오브젝트 제거
                            }
                            else
                            {
                                Debug.Log("마나가 없어서 쓸 수 없다");        // 마나가 없을 경우 출력
                            }
                        }

                    }
                    else if (cardType == 3)     // 스킬 카드
                    {
                            if (myMana >= cardCost)
                            {
                                player.GainingOrLosingValue("currentMana", -cardCost);            // 마나 감소
                                player.UsingDelay(0.5f);        // 카드 사용 딜레이
                                CCUse(cardCC01, cardCC02, cardPlusData);
                                SoundData.Instance.PlaySound(thisCard.itemCode);        // 카드 사용 사운드 재생
                                Destroy(gameObject);        // 카드 오브젝트 제거
                            }
                            else
                            {
                                Debug.Log("마나가 없어서 쓸 수 없다");        // 마나가 없을 경우 출력
                            }
                    }
                    else if (cardType == 4)     // 상태이상 카드
                    {
                        if (cardMethod == 0)     // 사용 가능한 상태이상 카드 (화상)
                        {
                            if (myMana >= cardCost)
                            {
                                player.GainingOrLosingValue("currentMana", -cardCost);            // 마나 감소
                                player.UsingDelay(0.5f);        // 카드 사용 딜레이
                                SoundData.Instance.PlaySound(thisCard.itemCode);        // 카드 사용 사운드 재생
                                Destroy(gameObject);        // 카드 오브젝트 제거
                            }
                            else
                            {
                                Debug.Log("마나가 없어서 쓸 수 없다");        // 마나가 없을 경우 출력
                            }
                        }
                        else if (cardMethod == 1 && cardLevel == 'C')        // 사용 불가능하나 최종 강화 시 사용 가능 카드 (환각)
                        {
                            if (myMana >= cardCost)
                            {
                                player.GainingOrLosingValue("currentMana", -cardCost);            // 마나 감소
                                player.UsingDelay(0.5f);        // 카드 사용 딜레이
                                SoundData.Instance.PlaySound(thisCard.itemCode);        // 카드 사용 사운드 재생
                                Destroy(gameObject);        // 카드 오브젝트 제거
                            }
                            else
                            {
                                Debug.Log("마나가 없어서 쓸 수 없다");
                            }
                        }
                    }
                }
                else if (cardLevel == 'I')       //무한대로 강화가 가능한 카드
                {
                    if (cardType == 1)       // 무한대로 강화가 가능한 공격 카드
                    {
                        if (myMana >= cardCost)
                        {
                            int thisCardLevel = this.GetComponent<CardDataLoad>().thisCardLevel;
                            player.CalculatePorbability();
                            player.GainingOrLosingValue("currentMana", -cardCost);            // 마나 감소
                            for (int i = 0; i < thisCardLevel; i++)
                            {
                                int num = UnityEngine.Random.Range(0, 100);
                                if (num <= player.player.hitProbability / 2)
                                {
                                    if (thisCard.adPower != 0)
                                    {
                                        MonsterData.Instance.GetDamage(player.player.adDamage + thisCard.adPower);
                                    }
                                    else if (thisCard.apPower != 0)
                                    {
                                        MonsterData.Instance.GetDamage(player.player.apDamage + thisCard.apPower);
                                    }
                                    Debug.Log(num + "/" + player.player.hitProbability / 2);
                                }
                                else
                                {
                                    Debug.Log("감나빗");       // 빗나갈 경우
                                }
                            }
                            SoundData.Instance.PlaySound(thisCard.itemCode);        // 카드 사용 사운드 재생
                            Destroy(gameObject);        // 카드 오브젝트 제거
                        }
                        else
                        {
                            Debug.Log("마나가 없어서 쓸 수 없다");        // 마나가 없을 경우 출력
                        }
                    }
                    else if (cardType == 2)      // 무한대로 강화가 가능한 방어 카드
                    {
                        if (myMana >= cardCost)
                        {
                            int thisCardLevel = this.GetComponent<CardDataLoad>().thisCardLevel;
                            player.GainingOrLosingValue("currentMana", -cardCost);            // 마나 감소
                            for (int i = 0; i < thisCardLevel; i++)     // 카드 레벨 만큼 반복
                            {
                                player.GainingOrLosingValue("shield", (player.player.apPower * thisCard.adPower));
                            }
                            SoundData.Instance.PlaySound(thisCard.itemCode);        // 카드 사용 사운드 재생
                            Destroy(gameObject);
                        }
                        else
                        {
                            Debug.Log("마나가 없어서 쓸 수 없다");        // 마나가 없을 경우 출력
                        }
                    }

                }
                else
                {
                    Debug.Log("돌아와");       // 아직 미 구현 상태의 카드
                }
            }
        }
        else
        {
            GetComponent<CardDataLoad>().PickCardAndIdFromDeck();
        }
        
    }


    public void CCUse(string cardCC01, string cardCC02, int stat05)
    { 
        if (cardCC01 != "NONE")
        {
            if (cardCC01 == "BURN")
            {
                Managers.Deck.AddCardToDeckById("104027A", 2);
            }
            else if (cardCC01 == "SLOTH")
            {
                player.GainingOrLosingValue("sloth", thisCard.stat_01);
            }
            else if (cardCC01 == "DRAW")
            {
                DrawCard.Instance.CreateSomeCards();
            }
            else if(cardCC01 == "FURY")
            {
                player.GainingOrLosingValue("fury", thisCard.stat_01);
            }
            else if(cardCC01 == "MANA")
            {
                player.GainingOrLosingValue("currentMana", thisCard.stat_02, true);
            }
            else if(cardCC01 == "BLIND")
            {
                player.GainingOrLosingValue("blind", thisCard.stat_01);
            }
            else if(cardCC01 == "HALLUCINATION")
            {
                Managers.Deck.AddCardToDeckById("104028A", 4);
            }
        }
        if (cardCC02 != "NONE" && stat05 == 2)
        {
            if (cardCC02 == "BURN")
            {
                Managers.Deck.AddCardToDeckById("104027A", 2);
            }
            else if (cardCC02 == "SLOTH")
            {
                player.GainingOrLosingValue("sloth", thisCard.stat_03);
            }
            else if (cardCC02 == "DRAW")
            {
                DrawCard.Instance.CreateSomeCards();
            }
            else if (cardCC02 == "FURY")
            {
                player.GainingOrLosingValue("fury", thisCard.stat_03);
            }
            else if (cardCC02 == "MANA")
            {
                player.GainingOrLosingValue("currentMana", thisCard.stat_04, true);
            }
            else if (cardCC02 == "BLIND")
            {
                player.GainingOrLosingValue("blind", thisCard.stat_03);
            }
            else if (cardCC02 == "HALLUCINATION")
            {
                Managers.Deck.AddCardToDeckById("104028A", 4);
            }
        }
        if(cardCC02 != "NONE" && stat05 != 5)
        {
            if (cardCC02 == "SLOTH")
            {
                MonsterData.Instance.GainingOrLosingCC("sloth",thisCard.stat_03 ,thisCard.stat_04, true);
            }
            else if(cardCC02 == "FURY")
            {
                MonsterData.Instance.GainingOrLosingCC("fury", thisCard.stat_03, thisCard.stat_04, true);
            }
            else if(cardCC02 == "BLIND")
            {
                MonsterData.Instance.GainingOrLosingCC("blind", thisCard.stat_03, thisCard.stat_04, true);
            }
            else if(cardCC02 == "POISON")
            {
                MonsterData.Instance.GainingOrLosingCC("poison", thisCard.stat_03, thisCard.stat_04, true);
            }
        }
    }
}
