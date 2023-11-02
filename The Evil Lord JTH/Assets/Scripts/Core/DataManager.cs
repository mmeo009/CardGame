using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public Entity_CardData entity_CardData;
    public Entity_MonsteraData entity_MonsteraData;
    public Entity_PatternData entity_PatternData;
    public Dictionary<string, Entity_CardData.Param> cardsDictionary = new Dictionary<string, Entity_CardData.Param>();
    public Dictionary<string, Entity_MonsteraData.Param> monstersDictionary = new Dictionary<string, Entity_MonsteraData.Param>();
    public Dictionary<string, Entity_PatternData.Param> monstersPatternDictionary = new Dictionary<string, Entity_PatternData.Param>();
    public Dictionary<string, AudioClip> soundDictionary = new Dictionary<string, AudioClip>();
    
    public void Init()
    {
        entity_CardData = (Entity_CardData)Managers.Resource.Load<ScriptableObject>("CardData");
        entity_MonsteraData = (Entity_MonsteraData)Managers.Resource.Load<ScriptableObject>("MonsterData");
        entity_PatternData = (Entity_PatternData)Managers.Resource.Load<ScriptableObject>("PatternData");

        if (entity_CardData != null)
        {
            foreach (Entity_CardData.Param cardData in entity_CardData.param)
            {
                if (!cardsDictionary.ContainsKey(cardData.id))
                {
                    cardsDictionary.Add(cardData.id, cardData);
                }
                else
                {
                    Debug.LogError("�ߺ��� ī�� ID�� ���� ī�尡 �ֽ��ϴ�: " + cardData.id);
                }
            }
        }
        else
        {
            Debug.LogError("CardDatabase�� �ε���� �ʾҽ��ϴ�.");
        }

        if (entity_MonsteraData != null)
        {
            foreach (Entity_MonsteraData.Param monsterData in entity_MonsteraData.param)
            {
                if (!monstersDictionary.ContainsKey(monsterData.id))
                {
                    monstersDictionary.Add(monsterData.id, monsterData);
                }
                else
                {
                    Debug.LogError("�ߺ��� ID�� ���� ���Ͱ� �ֽ��ϴ�: " + monsterData.id);
                }
            }
        }
        else
        {
            Debug.LogError("monsterDatabase�� �ε���� �ʾҽ��ϴ�.");
        }

        if (entity_PatternData != null)
        {
            foreach (Entity_PatternData.Param patternData in entity_PatternData.param)
            {
                if (!monstersPatternDictionary.ContainsKey(patternData.patternId))
                {
                    monstersPatternDictionary.Add(patternData.patternId, patternData);
                }
                else
                {
                    Debug.LogError("�ߺ��� ID�� ���� ���Ͱ� �ֽ��ϴ�: " + patternData.patternId);
                }
            }
        }
        else
        {
            Debug.LogError("patternDatabase�� �ε���� �ʾҽ��ϴ�.");
        }
    }
}
