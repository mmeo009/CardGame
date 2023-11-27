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

    private bool waitDraw = true;

    void Start()
    {
        DrawCard.Instance.TransformChack();
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
        if (PlayerData.Instance.playerAnimation == null)
        {
            PlayerData.Instance.playerAnimation = FindObjectOfType<PlayerAnimation>();
        }
        for (int i = 1; i < 11; i++)
        {
            if(i != 4)
            {
                string num = i.ToString("000");
                Managers.Deck.AddCardIntoDefaultDeck($"101{num}A", 4);
            }
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
        yield return new WaitForSeconds(2f);
    }

    IEnumerator DrawAndEffectTurn()
    {
        // 카드 뽑기
        DrawCard.Instance.CreateCardOneAtTheTime();
        PlayerData.Instance.CCChange();
        MonsterData.Instance.MonsterCCChange();
        //플레이어턴 비활성화
        _isPlayerTurnEnd = false;
        waitDraw = true;
        yield return new WaitForSeconds(3f);
    }

    IEnumerator PlayerTurn()
    {
        float playerTurnDuration = 2f;
        float elapsedTime = 0f;
        // 플레이어의 턴

        while (!isPlayerTurnEnd)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= playerTurnDuration)
            {
                waitDraw = false;
            }
            yield return null;
        }
    }

    IEnumerator PreviousEffectTurn()
    {
        DrawCard.Instance.MergeGridToCardGrid();
        DrawCard.Instance.Invoke("CardInToDeck", 1.5f);

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

    public void PlayerTurnEnd()
    {
        if(currentTurn == TurnState.Player && waitDraw == false && isPlayerTurnEnd == false)
        {
            isPlayerTurnEnd = true;
        }
    }
}
