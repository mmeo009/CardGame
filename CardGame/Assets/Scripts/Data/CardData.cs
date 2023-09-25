using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : MonoBehaviour
{
    public Entity_CardData cardDatabase; // �Ʒ� Awake �޼��忡�� �Ҵ��� ���Դϴ�

    [System.Serializable]
    public class CardDataEntry
    {
        public string id;
        public int count; // ī�� ����
    }
    // �� ����Ʈ
    public List<CardDataEntry> deck = new List<CardDataEntry>();

    private void Awake()
    {
        // Resources �������� ��ũ���ͺ� ������Ʈ �ҷ�����
        cardDatabase = Resources.Load<Entity_CardData>("CardData"); // "CardData"�� ���ҽ� ���� ���� ����Դϴ�
        if (cardDatabase == null)
        {
            Debug.LogError("CardDatabase �� ���ҽ� ���Ͽ� �����ϴ�.");
        }
        else
        {
            Debug.Log("ã�Ҵٷ�.");
        }
    }
}