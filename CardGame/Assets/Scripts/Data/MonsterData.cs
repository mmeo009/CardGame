using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MonsterData : GenericSingleton<MonsterData>
{
    public int monsterHp;
    public int monsterMaxHp;
    public int monsterAd;
    public int monsterAp;
    public string monsterName;
    public Entity_MonsteraData.Param monsterData;
    public List<Entity_PatternData.Param> patterns = new List<Entity_PatternData.Param>();
    public Image bg;

    public void LoadMonsterData(int level, int stageType, int strongDegree = 1, bool isBoss = false)
    {
        // 몬스터 불러오기
        monsterData = null;
        patterns.Clear();
        List<Entity_MonsteraData.Param> monsters = new List<Entity_MonsteraData.Param>();
        foreach(Entity_MonsteraData.Param _monsterData in Managers.Data.monsterDatabase.param) // 몬스터 스크립터블 옵젝에 있는 데이터들을 가져온다.
        {
            string id = _monsterData.id;
            char no3 = id[2];
            char no4 = id[3];
            char _level = (char)(level + '0');
            char _stageType = (char)(stageType + '0');
            char last = id[id.Length - 1];

            if (no3 == _level && no4 == _stageType && last == 'Z' && isBoss == true)
            {
                monsters.Add(_monsterData);
            }
            else if(no3 == _level && no4 == _stageType&& last != 'Z' && isBoss == false)
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
        string stage = monsterData.id.Substring(2,4);
        Debug.Log(stage);
        Image _bg = GameObject.FindGameObjectWithTag("BackGround").GetComponent<Image>();
        if (_bg != null)
        {
            bg = _bg;
            bg.sprite = Resources.Load<Sprite>($"Illustration/BG/{stage}");
        }

    }

    public void GetDamage(int amount)
    {
        monsterHp -= amount;
        if(monsterHp <= amount)
        {
            Debug.Log("깨꼬닭");
        }
    }
    public void PickPattern(string id = null)
    {
        Entity_PatternData.Param nextPattern = new Entity_PatternData.Param();
        if (id != null)
        {
            nextPattern = null;
            
            for (int i = 0;i < patterns.Count; i++)
            {
                if(patterns[i].patternId == id)
                {
                    nextPattern = patterns[i];
                }
            }
            if(nextPattern == null)
            {
                    Debug.Log($"{id}의 패턴이 존재하지 않습니다.");
                    int A = UnityEngine.Random.Range(0, patterns.Count);
                    nextPattern = patterns[A];
            }
        }
        else
        {
            int A = UnityEngine.Random.Range(0, patterns.Count);
            nextPattern = patterns[A];

        }
        Debug.Log(nextPattern.patternId);
    }

}
