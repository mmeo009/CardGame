using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager
{
    public enum Stage
    {
        MAIN,
        BATTLE,
        STORE,
        DEMON,
        TARVERN,
        FORGE,
        BOSS
    }
    public Stage stage = Stage.MAIN;
    public int stageNum;
    public int battleStageCount;
    public int strongDegree = 1;
    public int battleStageType = 1;
    public int level = 2;
    public void Reset()
    {
        stage = Stage.MAIN;
        stageNum = 0;
        battleStageCount = 0;
        strongDegree = 1;
        battleStageType = 1;
        level = 2;
    }
    public void SelectLevel()
    {
        if(stageNum == 0)
        {
            //battleStageType = UnityEngine.Random.Range(1, 4);
            stage = Stage.BATTLE;
            battleStageCount++;
            if (level == 1)
            {
                level = 2;
            }
            else
            {
                level = 1;
            }
        }
        else if(stageNum < 5 && battleStageCount > 2)
        {
            // int st = UnityEngine.Random.Range(0, 4);
            int st = UnityEngine.Random.Range(0, 3);
            switch (st)
            {
                case 0:
                    stage = Stage.TARVERN;
                    break;
                case 1:
                    stage = Stage.FORGE;
                    break;
                case 2:
                    stage = Stage.STORE;
                    break;
                case 3:
                    stage = Stage.DEMON;
                    break;
            }
        }
        else if(stageNum < 3 && battleStageCount <= 2)
        {
            // int st = UnityEngine.Random.Range(0, 5);
            int st = UnityEngine.Random.Range(0, 3);
            switch (st)
            {
                case 0:
                    stage = Stage.BATTLE;
                    battleStageCount++;
                    break;
                case 1:
                    stage = Stage.FORGE;
                    break;
                case 2:
                    stage = Stage.TARVERN;
                    break;
                case 3:
                    stage = Stage.DEMON;
                    break;
                case 4:
                    stage = Stage.STORE;
                    break;
            }
        }
        else if (stageNum < 5 && battleStageCount < 3)
        {
            stage = Stage.BATTLE;
            battleStageCount++;
        }
        else if(stageNum == 5)
        {
            stage = Stage.BOSS;
            stageNum = -1;
            battleStageCount = 0;
            strongDegree++;
        }
        stageNum++;
        Debug.Log("Stage : " + stage
                  + " StageNum : " + stageNum
                  + " battleStageType : " + battleStageType
                  + " battleStageCount : " + battleStageCount
                  + " Level : " + level);

        if(stage == Stage.BATTLE)
        {
            GameManager.Instance.MoveScene("Battle Scene");
        }
        else if(stage == Stage.BOSS)
        {
            GameManager.Instance.MoveScene("Test End Scene");
        }
        else if(stage == Stage.TARVERN)
        {
            GameManager.Instance.MoveScene("Tarvern Scene");
        }
        else if(stage == Stage.FORGE)
        {
            GameManager.Instance.MoveScene("Forge Scene");
        }
        else if(stage == Stage.DEMON)
        {
            GameManager.Instance.MoveScene("Demon Scene");
        }
        else if (stage == Stage.STORE)
        {
            GameManager.Instance.MoveScene("Store Scene");
        }
    }
    public void BattleStage()
    {
        if(stage == Stage.BOSS)
        {
            MonsterData.Instance.LoadMonsterData(level, battleStageType, strongDegree, true);
        }
        else
        {
            MonsterData.Instance.LoadMonsterData(level, battleStageType, strongDegree);
        }
    }
}
