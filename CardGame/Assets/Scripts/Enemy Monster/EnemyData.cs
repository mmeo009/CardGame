using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public Monster_EnemyData monsterDatabase;

    private void Awake()
    {
        monsterDatabase = GetComponent<Monster_EnemyData>();    
        if(monsterDatabase == null)
        {
            Debug.LogError("���� ���� �Ұ�");
        }
        else
        {
            Debug.Log("���Ͱ� ��Ÿ����");
        }
    }

    public class MonsterDataEntry
    {
        public string id;
        public int count;
    }

}
