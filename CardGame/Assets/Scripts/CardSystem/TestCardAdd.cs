using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCardAdd : MonoBehaviour
{

    private void Start()
    {
        this.gameObject.GetComponent<DrawCard>().TransformChack();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            // ī�带 ���� �߰�
            Managers.Deck.AddCardToDeckById("101001C", 2);
            Managers.Deck.AddCardToDeckById("101002C", 3);
            Managers.Deck.AddCardToDeckById("101003C", 5);
            Managers.Deck.AddCardToDeckById("101004C", 6);
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            int num = Random.Range(1, 5);
            // ������ ī�带 ����
            Managers.Deck.RemoveCardToDeckById($"10100{num}C", 1);
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            // ���� �ִ� ī�� ���� ���
            Managers.Deck.PrintDeck();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Managers.Deck.AddCardToDeckById("123321");
            Managers.Deck.RemoveCardToDeckById("123321");
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            this.gameObject.GetComponent<DrawCard>().CreateCardAllAtOnce();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.gameObject.GetComponent<DrawCard>().CreateCardOneAtTheTime();
        }
    }
}
