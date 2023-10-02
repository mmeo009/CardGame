using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Entity_CardData;

public class Monster_EnemyData : MonoBehaviour
{
    public List<Param1> param1 = new List<Param1>();
    
    [System.SerializableAttribute]
    public class Param1
    {
        public string id;
        public string MonsterName;
        public int MonsterRarity;
        public string text;
        public int attackPower;
    }

}
