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
        // ������ ������ �� �ʱ� �� ���� = ī�� ��ο�
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
                    // ���� �ݺ� ����
                    Debug.Log(currentTurn);
                    break;

                case TurnState.SubsequentEffect:
                    yield return StartCoroutine(SubsequentEffectTurn());
                    currentTurn = TurnState.DrawAndEffect;
                    Debug.Log(currentTurn);
                    break;
            }

            // ���� �ϱ��� ����մϴ�.
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
        // ī�� �̱�
        DrawCard.Instance.CreateCardOneAtTheTime();
        PlayerData.Instance.CCChange();
        MonsterData.Instance.MonsterCCChange();
        //�÷��̾��� ��Ȱ��ȭ
        _isPlayerTurnEnd = false;
        waitDraw = true;
        yield return new WaitForSeconds(3f);
    }

    IEnumerator PlayerTurn()
    {
        float playerTurnDuration = 2f;
        float elapsedTime = 0f;
        // �÷��̾��� ��

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

        // ���� ȿ��
        yield return new WaitForSeconds(2f);
    }

    IEnumerator EnemyTurn()
    {
        // ���� ��
        MonsterData.Instance.UsePattern();
        yield return new WaitForSeconds(2f);
    }

    IEnumerator SubsequentEffectTurn()
    {
        // ���� ȿ��
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
