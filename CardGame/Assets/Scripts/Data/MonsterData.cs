using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData : GenericSingleton<MonsterData>
{
    public int monsterHp;
    public int monsterMaxHp;
    public int monsterAd;
    public int monsterAp;
    public string monsterName;
    public Entity_MonsteraData.Param monsterData;
    public List<Entity_PatternData.Param> patterns = new List<Entity_PatternData.Param>();

    public void LoadMonsterData(int level, int stageType, int strongDegree = 1, bool isBoss = false)
    {
        // 몬스터 불러오기
        monsterData = null;
        patterns.Clear();
        List<Entity_MonsteraData.Param> monsters = new List<Entity_MonsteraData.Param>();
        foreach(Entity_MonsteraData.Param _monsterData in Managers.Data.monsterDatabase.param)
        {
            string id = _monsterData.id;
            char no3 = id[2];
            char no4 = id[3];
            char _level = (char)(level + '0');
            char _stageType = (char)(stageType + '0');
            char last = id[id.Length - 1];

            if ((no3 == _level && no4 == _stageType && !isBoss) || (no3 == _level && no4 == _stageType && last == 'Z' && isBoss))
            {
                monsters.Add(_monsterData);
            }
            Debug.Log(_monsterData.id);
        }
        if(monsters.Count > 0)
        {
            int monsterNum = UnityEngine.Random.Range(0, monsters.Count);
            monsterData = monsters[monsterNum];
        }
        else
        {
            Debug.Log("왜 없어!");
        }

        // 몬스터 정보 입력하기
        if(strongDegree == 1)
        {
            monsterMaxHp = monsterData.baseHp;
            monsterAd = monsterData.baseAd;
            monsterAp = monsterData.baseAp;
        }
        else if(strongDegree == 0)
        {
            Debug.Log("0은 불가능!");
        }
        else
        {
            int strong = monsterData.statPlus + strongDegree;
            monsterMaxHp = monsterData.baseHp + strong * 5;
            monsterAd = monsterData.baseAd + strong;
            monsterAp = monsterData.baseAp + strong;
        }
        monsterName = monsterData.monsterName;
        monsterHp = monsterMaxHp;

        // 몬스터 패턴 불러오기
        foreach(Entity_PatternData.Param _patternData in Managers.Data.patternDatabase.param)
        {
            if(_patternData.monsterId == monsterData.id)
            {
                patterns.Add(_patternData);
            }
        }
    }
}
