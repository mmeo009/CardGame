using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Util;

public class Turn : MonoBehaviour
{
    public enum TurnState
    {
        Draw,
        Player,
        PreviousEffect,
        Enemy,
        SubsequentEffect
    }
    private TurnState currentTurn;

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

    void Start()
    {
        // 게임을 시작할 때 초기 턴 상태 = 카드 드로우
        currentTurn = TurnState.Draw;
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        while (true)
        {
            switch (currentTurn)
            {
                case TurnState.Draw:
                    yield return StartCoroutine(DrawCard());
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
                    Debug.Log(currentTurn);
                    break;

                case TurnState.SubsequentEffect:
                    yield return StartCoroutine(SubsequentEffectTurn());
                    currentTurn = TurnState.Draw;
                    Debug.Log(currentTurn);
                    break;
            }

            // 다음 턴까지 대기합니다.
            yield return null;
        }
    }

    IEnumerator DrawCard()
    {
        // 카드 뽑기

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
        //플레이어턴 비활성화
        _isPlayerTurnEnd = false;
        // 이전 효과
        yield return new WaitForSeconds(2f);
    }

    IEnumerator EnemyTurn()
    {
        // 적의 턴

        yield return new WaitForSeconds(2f);
    }

    IEnumerator SubsequentEffectTurn()
    {
        // 이후 효과

        yield return new WaitForSeconds(2f);
    }
}
