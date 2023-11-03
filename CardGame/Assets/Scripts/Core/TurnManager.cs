using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : GenericSingleton<TurnManager>
{
    public enum TurnState
    {
        GetDatas,
        DrawAndEffect,
        Player,
        PreviousEffect,
        Enemy,
        SubsequentEffect
    }
    public TurnState currentTurn;

    [SerializeField]
    private bool _isPlayerTurnEnd = false;
    public bool isPlayerTurnEnd
    {
        get
        {
            return _isPlayerTurnEnd;
        }
        set
        {
            _isPlayerTurnEnd = value;
        }
    }
    private void Awake()
    {
        Managers.Data.GetResources();
        Managers.Data.DataIntoDictionary();
        DrawCard.Instance.TransformChack();
    }
    void Start()
    {
        // 게임을 시작할 때 초기 턴 상태 = 카드 드로우
        currentTurn = TurnState.GetDatas;
        PlayerData.Instance.CCChange();
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        while (true)
        {
            switch (currentTurn)
            {
                case TurnState.GetDatas:
                    yield return StartCoroutine(GetDatas());
                    currentTurn = TurnState.DrawAndEffect;
                    Debug.Log(currentTurn);
                    break;
                case TurnState.DrawAndEffect:
                    yield return StartCoroutine(DrawAndEffectTurn());
                    currentTurn = TurnState.Player;
                    Debug.Log(currentTurn);
                    break;

                case TurnState.Player:
                    yield return StartCoroutine(PlayerTurn());
                    currentTurn = TurnState.PreviousEffect;
                    Debug.Log(currentTurn);
                    break;

                case TurnState.PreviousEffect:
                    yield return StartCoroutine(PreviousEffectTurn());
                    currentTurn = TurnState.Enemy;
                    Debug.Log(currentTurn);
                    break;

                case TurnState.Enemy:
                    yield return StartCoroutine(EnemyTurn());
                    currentTurn = TurnState.SubsequentEffect;
                    // 몬스터 반복 생성
                    Debug.Log(currentTurn);
                    break;

                case TurnState.SubsequentEffect:
                    yield return StartCoroutine(SubsequentEffectTurn());
                    currentTurn = TurnState.DrawAndEffect;
                    Debug.Log(currentTurn);
                    break;
            }

            // 다음 턴까지 대기합니다.
            yield return null;
        }
    }
    IEnumerator GetDatas()
    {
        for(int i = 1; i < 11; i++)
                  {
                      string num = i.ToString("000");
                      Managers.Deck.AddCardIntoDefaultDeck($"101{num}A", 4);
                  }
 for (int i = 12; i < 22; i++)
  {
      string num = i.ToString("000");
      Managers.Deck.AddCardIntoDefaultDeck($"102{num}A", 4);
  }
        for (int i = 23; i < 27; i++)
        {
            string num = i.ToString("000");
            Managers.Deck.AddCardIntoDefaultDeck($"103{num}A", 4);
        }
        Managers.Deck.DeckSetting();
        Managers.Stage.SelectLevel();           //나중에는 필요 없음
        yield return new WaitForSeconds(2f);
    }

    IEnumerator DrawAndEffectTurn()
    {
        // 카드 뽑기
        DrawCard.Instance.CreateCardOneAtTheTime();
        PlayerData.Instance.CCChange();
        MonsterData.Instance.MonsterCCChange();
        yield return new WaitForSeconds(2f);
    }

    IEnumerator PlayerTurn()
    {
        // 플레이어의 턴

        while (!isPlayerTurnEnd)
        {
            yield return null;
        }
    }

    IEnumerator PreviousEffectTurn()
    {
        DrawCard.Instance.MergeGridToCardGrid();
        DrawCard.Instance.Invoke("CardInToDeck", 0.5f);
        //플레이어턴 비활성화
        _isPlayerTurnEnd = false;
        // 이전 효과
        yield return new WaitForSeconds(2f);
    }

    IEnumerator EnemyTurn()
    {
        // 적의 턴
        MonsterData.Instance.UsePattern();
        yield return new WaitForSeconds(2f);
    }

    IEnumerator SubsequentEffectTurn()
    {
        // 이후 효과
        MonsterData.Instance.PickPattern();
        yield return new WaitForSeconds(2f);
    }

}
