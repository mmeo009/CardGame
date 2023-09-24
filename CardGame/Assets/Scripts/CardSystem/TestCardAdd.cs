using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCardAdd : MonoBehaviour
{
    void Start()
    {
        // ī�带 ���� �߰�
        DeckData.Instance.AddCardToDeckById("101001A", 2);
        DeckData.Instance.AddCardToDeckById("101002A", 3);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            // ������ ī�带 ����
            DeckData.Instance.RemoveCardToDeckById("101001A", 1);
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            // ���� �ִ� ī�� ���� ���
            DeckData.Instance.PrintDeck();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            DeckData.Instance.AddCardToDeckById("123321");
            DeckData.Instance.RemoveCardToDeckById("123321");
        }
    }
}
