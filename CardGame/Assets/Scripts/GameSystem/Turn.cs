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
        // ������ ������ �� �ʱ� �� ���� = ī�� ��ο�
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

            // ���� �ϱ��� ����մϴ�.
            yield return null;
        }
    }

    IEnumerator DrawCard()
    {
        // ī�� �̱�

        yield return new WaitForSeconds(2f);
    }

    IEnumerator PlayerTurn()
    {
        // �÷��̾��� ��

        while (!isPlayerTurnEnd)
        {
            yield return null;
        }
    }

    IEnumerator PreviousEffectTurn()
    {
        //�÷��̾��� ��Ȱ��ȭ
        _isPlayerTurnEnd = false;
        // ���� ȿ��
        yield return new WaitForSeconds(2f);
    }

    IEnumerator EnemyTurn()
    {
        // ���� ��

        yield return new WaitForSeconds(2f);
    }

    IEnumerator SubsequentEffectTurn()
    {
        // ���� ȿ��

        yield return new WaitForSeconds(2f);
    }
}
