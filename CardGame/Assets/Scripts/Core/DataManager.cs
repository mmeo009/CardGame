using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public Entity_CardData cardDatabase;
    public Entity_MonsteraData monsterDatabase;
    public Entity_PatternData patternDatabase;
    public Dictionary<string, Entity_CardData.Param> cardsDictionary = new Dictionary<string, Entity_CardData.Param>();
    public Dictionary<string, Entity_MonsteraData.Param> monstersDictionary = new Dictionary<string, Entity_MonsteraData.Param>();
    public Dictionary<string, Entity_PatternData.Param> monstersPatternDictionary = new Dictionary<string, Entity_PatternData.Param>();
    public Dictionary<string, AudioClip> soundDictionary = new Dictionary<string, AudioClip>();
    public void GetResources()
    {
        Screen.SetResolution(1920, 1200, true);
        cardDatabase = Resources.Load<Entity_CardData>("CardData");
        monsterDatabase = Resources.Load<Entity_MonsteraData>("MonsterData");
        patternDatabase = Resources.Load<Entity_PatternData>("PatternData");
        if (cardDatabase == null)
        {
            Debug.LogError("CardDatabase 가 리소스 파일에 없습니다.");
        }
        else
        {
            Debug.Log("찾았다룡.");
        }
        AudioClip[] soundFiles = Resources.LoadAll<AudioClip>("SoundEffects");

        foreach (AudioClip sound in soundFiles)
        {
            soundDictionary[sound.name] = sound;
        }
    }
    public void DataIntoDictionary()
    {
        if (cardDatabase != null)
        {
            foreach (Entity_CardData.Param cardData in cardDatabase.param)
            {
                if (!cardsDictionary.ContainsKey(cardData.id))
                {
                    cardsDictionary.Add(cardData.id, cardData);
                }
                else
                {
                    Debug.LogError("중복된 카드 ID를 갖는 카드가 있습니다: " + cardData.id);
                }
            }
        }
        else
        {
            Debug.LogError("CardDatabase가 로드되지 않았습니다.");
        }

        if (monsterDatabase != null)
        {
            foreach (Entity_MonsteraData.Param monsterData in monsterDatabase.param)
            {
                if (!monstersDictionary.ContainsKey(monsterData.id))
                {
                    monstersDictionary.Add(monsterData.id, monsterData);
                }
                else
                {
                    Debug.LogError("중복된 ID를 갖는 몬스터가 있습니다: " + monsterData.id);
                }
            }
        }
        else
        {
            Debug.LogError("monsterDatabase가 로드되지 않았습니다.");
        }

        if (patternDatabase != null)
        {
            foreach (Entity_PatternData.Param patternData in patternDatabase.param)
            {
                if (!monstersPatternDictionary.ContainsKey(patternData.patternId))
                {
                    monstersPatternDictionary.Add(patternData.patternId, patternData);
                }
                else
                {
                    Debug.LogError("중복된 ID를 갖는 몬스터가 있습니다: " + patternData.patternId);
                }
            }
        }
        else
        {
            Debug.LogError("patternDatabase가 로드되지 않았습니다.");
        }
    }
    public void DebugCardDatas()
    {
        foreach (var cardData in cardsDictionary.Values)
        {
            Debug.Log("Card ID: " + cardData.id);
            Debug.Log("Item Code: " + cardData.cardCost);
            Debug.Log("Card Name: " + cardData.cardName);
        }
    }
}
