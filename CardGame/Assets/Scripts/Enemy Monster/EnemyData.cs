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
            Debug.LogError("몬스터 생성 불가");
        }
        else
        {
            Debug.Log("몬스터가 나타났다");
        }
    }

    public class MonsterDataEntry
    {
        public string id;
        public int count;
    }

}
